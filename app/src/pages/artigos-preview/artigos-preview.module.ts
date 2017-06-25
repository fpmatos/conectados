import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';

import { ConteudosComponentModule } from "../../components/conteudos/conteudos.module";

import { ArtigosPreviewPage } from './artigos-preview';

@NgModule({
  declarations: [
    ArtigosPreviewPage,
  ],
  imports: [
    IonicPageModule.forChild(ArtigosPreviewPage),
    ConteudosComponentModule
  ],
  exports: [
    ArtigosPreviewPage
  ]
})
export class ArtigosPreviewPageModule {}
