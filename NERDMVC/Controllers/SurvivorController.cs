using Microsoft.AspNetCore.Mvc;
using NERD.Database;
using NERD.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NERD.Controllers;

[ApiController]
[Route("[controller]")]
public class SurvivorController : ControllerBase
{
    // import NerdContext
    private readonly NerdContext _context;

    // constructor
    public SurvivorController(NerdContext context)
    {
        _context = context;
    }

    [HttpGet("/api/GetRelatives")]
    public JsonResult GetSurvivor(string firstname, string lastname)
    {
        var GetRelativesFromLastName =
            _context.Survivors.Where(x => x.lastName == lastname && x.firstName != firstname);
        return new JsonResult(GetRelativesFromLastName);
    }

    [HttpGet("/api/AlivePercentage")]
    public JsonResult SurvivorsAlive()
    {
        var SurvivorsAlive = _context.Survivors.Count(x => x.isAlive == "true");
        var SurvivorsTotal = _context.Survivors.Count();

        // For some reason this way won't work (Always returns 0) so have to do it in the other way
        //var AlivePercentage = (SurvivorsAlive / SurvivorsTotal) * 100;
        var AlivePercentage = 100 * SurvivorsAlive / SurvivorsTotal;

        return new JsonResult("There is: " + AlivePercentage + "% of the total survivors is still alive.");
    }

    [HttpGet("/api/GetAllSurvivors")]
    public JsonResult GetAllSurvivorData()
    {
        return new JsonResult(_context.Survivors.ToList());
    }

    [HttpPost("/api/AddSurvivor")]
    public JsonResult CreateEditSurvivor(object details)
    {
        SurvivorModel survivor = new SurvivorModel();
        var survivorJson = JObject.Parse(details.ToString());

        foreach (JProperty item in survivorJson.Properties())
        {
            survivor.SetProperty(item.Name, item.Value.ToString());
        }
        
        Console.WriteLine("Success: " + details);
        
        // Check if person forgot to change something
        if (survivor.gender == "string" || survivor.firstName == "string" || survivor.lastName == "string")
            return new JsonResult("You forgot to change something");

        // Check if isAlive is true or false
        if (survivor.isAlive != "true" && survivor.isAlive != "false")
            return new JsonResult(NotFound("isAlive can only be true or false"));

        if (survivor.id == 0)
        {
            survivor.id = _context.Survivors.Max(x => x.id) + 1;
            _context.Survivors.Add(survivor);
        }
        else
        {
            var survivorToEdit = _context.Survivors.Find(survivor.id);

            if (survivorToEdit == null)
                return new JsonResult(NotFound());

            // Check for hemisphere location
            if (survivor.latitude > 0 && survivorToEdit.latitude < 0)
            {
                return new JsonResult(
                    "You can't move to the Northern hemisphere since you live in the Southern hemisphere");
            }

            if (survivor.latitude < 0 && survivorToEdit.latitude > 0)
            {
                return new JsonResult(
                    "You can't move to the Southern hemisphere since you live in the Northern hemisphere");
            }
        }

        _context.SaveChanges();
        return new JsonResult(survivor);
    }
}