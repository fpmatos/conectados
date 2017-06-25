import { Page } from '../page/page';
import { UserData } from '../../providers/user-data';
import { NavController, NavParams, Events } from 'ionic-angular';

export class ProtectedPage extends Page{

  redirect:string;

  constructor(
    public events: Events,
    public navCtrl: NavController,
    public navParams: NavParams,
    public userData: UserData) {
      super(events);
  }

  ionViewCanEnter() {
    this.redirect = null;
    return this.verificarUsuarioLogado()
      .then(() => this.verificarUsuarioAceitouTermosDeUso())
      .then(() => this.verificarUsuarioViuTutorial())
      .catch((error) => this.redirecionarUsuario(error))
  }

  private verificarUsuarioLogado() {
    return this.userData.hasLoggedIn().then(usuarioLogado => {
      if (usuarioLogado){
        return Promise.resolve();
      }

      this.redirect = 'login';
      return Promise.reject("Usário ainda não está logado.")
    });
  }

  private verificarUsuarioAceitouTermosDeUso() {
    return this.userData.getHasAcceptedUseTerm().then(aceitouTermosDeUso => {
      if (aceitouTermosDeUso)
        return Promise.resolve();

      this.redirect = 'termos-de-uso';
      return Promise.reject("Usário ainda não aceitou os termos de uso.");
    });
  }

  private verificarUsuarioViuTutorial() {
    return this.userData.getHasSeenTutorial().then(visualizouTutorial => {
      if (visualizouTutorial)
        return Promise.resolve();

      this.redirect = 'tutorial';
      return Promise.reject("Usário ainda não vizualizou o tutorial.");
    });
  }

  private redirecionarUsuario(error:any) {
    if(this.redirect) {
      console.log(`redirecionado para: ${this.redirect}`);
      this.events.publish('page:forbiden', this.redirect);
      return Promise.reject(`redirecionado para: ${this.redirect}`);
    }

    console.error(error);
  }
}
