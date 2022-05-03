using Filmes.DATA.Interface.All;
using Filmes.DOMAIN.Entity.All;
using Filmes.SERVICE.Interface.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.SERVICE.Services.All
{
    public class FilmeService: CrudService<Filme,IFilmeRepository>,IFilmeService
    {
        public FilmeService(IFilmeRepository repository) : base(repository)
        {
        }

        public async Task<List<Filme>> AutoComplete(string q)
        {
            return await _repository.AutoComplete(q);
        }
    }
}
