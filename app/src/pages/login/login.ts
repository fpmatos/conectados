import { Page } from '../page/page';
import { PerfilData } from '../../providers/perfil-data';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IonicPage, NavController, NavParams, MenuController, Events } from 'ionic-angular';

import { UserData } from '../../providers/user-data';
/**
 * Generated class for the LoginPage page.
 *
 * See http://ionicframework.com/docs/components/#navigation for more info
 * on Ionic pages and navigation.
 */
@IonicPage({
  name: 'login'
})
@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
})
export class LoginPage extends Page {
  login: {matricula?: string, cpf?: string} = {};
  submitted = false;
  mensagemErro: string = null;

  constructor(
    public events: Events,
    public navCtrl: NavController,
    public navParams: NavParams,
    public userData: UserData,
    public menu: MenuController,
    public perfilData: PerfilData
    ) {
    super(events)
    menu.enable(false);
  }

  onLogin(form: NgForm) {
    this.submitted = true;

    if (form.valid) {
      this.userData.login(this.login)
        .then(() => this.perfilData.retornar().toPromise())
        .then(perfil => this.userData.setPerfilUsuario(perfil))
        .then(() => {
          this.menu.enable(true);
          this.navCtrl.setRoot('artigos-lista', {tag: 'noticias-recentes'});
        })
        .catch(error => {
          console.error('onLogin', error)
          this.mensagemErro = error;
        });
    }
  }

}
