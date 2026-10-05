using Microsoft.EntityFrameworkCore;
using ProjectAPICore.Models;

namespace ProjectAPICore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<FileRepository> FileRepositories{get;set;}
        public DbSet<NoteFile> NoteFiles{get;set;}
        public DbSet<User> Users{get;set;}
        // add the rest here
    }
}