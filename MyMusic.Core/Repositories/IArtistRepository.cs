using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMusic.Core.Models;

namespace MyMusic.Core.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
        // GetAllWithMusicsAsync returns a list of all artists with their musics
        Task<IEnumerable<Artist>> GetAllWithMusicsAsync();
        // GetWithMusicsByIdAsync returns an artist with its musics by id
        Task<Artist> GetWithMusicsByIdAsync(int id);
    }
}