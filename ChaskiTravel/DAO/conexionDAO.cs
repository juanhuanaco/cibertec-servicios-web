using Microsoft.Data.SqlClient;

namespace ChaskiTravel.DAO
{
    public class conexionDAO
    {
        SqlConnection cn = new SqlConnection(
        @"server = .;database = BDChaskiTravel;Trusted_Connection = True;" +
         "MultipleActiveResultSets = True;TrustServerCertificate = False;Encrypt = False");

        //lectura de la conexion, get
        public SqlConnection getcn
        {
            get { return cn; }
        }
    }
}
