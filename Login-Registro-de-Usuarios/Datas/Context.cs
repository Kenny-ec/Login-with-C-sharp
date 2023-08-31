namespace Login_Registro_de_Usuarios.Datas
{
    public class Context
    {
        public string conexion { get; }
        public Context(string cadenaConexion)
        {
            conexion = cadenaConexion;
        }
    }
}
