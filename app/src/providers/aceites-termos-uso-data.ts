import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HttpClientService } from "./http-client";

@Injectable()
export class AceitesTermosUsoData {
    constructor(private http:HttpClientService){

    }

    Aceitar(): Observable<any>{
        return this.http.post('AceitesTermoUso/', null);
    }

    VerificarSeJaAceitou(): Observable<any>{
        return this.http.get('AceitesTermoUso/jaAceitou/');
    }
}