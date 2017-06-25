import { environment } from './../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { HttpClientService } from './../shared/http-client.service';
import { Injectable } from '@angular/core';

@Injectable()
export class TermosDeUsoService {

  constructor(private httpClientService: HttpClientService) { }

  retornarTermosUsoAceitos(): Observable<any>{
    return this.httpClientService.get(`${environment.rootApiUrl}/AceitesTermoUso/`);
  }

  retornarTotal(): Observable<any>{
    return this.httpClientService.get(`${environment.rootApiUrl}/AceitesTermoUso/count`);
  }
}
