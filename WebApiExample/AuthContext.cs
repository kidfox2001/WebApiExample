using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiExample.Models;

using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace WebApiExample
{
    // Enable-Migrations เปิดการไมเกรด
    // Add-Migration ClientRefreshTokenCreate  สร้างไมเกรด
    // Update-Database 


    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext() : base("AuthContext")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}