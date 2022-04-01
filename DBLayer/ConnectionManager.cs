using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace MVCappCoreWeb.DBLayer
{
    public class ConnectionManager
    {
        public static SqlConnection GetConnection()
        {
            //SqlConnection connection = new SqlConnection(@"Data Source = SERVER-01-DELL\SQLExpress; Initial Catalog = TRACE;Persist Security Info=True; User ID = WorkUser; PWD = vik72003; Connection Timeout = 30");
            //SqlConnection connection = new SqlConnection(@"Data Source = SERVER-01-DELL\SQLExpress; Initial Catalog = ORBISTEST;Persist Security Info=True; User ID = WorkUser; PWD = vik72003;");
            SqlConnection connection = new SqlConnection(@"Data Source = SERVER-01-DELL\SQLExpress; Initial Catalog = ORBISTEST;Persist Security Info=True; User ID = ijelavic; PWD = JuMp.664; Connection Timeout = 30");
            //var AppName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["DefaultConnection"];
            //string Connection_String = AppName;
            ////string Connection_String = "Data Source=192.168.5.201\SQLExpress;Initial Catalog=TRACE;Integrated Security=True;User ID=WorkUser; PWD=vik72003; Connection Timeout=30";
            //SqlConnection connection = new SqlConnection(Connection_String);
            connection.Open();

            return connection;

        }
        public static SqlConnection GetConnectionEvents()
        {
            SqlConnection connection = new SqlConnection(@"Data Source = SERVER-01-DELL\SQLExpress; Initial Catalog = EventsDb;Persist Security Info=True; User ID = ijelavic; PWD = JuMp.664; Connection Timeout = 30");
            //var AppName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["DefaultConnection"];
            //string Connection_String = AppName;
            ////string Connection_String = "Data Source=192.168.5.201\SQLExpress;Initial Catalog=TRACE;Integrated Security=True;User ID=WorkUser; PWD=vik72003; Connection Timeout=30";
            //SqlConnection connection = new SqlConnection(Connection_String);
            connection.Open();

            return connection;

        }
    }
}
