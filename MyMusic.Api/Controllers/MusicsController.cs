using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Api.Resources;
using MyMusic.Api.Validators;
using MyMusic.Core.Models;
using MyMusic.Core.Services;

namespace MyMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicsController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;
        public MusicsController(IMusicService musicService, IMapper mapper)
        {
            _mapper = mapper;
            _musicService = musicService;
        }


        [HttpGet("api/musics")]
        public async Task<ActionResult<IEnumerable<Music>>> GetAllMusics()
        {
            var musics = await _musicService.GetAllWithArtist();
            var MusicResources = _mapper.Map<IEnumerable<Music>, IEnumerable<MusicResource>>(musics);
            return Ok(MusicResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MusicResource>> GetMusicById(int id)
        {
            var music = await _musicService.GetMusicById(id);
            var MusicResource = _mapper.Map<Music, MusicResource>(music);
            return Ok(MusicResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<MusicResource>> CreateMusic([FromBody] SaveMusicResource saveMusicResource)
        {
            var validator = new SaveMusicResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok
            var musicToCreate = _mapper.Map<SaveMusicResource, Music>(saveMusicResource);
            var newMusic = await _musicService.CreateMusic(musicToCreate);
            var music = await _musicService.GetMusicById(newMusic.Id);
            var MusicResource = _mapper.Map<Music, MusicResource>(music);
            return Ok(MusicResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MusicResource>> UpdateMusic(int id, [FromBody] SaveMusicResource saveMusicResource)
        {
            var validator = new SaveMusicResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);
            var requestIsInvalid = id == 0 || !validationResult.IsValid;
            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var musicToBeUpdate = await _musicService.GetMusicById(id);
            if (musicToBeUpdate == null)
                return NotFound();
            var music = _mapper.Map<SaveMusicResource, Music>(saveMusicResource);
            await _musicService.UpdateMusic(musicToBeUpdate, music);
            var updatedMusic = await _musicService.GetMusicById(id);
            var updatedMusicResource = _mapper.Map<Music, MusicResource>(updatedMusic);
            return Ok(updatedMusicResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusic(int id)
        {
            if (id == 0)
                return BadRequest();
            var music = await _musicService.GetMusicById(id);
            if (music == null)
                return NotFound();
            await _musicService.DeleteMusic(music);
            return NoContent();
        }
    }
}