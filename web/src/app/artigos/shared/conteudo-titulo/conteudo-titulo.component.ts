import {
    Component,
    OnDestroy,
    AfterViewInit,
    EventEmitter,
    Input,
    Output
} from '@angular/core';

@Component({
  selector: 'conteudo-titulo',
  templateUrl: './conteudo-titulo.component.html',
  styleUrls: ['./conteudo-titulo.component.scss']
})
export class ConteudoTituloComponent implements AfterViewInit, OnDestroy {
    public editor;
    public textoInicial;

    @Input() texto: string;
    @Input() elementId: String;
    @Output() textoChange: EventEmitter<String> = new EventEmitter<String>();

    ngOnInit() {
        this.textoInicial = this.texto;
    }

    ngAfterViewInit() {
        tinymce.init({
            selector: '#' + this.elementId,
            theme: 'inlite',
            inline: true,
            plugins: ['paste'],
            skin_url: '../../assets/skins/lightgray',
            toolbar: '',
            selection_toolbar: 'bold italic',
            insert_toolbar: '',
            setup: editor => {
                this.editor = editor;
                editor.on('keyup paste change', () => {
                    const content = editor.getContent();
                    this.texto = content;
                    this.textoAlterado(content);
                });
            },
        });
    }

    textoAlterado(newValue) {
        this.textoChange.emit(newValue);
    }

    ngOnDestroy() {
        tinymce.remove(this.editor);
    }
}
