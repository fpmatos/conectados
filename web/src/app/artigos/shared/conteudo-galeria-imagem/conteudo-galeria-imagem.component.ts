import { Component, OnInit, Input } from '@angular/core';
import { FileUploader, FileLikeObject, FileItem, FileUploaderOptions } from 'ng2-file-upload';
import {LayoutGaleriaImagemService} from '../layout-galeria-imagem.service';
import {ArquivoItem} from './arquivo-item.model';
import {  DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';

import { environment } from 'environments/environment';

@Component({
  selector: 'conteudo-galeria',
  templateUrl: './conteudo-galeria-imagem.component.html',
  styleUrls: ['./conteudo-galeria-imagem.component.scss']
})
export class ConteudoGaleriaImagemComponent implements OnInit {

  _listaArquivos: any[];
  @Input() set listaArquivos(listaArquivos){
    this._listaArquivos =   listaArquivos;
  }

  @Input() TipoArquivo: string;
  @Input() NumeroItens: number = 20;

  ListaInfinita: boolean;

  public FileUploader:FileUploader

  constructor(private layoutGaleriaImagemService: LayoutGaleriaImagemService, private _sanitizer: DomSanitizer) {

  }

  private UploadConfig(){

        var uploadOptions: FileUploaderOptions = {
          autoUpload: true,
          url: environment.uploadUrl,
          queueLimit: this.VerificarNumeroItensMaximoParaSelecao()
        };

        this.FileUploader =  new FileUploader(uploadOptions);

        this.FileUploader.onBeforeUploadItem = (fileItem: FileItem) => {

          let arquivoItem =
          {
            descricao: "",
            uploadId: null,
            upload:
            {
              id: null,
              nome: fileItem.file.name,
              descricao: "",
              tipo: fileItem._file.type,
              file: fileItem._file
            }
        };

          this.AdicionarItem(arquivoItem);
        }

        this.FileUploader.onSuccessItem = (fileItem: FileItem, response: any) => {

            var objResponse = JSON.parse(response);

            this.AtualizarIdItem(fileItem.file.name, objResponse.arquivoId, response.comErro, objResponse.mediaType, objResponse.width, objResponse.height);            
        }

        this.FileUploader.onErrorItem = (fileItem: FileItem, response: any) => {
            this.AtualizarIdItem(fileItem.file.name, null, "Envio com erro");
        }
  }

  RetornarStatus(item) {
    var result = "enviando...";

    if(item.uploadId)
      result = "Enviado.";
    else if(item.uploadComErro)
      result = "Envio com erro.";

      return result;
  }

  ExcluirItem(item){

    if(confirm("Tem certeza que deseja excluir?"))
    {
      let index = this._listaArquivos.indexOf(item);

      if(index > -1)
        this._listaArquivos.splice(index, 1);

        if(item.uploadId)
          this.layoutGaleriaImagemService.Excluir(item.uploadId).then(() => {
          });
    }
  }

  public AdicionarItem(item)  {

    if(!this._listaArquivos)
      this._listaArquivos = [];

    this._listaArquivos.push(item);
  }

  public VerificarSeLimiteAindaNaoExcedido(){
    return (this._listaArquivos.length < this.NumeroItens);
  }

  private VerificarNumeroItensMaximoParaSelecao(){
    let resultado = this.NumeroItens - this._listaArquivos.length;
    resultado = resultado > 0 ? resultado: 0;

    return resultado;
  }

  private AtualizarIdItem(fileName, arquivoId, error, mediaType = null, width = null, height = null) {
      let item = this.localizarItem(fileName);

      item.uploadId = arquivoId;
      item.upload.id = arquivoId;
      item.upload.uploadComErro = error;
      item.upload.mediaType = mediaType;
      item.upload.width = width;
      item.upload.height = height;

      console.log(item); 

      if(arquivoId){
        let reader = new FileReader();
        reader.onloadend = (e) => {
          let result:string = reader.result;
          let find = "base64,"
          item.upload.blob = result.substring(result.indexOf(find) + find.length);
        };

        if (item.upload.file) {                
            return reader.readAsDataURL(item.upload.file);
        }
      }      
  }

  private localizarItem(fileName) {
      let item = null;
      item = this._listaArquivos.find(_item  =>  _item &&_item.upload.nome == fileName);
      return item;
  }

  RetornarImagemData(content:string, mediaType:string){
    if(content)
      return `data:${mediaType};base64, ${content}`;

    return '';
  }

  ngOnInit() {
      this.UploadConfig();
  }

}
