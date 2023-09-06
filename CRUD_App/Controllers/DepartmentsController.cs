using CRUD_App.Models;
using CRUD_App.Dal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CRUD_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public Response GetAllDepartments()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbConnect").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.GetAllDepartments(connection);
            return response;
        }

        [HttpPost]
        [Route("AddDepartment")]
        public Response AddDepartment(Department dept)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbConnect").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.AddDepartment(connection, dept);
            return response;
        }

        [HttpPut]
        [Route("UpdateDepartment")]
        public Response UpdateDepartment(Department dept)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbConnect").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.UpdateDepartment(connection, dept);
            return response;
        }

        [HttpDelete]
        [Route("DeleteDepartment/{Id}")]
        public Response DeleteDepartment(int Id)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbConnect").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.DeleteDepartment(connection, Id);
            return response;
        }
    }
}
