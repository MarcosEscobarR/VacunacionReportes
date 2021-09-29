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
    [ApiController, Route("api/by-doses")]
    public class ByDosesController: ControllerBase
    {
        private readonly VacunadosDBContext _context;
        private readonly IMemoryCache _memoryCache;

        public ByDosesController(VacunadosDBContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(RequestMemoryKeys.ByDosesEndpoint, out var result))
            {
                return Ok(result);
            }
            
            var tasks = new List<Task>();
            GetTotalsTask(cancellationToken, out var total, out var totalFirstDose, out var totalSecondDose);

            tasks.AddRange(new List<Task>() {total, totalFirstDose, totalSecondDose});
            await Task.WhenAll(tasks);

            result = new ReportByDosesModel(totalFirstDose.Result, totalSecondDose.Result, total.Result);
            _memoryCache.Set(RequestMemoryKeys.ByDosesEndpoint, result, TimeSpan.FromDays(1));

            return Ok(result);
        }

        private void GetTotalsTask(CancellationToken cancellationToken, out Task<int> total, out Task<int> totalFirstDose,
            out Task<int> totalSecondDose)
        {
            total = _context.People.CountAsync(cancellationToken);
            totalFirstDose = GetDoseCount(1,cancellationToken);
            totalSecondDose = GetDoseCount(2, cancellationToken);
        }

        private Task<int> GetDoseCount(int dose, CancellationToken cancellationToken)
        {
            return _context.People
                .Where(p => p.Dose == dose)
                .CountAsync(cancellationToken);
        }
    }
}