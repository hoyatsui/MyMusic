using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMusic.Data.Configurations;
using MyMusic.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyMusic.Core.Auth;

namespace MyMusic.Data
{
    public class MyMusicDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public MyMusicDbContext(DbContextOptions<MyMusicDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                .ApplyConfiguration(new MusicConfiguration());
            builder
                .ApplyConfiguration(new ArtistConfiguration());
        }
    }
}