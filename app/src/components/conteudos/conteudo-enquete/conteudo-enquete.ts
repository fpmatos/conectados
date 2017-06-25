import { ArtigosData } from '.././../../providers/artigos-data';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'conteudo-enquete',
  templateUrl: './conteudo-enquete.html',

})
export class ConteudoEnqueteComponent {
  @Input() enquete:any;
  @Input() preview: boolean;
  selecionado:any;
  loading:boolean = false;

  constructor(
    private artigosData : ArtigosData
  ){}

  possuiResultado():boolean {
    return ( !this.enquete.dataEncerramentoEnquete || new Date(this.enquete.dataEncerramentoEnquete)  < new Date())
        || this.enquete.respostaEnqueteId
        || this.preview
  }

  responderEnquete(alternativa:any){
    this.loading = true;
    this.artigosData.responderEnquete(this.enquete.artigoId, this.enquete.id, alternativa.id).subscribe(() =>{
      alternativa.totalRespostas ++;
      this.enquete.respostaEnqueteId = alternativa.id;
      this.loading = false;
    });
  }
}
