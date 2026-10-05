using Microsoft.AspNetCore.Mvc;
using ProjectAPICore.Data;
using ProjectAPICore.Models;

namespace ProjectAPICore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotesController :ControllerBase
{
   private readonly ApplicationDbContext _context;
   public NotesController(ApplicationDbContext context){
   _context = context;
   }
   [HttpGet("{projectId}/{noteType}/{entityId}")]
   public IActionResult  GetNotes(int projectId, string noteType,int entityId){
    var notes = _context.Notes.Where(n=> n.ProjectId== projectId && n.TypeName == noteType && n.EntityId== entityId);
    return Ok(notes);
   }
   [HttpPost]
   public IActionResult CreateNote([FromBody] Note note){
        _context.Notes.Add(note);
        _context.SaveChanges();
        return Ok(note);
   }
}
