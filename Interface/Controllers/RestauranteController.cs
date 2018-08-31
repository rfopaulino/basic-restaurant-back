using Business.Dto.Restaurante;
using Business.Rule;
using Common;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Interface.Controllers
{
    [Route("[controller]")]
    public class RestauranteController : Controller
    {
        private readonly UnitOfWork _uow;
        private readonly RestauranteBusiness _business;

        public RestauranteController(UnitOfWork uow)
        {
            _uow = uow;
            _business = new RestauranteBusiness(uow);
        }

        [HttpGet("{id}")]
        [EnableCors("AllowAll")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_business.GetById(id)));
            }
            catch(DomainException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpPost]
        [EnableCors("AllowAll")]
        public IActionResult Post([FromBody] RestaurantePostDto dto)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, JsonConvert.SerializeObject(_business.Insert(dto).Id));
            }
            catch (DomainException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpPut("{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Put(int id, [FromBody] RestaurantePutDto dto)
        {
            try
            {
                _business.Update(id, dto);
                return Ok();
            }
            catch (DomainException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpGet("grid")]
        [EnableCors("AllowAll")]
        public IActionResult Grid([FromQuery] string nome)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_business.Grid(nome)));
            }
            catch (DomainException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("massdelete")]
        [EnableCors("AllowAll")]
        public IActionResult MassDelete([FromBody] List<int> ids)
        {
            try
            {
                _business.MassDelete(ids);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (DomainException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
