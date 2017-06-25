import { EnvVariables } from './../app/environment-variables/environment-variables.token';

import { AlertController } from 'ionic-angular';
import { Injectable, Inject } from '@angular/core';
import {Http, RequestOptions, RequestOptionsArgs, XHRBackend, Response, Request, Headers} from '@angular/http';
import {UserData} from './user-data';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/observable/fromPromise';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/of';

@Injectable()
export class HttpClientService extends Http{
    constructor(
        backend: XHRBackend,
        options: RequestOptions,
        private userData: UserData,
        private alertController: AlertController,
            @Inject(EnvVariables) public envVariables: any
            ){
        super(backend, options)
        console.log('HttpClientService:envVariables', envVariables)
    }

     get(url: string, options?: RequestOptionsArgs): Observable<Response> {

        options = this.RetornarOptionRequest(options);

        let optionObservable =  Observable.fromPromise(this.RetornarOptionRequest(options));
        return optionObservable.flatMap( options => {
            return super.get(this.RetornarUrlFull(url), options )
                            .map(res => res.json())
                            .catch((res) => { return this.ProcessarMensagemResposta(res) });
        });
    }

    post(url: string, body: any, options?: RequestOptionsArgs): Observable<Response>{

        options = this.RetornarOptionRequest(options);

        let optionObservable =  Observable.fromPromise(this.RetornarOptionRequest(options));
        return optionObservable.flatMap( _options => {
                return super.post(this.RetornarUrlFull(url), body, _options )
                            .map(res => res.json())
                            .catch((res) => { return this.ProcessarMensagemResposta(res) });
        });
    }

    put(url: string, body: any, options?: RequestOptionsArgs): Observable<Response>{
        options = this.RetornarOptionRequest(options);

        let optionObservable =  Observable.fromPromise(this.RetornarOptionRequest(options));
        return optionObservable.flatMap( options => {
                return super.put(this.RetornarUrlFull(url), body, options )
                            .map(res => res.json())
                            .catch((res) => { return this.ProcessarMensagemResposta(res) });
        });
    }

    delete(url: string, options?: RequestOptionsArgs): Observable<Response>{
        options = this.RetornarOptionRequest(options);

        let optionObservable =  Observable.fromPromise(this.RetornarOptionRequest(options));
        return optionObservable.flatMap( options => {
            return super.delete(this.RetornarUrlFull(url), options )
                            .map(res => res.json())
                            .catch((res) => { return this.ProcessarMensagemResposta(res) });
        });
    }

    patch(url: string, body: any, options?: RequestOptionsArgs): Observable<Response>{
        options = this.RetornarOptionRequest(options);

        let optionObservable =  Observable.fromPromise(this.RetornarOptionRequest(options));
        return optionObservable.flatMap( options => {
                return super.patch(this.RetornarUrlFull(url), body, options )
                            .map(res => res.json())
                            .catch((res) => { return this.ProcessarMensagemResposta(res) });
        });
    }

    head(url: string, options?: RequestOptionsArgs): Observable<Response>{
        options = this.RetornarOptionRequest(options);

        let optionObservable =  Observable.fromPromise(this.RetornarOptionRequest(options));
        return optionObservable.flatMap( options => {
            return super.head(this.RetornarUrlFull(url), options )
                            .map(res => res.json())
                            .catch((res) => { return this.ProcessarMensagemResposta(res) });
        });
    }

    options(url: string, options?: RequestOptionsArgs): Observable<Response> {
        options = this.RetornarOptionRequest(options);

        let optionObservable =  Observable.fromPromise(this.RetornarOptionRequest(options));
        return optionObservable.flatMap( options => {
            return super.options(this.RetornarUrlFull(url), options )
                            .map(res => res.json())
                            .catch((res) => { return this.ProcessarMensagemResposta(res) });
        });
    }

    private RetornarOptionRequest(options?: RequestOptionsArgs): Promise<RequestOptionsArgs> {

        let optionsRequest: RequestOptionsArgs = (options ? options : new RequestOptions());

        return this.userData.getTokenInfo().then(tokenInfo => {
            if( tokenInfo ) {

                let headers = new Headers();

                headers.append('Content-Type', 'application/json; charset=utf-8');
                headers.append("Cache-Control", "no-cache");
                headers.append("Cache-Control", "no-store");
                headers.append("If-Modified-Since", "Mon, 26 Jul 1997 05:00:00 GMT");

                headers.append("AuthorizationConectados", "Bearer " + tokenInfo.token);

                optionsRequest.headers = headers;
            }

            return optionsRequest;
        });
    }

    private RetornarUrlFull(reativeUrl: string): string{
        var url = this.envVariables.apiEndpoint + reativeUrl;
        return url;
    }

    private ProcessarMensagemResposta(resposta: any): any
    {
        console.error("http:error", resposta)
        switch(resposta.status)
        {
            case -1:
            case 502:
            case 0:
                this.exibirMensagem("Sem comunicação com a Duratex. Verifique sua conexao com a internet.", true);
                break;
            case 204:
                return Observable.of(true);
            case 404:
                this.exibirMensagem("Recurso não encontrado.", true);
                break;
            case 406:
                this.exibirMensagem(resposta.statusText);
                break;
            case 500:
                this.exibirMensagem(resposta.statusText, true);
                break;
            case 401:
                this.userData.logout();
                break;

        }

        return Observable.throw({} || 'Server error');
    }

    private exibirMensagem(mensagem: string, erro?: boolean){
        let confirm = this.alertController.create({
        title: erro === true ? 'Erro' : 'Alerta',
        message: mensagem,
        buttons: [
            {
            text: 'Fechar',
            handler: () => {

            }
            }
        ]
        });

        confirm.present();
    }
}
