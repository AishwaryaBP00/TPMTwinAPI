import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BacklogManagerComponent } from './backlog-manager/backlog-manager.component';
import { SprintPlanningComponent } from './sprint-planning/sprint-planning.component';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'backlog-manager', component: BacklogManagerComponent },
  { path: 'sprint-planning', component: SprintPlanningComponent },
  {path: '**', redirectTo: 'dashboard' } // Redirect any unknown paths to dashboard
];
