import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import {HttpClientService} from 'app/shared/http-client.service'
import { Observable } from 'rxjs/Observable';
import {environment} from 'environments/environment';

@Injectable()
export class ArtigosService {

  constructor(private httpClientService: HttpClientService) { 

  }

  RetornarArtigos(): Observable<any>{

      return this.httpClientService.get(`${environment.rootApiUrl}/Artigos`);
  }

  RetornarArtigo(id: number): Observable<Response>{
      return this.httpClientService.get(`${environment.rootApiUrl}/Artigos/${id}`);
  }

    Salvar(data: any): Observable<Response>{
        if(!data.id)
            return this.httpClientService.post(`${environment.rootApiUrl}/Artigos`, data);
        else
            return this.httpClientService.put(`${environment.rootApiUrl}/Artigos/${data.id}`, data);
  }

  RetornarComentarios(artigoId: number): Observable<any>{

      return this.httpClientService.get(environment.rootApiUrl +  `/Artigos/${artigoId}/comentarios/`);
  }

  NegativarComentario(artigoId, comentarioId, value: boolean): Observable<any>{
      return this.httpClientService.get(environment.rootApiUrl +  `/Artigos/${artigoId}/comentarios/${comentarioId}/negativa?value=${value}`);
  }

  Ativar(id: number, value: boolean): Observable<any>
  {
      return this.httpClientService.get(environment.rootApiUrl +  `/Artigos/${id}/ativacao/?value=${value}`);
  }  

  retornarTotal(): Observable<any>{
    return this.httpClientService.get(environment.rootApiUrl +  '/Artigos/total');
  }  
}
