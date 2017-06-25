import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'comentariosPipe'
})
export class ComentariosPipe implements PipeTransform {

  transform(value: any[], args?: any): any {
    
    var result = value;

    if(args.opcaoDenuncia && args.opcaoDenuncia !== 'Todos')    {
      let listarComDenuncia: boolean = args.opcaoDenuncia === 'Apenas denunciados';

      result = result.filter(item => item.denunciado == listarComDenuncia);
    }

    if(args.opcaoImproprio && args.opcaoImproprio !== 'Todos')    {
      let listarApenasImproprios: boolean = args.opcaoImproprio === 'Apenas impróprios';

      result = result.filter(item => item.marcadoComoImproprio == listarApenasImproprios);      
    }


    return result;      
  }
}
