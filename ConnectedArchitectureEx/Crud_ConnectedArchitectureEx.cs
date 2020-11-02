using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;//1.adding SqlServer library
using System.Net.Configuration;
using System.Data;// for stored procedure 
namespace ConnectedArchitectureEx
{
    class Region
    {
        public int RegionID { get; set; }
        public string RegionDescription { get; set; }

        internal void GetRegion()
        {
            Console.WriteLine("Enter regionID");
            RegionID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter region description");
            RegionDescription = Console.ReadLine();
        }

    }
    class DataAccess
    {
        //2.Create SQLconnection object
        //connection string
        SqlConnection con = null;
        //craeting object for command class
        SqlCommand cmd;

        public SqlConnection GetConnection()
        {
            // Windows authentication
            con = new SqlConnection(
            "Data Source = GHANSHYAM-PC\\SQLEXPRESS; Initial Catalog = Northwind; Integrated Security = true");
            con.Open();
            return con;
        }

        //Select
        public void DisplayRegion()
        {
            /* sqlconnection con1;
             *con1=GetConnection();*/
            //reuse of object
            con = GetConnection();
            SqlDataReader dr;

            //string s="select * from Region";
            cmd = new SqlCommand("select * from Region", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //console.writeline(dr[0]+" "+dr[1]);
                Console.WriteLine(dr["RegionID"] + "   " + dr["RegionDescription"]);
            }
        }

        //Execute scalar
        internal void GetbyExecuteScalar()
        {
            con = GetConnection(); 
            cmd = new SqlCommand("select count(RegionID) from Region", con); 
            int count= Convert.ToInt32(cmd.ExecuteScalar()); //execute scalar retuns object 
            Console.WriteLine("No of Region:{0}",count);
        }

        //Update
        internal void EditRegion()
        {
            Region region = new Region(); 
            Console.WriteLine("Please the which region description to be updated");
            DisplayRegion();
            region.GetRegion(); 
            con = GetConnection(); 
            cmd = new SqlCommand("update Region set RegionDescription=@RDesc where RegionID=@Rid", con);
            cmd.Parameters.AddWithValue("@Rid", region.RegionID); 
            cmd.Parameters.AddWithValue("@RDesc", region.RegionDescription); 
            int i = cmd.ExecuteNonQuery(); 
            Console.WriteLine("Rows Updated are:{0}", i);
        }

        //insert
        internal void EditInsertRegion()
        {
            Region region = new Region();
            Console.WriteLine("Please whatch the region description to be updated");
            region.GetRegion();
            con = GetConnection();
            cmd = new SqlCommand("insert Region values(@Rid,@RDesc)", con);

            cmd.Parameters.AddWithValue("@Rid", region.RegionID);
            cmd.Parameters.AddWithValue("@RDesc", region.RegionDescription);
            int i = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows Inserted are:{0}", i);
        }

        ///procedure call
        internal void CallProcedure()
        {
            con = GetConnection();
            Console.WriteLine("Enter the Customer ID"); 
            string custId =Console.ReadLine();
                                    //Procedurename 
            cmd = new SqlCommand("CustOrdersOrders", con);
                            //using System.Data 
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters. AddWithValue("@CustomerID", custId);//for one line below code
            //or 
            /*Sq1Parameter sqlParameter = new SqlParameter("@CustomerID", custId); 
             * sqlParameter.Direction = ParameterDirection.Input; 
             * sqlParameter.DbType = DbType. String; 
             * cmd.Parameters.Add(sqlParameter);*/

            SqlDataReader rdr;

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine(rdr[0]+" "+rdr[1]);
            }
        }
    }
    class Crud_ConnectedArchitectureEx
    {
        static void Main()
        {
            SqlConnection con;
            DataAccess d = new DataAccess();
            con = d.GetConnection();
            try
            {
                //d.EditInsertRegion();
                //d.DisplayRegion();
                //d.GetbyExecuteScalar();
                d.EditRegion();

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                con.Close();
            }
            Console.Read();
        }

    }
}
