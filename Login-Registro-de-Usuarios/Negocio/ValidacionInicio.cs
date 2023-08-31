using Login_Registro_de_Usuarios.Datas;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Login_Registro_de_Usuarios.Negocio
{
    public class ValidacionInicio
    {
        private readonly Context _context;
        public ValidacionInicio(Context context)
        {
            _context = context;
        }

        public async Task<bool> ExisteUsuario(string nombre)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_context.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_existeUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));

                        await conn.OpenAsync();

                        bool existeUsuario = (bool)await cmd.ExecuteScalarAsync();

                        if (existeUsuario)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
