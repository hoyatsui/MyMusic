using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMusic.Core.Models;
namespace MyMusic.Core.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetAllAritists();
        Task<Artist> GetArtistById(int id);
        Task<Artist> CreateArtist(Artist newArtist);
        Task UpdateArtist(Artist artistToBeUpdated, Artist artist);
        Task DeleteArtist(Artist artist);
    }
}