import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { Router } from '@angular/router';
import {AutenticacaoService} from 'app/shared/autenticacao.service';
import {LoginModel} from './login.model';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

    public Login: FormGroup;
    MensagemDeErro: string;

    constructor(public router: Router, private autenticacaoService: AutenticacaoService) { 
        autenticacaoService.Logoff();
    }

    private Validate(){
        this.Login = new FormGroup({
            NomeDeUsuario: new FormControl('', [
                Validators.required
            ]),
            Senha: new FormControl('', [
                Validators.required
            ])
        });
    }

    ngOnInit() { 
        this.Validate();
    }

  EfetuarLogin({ value, valid }: { value: LoginModel, valid: boolean }){

    this.autenticacaoService.Autenticar(value.NomeDeUsuario, value.Senha)
    .then(() => {
        this.router.navigate(['/']);
    })
    .catch(message => {
        this.MensagemDeErro = message;
    });
  }

}
