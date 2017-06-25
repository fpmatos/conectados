import { Injectable } from '@angular/core';
import { UsuarioModel } from './models/usuario.model';
import { AuthModel } from './models/auth.model';

const userStorageKey: string = "_currentUser";
const authStorageKey: string = "_auth";

@Injectable()
export class ContextoService {
    private User: UsuarioModel;


    public DefinirUsuario(user: UsuarioModel) {
        if(user)
            localStorage.setItem(userStorageKey, JSON.stringify(user));
        else
            localStorage.removeItem(userStorageKey);
    }

    public DefinirAutenticacaoDados(authModel: AuthModel) {

        if(authModel)
            localStorage.setItem(authStorageKey, JSON.stringify(authModel));
        else
            localStorage.removeItem(authStorageKey);
    }

    public RetornarUsuario() {
        let usuarioFormatoString = localStorage.getItem(userStorageKey);
        let usuario = null;

        if (usuarioFormatoString)
            usuario = JSON.parse(usuarioFormatoString);

        return usuario;
    }

        public RetornarAuth() {
        let authFormatoString = localStorage.getItem(authStorageKey);
        let authModel: AuthModel = null;

        if (authFormatoString)
            authModel = JSON.parse(authFormatoString);

        return authModel;
    }
}