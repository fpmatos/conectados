export class ArquivoItem
{
  constructor(
    public id?: string,
    public nome?: string,
    public descricao?: string,
    public tipo?: string,
    public arquivo?: any,
    public uploadComErro?: boolean    
  ){}

}