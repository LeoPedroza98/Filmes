import { Diretor } from ".";

export class Filme implements Shared.IEntity{
    id: number = 0;
    titulo: string = '';
    dataLancamento: string = '';
    sinopse: string = '';
    avaliacoes: number = 0;
    genero: string = '';
    poster: any = null;
    diretores: Filme[]=[];

    constructor(model?: Filme){

        if(!model)
            return;

        this.id = model.id;
        this.titulo = model.titulo;
        this.dataLancamento = model.dataLancamento;
        this.sinopse = model.sinopse;
        this.avaliacoes = model.avaliacoes;
        this.poster = model.poster;
        this.diretores = model.diretores;
        
    }
}