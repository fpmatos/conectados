import { TermosDeUsoService } from './termos-de-uso.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-termos-uso-aceitos',
  templateUrl: './termos-uso-aceitos.component.html',
  styleUrls: ['./termos-uso-aceitos.component.scss']
})
export class TermosUsoAceitosComponent implements OnInit {

  lista: any;

  constructor(private service: TermosDeUsoService) { }

  ngOnInit() {
    this.lista = this.service.retornarTermosUsoAceitos().map(res => res.json());
  }

}
