import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  today: Date = new Date();
  smartAgendaTasks = [
    { name: 'Finalize Sprint Plan', state: 'Open', impact: 'High', time: '09:00 AM' },
    { name: 'Review PR #42', state: 'In Progress', impact: 'Medium', time: '10:30 AM' },
    { name: 'Sync with QA', state: 'Pending', impact: 'Low', time: '02:00 PM' }
  ];
}
