namespace ProjectAPICore.Models
{
    public class FileRepository{
        public int FileRepositoryId{get; set;}
        public string FilePathName{get; set;}
        public string OriginalFileName{get; set;}
        public DateTime UploadedDate{get; set;}
    }
}