using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMusic.Core;
using MyMusic.Core.Repositories;
using MyMusic.Data.Repositories;

namespace MyMusic.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyMusicDbContext _context;
        private MusicRepository _musicRepository;
        private ArtistRepository _artistRepository;

        public UnitOfWork(MyMusicDbContext context)
        {
            this._context = context;
        }

        public IMusicRepository Musics => _musicRepository = _musicRepository ?? new MusicRepository(_context);

        public IArtistRepository Artists => _artistRepository = _artistRepository ?? new ArtistRepository(_context);

        public async Task<int> CommitAsync()
        {
            // SaveChangesAsync() method saves all changes made in this context to the database
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            // Dispose() method is called when the object is no longer needed
            _context.Dispose();
        }

    }
}