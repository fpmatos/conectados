import { Injectable } from '@angular/core';
import {HttpClientService} from 'app/shared/http-client.service'
import {environment} from 'environments/environment';
import { Observable } from 'rxjs/Observable';
import { Response} from '@angular/http';

@Injectable()
export class LayoutsService {

  constructor(private httpClientService: HttpClientService) { 

  }

  RetornarLayouts(){
      return this.httpClientService.get(`${environment.rootApiUrl}/layouts`);    
  }

  RetornarCopiaLayout(id){
      return this.httpClientService.get(`${environment.rootApiUrl}/layouts/${id}/copia`);    
  }

}
