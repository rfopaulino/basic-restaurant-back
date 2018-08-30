using Business.Dto.Restaurante;
using Common;
using Domain.Entity;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Rule
{
    public class RestauranteBusiness
    {
        private readonly UnitOfWork _uow;

        public RestauranteBusiness(UnitOfWork uow)
        {
            _uow = uow;
        }

        private bool Exists(int id)
        {
            return _uow.RestauranteRepository.Exists(id);
        }

        private void MassDelete(List<Restaurante> restaurantes)
        {
            foreach (var restaurante in restaurantes)
            {
                _uow.RestauranteRepository.Delete(restaurante);
            }
            _uow.SaveChanges();
        }

        public RestauranteGetIdDto GetById(int id)
        {
            if (Exists(id))
            {
                var db = _uow.RestauranteRepository.GetById(id);

                return new RestauranteGetIdDto
                {
                    Id = db.Id,
                    Nome = db.Nome
                };
            }
            else
                throw new DomainException(Messages.NotExistsRestaurant);
        }

        public Restaurante Insert(RestaurantePostDto dto)
        {
            var db = new Restaurante
            {
                Nome = dto.Nome.Trim()
            };

            _uow.RestauranteRepository.Add(db);
            _uow.SaveChanges();

            return db;
        }

        public void Update(int id, RestaurantePutDto dto)
        {
            if (Exists(id))
            {
                var db = _uow.RestauranteRepository.GetById(id);

                db.Nome = dto.Nome.Trim();

                _uow.RestauranteRepository.Edit(db);
                _uow.SaveChanges();
            }
            else
                throw new DomainException(Messages.NotExistsRestaurant);
        }

        public List<RestauranteGridDto> Grid()
        {
            var query = _uow.RestauranteRepository.GetAll();
            return query.Select(x => new RestauranteGridDto
            {
                Id = x.Id,
                Nome = x.Nome.Trim()
            }).ToList();
        }

        public void MassDelete(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                throw new DomainException(Messages.InconsistencyRequest);

            var restaurantes = _uow.RestauranteRepository.GetByIds(ids);
            MassDelete(restaurantes);
        }
    }
}
