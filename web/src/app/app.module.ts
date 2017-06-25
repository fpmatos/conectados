import { TermosUsoAceitosModule } from './termos-uso-aceitos/termos-uso-aceitos.module';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule, Http} from '@angular/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthGuard } from './shared';
import { AutenticacaoService } from './shared';
import { ContextoService } from './shared';
import { HttpClientService } from './shared';
import { PerfilService } from './shared';
import { NotificacaoService } from './shared';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


// AoT requires an exported function for factories
export function HttpLoaderFactory(http: Http) {
    return new TranslateHttpLoader(http, '/assets/i18n/', '.json');
}

@NgModule({
    declarations: [
        AppComponent        
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        AppRoutingModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [Http]
            },
        }),
        NgbModule.forRoot(),
        TermosUsoAceitosModule
    ],
    exports: [ ],
    providers: [AutenticacaoService, ContextoService, AuthGuard, PerfilService, HttpClientService, NotificacaoService ],
    bootstrap: [AppComponent]
})
export class AppModule { }
