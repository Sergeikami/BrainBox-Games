using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApiProjecte.Models;

namespace WebApiProjecte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidasController : ControllerBase
    {
        // GET: api/Partidas
        [HttpGet]
        public ActionResult<IEnumerable<Partida>> Get()
        {
            List<Partida> partidas = new List<Partida>();

            try
            {
                string query = "SELECT id_partida, id_usuario, fecha_partida, duracion, nivel_dificultad, estrellas FROM partidas";
                SqlCommand sqlCommand = new SqlCommand(query, BD.Connexio);

                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Partida p = new Partida
                    {
                        Id_partida = Convert.ToInt32(reader["id_partida"]),
                        Id_usuario = Convert.ToInt32(reader["id_usuario"]),
                        Fecha_partida = Convert.ToDateTime(reader["fecha_partida"]),
                        Duracion = Convert.ToInt32(reader["duracion"]),
                        Nivel_dificultad = Convert.ToInt32(reader["nivel_dificultad"]),
                        Estrellas = Convert.ToInt32(reader["estrellas"])
                    };
                    partidas.Add(p);
                }

                reader.Close();
                return Ok(partidas);
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

        // GET: api/Partidas/5
        [HttpGet("{id}")]
        public ActionResult<Partida> Get(int id)
        {
            try
            {
                string query = "SELECT id_partida, id_usuario, fecha_partida, duracion, nivel_dificultad, estrellas FROM partidas WHERE id_partida = @id";
                SqlCommand sqlCommand = new SqlCommand(query, BD.Connexio);
                sqlCommand.Parameters.AddWithValue("@id", id);

                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    Partida p = new Partida
                    {
                        Id_partida = Convert.ToInt32(reader["id_partida"]),
                        Id_usuario = Convert.ToInt32(reader["id_usuario"]),
                        Fecha_partida = Convert.ToDateTime(reader["fecha_partida"]),
                        Duracion = Convert.ToInt32(reader["duracion"]),
                        Nivel_dificultad = Convert.ToInt32(reader["nivel_dificultad"]),
                        Estrellas = Convert.ToInt32(reader["estrellas"])
                    };
                    reader.Close();
                    return Ok(p);
                }

                reader.Close();
return NotFound(new { error = $"No existe una partida con el id {id}" });
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

        // POST: api/Partidas
        [HttpPost]
        public ActionResult<Partida> Post([FromBody] Partida partida)
        {
            try
            {
                if (partida.Fecha_partida < new DateTime(1753, 1, 1))
                {
                    partida.Fecha_partida = DateTime.Now;
                }

                string query = @"INSERT INTO partidas (id_usuario, fecha_partida, duracion, nivel_dificultad, estrellas) 
                                 OUTPUT INSERTED.id_partida 
                                 VALUES (@id_usuario, @fecha_partida, @duracion, @nivel_dificultad, @estrellas)";

                SqlCommand sqlCommand = new SqlCommand(query, BD.Connexio);
                sqlCommand.Parameters.AddWithValue("@id_usuario", partida.Id_usuario);
                sqlCommand.Parameters.AddWithValue("@fecha_partida", partida.Fecha_partida);
                sqlCommand.Parameters.AddWithValue("@duracion", partida.Duracion);
                sqlCommand.Parameters.AddWithValue("@nivel_dificultad", partida.Nivel_dificultad);
                sqlCommand.Parameters.AddWithValue("@estrellas", partida.Estrellas);

                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                partida.Id_partida = Convert.ToInt32(sqlCommand.ExecuteScalar());

                return Ok(partida);
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

        // DELETE: api/Partidas/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                string query = "DELETE FROM partidas WHERE id_partida = @id";
                SqlCommand sqlCommand = new SqlCommand(query, BD.Connexio);
                sqlCommand.Parameters.AddWithValue("@id", id);

                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                int filasAfectadas = sqlCommand.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    return NotFound(new { error = $"No existe una partida con el id {id}" });
                }

                return Ok(new { mensaje = "Partida eliminada correctamente" });
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