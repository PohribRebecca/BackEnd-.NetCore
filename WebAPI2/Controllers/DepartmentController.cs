using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI2.Models;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId, DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSrouce = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSrouce))
            {
                myCon.Open();
                using(SqlCommand myComamand = new SqlCommand(query, myCon))
                {
                    myReader = myComamand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Department dep)
    {
        string query = @"
                        insert into dbo.Department values
                        ('"+dep.DepartmentName+@"')
                         ";
        DataTable table = new DataTable();
        string sqlDataSrouce = _configuration.GetConnectionString("EmployeeAppCon");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSrouce))
        {
            myCon.Open();
            using (SqlCommand myComamand = new SqlCommand(query, myCon))
            {
                myReader = myComamand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }
        return new JsonResult("Added Successfully");
    }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"
                        update dbo.Department set   
                        DepartmentName = '" +dep.DepartmentName + @"'
                        where DepartmentId = " +dep.DepartmentId+ @"
                        ";
            DataTable table = new DataTable();
            string sqlDataSrouce = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSrouce))
            {
                myCon.Open();
                using (SqlCommand myComamand = new SqlCommand(query, myCon))
                {
                    myReader = myComamand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        delete from dbo.Department 
                        where DepartmentId = " + id + @"
                        ";
            DataTable table = new DataTable();
            string sqlDataSrouce = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSrouce))
            {
                myCon.Open();
                using (SqlCommand myComamand = new SqlCommand(query, myCon))
                {
                    myReader = myComamand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }

    
}

