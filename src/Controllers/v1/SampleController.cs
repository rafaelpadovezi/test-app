using Asp.Versioning;
using TestApp.Core.Models;
using TestApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controllers.v1;

[ApiVersion("1")]
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class SampleController : ControllerBase
{
    private readonly AppDbContext _context;

    public SampleController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(new Sample
        {
            Name = "v1.Sample1",
            Value = 1
        });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var samples = new List<Sample>
        {
            new()
            {
                Name = "v1.Sample1",
                Value = 1
            },
            new()
            {
                Name = "v1.Sample2",
                Value = 2
            }
        };
        return Ok(samples);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Sample sample)
    {
        _context.Add(sample);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new {id = sample.Id, version = "1.0"}, sample);
    }
}