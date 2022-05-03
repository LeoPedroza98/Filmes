import { http, httpHeader } from "../ApiConfig";
import { Service } from "../shared/Service";

export class GeneroService extends Service {
    constructor() {
        super('genero');
    }

    public AutoComplete(q: string) {
        return http.get(`${this.GetNomeControle()}/AutoComplete`, {
            params: {
                q: q
            },
            headers: httpHeader.headers
        })
    }
}