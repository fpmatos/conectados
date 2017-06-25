import { ComentariosListaComponent } from './../comentarios/comentarios-lista/comentarios-lista.component';
import { ComentariosModule } from './../comentarios/comentarios.module';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartsModule as Ng2Charts } from 'ng2-charts';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FileUploadModule } from 'ng2-file-upload';

import { ArtigosRoutingModule } from './artigos-routing.module';
import { ArtigosComponent } from './artigos.component';
import { PageHeaderModule } from '../shared';

import { ArtigosService } from './artigos.service';
import { ArtigosListaComponent } from './artigos-lista/artigos-lista.component';
import { ArtigosDetalheComponent } from './artigos-detalhe/artigos-detalhe.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {ConteudoGaleriaImagemComponent} from './shared/conteudo-galeria-imagem/conteudo-galeria-imagem.component';
import {LayoutGaleriaImagemService} from './shared/layout-galeria-imagem.service';
import {ImagePreview} from './shared/conteudo-galeria-imagem/image-preview';
import { TagsSelectComponent } from './shared/tags-select/tags-select.component';
import {TagsService} from './shared/tags.service';
import { ConteudoVideoComponent } from './shared/conteudo-video/conteudo-video.component';
import { ConteudoEnqueteComponent } from './shared/conteudo-enquete/conteudo-enquete.component';
import { ConteudoTextoComponent } from './shared/conteudo-texto/conteudo-texto.component';
import { ConteudoTituloComponent } from './shared/conteudo-titulo/conteudo-titulo.component';
import {LayoutsService} from './shared/layouts.service';
import { ConteudoImagemComponent } from './shared/conteudo-imagem/conteudo-imagem.component';
import { SharedPipesModule } from "app/shared/pipes/shared-pipes.module";
import {NgxMyDatePickerModule } from 'ngx-mydatepicker';


@NgModule({
    imports: [
        SharedPipesModule,
        CommonModule,
        FileUploadModule,
        Ng2Charts,
        ArtigosRoutingModule,
        PageHeaderModule,
        FormsModule,
        NgbModule.forRoot(),
        ReactiveFormsModule,
        ComentariosModule,
        ReactiveFormsModule,
        NgxMyDatePickerModule
    ],
    exports: [FormsModule, ComentariosModule],
    declarations: [
            ArtigosComponent, 
            ArtigosListaComponent, 
            ArtigosDetalheComponent, 
            ConteudoGaleriaImagemComponent,
            ImagePreview,
            TagsSelectComponent,
            ConteudoVideoComponent,
            ConteudoEnqueteComponent,
            ConteudoTextoComponent,
            ConteudoTituloComponent,
            ConteudoImagemComponent
            
            ],
    providers: [ArtigosService, LayoutGaleriaImagemService, TagsService, LayoutsService],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ArtigosModule { }
