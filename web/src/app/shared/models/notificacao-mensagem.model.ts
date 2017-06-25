export enum MensagemTipo{
    Erro,
    Alerta
}

export class NotificacaoMensagemModel{
    constructor(
        public MensagemTipo: MensagemTipo, 
        public Mensagem: string){

    }
}