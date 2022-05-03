import { Diretor,Filme } from ".";

export class FilmeDiretor implements Shared.IEntity{
    id: number = 0;
    filmeId: number = 0;
    filme!: Filme;
    diretorId:number = 0;
    diretor!:Diretor;
    
    constructor(model?: FilmeDiretor){

        if(!model)
            return;

        this.id = model.id;
        this.filmeId = model.filmeId;
        this.filme = model.filme;
        this.diretorId = model.diretorId;
        this.diretor = model.diretor;
    }
}