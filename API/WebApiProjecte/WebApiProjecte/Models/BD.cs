using Microsoft.Data.SqlClient;
namespace WebApiProjecte.Models

{
    public class BD
    {

        public static SqlConnection Connexio { get; set; } = new SqlConnection
            ("Server=51.83.192.177;Database=jocs_dam05;User Id=jocs_dam05;Password=jocs_dam05;TrustServerCertificate=true;");
    }
}
