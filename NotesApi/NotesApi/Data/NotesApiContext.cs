using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NotesApi.Models
{
    public class NotesApiContext : DbContext
    {
        public NotesApiContext (DbContextOptions<NotesApiContext> options)
            : base(options)
        {
        }

        public DbSet<NotesApi.Models.Note> Note { get; set; }
    }
}
