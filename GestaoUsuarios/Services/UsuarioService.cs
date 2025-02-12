using AutoMapper;
using Dapper;
using GestaoUsuarios.DTO;
using GestaoUsuarios.Interface;
using GestaoUsuarios.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestaoUsuarios.Services
{
	public class UsuarioService : IUsuarioInterface
	{
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;


		public UsuarioService(IConfiguration configuration, IMapper mapper)
		{
			// necessario para acessar appsettings de dentro do meu usuario service onde guarda nossa string de conexão
			_configuration = configuration;
			_mapper = mapper;
		}

		public async Task<ResponseModel<List<UsuarioListarDto>>> GetAllUsuarios()
		{
			var response = new ResponseModel<List<UsuarioListarDto>>();
			// using usa a conexão e depois fecha automaticamente
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				var usuariosBanco = await connection.QueryAsync<Usuario>("select * from usuarios");

				if (usuariosBanco?.Count() == 0)
				{
					response.Mensagem = "Nenhum usuário localizado";
					response.Status = false;
					return response;
				}

				var usuarioMapeado = _mapper.Map<List<UsuarioListarDto>>(usuariosBanco);


				response.Dados = usuarioMapeado;
				response.Mensagem = "Usuarios localizdos com sucessso";
				return response;
			}

		}


		public async Task<ResponseModel<UsuarioListarDto>> GetUsuarioId(int? Id)
		{
			var response = new ResponseModel<UsuarioListarDto>();
			// using usa a conexão e depois fecha automaticamente
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				var usuariosBanco = await connection.QueryFirstOrDefaultAsync<Usuario>($"select * from usuarios where Id = {Id}");

				if (usuariosBanco is null)
				{
					response.Mensagem = "Nenhum usuário localizado";
					response.Status = false;
					return response;
				}

				var usuarioMapeado = _mapper.Map<UsuarioListarDto>(usuariosBanco);


				response.Dados = usuarioMapeado;
				response.Mensagem = "Usuarios localizdos com sucessso";
				return response;
			}
		}

		public async Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
		{
			var response = new ResponseModel<List<UsuarioListarDto>>();
			// using usa a conexão e depois fecha automaticamente
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				var usuariosBanco = await connection.ExecuteAsync($"insert into usuarioS (Nomecompleto, Email, Cargo, Salario, CPF, Senha, Ativo)" +
					$"                                            values(@Nomecompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Ativo)",
					                                               usuarioCriarDto);

				if (usuariosBanco == 0)
				{
					response.Mensagem = $"Ocorreu um erro ao cadatrar o usuario {usuarioCriarDto.NomeCompleto}";
					response.Status = false;
					return response;
				}

				var usuarios = await ListarUsuario(connection);
				var usuarioMap = _mapper.Map<List<UsuarioListarDto>>(usuarios);

				response.Dados = usuarioMap;
				response.Mensagem = "Usuarios Listados com sucessso";
				return response;
			}

		}

		public async Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
		{
			var response = new ResponseModel<List<UsuarioListarDto>>();
			// using usa a conexão e depois fecha automaticamente
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				var usuariosBanco = await connection.ExecuteAsync($"update usuarios set Nomecompleto = @Nomecompleto,  Email = @Email,  Cargo = @Cargo,  Salario = @Salario,  CPF = @CPF, Ativo = @Ativo where Id  = @Id", usuarioEditarDto);
					                                           

				if (usuariosBanco == 0)
				{
					response.Mensagem = $"Ocorreu um erro ao Editar o usuário {usuarioEditarDto.NomeCompleto}";
					response.Status = false;
					return response;
				}

				var usuarios = await ListarUsuario(connection);
				var usuarioMap = _mapper.Map<List<UsuarioListarDto>>(usuarios);

				response.Dados = usuarioMap;
				response.Mensagem = "Usuarios Listados com sucessso";
				return response;
			}
		}

		public async Task<ResponseModel<List<UsuarioListarDto>>> DeletarUsuario(int UsuarioId)
		{
			var response = new ResponseModel<List<UsuarioListarDto>>();
			// using usa a conexão e depois fecha automaticamente
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				var usuariosBanco = await connection.ExecuteAsync($" delete from usuarios where Id = {UsuarioId}");

				if (usuariosBanco == 0)
				{
					response.Mensagem = $"Não foi possivel deletar o usuario Id {UsuarioId}";
					response.Status = false;
					return response;
				}

				var usuarios = await ListarUsuario(connection);
				var usuarioMap = _mapper.Map<List<UsuarioListarDto>>(usuarios);

				response.Dados = usuarioMap;
				response.Mensagem = "Usuarios Listados com sucessso";
				return response;
			}
		}

		private static async Task<IEnumerable<Usuario>> ListarUsuario(SqlConnection connection)
		{
			return await connection.QueryAsync<Usuario>("select * from usuarios");
		}
	}
}
