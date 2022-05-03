import { http } from "../ApiConfig";
   
export class AutenticadorService {

    private _nomeControle = 'autenticador';

    public async AutenticarUsuario(usuario: any){
        return await http.post(`${this._nomeControle}/usuario`, usuario);
    }

    public async ResetarSenha(usuario: any){
        return await http.post(`${this._nomeControle}/resetarSenha`, usuario);
    }
}