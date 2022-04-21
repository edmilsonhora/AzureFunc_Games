using Games_Func.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games_Func.Domain.DataAccess
{
   public class Contexto :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=GamesDb.db");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Record>  Records { get; set; }
    }
}
