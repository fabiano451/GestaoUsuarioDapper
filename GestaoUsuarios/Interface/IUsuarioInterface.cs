using GestaoUsuarios.DTO;
using GestaoUsuarios.Models;

namespace GestaoUsuarios.Interface
{
	public interface IUsuarioInterface
	{
		Task<ResponseModel<List<UsuarioListarDto>>> GetAllUsuarios();
		Task<ResponseModel<UsuarioListarDto>> GetUsuarioId(int? Id);
		Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto);
		Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto);
		Task<ResponseModel<List<UsuarioListarDto>>> DeletarUsuario(int UsuarioId);
	}
}

