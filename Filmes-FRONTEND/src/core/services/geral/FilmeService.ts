import { http, httpHeader } from "../ApiConfig";
import { Service } from "../shared";

export class FilmeService extends Service{

    constructor() {
        super('filme');
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