import { SharedPipesModule } from './../shared/pipes/shared-pipes.module';
import { ComentariosPipe } from './comentarios-lista/comentarios.pipe';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ComentariosService } from './comentarios.service';
import { PageHeaderModule } from './../shared/modules/page-header/page-header.module';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ComentariosRoutingModule } from './comentarios-routing.module';
import { ComentariosListaComponent } from './comentarios-lista/comentarios-lista.component';
import { ComentariosComponent } from './comentarios.component';

@NgModule({
  imports: [
    CommonModule,
    ComentariosRoutingModule,
    PageHeaderModule,
    ReactiveFormsModule,
    FormsModule,
    SharedPipesModule
  ],
  exports: [FormsModule, ComentariosListaComponent],
  declarations: [ComentariosListaComponent, ComentariosComponent, ComentariosPipe],
  providers: [ComentariosService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ComentariosModule { }
