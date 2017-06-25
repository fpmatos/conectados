import {
    Component,
    Input
} from '@angular/core';

@Component({
    selector: 'conteudo-texto',
    templateUrl: './conteudo-texto.html',
})
export class ConteudoTextoComponent {
    @Input() texto: string;
    @Input() preview: boolean;
}
