import { Injectable } from '@angular/core';
import {HttpClientService} from 'app/shared/http-client.service'
import {environment} from 'environments/environment';
import { Observable } from 'rxjs/Observable';
import { Response} from '@angular/http';

@Injectable()
export class TagsService {
  constructor(private httpClientService: HttpClientService) { 

  }

  RetornarTags(): Observable<Response>{
      return this.httpClientService.get(`${environment.rootApiUrl}/tags`);
  }
}
