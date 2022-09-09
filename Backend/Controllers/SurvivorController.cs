using System;
using Models;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Udlejnings_API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SurvivorController : Controller{

        private readonly ISurvivorRepos _repos;

        public SurvivorController(ISurvivorRepos reposContext){
            _repos = reposContext ?? throw new ArgumentNullException(nameof(reposContext));
        }

        [HttpGet]
        public IActionResult GetSurvivors(){
            var _survivors = _repos.GetSurvivors();

            if (_survivors != null)
                return Ok(_survivors);
            else 
                return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetSurvivor(int id){
            var _survivors = _repos.GetSurvivor(id);

            if (_survivors != null)
                return Ok(_survivors);
            else 
                return NotFound();
        }

        [HttpGet("hemisphere/{hemisphere}/{id}")]
        public IActionResult GetSurvivorByHemisphere(string hemisphere,int id){
            var _survivors = _repos.GetSurvivorByHemisphere(hemisphere,id);

            if (_survivors != null)
                return Ok(_survivors);
            else 
                return NotFound();
        }

        [HttpGet("hemisphere/{hemisphere}")]
        public IActionResult GetSurvivorsByHemisphere(string hemisphere){
            var _survivors = _repos.GetSurvivorsByHemisphere(hemisphere);

            if (_survivors != null)
                return Ok(_survivors);
            else 
                return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSurvivor(int id){
            var _survivors = _repos.DeleteSurvivor(id);

            if (_survivors != false)
                return Ok(_survivors);
            else 
                return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSurvivor(int id,[FromBody] Survivor input){
            var _survivors = _repos.UpdateSurvivor(id, input);

            if (_survivors != false)
                return Ok(_survivors);
            else 
                return NotFound();
        }

        [HttpPost]
        public IActionResult CreateSurvivor([FromBody] Survivor input){
            var _survivor = _repos.CreateSurvivor(input);

            if (_survivor == null)
                return BadRequest();
        
            return CreatedAtAction("GetRent", new { id = _survivor.Survivor_ID }, _survivor);
        }
    }
}