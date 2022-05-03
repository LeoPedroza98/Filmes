import { Service } from "../shared/Service";
import { http, httpHeader } from "../ApiConfig";

export class UsuarioService extends Service {
    constructor() {
        super('usuario')
    }
    public async AlterarSenha(id: number, senhaAntiga: string, senhaNova: string) {

        const senhas: { usuarioId: number, senhaAntiga: string, senhaNova: string } = {
            usuarioId: id,
            senhaAntiga: senhaAntiga,
            senhaNova: senhaNova
        };

        return await http.post(`${this.GetNomeControle()}/AlterarSenha`, senhas);
    }

    public MudarTema(id: number) {

        return http.put(`${this.GetNomeControle()}/${id}/MudarTema`, null);
    }

    public AutoComplete(q: string, aluno: boolean = false) {
        return http.get(`${this.GetNomeControle()}/AutoComplete`, {
            params: {
                q: q,
                trazerAluno: aluno
            },
            headers: httpHeader.headers
        })
    }

    public VincularEquipe(id: number, usuarioId: number, percentual: number) {
        return http.put(`${this.GetNomeControle()}/${id}/VincularEquipe/${usuarioId}`, undefined, {
            params: {
                percentual
            }
        })
    }
    
    public DesvincularEquipe(usuarioId: number) {
        return http.put(`${this.GetNomeControle()}/DesvincularEquipe/${usuarioId}`)
    }

    public ListarGestores(){
        return http.get(`${this.GetNomeControle()}?$filter= perfilId ne 3`, {
            headers: httpHeader.headers
        })
    }
}