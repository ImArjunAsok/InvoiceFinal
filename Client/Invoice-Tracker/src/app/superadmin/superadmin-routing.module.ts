import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SuperadminLayoutComponent } from './superadmin-layout/superadmin-layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '', component: SuperadminLayoutComponent, children: [
      { path: 'dashboard', component: DashboardComponent },
      // { path: 'booking/:id', component: AppointmentBookingComponent },
      // { path: 'appointment-history', component: AppointmentHistoryComponent},
      // { path: 'profile', component: PatientProfileComponent},
      // { path: 'appointment-details/:appointmentId/:doctorId', component: AppointmentDetailsComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SuperadminRoutingModule { }
