using FileStore.Domain;
using FileStore.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class TestController : ControllerBase
{
    private readonly DatabaseContext _dbContext;

    public TestController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var a = Guid.NewGuid();
        await _dbContext.Tests.AddAsync(new Files()
        {
            Id = a
        });

        await _dbContext.SaveChangesAsync();
        
        
        return Ok(a);
    }
    
    [HttpGet("Get")]
    public async Task<IActionResult> Get2()
    {
        var a = await _dbContext.Tests.ToListAsync();
        
        return Ok(a);
    }
}