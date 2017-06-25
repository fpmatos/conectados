import { SafePipesModule } from '../../pipes/safe-pipes/safe-pipes.module';
import { NgModule } from '@angular/core';
import { IonicModule } from 'ionic-angular';

import { ConteudoVideoComponent } from './conteudo-video/conteudo-video';
import { ConteudoTituloComponent } from './conteudo-titulo/conteudo-titulo';
import { ConteudoTextoComponent } from './conteudo-texto/conteudo-texto';
import { ConteudoImagemComponent } from './conteudo-imagem/conteudo-imagem';
import { ConteudoGaleriaImagemComponent } from './conteudo-galeria-imagem/conteudo-galeria-imagem';
import { ConteudoEnqueteComponent } from './conteudo-enquete/conteudo-enquete';
import { ConteudoCurtirComponent } from './conteudo-curtir/conteudo-curtir';
import { HeilbaumPhotoswipeModule } from "heilbaum-ionic-photoswipe/dist";

@NgModule({
  declarations: [
    ConteudoCurtirComponent,
    ConteudoEnqueteComponent,
    ConteudoGaleriaImagemComponent,
    ConteudoImagemComponent,
    ConteudoTextoComponent,
    ConteudoTituloComponent,
    ConteudoVideoComponent
  ],
  imports: [
    IonicModule,
    HeilbaumPhotoswipeModule,
    SafePipesModule
  ],
  exports: [
    ConteudoCurtirComponent,
    ConteudoEnqueteComponent,
    ConteudoGaleriaImagemComponent,
    ConteudoImagemComponent,
    ConteudoTextoComponent,
    ConteudoTituloComponent,
    ConteudoVideoComponent
  ]
})
export class ConteudosComponentModule {}
