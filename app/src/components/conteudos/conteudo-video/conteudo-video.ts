import { DomSanitizer } from '@angular/platform-browser';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'conteudo-video',
  templateUrl: './conteudo-video.html',
})
export class ConteudoVideoComponent {
  _videoLink: string;

  constructor(public sanitizer:DomSanitizer) {
    
  }
  @Input() preview: boolean;
  @Input() set VideoLink(VideoLink:string){
      this._videoLink = `https://www.youtube.com/embed/${VideoLink}?ecver=2`;
  }
}
