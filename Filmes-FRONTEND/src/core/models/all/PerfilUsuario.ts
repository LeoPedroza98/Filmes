export class PerfilUsuario implements Shared.IEntity {
    id: number = 0;
    nome: string = '';
    role: string = '';

    constructor(model?: PerfilUsuario) {
        if (!model)
            return

        this.id = model.id
        this.nome = model.nome
        this.role = model.role
    }
}