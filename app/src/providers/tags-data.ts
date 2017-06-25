import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HttpClientService } from "./http-client";

@Injectable()
export class TagsData {

  public tags:Tag[] = [
    { tag: "noticias-recentes", title: "Notícias Recentes",                        icon: 'globe'},
    { tag: "destaques",         title: "Destaques",          tagId: 2,             icon: 'star' },
    { tag: "itens-curtidos",    title: "Ítens Curtidos",     apenasCurtidos: true, icon: 'ios-thumbs-up' },
    { tag: "enquetes",          title: "Enquetes",           tagId: 3,             icon: 'stats' },
    { tag: "duraseg",           title: "Duraseg",            tagId: 4 },
    { tag: "duratex-no-mundo",  title: "Duratex no mundo",   tagId: 5 },
    { tag: "fique-por-dentro",  title: "Fique por dentro",   tagId: 6 },
    { tag: "voce-sabia",        title: "Você sabia?",        tagId: 7 },
    { tag: "nossa-gente",       title: "Nossa gente",        tagId: 8 },
    { tag: "nossas-unidades",   title: "Nossas unidades",    tagId: 9 },
    { tag: "nossos-resultados", title: "Nossos resultados",  tagId: 11 },
    { tag: "reconhecimentos",   title: "Reconhecimentos",    tagId: 12 },
    { tag: "imagens",           title: "Imagens",            tagId: 13 },
    { tag: "vídeos",            title: "Vídeos",             tagId: 14 }
  ]

  constructor(private http: HttpClientService) {

  }

  Retornar(tagId?: number): Observable<any> {
    if (tagId)
      return this.http.get(`tags/${tagId}`);
    else
      return this.http.get(`tags`);
  }

  obterPorTag(busca:string){
    return this.tags.find((tag) => tag.tag == busca)
  }

  obterPorTagId(busca:number){
    return this.tags.find((tag) => tag.tagId == busca)
  }
}

export class Tag {
  tag:string
  title:string
  tagId?:number
  apenasCurtidos?:boolean
  icon?:string
}
