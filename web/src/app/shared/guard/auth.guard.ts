import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Router } from '@angular/router';
import { AutenticacaoService } from 'app/shared/autenticacao.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private autenticacaoService: AutenticacaoService) { }

    canActivate() {
        if(!this.autenticacaoService.UsuarioAutenticado())
        {
            this.router.navigate(['/login']);
            return false;
        }
        else
            return true;
    }
}
