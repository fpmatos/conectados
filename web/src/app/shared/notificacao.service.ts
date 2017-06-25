import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import {NotificacaoMensagemModel, MensagemTipo} from './models/notificacao-mensagem.model';

@Injectable()
export class NotificacaoService {

  public NotificarErro(mensagem: string){
    this.MensagemObserver.next(new NotificacaoMensagemModel(MensagemTipo.Erro, mensagem));
  }

  public NotificarAviso(mensagem: string){
    this.MensagemObserver.next(new NotificacaoMensagemModel(MensagemTipo.Alerta, mensagem));

  }

  public Notificador: Observable<NotificacaoMensagemModel>;

  private MensagemObserver: Subject<NotificacaoMensagemModel>;

  constructor() { 
    this.MensagemObserver = new Subject<NotificacaoMensagemModel>();
    this.Notificador = this.MensagemObserver.asObservable();    
  }

}
