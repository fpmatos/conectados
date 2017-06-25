import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { INgxMyDpOptions, IMyDateModel } from "ngx-mydatepicker";

@Component({
  selector: 'conteudo-enquete',
  templateUrl: './conteudo-enquete.component.html',
  styleUrls: ['./conteudo-enquete.component.scss']
})
export class ConteudoEnqueteComponent implements OnInit {


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

  _alternativas: any[];

  _dataEncerramento: any;  

  @Input() set dataEncerramento(dataEncerramento){

    if(dataEncerramento)
    { 
      let date = new Date(dataEncerramento);
      this._dataEncerramento = { date: { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDay() + 1  }, dateRef: date};
    }
  }

  @Input() set alternativas(alternativas){
    this._alternativas = alternativas;
  };  

  @Output() dataEncerramentoChange: EventEmitter<String> = new EventEmitter<String>();

  constructor() { }

  ExcluirAlternativa(item){
    var index = this._alternativas.indexOf(item);
    this._alternativas.splice(index, 1)
  }

  AdicionarAlternativa(){
    if(!this._alternativas.find(item => item.descricao == ''))
      this._alternativas.push({descricao: '', id: 0});
  }

  ngOnInit() {
 
  }

   onDateChanged(event: IMyDateModel): void {
      this.dataEncerramentoChange.emit( event && event.jsdate ? (new Date(event.jsdate)).toUTCString() : null);   

      if(this._dataEncerramento)
        this._dataEncerramento["dateRef"] =  event && event.jsdate ? new Date(event.jsdate) : null;
    }

    resetarData(){ 
      this._dataEncerramento = null;
      this.dataEncerramentoChange.emit(null);   
    }
}
