using Business.Dto.Prato;
using Common;
using Domain.Entity;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Rule
{
    public class PratoBusiness
    {
        private readonly UnitOfWork _uow;

        public PratoBusiness(UnitOfWork uow)
        {
            _uow = uow;
        }

        private bool Exists(int id)
        {
            return _uow.PratoRepository.Exists(id);
        }

        private void MassDelete(List<Prato> pratos)
        {
            foreach (var prato in pratos)
            {
                _uow.PratoRepository.Delete(prato);
            }
            _uow.SaveChanges();
        }

        public PratoGetIdDto GetById(int id)
        {
            if (Exists(id))
            {
                var db = _uow.PratoRepository.GetById(id);

                return new PratoGetIdDto
                {
                    Id = db.Id,
                    IdRestaurante = db.IdRestaurante,
                    NomePrato = db.NomePrato.Trim(),
                    Preco = db.Preco
                };
            }
            else
                throw new DomainException(Messages.NotExistsDish);
        }

        public Prato Insert(PratoPostDto dto)
        {
            var db = new Prato
            {
                IdRestaurante = dto.IdRestaurante,
                NomePrato = dto.NomePrato.Trim(),
                Preco = dto.Preco
            };

            _uow.PratoRepository.Add(db);
            _uow.SaveChanges();

            return db;
        }

        public void Update(int id, PratoPutDto dto)
        {
            if (Exists(id))
            {
                var db = _uow.PratoRepository.GetById(id);

                db.IdRestaurante = dto.IdRestaurante;
                db.NomePrato = dto.NomePrato.Trim();
                db.Preco = dto.Preco;

                _uow.PratoRepository.Edit(db);
                _uow.SaveChanges();
            }
            else
                throw new DomainException(Messages.NotExistsDish);
        }

        public List<PratoGridDto> Grid()
        {
            var query = _uow.PratoRepository.GetAll();
            return query.Select(x => new PratoGridDto
            {
                Id = x.Id,
                NomeRestaurante = x.Restaurante.Nome.Trim(),
                NomePrato = x.NomePrato.Trim(),
                Preco = x.Preco
            }).ToList();
        }

        public void MassDelete(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                throw new DomainException(Messages.InconsistencyRequest);

            var pratos = _uow.PratoRepository.GetByIds(ids);
            MassDelete(pratos);
        }
    }
}
