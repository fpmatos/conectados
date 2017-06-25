import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'conteudo-video',
  templateUrl: './conteudo-video.component.html',
  styleUrls: ['./conteudo-video.component.scss']
})
export class ConteudoVideoComponent implements OnInit {

  _videoLink: string;

  @Input() set VideoLink(VideoLink){
      this.Alterando = !VideoLink;
      this.ultimaUrlVideo = VideoLink;
      this._videoLink = VideoLink;
  }

  @Output() VideoLinkChange:EventEmitter<String> = new EventEmitter<String>();

  ultimaUrlVideo: string;
  Alterando: boolean = false;

  constructor() { }

  ModificarLinkVideo(){
    this.Alterando = true;
  }

  EfetivarAlterar(){
    this.VideoLinkChange.emit(this._videoLink);
    this.ultimaUrlVideo = this._videoLink;
    this.Alterando = false;
  }

  CancelarAlteracao(){
    this.Alterando = false;
    this._videoLink = this.ultimaUrlVideo;
  }

  ExibirThumbnailVideo(){
    return  this.ultimaUrlVideo && !this.Alterando;
  }

  ExibirOpcaoAlteracaoUrlVideo(){
    return  !this.Alterando && this._videoLink;
  }

    ExibirOpcaoCancelarAlteracao(){
    return this.Alterando;
  }

    ExibirOpcaoEfetivarAlteracao(){
    return this.Alterando && this._videoLink;
  }

  ExibirCampoAlteracaoUrlVideo(){
    return  this.Alterando;
  }

  ngOnInit() {
  }

  GetTumbnailVideoUrl(){
    return `http://img.youtube.com/vi/${this.ultimaUrlVideo}/default.jpg`;
  }
}
