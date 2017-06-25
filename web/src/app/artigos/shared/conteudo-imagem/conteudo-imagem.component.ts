import { Component, OnInit, Input, Renderer } from '@angular/core';
import { FileUploader, FileLikeObject, FileItem, FileUploaderOptions } from 'ng2-file-upload';
import {  DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';
import {LayoutGaleriaImagemService} from '../layout-galeria-imagem.service';
import { environment } from 'environments/environment';

@Component({
  selector: 'conteudo-imagem',
  templateUrl: './conteudo-imagem.component.html',
  styleUrls: ['./conteudo-imagem.component.scss']
})
export class ConteudoImagemComponent implements OnInit {

  @Input() tipoArquivo: string;
  @Input() imagem: any;
  public fileUploader:FileUploader;
  Alterando: boolean = false;

  constructor(private layoutGaleriaImagemService: LayoutGaleriaImagemService, private _sanitizer: DomSanitizer,private renderer: Renderer) { }


  private UploadConfig(){

        var uploadOptions: FileUploaderOptions = {
          autoUpload: true,
          url: environment.uploadUrl    
        };

        this.fileUploader =  new FileUploader(uploadOptions);    

        this.fileUploader.onBeforeUploadItem = (fileItem: FileItem) => {
            this.imagem.upload.file = fileItem._file;   
            this.Alterando = false;     
        }

        this.fileUploader.onSuccessItem = (fileItem: FileItem, response: any) => {

            var objResponse = JSON.parse(response);        
            this.imagem.uploadId = objResponse.arquivoId;
            this.imagem.upload.id = objResponse.arquivoId;
            this.imagem.upload.mediaType = objResponse.mediaType;
            this.imagem.upload.width = objResponse.width;
            this.imagem.upload.height = objResponse.height;
            this.imagem.upload.uploadComErro = response.comErro;

            let reader = new FileReader();
            reader.onloadend = (e) => {
              let result:string = reader.result;
              console.log(result);
              let find = "base64,"
              this.imagem.upload.blob = result.substring(result.indexOf(find) + find.length);
            };

            if (this.imagem.upload.file) {                
                return reader.readAsDataURL(this.imagem.upload.file);
            }
        }

        this.fileUploader.onErrorItem = (fileItem: FileItem, response: any) => {
            this.imagem.upload.uploadComErro = "Envio com erro.";
        }
  }

  RetornarStatus() {
    var result = "enviando...";

    if(this.imagem.uploadId)
      result = "Enviado.";
    else if(this.imagem.uploadComErro)
      result = "Envio com erro.";

      return result;
  }

  Alterar(){
    this.Alterando = true;
  }

  CancelarAlteracao(){
    this.Alterando = false;
  }

  ExibirImagem(){
    return  !this.Alterando && this.imagem.uploadId;
  } 

  ExibirStatus(){
    return  !this.Alterando && this.imagem.upload.file;
  }

  ExibirOpcaoSelecaoImagem(){
    return  this.Alterando;
  }  

  ExibirOpcaoAlteracao(){
    return this.imagem.uploadId && !this.Alterando;
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
