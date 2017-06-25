import { Injectable } from '@angular/core';
import {HttpClientService} from 'app/shared/http-client.service'
import {environment} from 'environments/environment';

@Injectable()
export class LayoutGaleriaImagemService {

  constructor(private httpClientService: HttpClientService) { 

  }

  Excluir(id): Promise<any>{

      return this.httpClientService.delete(`${environment.rootApiUrl}/Uploads/${id}`)
        .toPromise();
  }

}
