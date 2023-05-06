using Microsoft.Data.SqlClient;

namespace ChaskiTravel.DAO
{
    public class conexionDAO
    {
        //definicion de la conexion ( Cambien el server DESKTOP-OE6VUV5, por el de ustedes 
        //para que funcione la conexion a la bd)
        SqlConnection cn = new SqlConnection(
        @"server = JDHQ\SQLEXPRESS;database = BDAtipax;Trusted_Connection = True;" +
         "MultipleActiveResultSets = True;TrustServerCertificate = False;Encrypt = False");

        //lectura de la conexion, get
        public SqlConnection getcn
        {
            get { return cn; }
        }
    }
}
