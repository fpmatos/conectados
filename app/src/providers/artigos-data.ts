import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HttpClientService } from "./http-client";


@Injectable()
export class ArtigosData{
    constructor(private http: Http, private httpClientService: HttpClientService){

    }

    pesquisarArtigos(objParameter?: any): Observable<any> {
        return this.httpClientService.post(`artigosFeed/pesquisa`, objParameter);
    }

    retornarArtigo(artigoId: number){
        return this.httpClientService.get(`artigosFeed/${artigoId}`);
    }

    curtirArtigo(artigoId: number){
        return this.httpClientService.post(`artigosFeed/${artigoId}/curtir/`, null);
    }   

    descurtirArtigo(artigoId: number){
        return this.httpClientService.post(`artigosFeed/${artigoId}/descurtir/`, null);
    } 

    responderEnquete(artigoId: number, enqueteId:number, alternativaId: number){
        return this.httpClientService.post(`artigosFeed/${artigoId}/Enquetes/${enqueteId}/Alternativas/${alternativaId}/Responder`, null);
    }    

    retornarComentarios(artigoId: number){
        //return this.http.get(`${ROOT_API_URL}artigosFeed/${artigoId}/comentarios`);
        return this.httpClientService.get(`artigosFeed/${artigoId}/comentarios`)
    }

    postarComentario(artigoId: number, comentario: string){
        return this.httpClientService.post(`artigosFeed/${artigoId}/comentar/?mensagem=${comentario}`, null);
    }


    denunciarComentario(artigoId: number, comentarioId: number){
        return this.httpClientService.post(`artigosFeed/${artigoId}/comentarios/${comentarioId}/denunciar/`, null);
    }        
}