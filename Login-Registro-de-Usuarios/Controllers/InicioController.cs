using Login_Registro_de_Usuarios.Datas;
using Login_Registro_de_Usuarios.Models;
using Login_Registro_de_Usuarios.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Login_Registro_de_Usuarios.Controllers
{
    public class InicioController : Controller
    {
        private readonly Context _context;
        private readonly ValidacionInicio _validacion;
        public InicioController(Context context, ValidacionInicio validacion)
        {
            _context = context;
            _validacion = validacion;
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string nombre, string clave)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_context.conexion))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_verificarCredenciales", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Clave", clave);

                        bool existeUsuario = (bool)await cmd.ExecuteScalarAsync();
                        await conn.CloseAsync();
                        if (existeUsuario)
                        {
                            return RedirectToAction("Inicio", "Inicio");
                        }
                        else
                        {
                            ViewData["Mensaje"] = "El nombre o clave no coincide";
                            return View();
                        }
                    }
                    
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult RegistroUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistroUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_context.conexion))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_registrarUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                        
                        bool existeUsuario = await _validacion.ExisteUsuario(usuario.Nombre);
                        if (existeUsuario)
                        {
                            ViewData["Mensaje"] = "Ya existe un usuario con ese nombre";
                            return View();
                        }

                        await cmd.ExecuteNonQueryAsync();
                        await conn.CloseAsync();
                        return RedirectToAction("IniciarSesion", "Inicio");
                    }
                }
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult Inicio()
        {
            return View();
        }
    }
}
