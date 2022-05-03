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
    public class DiretorService : CrudService<Diretor, IDiretorRepository>, IDiretorService
    {
        public DiretorService(IDiretorRepository repository) : base(repository)
        {
        }

        public async Task<List<Diretor>> AutoComplete(string q)
        {
            return await _repository.AutoComplete(q);
        }
    }
}
