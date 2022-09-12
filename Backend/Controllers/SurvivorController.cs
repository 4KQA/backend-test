using System;
using Models;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SurvivorController : Controller{

        private readonly ISurvivorRepos _repos;

        public SurvivorController(ISurvivorRepos reposContext){
            _repos = reposContext ?? throw new ArgumentNullException(nameof(reposContext));
        }

        [HttpGet("stats/")]
        public IActionResult GetStats(){
            var _survivorsStats = _repos.GetStats();

            if (_survivorsStats != null)
                return Ok(_survivorsStats);
            else 
                return BadRequest();
        }

        [HttpGet("search/{lastname}")]
        public IActionResult SearchLastname(string lastname){
            var _survivorsStats = _repos.SearchByLastname(lastname);

            if (_survivorsStats != null)
                return Ok(_survivorsStats);
            else 
                return NotFound();
        }

        [HttpGet]
        public IActionResult GetSurvivors(){
            var _survivors = _repos.GetSurvivors();

            if (_survivors != null)
                return Ok(_survivors);
            else 
                return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetSurvivor(int id){
            var _survivors = _repos.GetSurvivor(id);

            if (_survivors != null)
                return Ok(_survivors);
            else 
                return NotFound();
        }

        [HttpGet("relitives/{id}")]
        public IActionResult GetSurvivorRelitives(int id){
            var _survivor = _repos.GetSurvivor(id);
            var _survivorRelitives = _repos.Relitives(_survivor);

            if (_survivorRelitives != null)
                return Ok(_survivorRelitives);
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
            var _survivor = _repos.UpdateSurvivor(id, input);

            if (_survivor == "Succes, survivor updated.")
                return Ok(_survivor);
            else if (_repos.Exists(id) == false)
                return NotFound(_survivor);
            else
                return BadRequest(_survivor);
        }

        [HttpPost]
        public IActionResult CreateSurvivor([FromBody] Survivor input){
            var _survivor = _repos.CreateSurvivor(input);

            if (_survivor == null)
                return BadRequest();
        
            return CreatedAtAction("GetSurvivor", new { id = _survivor.Survivor_ID }, _survivor);
        }


    }
}
