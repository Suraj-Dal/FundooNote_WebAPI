using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class fundooContext : DbContext
    {
        public fundooContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<UserEntity> userEntities { get; set; }
        public DbSet<NotesEntity> notesEntities { get; set; }
        public DbSet<CollaboratorEntity> collaboratorEntities { get; set; }
        public DbSet<LabelEntity> labelEntities { get; set; }
    }
}
