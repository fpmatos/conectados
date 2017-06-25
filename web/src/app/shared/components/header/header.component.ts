import { Component, OnInit } from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {ContextoService} from 'app/shared/contexto.service';
import { UsuarioModel } from 'app/shared/models/usuario.model';


@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

    NomeUsuario: string;

    constructor(private translate: TranslateService, private contextService: ContextoService) { 

    }

    ngOnInit() {
        let usuario = this.contextService.RetornarUsuario();
       this.NomeUsuario = usuario.nomeDePerfil;
    }

    toggleSidebar() {
        const dom: any = document.querySelector('body');
        dom.classList.toggle('push-right');
    }

    rltAndLtr() {
        const dom: any = document.querySelector('body');
        dom.classList.toggle('rtl');
    }

    onLoggedout() {
        localStorage.removeItem('isLoggedin');
    }

    changeLang(language: string) {
        this.translate.use(language);
    }
}
