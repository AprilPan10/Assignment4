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
    public class StudentDataController : ApiController
    {
        //Code Credit: Christine Bittle
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();
        //This Controller Will access the students table of our School database.
        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of students (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Student> ListStudents()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Select * from students";
            cmd.CommandText = query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Students
            List<Student> Students = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFName = ResultSet["studentfname"].ToString();
                string StudentLName = ResultSet["studentlname"].ToString();
                string StudentStudentnumber = ResultSet["studentnumber"].ToString();
                DateTime StudentEnroldate = (DateTime)ResultSet["enroldate"];
                
                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFName;
                NewStudent.StudentLname = StudentLName;
                NewStudent.StudentStudentnumber = StudentStudentnumber;
                NewStudent.StudentEnroldate = StudentEnroldate;
               
                //Add the Student Name to the List
                Students.Add(NewStudent);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of students names
            return Students;
        }
        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Select * from students where studentid =" + id;
            cmd.CommandText = query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFName = ResultSet["studentfname"].ToString();
                string StudentLName = ResultSet["studentlname"].ToString();
                string StudentStudentnumber = ResultSet["studentnumber"].ToString();
                DateTime StudentEnroldate = (DateTime)ResultSet["enroldate"];

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFName;
                NewStudent.StudentLname = StudentLName;
                NewStudent.StudentStudentnumber = StudentStudentnumber;
                NewStudent.StudentEnroldate = StudentEnroldate;

            }
            return NewStudent;
        }
    }
}
