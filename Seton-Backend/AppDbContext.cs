using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
namespace Seton_Backend
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NoteSharing> NoteSharings { get; set; }
        public DbSet<VoiceNote> VoiceNotes { get; set; }
        public DbSet<Integration> Integrations { get; set; }
        public DbSet<NoteVersion> NoteVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FolderNote>()
                .HasKey(fn => new { fn.FolderId, fn.NoteId });

            modelBuilder.Entity<NoteTag>()
                .HasKey(nt => new { nt.NoteId, nt.TagId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
