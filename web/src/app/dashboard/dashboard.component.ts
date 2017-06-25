import { TermosDeUsoService } from './../termos-uso-aceitos/termos-de-uso.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ComentariosService } from './../comentarios/comentarios.service';
import { ArtigosService } from './../artigos/artigos.service';
import { Component, OnInit, Sanitizer } from '@angular/core';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

    totalArtigos: number = 0;
    totalComentarios: number = 0;
    totalComentariosDenunciados: number = 0;
    totalTermosUsoAceitos: number = 0;

    constructor(private artigosService: ArtigosService, private comentariosService: ComentariosService, private sanitize: DomSanitizer, private termosSeUsoService: TermosDeUsoService) { 
        
    }
    ngOnInit() {
        this.artigosService.retornarTotal()
        .map(res => res.json())
        .subscribe(item => {
            this.totalArtigos = item;
        });

        this.comentariosService.retornarTotal()
        .map(res => res.json())
        .subscribe(item => {
            this.totalComentarios = item;
        });     

        this.comentariosService.retornarTotal(true)
        .map(res => res.json())
        .subscribe(item => {
            this.totalComentariosDenunciados = item;
        });      

        this.termosSeUsoService.retornarTotal()
        .map(res => res.json())
        .subscribe(item => {
            this.totalTermosUsoAceitos = item;
        });         
    }

    retornarLinkDenunciados(){
        var urlEncoding: any = this.sanitize.bypassSecurityTrustUrl('/comentarios/lista?apenasDenunciados=true');
        console.log(urlEncoding.changingThisBreaksApplicationSecurity);
        return urlEncoding.changingThisBreaksApplicationSecurity;
    }

}
