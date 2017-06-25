import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ArtigosComponent } from './artigos.component';
import { ArtigosListaComponent } from './artigos-lista/artigos-lista.component';

import { ArtigosDetalheComponent } from './artigos-detalhe/artigos-detalhe.component';

const routes: Routes = [
    { 
        path: '', 
        component: ArtigosComponent, 
        children: [
            {
                path: 'lista', 
                component: ArtigosListaComponent 
            }, 
            {
                path: 'detalhe/:id', 
                component: ArtigosDetalheComponent 
            },       
            {
                path: 'new', 
                component: ArtigosDetalheComponent 
            },                     
            {
                path: '',
                redirectTo: 'lista'
            }         
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ArtigosRoutingModule { }
