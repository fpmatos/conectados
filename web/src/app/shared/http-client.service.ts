import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import {Http, RequestOptions, RequestOptionsArgs, XHRBackend, Response, Request, Headers} from '@angular/http';
import { Router } from '@angular/router';
import {environment} from 'environments/environment';
import { ContextoService } from './contexto.service';
import { AutenticacaoService } from './autenticacao.service';
import { NotificacaoService } from './notificacao.service';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';


@Injectable()
export class HttpClientService extends Http{
    constructor(
        backend: XHRBackend, 
        options: RequestOptions, 
            private contextoService: ContextoService,
            private notificacaoService: NotificacaoService,
            private router: Router,
            ){
        super(backend, options)
    }

    get(url: string, options?: RequestOptionsArgs): Observable<Response>{
        
        let optionsRequest = this.RetornarOptionRequest(options);
        let observable = super.get(this.RetornarUrlFull(url), optionsRequest)
            .catch((res) => { return this.ProcessarMensagemResposta(res) });

        this.ProcessarMensagemResposta(observable);

        return observable;
    }

    request(url: string | Request, options?: RequestOptionsArgs): Observable<Response>{
        var optionsRequest = this.RetornarOptionRequest(options);
        let observable = super.request(this.RetornarUrlFull(url), optionsRequest);
        this.ProcessarMensagemResposta(observable);

        return observable;        
    }

    post(url: string, body: any, options?: RequestOptionsArgs): Observable<Response>{
        let optionsRequest = this.RetornarOptionRequest(options);
        let observable = super.post(this.RetornarUrlFull(url), body, optionsRequest);
        this.ProcessarMensagemResposta(observable);

        return observable;
    }

    put(url: string, body: any, options?: RequestOptionsArgs): Observable<Response>{
        var optionsRequest = this.RetornarOptionRequest(options);
        var observable =  super.put(this.RetornarUrlFull(url), body, optionsRequest);
        this.ProcessarMensagemResposta(observable);

        return observable;        
    }

    delete(url: string, options?: RequestOptionsArgs): Observable<Response>{
        let optionsRequest = this.RetornarOptionRequest(options);
        let observable = super.delete(this.RetornarUrlFull(url), optionsRequest);
        this.ProcessarMensagemResposta(observable);

        return observable;        
    }

    patch(url: string, body: any, options?: RequestOptionsArgs): Observable<Response>{
        var optionsRequest = this.RetornarOptionRequest(options);
        var observable =  super.patch(this.RetornarUrlFull(url), body, optionsRequest);
        this.ProcessarMensagemResposta(observable);

        return observable; 
    }

    head(url: string, options?: RequestOptionsArgs): Observable<Response>{
        var optionsRequest = this.RetornarOptionRequest(options);
        var observable =  super.head(this.RetornarUrlFull(url), optionsRequest);

        this.ProcessarMensagemResposta(observable);
        return observable;
    }

    options(url: string, options?: RequestOptionsArgs): Observable<Response> {
        var optionsRequest = this.RetornarOptionRequest(options);
        var observable =  super.options(this.RetornarUrlFull(url), optionsRequest);
        this.ProcessarMensagemResposta(observable);

        return observable;         
    }

    private RetornarOptionRequest(options?: RequestOptionsArgs): RequestOptionsArgs{
        let authModel = this.contextoService.RetornarAuth();
        let optionsRequest: RequestOptionsArgs = (options ? options : new RequestOptions());        
        let headers = new Headers();

        if(authModel && authModel.Token)
        {
            headers.append('Content-Type', 'application/json; charset=utf-8');
            headers.append("Cache-Control", "no-cache");
            headers.append("Cache-Control", "no-store");
            headers.append("If-Modified-Since", "Mon, 26 Jul 1997 05:00:00 GMT");

            headers.append("AuthorizationConectados", "Bearer " + authModel.Token);
        }

        optionsRequest.headers = headers;

        return optionsRequest;
    }

    private RetornarUrlFull(reativeUrl): string{
        var url = environment.rootApiUrl + reativeUrl;
        return reativeUrl;
    }   

    private ProcessarMensagemResposta(resposta: any): any
    {
        switch(resposta.status)
        {
            case -1:
            case 502:
            case 0:
                this.notificacaoService.NotificarErro("Sem comunicação com o servidor. Verifique sua conexao com a internet.");        
                break;
            case 204:
                return Observable.of(true);
            case 404:
                this.notificacaoService.NotificarErro("Recurso não encontrado.");
                break;
            case 406:
                this.notificacaoService.NotificarAviso(resposta.statusText);   
                break;
            case 500:
                this.notificacaoService.NotificarErro(resposta.statusText);
                break;
            case 401:
                this.router.navigate(['/login']);
                break;

        }

        return Observable.throw({} || 'Server error');
    }

    // private ProcessarMensagemResposta(observable: Observable<Response>)
    // {
    //     //  observable
    //     //     .subscribe(res => {}, response => {
    //     //             switch(response.status)
    //     //             {
    //     //                 case -1:
    //     //                 case 502:
    //     //                     this.notificacaoService.NotificarErro("Sem comunicação com o servidor. Verifique sua conexao com a internet.");           
    //     //                     break;
    //     //                 case 404:
    //     //                     this.notificacaoService.NotificarErro("Recurso não encontrado.");
    //     //                     break;
    //     //                 case 406:
    //     //                     this.notificacaoService.NotificarAviso(response.statusText);      
    //     //                     break;          
    //     //                 case 500:
    //     //                     this.notificacaoService.NotificarErro(response.statusText);
    //     //                     break;
    //     //                 case 401:
    //     //                     this.router.navigate(['/login']);
    //     //                     break;

    //     //             }
    //     //     });

    //     observable.map(res => {console.log("teste:", res)});
    // }
}