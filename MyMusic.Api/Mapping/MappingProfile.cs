using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyMusic.Api.Resources;
using MyMusic.Core.Auth;
using MyMusic.Core.Models;

namespace MyMusic.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Music, MusicResource>();
            CreateMap<Artist, ArtistResource>();

            // Resource to Domain
            CreateMap<MusicResource, Music>();
            CreateMap<ArtistResource, Artist>();
            CreateMap<SaveMusicResource, Music>();
            CreateMap<SaveArtistResource, Artist>();
            CreateMap<UserSignUpResource, User>().ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
        }
    }
}