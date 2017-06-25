import { ComentariosListaComponent } from './comentarios-lista/comentarios-lista.component';
import { ComentariosComponent } from './comentarios.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {path: '', component: ComentariosComponent, children: [
    {
      path: '',
      component: ComentariosListaComponent
    },
    {
      path: 'denunciados',
      redirectTo: '?apenasDenunciados=true'
       
    }
  ]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ComentariosRoutingModule { }
