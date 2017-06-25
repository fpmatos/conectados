import { INgxMyDpOptions } from 'ngx-mydatepicker';
import { IMyDateModel } from 'ngx-mydatepicker';
import { Component, OnInit, ViewChildren, QueryList, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LayoutsService } from '../shared/layouts.service';
import { ArtigosService } from '../artigos.service';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { environment } from 'environments/environment';


@Component({
  selector: 'app-artigos-detalhe',
  templateUrl: './artigos-detalhe.component.html',
  styleUrls: ['./artigos-detalhe.component.scss']
})
export class ArtigosDetalheComponent implements OnInit {
   public appUrl = environment.appUrl;
   public previewUrl = this.appUrl + "/#/artigos-preview";
   public androidUrl = this.previewUrl + "?production=true&ionicplatform=android";
   public iosUrl = this.previewUrl + "?production=true&ionicplatform=ios";
   public windowsUrl = this.previewUrl + "?production=true&ionicplatform=windows";
   time: any;

   private dtOptions: INgxMyDpOptions = {
        // other options...
        dateFormat: 'dd/mm/yyyy',
        dayLabels:
        {
          su: 'Dom',
          mo: 'Seg',
          tu: 'Ter',
          we: 'Qua',
          th: 'Qui',
          fr: 'Sex',
          sa: 'Sab'
        },
        monthLabels:
        {
          1: 'Jan',
          2: 'Fev',
          3: 'Mar',
          4: 'Abr',
          5: 'Mai',
          6: 'Jun',
          7: 'Jul',
          8: 'Ago',
          9: 'Set',
          10: 'Out',
          11: 'Nov',
          12: 'Dez'
        },
        todayBtnTxt: 'Hoje'
    };

  public artigo: any;

  _dataPublicacao: any;
  Teste: any;
  layouts: any;
  layoutIdSelecionado: any = "";
  previewPlatform: string;
  iosActive: boolean = false;
  androidActive: boolean = true;
  windowsActive: boolean = false;
  iframeLoading = false;
  @ViewChildren('iframe') iframes: QueryList<ElementRef>;

  constructor(public router: Router, private route: ActivatedRoute, private layoutsService: LayoutsService, private artigosService: ArtigosService) {
    this.setPlatform("android");
  }

  // ngAfterViewInit(){
  //   var artigo = this.artigo;
  //   this.iframes.forEach(iframe => {
  //       iframe.nativeElement.onload = function(){
  //           this.contentWindow.postMessage(artigo,'*');
  //       }
  //   });
  // }

  updateIframes() {
    if (this.iframeLoading)
      return;
    this.iframeLoading = true;
    console.log(this.artigo);
    this.iframes.forEach(iframe => {
      let doc = iframe.nativeElement.contentWindow;
      doc.postMessage(this.artigo, '*');
    });

    setTimeout(() => {
      this.iframeLoading = false;
    }, 2000);
  }

  setPlatform(platform: string) {
    this.previewPlatform = platform;
    this.iosActive = platform == "ios";
    this.androidActive = platform == "android";
    this.windowsActive = platform == "windows"
  }

  Salvar() {
    this.artigo.titulo = this.RetornarTituloConteudoArtigo();

    this.artigosService
      .Salvar(this.artigo)
      .map(res => res.json())
      .subscribe(res => {
        if (!this.artigo.id) {
          this.artigo = res;
        }
        this.router.navigate(['/artigos', 'lista']);
      });
  }

  LayoutSelecionado() {

    this.layoutsService
      .RetornarCopiaLayout(this.layoutIdSelecionado)
      .map(res =>
        res.json()
      )
      .subscribe((res) => {
        this.artigo = res;
        this.updateIframes();
        return res;
      });
  }

  private CarregarArtigo() {

    this.route.params.subscribe(params => {
      if (params["id"]) {
        this.CarregarArtigoExistente(params["id"]);
      }
      else
        this.CarregarNovoArtigo();
    });
  }

  private CarregarArtigoExistente(id: number) {
    return this.artigosService
      .RetornarArtigo(id)
      .map(response => response.json())
      .subscribe(value => {
        this.artigo = value;
        this.layoutIdSelecionado = value.layoutId;
        this.definirPeriodoPublicacao();
        this.updateIframes();
      });
  }

  private definirPeriodoPublicacao() {
    if(this.artigo.dataPublicacao)
    {
        let dateObject = new Date(this.artigo.dataPublicacao);
        this._dataPublicacao = { date: { year: dateObject.getFullYear(), month: dateObject.getMonth() + 1, day: dateObject.getDay() + 1  }};
    }
  }

  private CarregarNovoArtigo() {
    this.artigo = null;
  }

  private CarregarLayouts(): Observable<any> {
    this.layouts =
      this
        .layoutsService
        .RetornarLayouts()
        .map(response => response.json());

    return this.layouts;
  }

  private RetornarTituloConteudoArtigo() {
    let titulo = "";

    var conteudoTitulo = this.RetornarPrimeiroConteudosPorTipo("Titulo");

    if (conteudoTitulo)
      titulo = conteudoTitulo.textoTitulo;

    return titulo;
  }

  private RetornarPrimeiroConteudosPorTipo(tipo) {
    let conteudos: any[] = this.artigo.conteudos;

    return conteudos.find(conteudo => conteudo.tipoConteudo == tipo);
  }

  ngOnInit() {

    this.CarregarArtigo();
    this.CarregarLayouts()
      .subscribe(value => {
        this.CarregarArtigo();
      });

    window.addEventListener('message', (event) => {
      // IMPORTANT: Check the origin of the data!
      if (event.origin.indexOf(this.appUrl) == 0) {
        // Read and elaborate the received data
        event.source.postMessage(this.artigo, event.origin);
      }
    });
  }

  dataPublicacaoAlterada(event: IMyDateModel){
    this.artigo["dataPublicacao"] = event.jsdate ? (new Date(event.jsdate)).toUTCString() : null;
  }


  resetarDataPublicacao(){
    this._dataPublicacao = null;
    this.artigo.dataPublicacao = null;
  }

  testar() {

    let x: Date = new Date(this.artigo.dataPublicacao);

    console.log(x);

      x.setHours(15);

      console.log(x);

      this.artigo.dataPublicacao = x.toLocaleString();

  }
}
