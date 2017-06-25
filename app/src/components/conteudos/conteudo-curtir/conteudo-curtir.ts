import { Events } from 'ionic-angular';
import {
    Component,
    Input,
    EventEmitter,
    Output
} from '@angular/core';
import { ArtigosData } from "../../../providers/artigos-data";

@Component({
    selector: 'conteudo-curtir',
    templateUrl: './conteudo-curtir.html',
})
export class ConteudoCurtirComponent {
    loading:boolean = false;

    @Input() artigo: any;
    @Input() preview: boolean;
    @Output() artigoChange: EventEmitter<any> = new EventEmitter<any>();

    constructor(
      private events: Events,
      private artigosData : ArtigosData
    ){}

    curtir() {
        this.loading = true;
        this.artigosData.curtirArtigo(this.artigo.id).subscribe(() => {
            this.artigo.totalCurtidas++;
            this.artigo.usuarioJaCurtiu = true;
            this.artigoChange.emit(this.artigo);
            this.events.publish('user:curtir');
            this.loading = false;
        });
    }

    descurtir() {
        this.loading = true;
        this.artigosData.descurtirArtigo(this.artigo.id).subscribe(() => {
            this.artigo.totalCurtidas--;
            this.artigo.usuarioJaCurtiu = false;
            this.artigoChange.emit(this.artigo);
            this.events.publish('user:descurtir');
            this.loading = false;
        });
    }
}
