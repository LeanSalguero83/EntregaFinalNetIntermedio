using Api.Interfaces;
using Api.Services;
using Common.Helpers;
using Data;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RecuperarCuentaController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly IRecuperarCuentaService _recuperarcuentaservice;

		public RecuperarCuentaController(IRecuperarCuentaService recuperarcuentaservice,IConfiguration configuration)
		{
			_recuperarcuentaservice = recuperarcuentaservice;
			_configuration = configuration;

		}

		[HttpPost]
		[Route("GuardarCodigo")]
		public bool GuardarCodigo(LoginDto login)
		{
			try
			{
				
				var usuario = _recuperarcuentaservice.BuscarUsuarios(login);
				if (usuario != null)
				{
					usuario.Codigo = login.Codigo;
					return _recuperarcuentaservice.GuardarCodigo(usuario);
				}
				else
				{
					return false;
				}


			}
			catch (Exception ex)
			{
				
				throw ex;
			}
		}

		[HttpPost]
		[Route("CambiarClave")]
		public bool CambiarClave(LoginDto login)
		{
			try
			{
				
				var usuario = _recuperarcuentaservice.BuscarUsuarios(login);
				if (usuario != null)
				{
					usuario.Codigo = null;
					usuario.Clave = EncryptHelper.Encriptar(login.Clave);
					return _recuperarcuentaservice.GuardarCodigo(usuario);
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				
				throw ex;
			}
		}
	}
}