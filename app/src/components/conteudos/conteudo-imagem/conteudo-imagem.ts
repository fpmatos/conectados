import { Component, Input } from '@angular/core';

import {
    HeilbaumPhotoswipeController,
    PhotoswipeItem,
    PhotoswipeOptions,
    HeilbaumPhotoswipe } from "heilbaum-ionic-photoswipe/dist";

@Component({
  selector: 'conteudo-imagem',
  templateUrl: './conteudo-imagem.html',
})
export class ConteudoImagemComponent {
  photoswipeItems: PhotoswipeItem[];
  singlePhotoswipeItem: PhotoswipeItem;
  @Input() preview: boolean;
  @Input() set imagem(imagem:any){
    this.photoswipeItems = [imagem].map((imagem:any) => <PhotoswipeItem>{
      title: imagem.descricao,
      h: imagem.upload.height,
      w: imagem.upload.width,
      src: this.retornarImagemData(imagem.upload.blob, imagem.upload.mediaType)
    });
    this.singlePhotoswipeItem = this.photoswipeItems[0];
  }

  constructor(private pswpCtrl: HeilbaumPhotoswipeController) {}

  retornarImagemData(content:string, mediaType:string){
    return `data:${mediaType};base64, ${content}`;
  }

  pswpSingleThumbnail(): void {
    const options: PhotoswipeOptions = {
        history: false,
        clickToCloseNonZoomable: false,
        showHideOpacity: true,
        shareEl: false,
    };

    const pswp: HeilbaumPhotoswipe = this.pswpCtrl.create(this.photoswipeItems, options);
    pswp.present({ animate: false });
    pswp.setLeavingOpts({ animate: false });
  }
}
