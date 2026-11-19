using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApiProjecte.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProjecte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarisController : ControllerBase
    {
        // GET: api/<UsuarisController>
        [HttpGet]
        public ActionResult<IEnumerable<Usuari>> Get()
        {
            List<Usuari> usuaris = new List<Usuari>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT id_usuario, nombre_usuario, hash_contr, id_rol, fecha_registro FROM usuarios", BD.Connexio);

                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Usuari u = new Usuari
                    {
                        Id_usuario = Convert.ToInt32(reader["id_usuario"]),
                        Nombre_usuario = reader["nombre_usuario"].ToString() ?? string.Empty,
                        Hash_contr = reader["hash_contr"].ToString() ?? string.Empty,
                        Id_rol = Convert.ToInt32(reader["id_rol"]),
                        Fecha_registro = Convert.ToDateTime(reader["fecha_registro"])
                    };
                    usuaris.Add(u);
                }

                reader.Close();
                return Ok(usuaris);
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

        // GET api/<UsuarisController>/5
        [HttpGet("{id}")]
        public ActionResult<Usuari> Get(int id)
        {
            Usuari? usuari = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT id_usuario, nombre_usuario, hash_contr, id_rol, fecha_registro FROM usuarios WHERE id_usuario = @Id_usuario", BD.Connexio);
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@Id_usuario", id);

                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    usuari = new Usuari
                    {
                        Id_usuario = Convert.ToInt32(reader["id_usuario"]),
                        Nombre_usuario = reader["nombre_usuario"].ToString() ?? string.Empty,
                        Hash_contr = reader["hash_contr"].ToString() ?? string.Empty,
                        Id_rol = Convert.ToInt32(reader["id_rol"]),
                        Fecha_registro = Convert.ToDateTime(reader["fecha_registro"])
                    };
                }

                reader.Close();

                if (usuari == null)
                {
                    return NotFound(new { missatge = "No s'ha trobat cap usuari amb aquest ID" });
                }

                return Ok(usuari);
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

        // POST api/<UsuarisController>
        [HttpPost]
        public ActionResult Post([FromBody] Usuari usuari)
        {
            try
            {
                if (BD.Connexio.State != System.Data.ConnectionState.Open)
                {
                    BD.Connexio.Open();
                }

                // 1. Comprobar si ya existe un usuario con el mismo nombre o ID
                string checkQuery = "SELECT COUNT(*) FROM usuarios WHERE nombre_usuario = @Nombre_usuario OR id_usuario = @Id_usuario";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, BD.Connexio))
                {
                    checkCommand.Parameters.AddWithValue("@Nombre_usuario", usuari.Nombre_usuario);
                    checkCommand.Parameters.AddWithValue("@Id_usuario", usuari.Id_usuario);

                    int existe = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existe > 0)
                    {
                        return Conflict(new { missatge = "Ja existeix un usuari amb aquest nom o ID" });
                    }
                }

                // 2. Si no existe, procedemos a insertarlo
                string insertQuery = "INSERT INTO usuarios (nombre_usuario, hash_contr, id_rol, fecha_registro) " +
                                     "VALUES (@Nombre_usuario, @Hash_contr, @Id_rol, @Fecha_registro)";

                using (SqlCommand sqlCommand = new SqlCommand(insertQuery, BD.Connexio))
                {
                    sqlCommand.Parameters.AddWithValue("@Nombre_usuario", usuari.Nombre_usuario);
                    sqlCommand.Parameters.AddWithValue("@Hash_contr", usuari.Hash_contr);
                    sqlCommand.Parameters.AddWithValue("@Id_rol", usuari.Id_rol);
                    sqlCommand.Parameters.AddWithValue("@Fecha_registro", usuari.Fecha_registro == default ? DateTime.Now : usuari.Fecha_registro);

                    sqlCommand.ExecuteNonQuery();
                }

                return Ok(new { missatge = "Usuari afegit correctament" });
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

        // DELETE api/<UsuarisController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM usuarios WHERE id_usuario = @Id_usuario", BD.Connexio);
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@Id_usuario", id);

                BD.Connexio.Open();
                int filesAfectades = sqlCommand.ExecuteNonQuery();

                if (filesAfectades == 0)
                {
                    return NotFound(new { missatge = "No s'ha trobat cap usuari amb aquest ID" });
                }

                return Ok(new { missatge = "Usuari eliminat correctament" });
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
