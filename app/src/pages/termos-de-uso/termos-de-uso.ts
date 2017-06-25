import { AceitesTermosUsoData } from '../../providers/aceites-termos-uso-data';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, MenuController, Events } from 'ionic-angular';
import { UserData } from "../../providers/user-data";
import { Page } from "../page/page";

/**
 * Generated class for the TermosDeUsoPage page.
 *
 * See http://ionicframework.com/docs/components/#navigation for more info
 * on Ionic pages and navigation.
 */
@IonicPage({
  name: 'termos-de-uso'
})
@Component({
  selector: 'page-termos-de-uso',
  templateUrl: 'termos-de-uso.html',
})
export class TermosDeUsoPage extends Page {
  botaoAceitarVisivel: boolean = false;
  loading: boolean = false;

  constructor(
    public events: Events,
    public navCtrl: NavController,
    public navParams: NavParams,
    public menu: MenuController,
    public userData: UserData,
    public aceitesTermoUsoData: AceitesTermosUsoData
    ) {
      super(events);
      this.userData.getHasAcceptedUseTerm().then(value => {
        if(!value)
          menu.enable(false);

        this.botaoAceitarVisivel = !value;
      });
  }

  aceitarTermosDeUso() {
    
    this.loading = true;

    this.aceitesTermoUsoData.Aceitar().subscribe(() => {
          this.userData.setHasAcceptedUseTerms().then(() =>{
              this.loading = false;
              this.menu.enable(true);
              this.navCtrl.setRoot('artigos-lista', {tag: 'noticias-recentes'});
      });
    });
  }
}
