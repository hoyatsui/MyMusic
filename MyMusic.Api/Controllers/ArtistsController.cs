using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Api.Resources;
using MyMusic.Api.Validators;
using MyMusic.Core.Models;
using MyMusic.Core.Services;


namespace MyMusic.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistsController(IArtistService artistService, IMapper mapper)
        {
            _mapper = mapper;
            _artistService = artistService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ArtistResource>>> GetAllArtists()
        {
            var artists = await _artistService.GetAllAritists();
            var artistResources = _mapper.Map<IEnumerable<Artist>, IEnumerable<Artist>>(artists);
            return Ok(artistResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistResource>> GetArtistById(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            var artistResource = _mapper.Map<Artist, Artist>(artist);
            return Ok(artistResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<ArtistResource>> CreateArtist([FromBody] SaveArtistResource saveArtistResource)
        {
            var validator = new SaveArtistResourceValidator();
            var validationResult = await validator.ValidateAsync(saveArtistResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok
            var artistToCreate = _mapper.Map<SaveArtistResource, Artist>(saveArtistResource);
            var newArtist = await _artistService.CreateArtist(artistToCreate);
            var artist = await _artistService.GetArtistById(newArtist.Id);
            var artistResource = _mapper.Map<Artist, Artist>(artist);
            return Ok(artistResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArtistResource>> UpdateArtist(int id, [FromBody] SaveArtistResource saveArtistResource)
        {
            var validator = new SaveArtistResourceValidator();
            var validationResult = await validator.ValidateAsync(saveArtistResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok
            var artistToBeUpdate = await _artistService.GetArtistById(id);
            if (artistToBeUpdate == null)
                return NotFound();
            var artist = _mapper.Map<SaveArtistResource, Artist>(saveArtistResource);
            await _artistService.UpdateArtist(artistToBeUpdate, artist);
            var updatedArtist = await _artistService.GetArtistById(id);
            var updatedArtistResource = _mapper.Map<Artist, Artist>(updatedArtist);
            return Ok(updatedArtistResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            await _artistService.DeleteArtist(artist);
            return NoContent();
        }
    }
}