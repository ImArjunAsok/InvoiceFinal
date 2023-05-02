import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RouteGuard } from './helpers/guard/route.guard';

const routes: Routes = [
  { path: '', component: LoginComponent },
  {
    path: 'superadmin', 
    canActivate: [RouteGuard],
    loadChildren: () => import('./superadmin/superadmin.module').then(m => m.SuperadminModule)
  },
  {
    path: 'buhead',
    canActivate: [RouteGuard],
    loadChildren: () => import('./buhead/buhead.module').then(m => m.BuheadModule)
  },
  {
    path: 'projectmanager',
    canActivate: [RouteGuard],
    loadChildren: () => import('./projectmanager/projectmanager.module').then(m=>m.ProjectmanagerModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
