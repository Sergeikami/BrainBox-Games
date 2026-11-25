using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApiProjecte.Models;

namespace WebApiProjecte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasPartidaController : ControllerBase
    {
        // PUT: api/PreguntasPartida
        [HttpPut]
        public ActionResult Put([FromBody] PreguntasPartida preguntaPartida)
        {
            try
            {
                string query = @"UPDATE preguntas_partidas 
                                 SET acierto = @acierto 
                                 WHERE id_partida = @id_partida AND id_pregunta = @id_pregunta";

                SqlCommand sqlCommand = new SqlCommand(query, BD.Connexio);
                sqlCommand.Parameters.AddWithValue("@acierto", preguntaPartida.Acierto);
                sqlCommand.Parameters.AddWithValue("@id_partida", preguntaPartida.Id_partida);
                sqlCommand.Parameters.AddWithValue("@id_pregunta", preguntaPartida.Id_pregunta);

                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                int filasAfectadas = sqlCommand.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    return NotFound(new { error = $"No existe la pregunta {preguntaPartida.Id_pregunta} asociada a la partida {preguntaPartida.Id_partida}" });
                }

                return Ok(preguntaPartida);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            finally
            {
                if (BD.Connexio.State == System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Close();
                }
            }
        }
    }
}