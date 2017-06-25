import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HttpClientService } from "./http-client";

@Injectable()
export class PerfilData{
    constructor(private http:HttpClientService){

    }

    retornar(): Observable<any>{
        return this.http.get(`MeuPerfil/`);     
    }
}