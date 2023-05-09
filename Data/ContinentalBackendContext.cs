using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContinentalBackend.Models;

namespace ContinentalBackend.Data
{
    public class ContinentalBackendContext : DbContext
    {
        public ContinentalBackendContext (DbContextOptions<ContinentalBackendContext> options)
            : base(options)
        {
        }

        public DbSet<ContinentalBackend.Models.Alerta> Alerta { get; set; } = default!;
    }
}
