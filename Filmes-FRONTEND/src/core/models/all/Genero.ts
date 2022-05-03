export class Genero implements Shared.IEntity {
    id: number = 0;
    nome: string = '';

    constructor(model?: Genero) {

        if (!model) {
            return;
        }

        this.id = model.id;
        this.nome = model.nome;
    }
}