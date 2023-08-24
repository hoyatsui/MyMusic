using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Models;
using MyMusic.Core.Repositories;

namespace MyMusic.Data.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        // MyMusicDbContext is the database context, which is injected into the constructor. :base means that the constructor of the base class is called, which is the Repository class.
        public ArtistRepository(MyMusicDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Artist>> GetAllWithMusicsAsync()
        {
            // Include(a => a.Musics) means that the Musics property of the Artist class is included in the query.
            // ToListAsync() means that the query is executed and the result is returned as a list asynchronously.
            return await MyMusicDbContext.Artists
                .Include(a => a.Musics)
                .ToListAsync();
        }

        public async Task<Artist> GetWithMusicsByIdAsync(int id)
        {
            // FirstOrDefaultAsync(a => a.Id == id) means that the first artist with the given id is returned asynchronously.
            return await MyMusicDbContext.Artists
                .Include(a => a.Musics)
                .SingleOrDefaultAsync(a => a.Id == id);
        }



        // get{return Context as MyMusicDbContext;} means that the Context property of the base class is cast to MyMusicDbContext.
        private MyMusicDbContext MyMusicDbContext
        {
            get { return Context as MyMusicDbContext; }
        }
    }
}