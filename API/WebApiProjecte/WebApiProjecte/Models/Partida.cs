namespace WebApiProjecte.Models
{
    public class Partida
    {

        public int Id_partida { get; set; }

        public int Id_usuario { get; set; }

        public DateTime Fecha_partida { get; set; }

        public int Duracion { get; set; }

        public int Nivel_dificultad { get; set; }

        public int Estrellas { get; set; }
    }
}
