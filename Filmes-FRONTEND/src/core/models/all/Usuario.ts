import { PerfilUsuario } from ".";

export class Usuario implements Shared.IEntity {
    id: number = 0;
    login: string = '';
    senha: string = '';
    perfil!: PerfilUsuario;
    ativo: boolean = true;
    foto: any = null;
    temaEscuro: boolean = false;

    constructor(model?: Usuario) {
        if (!model)
            return;

        this.id = model.id
        this.login = model.login
        this.senha = model.senha
        this.perfil = model.perfil
        this.ativo = model.ativo
        this.foto = model.foto
        this.temaEscuro = model.temaEscuro
    }
}