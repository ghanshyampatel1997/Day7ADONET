using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;//to take data from app.config as from  xml


namespace ConnectedArchitectureEx
{
    class Shipper
    {
        public int Shipperid { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public void GetShipper()
        {
            Console.WriteLine("Enter ShipperName");
            CompanyName = Console.ReadLine();
            Console.WriteLine("Enter phoneNo");
            Phone = Console.ReadLine();
        }
    }
    class BasicADO
    {
        
        static void Main()
        {
            //2. create SqlConeection object
            //connection string
            SqlConnection con = null;
            //creating object for command class
            SqlCommand cmd;

            try
            {
                //Windows authentication
                con = new SqlConnection(
                "Data Source = GHANSHYAM-PC\\SQLEXPRESS; Initial Catalog = Northwind; Integrated Security = true");
                //Sql server authentication
                //con = new SqlConnection(
                //"Data Source= GHANSHYAM-PC\\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa;Password=123#");

                Console.WriteLine("Connected with SQLserver");

                //open the connection
                con.Open();

                ////insert operation
                //cmd = new SqlCommand("insert into Shippers values('Sai','4546567555555676')", con);
                //Console.WriteLine("Affected rows are:{0}", cmd.ExecuteNonQuery());


                ////delete Operation
                //cmd = new SqlCommand("delete from Shippers where Phone='4546567555555676'", con);
                //int rows = cmd.ExecuteNonQuery();// returns int of effected rows
                //Console.WriteLine("Affected rows are:{0}", rows);

                //Dynamic insertion
                //creating object for shipperclass
                Shipper shipper = new Shipper();
                shipper.GetShipper();//calling method to get input from user

                //insert
                /*
                cmd = new SqlCommand("insert into Shippers values(@cname,@phoneNo)", con);

                cmd.Parameters.AddWithValue("@cname", shipper.CompanyName);
                cmd.Parameters.AddWithValue("@phoneNo", shipper.Phone);
                int i= cmd.ExecuteNonQuery();// returns int of effected rows
                Console.WriteLine("effected rows are:{0}", i);
                cmd.Parameters.Clear();//to clear all parameters
                */

                //delete operationwith parameters
                Console.WriteLine("Enter the CompanyName to be deleted");
                shipper.CompanyName = Console.ReadLine();
                string delcon = "delete from Shippers where CompanyName=@Cname";
                cmd = new SqlCommand(delcon, con);
                cmd.Parameters.AddWithValue("@Cname", shipper.CompanyName);
                int j= cmd.ExecuteNonQuery();// returns int of effected rows
                Console.WriteLine("effected rows are:{0}", j);

            }
            catch (Exception e)
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
