import { ComentariosComponentModule } from './../../components/comentarios/comentarios.module';
import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';

import { ConteudosComponentModule } from '../../components/conteudos/conteudos.module';

import { ArtigosDetalhePage } from './artigos-detalhe';

@NgModule({
  declarations: [
    ArtigosDetalhePage,
  ],
  imports: [
    IonicPageModule.forChild(ArtigosDetalhePage),
    ConteudosComponentModule,
    ComentariosComponentModule
  ],
  exports: [
    ArtigosDetalhePage
  ]
})
export class ArtigosDetalhePageModule {}
