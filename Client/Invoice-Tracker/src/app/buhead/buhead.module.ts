import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BuheadRoutingModule } from './buhead-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';


@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    BuheadRoutingModule
  ]
})
export class BuheadModule { }
