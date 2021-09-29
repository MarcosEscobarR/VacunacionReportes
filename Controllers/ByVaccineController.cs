using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using VacunadosReporte.Commonds;
using VacunadosReporte.Models;
using VacunadosReporte.Persistent;

namespace VacunadosReporte.Controllers
{
    [ApiController, Route("api/by-vaccine")]
    public class ByVaccineController: ControllerBase
    {
        private readonly VacunadosDBContext _context;
        private readonly IMemoryCache _memoryCache;

        public ByVaccineController(VacunadosDBContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {

            if (_memoryCache.TryGetValue(RequestMemoryKeys.ByVaccineEndpoint, out var result))
            {
                return Ok(result);
            }
            
            var tasks = new List<Task>();
            GetResult(cancellationToken, out var total, out var pfizer, out var astrazeneca, out var sputnik);

            tasks.AddRange(new List<Task>(){total, pfizer, astrazeneca, sputnik});
            await Task.WhenAll(tasks);
            
            result = new ReportByVaccineModel(total.Result, pfizer.Result, astrazeneca.Result, sputnik.Result);

            _memoryCache.Set(RequestMemoryKeys.ByVaccineEndpoint, result, TimeSpan.FromDays(1));
            return Ok(result);
        }

        private void GetResult(CancellationToken cancellationToken, out Task<int> total,out Task<int> pfizer, out Task<int> astrazeneca, out Task<int> sputnik)
        {
            total = _context.People.CountAsync(cancellationToken);
            pfizer = GetVaccineCountTask("PFIZER", cancellationToken);  
            astrazeneca = GetVaccineCountTask("ASTRAZENECA", cancellationToken);
            sputnik = GetVaccineCountTask("SPUTNIK", cancellationToken);
        }

        private Task<int> GetVaccineCountTask(string vaccine, CancellationToken cancellationToken)
        {
            return _context.People
                .Where(v => EF.Functions.Like(v.VaccineDescription, $"%{vaccine}%"))
                .CountAsync(cancellationToken: cancellationToken);
        }
    }
}