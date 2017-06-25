import { UserData } from './user-data';
import { GoogleAnalytics } from '@ionic-native/google-analytics';
import { Platform, Events } from 'ionic-angular';
import {Injectable, Inject} from '@angular/core';
import { EnvVariables } from './../app/environment-variables/environment-variables.token';

@Injectable()
export class Analytics{
    codigoDeAcompanhamento = 'UA-100887695-1';

    constructor(
      private platform : Platform,
      private ga:GoogleAnalytics,
      private userData:UserData,
      private events:Events,
      @Inject(EnvVariables) public envVariables: any){

      this.codigoDeAcompanhamento = envVariables.googleAnalyticsId;

      events.subscribe('user:perfil', (perfil:any) =>
        this.executarQuandoDisponivel(()=> {
          console.log('user:perfil', perfil);
          this.definirUsuario(perfil)
        })
      );

      events.subscribe('user:visualizarPagina', (pagina:string) =>
        this.executarQuandoDisponivel(()=> {
          console.log('user:visualizarPagina', pagina);
          this.ga.trackView(pagina)
        })
      );

      events.subscribe('user:curtir', () =>
        this.executarQuandoDisponivel(()=> {
          console.log('user:curtir');
          this.ga.trackEvent('curtir', 'click')
        })
      );

      events.subscribe('user:descurtir', () =>
        this.executarQuandoDisponivel(()=> {
          console.log('user:descurtir');
          this.ga.trackEvent('descurtir', 'click')
        })
      );

      events.subscribe('user:comentar', () =>
        this.executarQuandoDisponivel(()=> {
          console.log('user:comentar');
          this.ga.trackEvent('comentar', 'click')
        })
      );
    }

    inicializar(){
      this.executarQuandoDisponivel(() => {
        this.ga.debugMode();

        return this.ga.startTrackerWithId(this.codigoDeAcompanhamento)
          .then(() => {
            console.log('Google analytics is ready now');
            return this.userData.getPerfilUsuario()
          })
          .then((perfil) => {
            this.definirUsuario(perfil);
          })
          .catch(e => console.log('Error starting GoogleAnalytics', e));
      });
    }

    visualizarPagina(pagina:string): Promise<any> {
      return this.executarQuandoDisponivel(() =>
        this.ga.trackView(pagina))
    }

    private definirUsuario(perfil:any){
      if(perfil){
        this.ga.setUserId(perfil.nomeDeUsuario);
        this.ga.trackMetric('nome', perfil.nomeDePerfil)
        this.ga.trackMetric('aceitouTermoUso', perfil.aceitouTermoUso);
      }
    }

    private executarQuandoDisponivel(comando: Function){
      return this.platform.ready()
        .then(() => {
          if (this.platform.is('cordova')) {
            return comando();
          }
        })
    }
}
