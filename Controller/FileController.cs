using FileStore.Domain;
using FileStore.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.Controller;

[Controller]
[Route("[Controller]")]
public class FileController : ControllerBase
{
    private readonly DatabaseContext _dbContext;

    public FileController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<Guid>> CreateFile()
    {
        await _dbContext.TTestEntities.AddAsync(new TTestEntity()
        {
            Id = Guid.NewGuid()
        });
        
        
        return Guid.NewGuid();
    }
    
    [HttpGet("Get")]
    public async Task<ActionResult<List<TTestEntity>>> CreateFile2()
    {
        var results = await _dbContext.TTestEntities.ToListAsync();

        return results;
    }
}