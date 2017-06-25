import { UserData } from '../../providers/user-data';
import { ProtectedPage } from '../protected-page/protected-page';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, Refresher, Events } from 'ionic-angular';

import {TagsData} from '../../providers/tags-data';
import { ArtigosData } from "../../providers/artigos-data";
/**
 * Generated class for the ArtigosListaPage page.
 *
 * See http://ionicframework.com/docs/components/#navigation for more info
 * on Ionic pages and navigation.
 */
const ARTIGOS_POR_REQUEST: number = 10;

@IonicPage({
  name: 'artigos-lista',
  segment: 'artigos-lista/:tag'
})
@Component({
  selector: 'page-artigos-lista',
  templateUrl: 'artigos-lista.html',
})
export class ArtigosListaPage  extends ProtectedPage {
  private tagIdsGalerias: number[] = [13, 14];

  lista: any[] = [];
  parameterFilterData: any = {};
  configData: any = {};

  constructor(
    public events: Events,
    public navCtrl: NavController,
    public navParams: NavParams,
    public userData: UserData,
    public tagsData: TagsData,
    public artigosData: ArtigosData) {
      super(events, navCtrl, navParams, userData);
  }

  ionViewDidLoad(){
    super.ionViewDidLoad();
    var tag = this.navParams.get("tag");
    var config = this.tagsData.obterPorTag(tag);

    this.definirConfiguracao(config);
  }

  verificarSeTipoListagemGaleria(): boolean {
        let result = false;

        if(this.configData)
            result = this.tagIdsGalerias.find(item => item === this.configData.tagId) > -1;

        return result;
    }

    definirConfiguracao(data: any) {
        this.configData = data;
        this.carregarLista();
    }

    irParaArtigo(item: any){
        this.navCtrl.push('artigos-detalhe', {
            id: item.id
        });
    }

    verificarSeDestaque(item: any){
        return this.verificarSeContemTag(item, 'Destaques');
    }

    VerificarSeBanner(item: any){
        return this.verificarSeContemTag(item, 'Banners');
    }

    verificarSeContemTag(item: any, tagName: string){
        let tags: any[] = item.tags;
        return tags.find(item => item.nome === tagName) != undefined;
    }

    doRefresh(refresher: Refresher){
        this.carregarLista().then(() => {
            refresher.complete();
        });
    }

    doInfinite(): Promise<any>{
        return new Promise(resolve => {
            this.carregarLista(true).then(() =>{
                resolve();
            });
        });
    }

    verificarSeUltimaPagina(): boolean {
        var  result =
        this.parameterFilterData.todosRegistrosLidos != undefined
            && this.parameterFilterData.todosRegistrosLidos === true;

            return result;
    }

    irParaEditoria(editoria: any){
        alert(editoria)
    }

    private carregarLista(proximaPagina?: boolean): Promise<any> {

        let tagId = this.configData.tagId;
        let apenasCurtidos = this.configData.apenasCurtidos === true;

        if(!proximaPagina)
            this.parameterFilterData = {};

        return new Promise((resolve, reject) => {
            this.artigosData.pesquisarArtigos({
                tagId: tagId,
                apenasCurtidos: apenasCurtidos,
                paginaCorrente: this.parameterFilterData.paginaCorrente,
                porRequisicao: ARTIGOS_POR_REQUEST
            })
                    .subscribe(result => {

                        if(!proximaPagina)
                            this.lista = result.data;
                        else
                            this.adicionarItensNaLista(result.data);

                        this.parameterFilterData = result.metadata;
                        resolve();
                    });

        });
    }

    private adicionarItensNaLista(itens: any[]){
        for(let item of itens){
            this.lista.push(item);
        }
    }

}
