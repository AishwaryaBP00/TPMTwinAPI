import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SprintCandidateSummaryDto } from '../api/models/sprint-candidate-summary-dto';
import { SprintCandidatesService } from '../api/services/sprint-candidates.service';

@Component({
  selector: 'app-backlog-manager',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './backlog-manager.component.html',
  styleUrls: ['./backlog-manager.component.scss']
})

export class BacklogManagerComponent implements OnInit {
  backlogData: SprintCandidateSummaryDto[] = [];
  searchTerm: string = '';

  constructor(private sprintCandidatesService: SprintCandidatesService) {}

  ngOnInit(): void {
    this.sprintCandidatesService.apiSprintCandidatesSummaryGet$Json().subscribe(data => {
      this.backlogData = data;
    });
  }

  getRelativeTime(dateString: string | undefined | null): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    const now = new Date();
    const diffMs = now.getTime() - date.getTime();
    const diffSec = Math.floor(diffMs / 1000);
    const diffMin = Math.floor(diffSec / 60);
    const diffHr = Math.floor(diffMin / 60);
    const diffDay = Math.floor(diffHr / 24);
    if (diffDay > 0) return `${diffDay} day${diffDay > 1 ? 's' : ''} ago`;
    if (diffHr > 0) return `${diffHr} hour${diffHr > 1 ? 's' : ''} ago`;
    if (diffMin > 0) return `${diffMin} minute${diffMin > 1 ? 's' : ''} ago`;
    return 'just now';
  }

  filteredBacklogData(): SprintCandidateSummaryDto[] {
    if (!this.searchTerm) return this.backlogData;
    const term = this.searchTerm.toLowerCase();
    return this.backlogData.filter(item =>
      (item.id && item.id.toString().toLowerCase().includes(term)) ||
      (item.title && item.title.toLowerCase().includes(term)) ||
      (item.status && item.status.toLowerCase().includes(term)) ||
      (item.tags && Array.isArray(item.tags) && item.tags.join(' ').toLowerCase().includes(term)) ||
      (item.priority && item.priority.toLowerCase().includes(term)) ||
      (item.aiInsights && Array.isArray(item.aiInsights) && item.aiInsights.join(' ').toLowerCase().includes(term))
    );
  }
}
