import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SuperadminRoutingModule } from './superadmin-routing.module';
import { SuperadminLayoutComponent } from './superadmin-layout/superadmin-layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';


@NgModule({
  declarations: [
    SuperadminLayoutComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    SuperadminRoutingModule
  ]
})
export class SuperadminModule { }
