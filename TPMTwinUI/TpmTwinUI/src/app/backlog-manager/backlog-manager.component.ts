import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-backlog-manager',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './backlog-manager.component.html',
  styleUrls: ['./backlog-manager.component.scss']
})
export class BacklogManagerComponent {
  backlogData = [
    {
      id: 1,
      title: 'User login functionality',
      status: 'Open',
      tags: 'auth, user',
      priority: 'High',
      lastUpdated: '2025-07-30',
      aiInsights: 'Consider OAuth2 for better security.'
    },
    {
      id: 2,
      title: 'Dashboard UI improvements',
      status: 'In Progress',
      tags: 'UI, dashboard',
      priority: 'Medium',
      lastUpdated: '2025-07-28',
      aiInsights: 'Add quick stats widget.'
    },
    {
      id: 3,
      title: 'Sprint planning automation',
      status: 'Closed',
      tags: 'sprint, automation',
      priority: 'Low',
      lastUpdated: '2025-07-25',
      aiInsights: 'Automate backlog grooming.'
    }
  ];
}
