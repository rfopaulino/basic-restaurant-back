using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.EntityFramework.Repository
{
    public class PratoRepository : GenericRepository<Prato>
    {
        private readonly Model _model;

        public PratoRepository(Model model)
            :base(model)
        {
            _model = model;
        }

        public List<Prato> GetByIds(List<int> ids)
        {
            return _model.Prato
                .Where(x => ids.Contains(x.Id))
                .ToList();
        }
    }
}
