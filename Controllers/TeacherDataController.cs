using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assign3.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Web.Http.Cors;


namespace Assign3.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Code Credit: Christine Bittle
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();
        //This Controller Will access the teachers table of our School database.Non-Deterministic.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]

        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {


            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY

            //cmd.CommandText = "Select * from Teachers";



            cmd.CommandText = "Select * from Teachers";
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";


            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string TeacherEmployeenumber = ResultSet["employeenumber"].ToString();
                DateTime TeacherHiredate = (DateTime)ResultSet["hiredate"];

                string TeacherSalary = ResultSet["salary"].ToString();
                //decimal TeacherSalary = (decimal)ResultSet["salary"];



                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFName;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.TeacherEmployeenumber = TeacherEmployeenumber;
                NewTeacher.TeacherHiredate = TeacherHiredate;
                NewTeacher.TeacherSalary = TeacherSalary;

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;

        }
        /// <summary>
        /// Finds an teacher from the MySQL Database through an id. Non-Deterministic.
        /// </summary>
        /// <param name="id">The Teacher ID</param>
        /// <returns>Teacher object containing information about the teacher with a matching ID. Empty Teacher if the ID does not match any teachers in the system.</returns>
        /// <example>api/TeacherData/FindTeacher/1 -> {Teacher Object}</example>




        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY

            string query = "Select teachers.*, IFNULL(classid,0) as classid, classname from teachers left join classes on teachers.teacherid = classes.teacherid where teachers.teacherid =" + id;



            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string TeacherEmployeenumber = ResultSet["employeenumber"].ToString();
                DateTime TeacherHiredate = (DateTime)ResultSet["hiredate"];
                string TeacherSalary = ResultSet["salary"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                int ClassId = Convert.ToInt32(ResultSet["classid"]);

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFName;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.TeacherEmployeenumber = TeacherEmployeenumber;
                NewTeacher.TeacherHiredate = TeacherHiredate;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.ClassName = ClassName;
                NewTeacher.ClassId = ClassId;



            }
            return NewTeacher;
        }

        // <summary>
        // 
        // </summary>
        // <param name="id"></param>
        // <example>POST: /api/TeacherData/DeleteTeacher/1</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();
            string query = "Delete from teachers where teacherid=@id";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }
        /// <summary>
        /// Adds an Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacherr">An object with fields that map to the columns of the teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"April",
        ///	"TeacherLname":"Pan",
        ///	"TeacherEmployeenumber":"123",
        ///	"TeacherHiredate":"11-02-2019",
        ///	"TeacherSalary":"16"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Debug.WriteLine(NewTeacher.TeacherFname);
            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            string query = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary ) values (@TeacherLname, @TeacherFname, @TeacherEmployeenumber, @TeacherHiredate, @TeacherSalary)";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherEmployeenumber", NewTeacher.TeacherEmployeenumber);
            cmd.Parameters.AddWithValue("@TeacherHiredate", NewTeacher.TeacherHiredate);
            cmd.Parameters.AddWithValue("@TeacherSalary", NewTeacher.TeacherSalary);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        /// <summary>
        /// Updates an Teacher on the MySQL Database. Non-Deterministic.
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the teacher's table.</param>
        /// <example>
        /// POST api/Teacher/UpdateTeacher/5 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        /// ///	"TeacherFname":"April",
        ///	"TeacherLname":"Pan",
        ///	"TeacherEmployeenumber":"123",
        ///	"TeacherHiredate":"11-02-2019",
        ///	"TeacherSalary":"19"
        /// }
        /// </example>

        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(TeacherInfo.TeacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@TeacherEmployeenumber, hiredate=@TeacherHiredate, salary=@TeacherSalary  where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherEmployeenumber", TeacherInfo.TeacherEmployeenumber);
            cmd.Parameters.AddWithValue("@TeacherHiredate", TeacherInfo.TeacherHiredate);
            cmd.Parameters.AddWithValue("@TeacherSalary", TeacherInfo.TeacherSalary);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
    }
}