import { TermosUsoAceitosComponent } from './termos-uso-aceitos.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '', 
    component: TermosUsoAceitosComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TermosUsoAceitosRoutingModule { }
