import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';
import { HttpClientService } from './http-client.service';
import {environment} from 'environments/environment';

@Injectable()
export class PerfilService {

  constructor(private httpClientService: HttpClientService) { 

  }

  RetornarPerfil(): Promise<any>
  {
      return this.httpClientService.get(`${environment.rootApiUrl}/MeuPerfil`)
       .map(res => res.json())
      .toPromise<any>();
  }
}
