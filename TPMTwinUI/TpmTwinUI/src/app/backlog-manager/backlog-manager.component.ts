
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
}
