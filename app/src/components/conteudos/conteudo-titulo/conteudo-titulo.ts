import {
    Component,
    Input
} from '@angular/core';

@Component({
  selector: 'conteudo-titulo',
  templateUrl: './conteudo-titulo.html',
})
export class ConteudoTituloComponent {
    @Input() texto: string;
    @Input() preview: boolean;
}
