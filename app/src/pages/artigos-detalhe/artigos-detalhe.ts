import { UserData } from '../../providers/user-data';
import { ProtectedPage } from '../protected-page/protected-page';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, Events } from 'ionic-angular';
import { Observable } from 'rxjs/Observable';
import { ArtigosData } from "../../providers/artigos-data";


/**
 * Generated class for the ArtigosDetalhePage page.
 *
 * See http://ionicframework.com/docs/components/#navigation for more info
 * on Ionic pages and navigation.
 */
@IonicPage({
  name: 'artigos-detalhe',
  segment: 'artigos-detalhe/:id'
})
@Component({
  selector: 'page-artigos-detalhe',
  templateUrl: 'artigos-detalhe.html',
})
export class ArtigosDetalhePage extends ProtectedPage {
  artigo$: Observable<any>;

  constructor(
    public events: Events,
    public navCtrl: NavController,
    public navParams: NavParams,
    public userData: UserData,
    public artigosData:ArtigosData) {
    super(events, navCtrl,navParams, userData);
  }

  ionViewDidLoad(){
    super.ionViewDidLoad();
    var id = +this.navParams.get("id");
    this.artigo$ = this.artigosData.retornarArtigo(id);
  }
}
