using Infrastructure.EntityFramework.Repository;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : BaseUnitOfWork
    {
        private RestauranteRepository _restauranteRepository;
        private PratoRepository _pratoRepository;

        public UnitOfWork(string connectionString)
            :base(connectionString)
        {
            SetRepositories();
        }

        private void SetRepositories()
        {
            _restauranteRepository = new RestauranteRepository(Context);
            _pratoRepository = new PratoRepository(Context);
        }

        public RestauranteRepository RestauranteRepository { get { return _restauranteRepository; } }

        public PratoRepository PratoRepository { get { return _pratoRepository; } }
    }
}
