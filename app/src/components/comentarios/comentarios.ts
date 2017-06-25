import { Component, Input } from '@angular/core';
import { NavController, NavParams, AlertController, Events } from 'ionic-angular';
import { ArtigosData} from '../../providers/artigos-data';
/**
 * Generated class for the ComentariosPage page.
 *
 * See http://ionicframework.com/docs/components/#navigation for more info
 * on Ionic pages and navigation.
 */
@Component({
  selector: 'comentarios',
  templateUrl: 'comentarios.html',
})
export class ComentariosComponent    {

  comentarios: any = null;
  _artigoId: number;
  textoComentario: any;
  @Input()
  set artigoId(artigoId: number) {
    this._artigoId = artigoId;

  }

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    private artigosData: ArtigosData,
    private alertController: AlertController,
    private events: Events
    ) {

  }

  carregarComentarios(){

      this.comentarios =
        this
        .artigosData
        .retornarComentarios(this._artigoId);
  }

  reportar(item: any)  {

    let confirm = this.alertController.create({
      title: 'Denúncia de comentário',
      message: 'Deseja realmente denunciar o comentário?',
      buttons: [
        {
          text: 'Não',
          handler: () => {

          }
        },
        {
          text: 'Sim',
          handler: () => {
            this.artigosData
              .denunciarComentario(this._artigoId, item.id)
              .subscribe(result =>{});
          }
        }
      ]
    });

    confirm.present();
  }

  enviarComentario(){
    this.artigosData.postarComentario(this._artigoId, this.textoComentario).subscribe(() =>{
      this.textoComentario = "";
      this.carregarComentarios();
      this.events.publish('user:comentar');
    });
  }
}
