using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICountries.Models;

namespace WebAPICountries.Controllers
{
    [Produces("application/json")]
    [Route("api/Pais/{PaisId}/Provincia")]
    public class ProvinciaController : Controller
    {
        public readonly ApplicationDbContext _dbContext;

        public ProvinciaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Provincia> GetAll(int PaisId)
        {
            return _dbContext.Provincias.Where(x => x.PaisId == PaisId).ToList();
        }

        [HttpGet("{id}", Name = "NewProvince")]
        public IActionResult Get(int id)
        {
            Provincia provincia = _dbContext.Provincias.FirstOrDefault(x => x.Id == id);

            if (provincia == null)
            {
                return NotFound();
            }

            return Ok(provincia);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Provincia provincia, int PaisId)
        {
            provincia.PaisId = PaisId;

            if (ModelState.IsValid)
            {
                _dbContext.Provincias.Add(provincia);
                _dbContext.SaveChanges();
                return new CreatedAtRouteResult("NewProvince", new { id = provincia.Id }, provincia);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Provincia provincia, int id)
        {
            if (provincia.Id != id)
            {
                return BadRequest();
            }

            _dbContext.Entry(provincia).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Provincia provincia = _dbContext.Provincias.Find(id);

            if (provincia == null)
            {
                return NotFound();
            }

            _dbContext.Provincias.Remove(provincia);
            _dbContext.SaveChanges();
            return Ok(provincia);
        }
    }
}