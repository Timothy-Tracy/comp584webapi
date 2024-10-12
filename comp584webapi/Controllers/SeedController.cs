using comp584webapi.Data;
using CsvHelper;
using CsvHelper.Configuration;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace comp584webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(MydbContext db, IHostEnvironment environment) : ControllerBase
    {
        private readonly string _pathName = Path.Combine(environment.ContentRootPath, "Data/worldcities.csv");
        [HttpPost ("Countries")]
        public async Task<IActionResult> ImportCountriesAsync()
        {
            // create a lookup dictionary containing all the countries already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, Country> countriesByName = db.Countries
                .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<WorldCitiesCSV> records = csv.GetRecords<WorldCitiesCSV>().ToList();
            foreach (WorldCitiesCSV record in records)
            {
                if (countriesByName.ContainsKey(record.country))
                {
                    continue;
                }

                Country country = new()
                {
                    Name = record.country,
                    Iso2 = record.iso2,
                    Iso3 = record.iso3
                };
                await db.Countries.AddAsync(country);
                countriesByName.Add(record.country, country);
            }

            await db.SaveChangesAsync();

            return new JsonResult(countriesByName.Count);
        }
        public async Task<IActionResult> ImportCitiesAsync()
        {

        }
    }
}
