using Business.Dto.Prato;
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
    public class PratoController : Controller
    {
        private readonly UnitOfWork _uow;
        private readonly PratoBusiness _business;

        public PratoController(UnitOfWork uow)
        {
            _uow = uow;
            _business = new PratoBusiness(uow);
        }
        
        [HttpGet("{id}")]
        [EnableCors("AllowAll")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_business.GetById(id)));
            }
            catch (DomainException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpPost]
        [EnableCors("AllowAll")]
        public IActionResult Post([FromBody] PratoPostDto dto)
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
        public IActionResult Put(int id, [FromBody] PratoPutDto dto)
        {
            try
            {
                _business.Update(id, dto);
                return Ok(true);
            }
            catch (DomainException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpGet("grid")]
        [EnableCors("AllowAll")]
        public IActionResult Grid(string id)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_business.Grid()));
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
