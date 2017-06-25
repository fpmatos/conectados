import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: 'dashboard', loadChildren: 'app/dashboard/dashboard.module#DashboardModule' },
            { path: 'artigos', loadChildren: 'app/artigos/artigos.module#ArtigosModule' },
            { path: 'comentarios', 
                 children: [
                 { path: 'lista', loadChildren: 'app/comentarios/comentarios.module#ComentariosModule' },
                 { path: 'denunciados', loadChildren: 'app/comentarios/comentarios.module#ComentariosModule'},
                    { path: '', redirectTo: 'lista'}                    
                 ]
            },
                
            { path: 'termosUsoAceitos', loadChildren: 'app/termos-uso-aceitos/termos-uso-aceitos.module#TermosUsoAceitosModule' },
            {
                path: 'demo', children: [
                    { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
                    { path: 'charts', loadChildren: './charts/charts.module#ChartsModule' },
                    { path: 'tables', loadChildren: './tables/tables.module#TablesModule' },
                    { path: 'forms', loadChildren: './form/form.module#FormModule' },
                    { path: 'bs-element', loadChildren: './bs-element/bs-element.module#BsElementModule' },
                    { path: 'grid', loadChildren: './grid/grid.module#GridModule' },
                    { path: 'components', loadChildren: './bs-component/bs-component.module#BsComponentModule' },
                    { path: 'blank-page', loadChildren: './blank-page/blank-page.module#BlankPageModule' },
                    { path: '', redirectTo: 'dashboard'}
                ]
            },
            {
                path: '', redirectTo: 'dashboard'
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LayoutRoutingModule { }
