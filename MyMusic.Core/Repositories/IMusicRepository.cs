using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMusic.Core.Models;

namespace MyMusic.Core.Repositories
{
    public interface IMusicRepository : IRepository<Music>
    {
        // GetAllWithArtistAsync returns a list of all music with their artist
        Task<IEnumerable<Music>> GetAllWithArtistAsync();
        // GetWithArtistByIdAsync returns a music with its artist by id
        Task<Music> GetWithArtistByIdAsync(int id);
        // GetAllWithArtistByArtistIdAsync returns a list of all music with their artist by artist id
        Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId);
    }
}