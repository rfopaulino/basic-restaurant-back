using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.EntityFramework.Repository
{
    public class RestauranteRepository : GenericRepository<Restaurante>
    {
        private readonly Model _model;

        public RestauranteRepository(Model model)
            :base(model)
        {
            _model = model;
        }

        public List<Restaurante> GetByIds(List<int> ids)
        {
            return _model.Restaurante
                .Where(x => ids.Contains(x.Id))
                .ToList();
        }
    }
}
