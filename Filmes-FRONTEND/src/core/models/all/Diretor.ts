import { FilmeDiretor } from "./FilmeDiretor";
import { Genero } from "./Genero";

export class Diretor implements Shared.IEntity{
    id: number = 0;
    nome: string = '';
    dataNascimento: string = '';
    nacionalidade: string = '';
    generoId: number = 0;
    genero!: Genero;
    filmes: FilmeDiretor[]=[];

    constructor(model?: Diretor){

        if(!model)
            return;

        this.id = model.id;
        this.nome = model.nome;
        this.dataNascimento = model.dataNascimento;
        this.nacionalidade = model.nacionalidade;
        this.generoId = model.generoId;
        this.genero = model.genero;
        this.filmes = model.filmes
    }
}