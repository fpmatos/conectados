import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ContextoService } from './contexto.service';
import { UsuarioModel } from './models/usuario.model';
import { PerfilService} from 'app/shared/perfil.service';
import { Http, RequestOptions, Headers, Response } from '@angular/http';

import { environment } from 'environments/environment';


@Injectable()
export class AutenticacaoService {

    constructor(
        private contexto: ContextoService, 
        private http: Http, 
        private perfilService: PerfilService) {
            
    }

    public Logoff(){
        this.contexto.DefinirAutenticacaoDados(null);
        this.contexto.DefinirUsuario(null);
    }

    public Autenticar(nomeUsuario: string, senha: string): Promise<any> {

        let data = "grant_type=password&username=" + nomeUsuario + "&password=" + senha;
        let options = new RequestOptions();

        options.headers = new Headers();
        options.headers.append("Content-Type", "application/x-www-form-urlencoded");
        options.headers.append("App", "cms");

        return new Promise((resolve, reject) => {
        this.http.post(environment.authenticateUrl, data, options)
            .map(response => response.json())
            .subscribe(result => {
                this.contexto.DefinirAutenticacaoDados({
                    Token: result.access_token,
                    ExpireIn: result.expires_in                   
                    
                });

                this.DefinirPerfilUsuario()
                .then(() => {
                    resolve(true);
                });               

            }, function(response: Response){
                
                let description: string;

                switch(response.status)
                {
                    case 0:
                    case -1:
                    case 502:
                        description = "Sem comunicação com o servidor. Verifique sua conexao com a internet.";           
                        break;
                    default: 
                        description = response.json().error_description;          
                        break;
                }               
                
                reject(description);
            });
        });
    }

    public UsuarioAutenticado(): boolean {

        let result = false;

        if(this.contexto.RetornarAuth())
            result = true;
        
        return result;
    }

    private DefinirPerfilUsuario(): Promise<any> {

        return new Promise((resolve, reject) => 
            this.perfilService.RetornarPerfil()
            .then( response => {

                let usuario: UsuarioModel = response;
                this.contexto.DefinirUsuario(usuario);
                resolve(true);
            })
            .catch(() => {
                reject(false);
            })

        );
    }
}