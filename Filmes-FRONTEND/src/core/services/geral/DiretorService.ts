import { http, httpHeader } from "../ApiConfig";
import { Service } from "../shared";

export class DiretorService extends Service{

    constructor() {
        super('diretor');
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