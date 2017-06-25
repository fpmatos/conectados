import { Component, Input } from '@angular/core';

import {
    HeilbaumPhotoswipeController,
    PhotoswipeItem,
    PhotoswipeOptions,
    HeilbaumPhotoswipe } from "heilbaum-ionic-photoswipe/dist";

@Component({
  selector: 'conteudo-galeria',
  templateUrl: './conteudo-galeria-imagem.html',
})
export class ConteudoGaleriaImagemComponent {

  photoswipeItems: PhotoswipeItem[];
  @Input() preview: boolean;
  @Input() set listaArquivos(listaArquivos:object[]){
    this.photoswipeItems = listaArquivos.map((imagem:any) => <PhotoswipeItem>{
      title: imagem.descricao,
      h: imagem.upload.height,
      w: imagem.upload.width,
      src: this.retornarImagemData(imagem.upload.blob, imagem.upload.mediaType)
    });
  }

  constructor(private pswpCtrl: HeilbaumPhotoswipeController) {}

  retornarImagemData(content:string, mediaType:string){
    return `data:${mediaType};base64, ${content}`;
  }

  pswpMultiThumbnail(index: number): void {
    const options: PhotoswipeOptions = {
        index: index,
        history: false,
        clickToCloseNonZoomable: false,
        showHideOpacity: true,
        shareEl: false
    };

    const pswp: HeilbaumPhotoswipe = this.pswpCtrl.create(this.photoswipeItems, options);
    pswp.present({ animate: false });
    pswp.setLeavingOpts({ animate: false });
  }
}
