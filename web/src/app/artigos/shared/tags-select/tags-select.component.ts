import { Component, OnInit, Input } from '@angular/core';
import {TagsService} from '../tags.service';

@Component({
  selector: 'tags-select',
  templateUrl: './tags-select.component.html',
  styleUrls: ['./tags-select.component.scss']
})
export class TagsSelectComponent implements OnInit {

  _tagsSelecionadas: number[] = [];
  _tags: any[];

  @Input() set tagsSelecionadas(tagsSelecionadas){
      
      this._tags = tagsSelecionadas;
      
      if(tagsSelecionadas)
      tagsSelecionadas.forEach(element => {
        this._tagsSelecionadas.push(element.id);
      });
  };

  lista: any;
  constructor(private tagsService: TagsService) { 
    this.lista = tagsService.RetornarTags().map(res => res.json());
  }

  tagsAlteradas(tagsIds: number[]){    

    let idsExcluir = [];

    tagsIds.forEach(id => {
        let tagIndex = this._tags.findIndex(item => item.id == id);

        if(tagIndex < 0)
        {
          this._tags.push({id: id});
        }
    });
    
    this._tags.forEach(item => {
      let idIndex = tagsIds.findIndex(_id => _id == item.id);

      if(idIndex < 0)
        idsExcluir.push(item.id);
    })
    
    idsExcluir.forEach(_id =>{
      let idIndex = this._tags.findIndex(tag => _id == tag.id);

      if(idIndex > -1)
        this._tags.splice(idIndex, 1);
    });
  }

  ngOnInit() {

  }

}
