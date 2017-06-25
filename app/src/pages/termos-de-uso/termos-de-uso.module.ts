import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { TermosDeUsoPage } from './termos-de-uso';

@NgModule({
  declarations: [
    TermosDeUsoPage,
  ],
  imports: [
    IonicPageModule.forChild(TermosDeUsoPage),
  ],
  exports: [
    TermosDeUsoPage
  ]
})
export class TermosDeUsoPageModule {}
