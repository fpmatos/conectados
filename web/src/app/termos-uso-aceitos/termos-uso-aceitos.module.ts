import { TermosDeUsoService } from './termos-de-uso.service';
import { PageHeaderModule } from './../shared/modules/page-header/page-header.module';
import { TermosUsoAceitosComponent } from './termos-uso-aceitos.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TermosUsoAceitosRoutingModule } from './termos-uso-aceitos-routing.module';
import { SharedPipesModule } from "app/shared";

@NgModule({
  imports: [
    CommonModule,
    PageHeaderModule,
    SharedPipesModule,
    TermosUsoAceitosRoutingModule
  ],
  declarations: [TermosUsoAceitosComponent],
  providers: [TermosDeUsoService]
})
export class TermosUsoAceitosModule { 
  
}
