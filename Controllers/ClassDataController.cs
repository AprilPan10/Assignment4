using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assign3.Models;
using MySql.Data.MySqlClient;


namespace Assign3.Controllers
{
    public class ClassDataController : ApiController
    {
        //Code Credit: Christine Bittle
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();
        //This Controller Will access the classes table of our School database.
        /// <summary>
        /// Returns a list of Classes in the system
        /// </summary>
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns>
        /// A list of classes 
        /// </returns>
        [HttpGet]
        public IEnumerable<Class> ListClasses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Select * from classes";
            cmd.CommandText = query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Classes
            List<Class> Classes = new List<Class> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassName = ResultSet["classname"].ToString();
                string ClassCode = ResultSet["classcode"].ToString();
                DateTime ClassStartdate = (DateTime)ResultSet["startdate"];
                DateTime ClassFinishdate = (DateTime)ResultSet["finishdate"];


                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassName = ClassName;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassStartdate = ClassStartdate;
                NewClass.ClassFinishdate = ClassFinishdate;


                //Add the Class Name to the List
                Classes.Add(NewClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of class names
            return Classes;
        }
        [HttpGet]
        public Class FindClass(int id)
        {
            Class NewClass = new Class();
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Select * from classes where classid =" + id;
            cmd.CommandText = query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassName = ResultSet["classname"].ToString();
                string ClassCode = ResultSet["classcode"].ToString();
                DateTime ClassStartdate = (DateTime)ResultSet["startdate"];
                DateTime ClassFinishdate = (DateTime)ResultSet["finishdate"];
                
                NewClass.ClassId = ClassId;
                NewClass.ClassName = ClassName;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassStartdate = ClassStartdate;
                NewClass.ClassFinishdate = ClassFinishdate;



            }
            return NewClass;
        }
    }
}