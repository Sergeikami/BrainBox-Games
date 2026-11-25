using System.Text.Json.Serialization.Metadata;

namespace WebApiProjecte.Models
{
    public class PreguntasPartida
    {

        public int Id_partida { get; set; }

        public int Id_pregunta { get; set; }

        public int Acierto { get; set; }
    }
}
