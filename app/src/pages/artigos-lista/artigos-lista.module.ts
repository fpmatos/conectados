import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';

import { ArtigosListaPage } from './artigos-lista';
import { SafePipesModule } from "../../pipes/safe-pipes/safe-pipes.module";

@NgModule({
  declarations: [
    ArtigosListaPage,
  ],
  imports: [
    IonicPageModule.forChild(ArtigosListaPage),
    SafePipesModule
  ],
  exports: [
    ArtigosListaPage
  ]
})
export class ArtigosListaPageModule {}
