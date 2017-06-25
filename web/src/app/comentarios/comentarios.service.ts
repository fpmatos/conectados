import { HttpClientService } from 'app/shared/http-client.service';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';

@Injectable()
export class ComentariosService {

  constructor(private httpClientService: HttpClientService) { }

  RetornarComentarios(artigoId?: number): Observable<any>{
    return this.httpClientService.get(`${environment.rootApiUrl}/Comentarios/?artigoId=${artigoId ? artigoId : ""}`);
  }

  NegativarComentario(artigoId, comentarioId, value: boolean): Observable<any>{
      return this.httpClientService.get(environment.rootApiUrl +  `/Artigos/${artigoId}/comentarios/${comentarioId}/negativa?value=${value}`);
  }  

  enviarComentario(artigoId, comentario: string): Observable<any>{
      return this.httpClientService.post(environment.rootApiUrl +  '/Comentarios/', { artigoId: artigoId, mensagem: comentario});
  }    

  retornarTotal(apenasDenunciados?: boolean): Observable<any>{
    return this.httpClientService.get(environment.rootApiUrl +  `/Comentarios/total/?denunciados=${apenasDenunciados == undefined ? '' : (apenasDenunciados === true ? 'true' : 'false') }`);
  }

}
