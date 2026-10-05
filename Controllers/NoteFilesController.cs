using Microsoft.AspNetCore.Mvc;
using ProjectAPICore.Data;
using ProjectAPICore.Models;

namespace ProjectAPICore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NoteFilesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public NoteFilesController(ApplicationDbContext context){
        _context = context;
    }
    [HttpPost]
    public IActionResult CreateNoteFiles([FromBody] NoteFile noteFile){
        _context.NoteFiles.Add(noteFile);
        _context.SaveChanges();
        return Ok(noteFile);
    }

}