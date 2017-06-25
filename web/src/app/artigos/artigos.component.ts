import { Component, OnInit } from '@angular/core';
import { ArtigosService } from './artigos.service';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';


@Component({
    selector: 'app-artigos',
    templateUrl: './artigos.component.html',
    styleUrls: ['./artigos.component.scss']
})
export class ArtigosComponent implements OnInit {
  

    constructor(artigosService: ArtigosService) { 


    }

    ngOnInit() { 

    }
}
