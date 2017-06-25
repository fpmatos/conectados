import { GoogleAnalytics } from '@ionic-native/google-analytics';
import { Injectable, Inject } from '@angular/core';
import { Http, RequestOptions, Headers, Response } from '@angular/http';

import { Events } from 'ionic-angular';
import { Storage } from '@ionic/storage';

import { EnvVariables } from './../app/environment-variables/environment-variables.token';

@Injectable()
export class UserData {

  _favorites: string[] = [];
  HAS_LOGGED_IN = 'hasLoggedIn';
  HAS_SEEN_TUTORIAL = 'hasSeenTutorial';
  HAS_ACCEPTED_USE_TERM = 'hasAcceptedUseTerm';
  TOKEN_INFO = 'tokenInfo';
  PERFIL_USUARIO = 'perfilUsuario';

  private tokenInfo: {};
  private logado: boolean;
  private visualizouTutorial: boolean;
  private aceitouTermosDeUso: boolean;

  constructor(
    public http: Http,
    public events: Events,
    public storage: Storage,
    public ga: GoogleAnalytics,
    @Inject(EnvVariables) public envVariables: any
  ) {
  }


  signup(username: string): void {
    this.storage.set(this.HAS_LOGGED_IN, true);

    this.events.publish('user:signup');
  };


  hasFavorite(sessionName: string): boolean {
    return (this._favorites.indexOf(sessionName) > -1);
  };

  addFavorite(sessionName: string): void {
    this._favorites.push(sessionName);
  };

  removeFavorite(sessionName: string): void {
    let index = this._favorites.indexOf(sessionName);
    if (index > -1) {
      this._favorites.splice(index, 1);
    }
  };

  login(user: any): Promise<any> {
      let data = "grant_type=password&username=" + user.matricula + "&password=" + user.cpf;
      let options = new RequestOptions();

      options.headers = new Headers();
      options.headers.append("Content-Type", "application/x-www-form-urlencoded");
      options.headers.append("App", "mobile");

        return new Promise((resolve, reject) => {
          this.http.post(this.envVariables.authenticateUrl, data, options)
              .map(response => response.json())
              .subscribe(result => {
                  this.logado = true;
                  this.setTokenInfo({ token: result.access_token, ExpireIn: result.expires_in })
                    .then(() => {
                      this.events.publish('user:login');
                      resolve(result);
                    });
              }, function(response: Response) {
                  this.logado = false;
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
  };

  logout() {
    this.logado = false;
    return this.setTokenInfo(null)
      .then(() => this.storage.remove('username'))
      .then(() => this.events.publish('user:logout'));
  };

  getUsername(): Promise<string> {
    return this.storage.get('username').then((value) => {
      return value;
    });
  };

  hasLoggedIn(): Promise<boolean> {
    if(this.logado !== undefined)
      return Promise.resolve(this.logado);

    return this.getTokenInfo().then((value) => {
      this.logado = value != undefined;
      return this.logado;
    })
    .catch(() => {
      this.logado = false;
      return  this.logado;
    });
  };

  getTokenInfo(): Promise<any>{
    if(this.tokenInfo !== undefined)
      return Promise.resolve(this.tokenInfo);

    return this.storage.get(this.TOKEN_INFO).then((value) => {
      this.tokenInfo = value;
      return value;
    });
  }

  setTokenInfo(data: any) {
    this.tokenInfo = data;

    if(!data)
      return this.storage.remove(this.TOKEN_INFO);
    else
      return this.storage.set(this.TOKEN_INFO, data);
  };


  setHasSeenTutorial(trueFalse = true) {
    this.visualizouTutorial = trueFalse;

    return this.storage.set(this.HAS_SEEN_TUTORIAL, true).then(() => {
      return this.events.publish('user:' + this.HAS_SEEN_TUTORIAL);
    });
  };

  setPerfilUsuario(data: any): Promise<any>{
    return this.storage.set(this.PERFIL_USUARIO, data)
      .then((perfil) => {
        this.events.publish('user:perfil', perfil);
        return perfil;
      });
  }

  getPerfilUsuario(): Promise<any> {
    return this.storage.get(this.PERFIL_USUARIO);
  };

  setHasAcceptedUseTerms(): Promise<any>
  {
    var promise =
        this.getPerfilUsuario()
          .then(perfil => {
            perfil.aceitouTermoUso = true;

            this.setPerfilUsuario(perfil)
            .then(() =>{

            });

            return perfil;
          });

          return promise;
  };

  getHasSeenTutorial(): Promise<boolean> {
    if(this.visualizouTutorial !== undefined)
      return Promise.resolve(this.visualizouTutorial);

    return this.storage.get(this.HAS_SEEN_TUTORIAL).then((value) => {
      this.visualizouTutorial =  value === true;;
      return this.visualizouTutorial;
    });
  };

  getHasAcceptedUseTerm(): Promise<boolean> {
    // if(this.aceitouTermosDeUso !== undefined)
    //   return Promise.resolve(this.aceitouTermosDeUso);

    // return this.storage.get(this.HAS_ACCEPTED_USE_TERM).then((value) => {
    //   this.aceitouTermosDeUso = value === true;;
    //   return this.aceitouTermosDeUso;
    // });

    return this.getPerfilUsuario().then(value => {
      return value.aceitouTermoUso === true;
    });

  };
}
