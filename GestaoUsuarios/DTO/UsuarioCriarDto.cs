namespace GestaoUsuarios.DTO
{
	public class UsuarioCriarDto
	{
		public string NomeCompleto { get; set; }

		public string Email { get; set; }

		public string Cargo { get; set; }

		public double Salario { get; set; }

		public string CPF { get; set; }

		public bool Ativo { get; set; }

		public string Senha { get; set; }
	}
}
