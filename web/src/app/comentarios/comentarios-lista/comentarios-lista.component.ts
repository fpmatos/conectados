import { ActivatedRoute, Params, Router } from '@angular/router';
import { ComentariosService } from './../comentarios.service';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-comentarios-lista',
  templateUrl: './comentarios-lista.component.html',
  styleUrls: ['./comentarios-lista.component.scss']
})
export class ComentariosListaComponent implements OnInit {

  @Input() ArtigoId: number;
  Itens: any[] = [];
  comentario: string;
  OpcoesVisualizacaoImproprios: any[] = ['Todos','Apenas impróprios','Apenas não impróprios'];
  OpcaoVisualizacaoImproprioSelecionado: string = 'Todos';

  OpcoesVisualizacaoDenunciado: any[] = ['Todos','Apenas denunciados','Apenas não denunciados'];
  OpcaoVisualizacaoDenunciadoSelecionado: string = 'Todos';

  constructor(
    private comentariosService: ComentariosService, 
    private activatedRoute: ActivatedRoute, private router: Router) {     
  }

  CarregarLista(){

    this.comentariosService.RetornarComentarios(this.ArtigoId)
      .map(res => res.json())
      .toPromise<any>()
      .then((result) => {
        this.Itens = result;
      });
  }
  
  podeComentar(){
    return this.ArtigoId != undefined;
  }

  MarcarComoConfiavel(item){
        this.MarcarComentarioComoOfensivo(item, false);
  }

  MarcarComoOfensivo(item){
    this.MarcarComentarioComoOfensivo(item, true);
  }

  private MarcarComentarioComoOfensivo(item, value: boolean)
  {
      this.comentariosService.NegativarComentario(item.artigoId, item.id,  value).toPromise().then(() => {
        item.marcadoComoImproprio = value;
      });        
  }

  ngOnInit() {

    this.CarregarLista();

    if(this.router.url.indexOf('denunciados') > -1)
      this.OpcaoVisualizacaoDenunciadoSelecionado = "Apenas denunciados";  

    // this.activatedRoute.queryParams.subscribe((params: Params) => {
    //   console.log("params:", this.activatedRoute.data);

    //   if(params["apenasDenunciados"])
    //     this.OpcaoVisualizacaoDenunciadoSelecionado = "Apenas denunciados";    
    // });    
  }

  enviarComentario(){
    this.comentariosService.enviarComentario(this.ArtigoId, this.comentario)
    .map(item => item.json())
    .subscribe(item => {
      this.CarregarLista();
      this.comentario = "";
    });
  }  
}
