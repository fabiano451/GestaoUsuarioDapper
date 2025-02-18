using GestaoUsuarios.DTO;
using GestaoUsuarios.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoUsuarios.Controllers
{
	[Route("api/usuario")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		private readonly IUsuarioInterface _usuarioInterface;

		public UsuarioController(IUsuarioInterface usuarioInterface)
		{
			_usuarioInterface = usuarioInterface;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsuarios()
		{
			var usuarios = await _usuarioInterface.GetAllUsuarios();

			if(usuarios.Status is false)
			{
				return NotFound(usuarios);
			}

			return Ok(usuarios);
		}

		[HttpGet("{usuarioId}")]
		public async Task<IActionResult> GetUsuariosId(int usuarioId)
		{
			var usuarios = await _usuarioInterface.GetUsuarioId(usuarioId);

			if (usuarios.Status is false)
			{
				return NotFound(usuarios);
			}

			return Ok(usuarios);
		}

		[HttpPost()]
		public async Task<IActionResult> CreateUsuario(UsuarioCriarDto usuarioCriarDto)
		{
			var usuarios = await _usuarioInterface.CriarUsuario(usuarioCriarDto);

			if (usuarios.Status is false)
			{
				return BadRequest(usuarios);
			}

			return Ok(usuarios);
		}

		[HttpPut()]
		public async Task<IActionResult> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
		{
			var usuarios = await _usuarioInterface.EditarUsuario(usuarioEditarDto);

			if (usuarios.Status is false)
			{
				return BadRequest(usuarios);
			}

			return Ok(usuarios);
		}

		[HttpDelete("{usuarioId}")]
		public async Task<IActionResult> Delete(int usuarioId)
		{
			var usuarios = await _usuarioInterface.DeletarUsuario(usuarioId);

			if (usuarios.Status is false)
			{
				return BadRequest(usuarios);
			}

			return Ok(usuarios);
		}
	}
}
