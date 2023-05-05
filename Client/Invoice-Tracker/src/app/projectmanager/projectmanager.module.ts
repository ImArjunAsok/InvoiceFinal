import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProjectmanagerRoutingModule } from './projectmanager-routing.module';
import { HomepageComponent } from './homepage/homepage.component';


@NgModule({
  declarations: [
    HomepageComponent
  ],
  imports: [
    CommonModule,
    ProjectmanagerRoutingModule
  ]
})
export class ProjectmanagerModule { }
