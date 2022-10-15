import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RequestFormComponent } from './ui/components/requestform.component';

const routes: Routes = [
  { path: '', redirectTo: '/requestform', pathMatch: 'full' },
  { path: 'requestform', component: RequestFormComponent   }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
