using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApiProjecte.Models;

namespace WebApiProjecte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasController : ControllerBase
    {
        // PUT: api/Preguntas/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] bool acierto)
        {
            try
            {
                string query = acierto
                    ? "UPDATE preguntas SET total_aciertos = total_aciertos + 1 WHERE id_pregunta = @id"
                    : "UPDATE preguntas SET total_errores = total_errores + 1 WHERE id_pregunta = @id";

                SqlCommand sqlCommand = new SqlCommand(query, BD.Connexio);
                sqlCommand.Parameters.AddWithValue("@id", id);

                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                int filasAfectadas = sqlCommand.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    return NotFound(new { error = $"No existe una pregunta con el id {id}" });
                }

                return Ok(new { mensaje = acierto ? "Acierto sumado correctamente" : "Error sumado correctamente" });
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