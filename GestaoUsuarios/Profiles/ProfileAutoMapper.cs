using AutoMapper;
using GestaoUsuarios.DTO;
using GestaoUsuarios.Models;

namespace GestaoUsuarios.Profiles
{
	public class ProfileAutoMapper : Profile
	{
		public ProfileAutoMapper()
		{
			CreateMap<Usuario, UsuarioListarDto>();
			CreateMap<UsuarioListarDto, Usuario>();
			CreateMap<UsuarioCriarDto, Usuario>();
		}
	}
}
