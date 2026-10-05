using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectAPICore.Models
{
public class Note{
    public int NoteId {get;set;}
    public int ProjectId{get; set;}
    public int EntityId{get; set;}
    public string TypeName{get;set;}
    public string NoteText{get;set;} 
    public int ModifiedBy{get;set;}
    [ForeignKey("ModifiedBy")]
    public User? ModifiedByUser{get;set;}
    public DateTime ModifiedDate{get;set;}
}
}