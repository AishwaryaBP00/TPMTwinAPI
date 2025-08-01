import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SprintCandidateSummaryDto } from '../api/models/sprint-candidate-summary-dto';
import { SprintCandidatesService } from '../api/services/sprint-candidates.service';

@Component({
  selector: 'app-backlog-manager',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './backlog-manager.component.html',
  styleUrls: ['./backlog-manager.component.scss']
})

export class BacklogManagerComponent implements OnInit {
  backlogData: SprintCandidateSummaryDto[] = [];

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
}
