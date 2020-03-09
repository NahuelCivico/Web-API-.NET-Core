using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICountries.Models;

namespace WebAPICountries.Controllers
{
    [Produces("application/json")]
    [Route("api/Pais")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaisController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public PaisController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Pais> Get()
        {
            return _dbContext.Paises.ToList();
        }

        [HttpGet("{id}", Name = "NewCountry")]
        public IActionResult Get(int id)
        {
            Pais pais = _dbContext.Paises.Include(x => x.Provincias).FirstOrDefault(x => x.Id == id);

            if ( pais == null)
            {
                return NotFound();
            }

            return Ok(pais);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pais pais)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Paises.Add(pais);
                _dbContext.SaveChanges();
                return new CreatedAtRouteResult("NewCountry", new { id = pais.Id }, pais);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Pais pais, int id)
        {
            if (pais.Id != id)
            {
                return BadRequest();
            }

            _dbContext.Entry(pais).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Pais pais = _dbContext.Paises.Find(id);

            if (pais == null)
            {
                return NotFound();
            }

            _dbContext.Paises.Remove(pais);
            _dbContext.SaveChanges();
            return Ok(pais);
        }
    }
}