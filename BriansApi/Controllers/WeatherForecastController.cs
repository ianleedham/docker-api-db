using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BriansApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BriansController : ControllerBase
{
    private readonly ILogger<BriansController> _logger;
    private readonly ApplicationContext _context;

    public BriansController(ILogger<BriansController> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost(Name = "Students")]
    public async Task Post(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }
    
    [HttpPut(Name = "Students")]
    public async Task Put(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }
    
    [HttpGet(Name = "Students")]
    public IEnumerable<Student> Get()
    {
        return _context.Students.ToList();
    }
}