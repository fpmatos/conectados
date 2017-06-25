import { environment } from './../environments/environment';
import { Component, ViewChild, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import {NotificacaoService} from './shared/notificacao.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {

    @ViewChild("content") private engineModal: TemplateRef<any>;
    Mensagem: string;

    constructor(private translate: TranslateService, private notificacaoService: NotificacaoService, private modalService: NgbModal) {
 
        console.log('environment config', environment);
 
        translate.addLangs(['en', 'fr', 'ur']);
        translate.setDefaultLang('en');

        const browserLang = translate.getBrowserLang();
        translate.use(browserLang.match(/en|fr|ur/) ? browserLang : 'en');

        notificacaoService.Notificador.subscribe((mensagem) => {
            this.Mensagem = mensagem.Mensagem;
            this.open();
        });
    }

    open() {
        this.modalService.open(this.engineModal).result.then((result) => {
            
        });
    }    

    ExibirMensagem(tipo){

    }

}

