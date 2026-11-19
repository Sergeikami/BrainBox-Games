namespace WebApiProjecte.Models
{
    public class Usuari
    {

        public int Id_usuario { get; set; }
        
        public String Nombre_usuario { get; set; }

        public string Hash_contr { get; set; }

        public int Id_rol { get; set; }

        public DateTime Fecha_registro { get; set; }
    }
}
