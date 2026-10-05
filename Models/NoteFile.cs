namespace ProjectAPICore.Models
{
    public class NoteFile{
        public int NoteFileId{get; set;}
        public int NoteId{get; set;}
        public int FileRepositoryId{get; set;}
        public Note? Note{get;set;}
        public FileRepository? FileRepository{get;set;}
    }
}