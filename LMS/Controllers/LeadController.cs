using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using LMS.Models;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using LMS.Services;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.Controllers
{
    [Microsoft.AspNetCore.Cors.EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        HttpResponseMessage response = new HttpResponseMessage();
        data_access da = new data_access();

        //GET: api/<LeadController>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            string sql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) as sl,l.*,p.ProductName,a.FirstName as Agent from Leads l inner join Products p on p.ProductId=l.ProductId join Agents a on a.AgentId = l.AssignedAgentId";
            DataTable dt = da.GetDataTableByCommand(sql);
            if (dt.Rows.Count > 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);//200
                response.Content = new StringContent(JsonConvert.SerializeObject(dt), Encoding.UTF8, "application/json");
                return response;
            }
            else {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);//404
                var error = new { Code = 500, Message = "Something went wrong!" };
                response.Content = new StringContent(JsonConvert.SerializeObject(error), Encoding.UTF8, "application/json");
                return response;
            }
        }

        // GET api/<LeadController>/5
        [HttpGet("{LeadId}")]
        public HttpResponseMessage Get(int LeadId)
        {
            string sql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) as sl,l.*,p.ProductName,a.FirstName as Agent from Leads l inner join Products p on p.ProductId=l.ProductId join Agents a on a.AgentId = l.AssignedAgentId where LeadId='" + LeadId + "'";
            DataTable dt = da.GetDataTableByCommand(sql);
            if (dt.Rows.Count > 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);//200
                response.Content = new StringContent(JsonConvert.SerializeObject(dt), Encoding.UTF8, "application/json");
                return response;
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);//404
                var error = new { Code = 500, Message = "Something went wrong!" };
                response.Content = new StringContent(JsonConvert.SerializeObject(error), Encoding.UTF8, "application/json");
                return response;
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Lead lead)
        {
            Hashtable ht = new Hashtable();
            ht.Add("FirstName", lead.FirstName);
            ht.Add("LastName", lead.LastName);
            ht.Add("Email", lead.Email);
            ht.Add("PhoneNumber", lead.PhoneNumber);
            ht.Add("ProductId", lead.ProductId);
            ht.Add("AssignedAgentId", lead.AssignedAgentId);
            ht.Add("Source", lead.Source);
            ht.Add("Status", lead.Status);
            DataTable dt = da.ExecuteStoredProcedure("lead_insert", ht);
            if (dt.Rows.Count > 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);//200
                response.Content = new StringContent(JsonConvert.SerializeObject(dt), Encoding.UTF8, "application/json");
                return response;
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);//404
                var error = new { Code = 500, Message = "Something went wrong!" };
                response.Content = new StringContent(JsonConvert.SerializeObject(error), Encoding.UTF8, "application/json");
                return response;
            }
        }

        [HttpDelete("{LeadId}")]
        public HttpResponseMessage Delete(int LeadId)
        {
            string sql = "delete from Leads where LeadId='" + LeadId + "'";
            da.ExecuteScalar(sql);
            DataTable dt = null;
            if (dt.Rows.Count > 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);//200
                response.Content = new StringContent(JsonConvert.SerializeObject(dt), Encoding.UTF8, "application/json");
                return response;
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);//404
                var error = new { Code = 500, Message = "Something went wrong!" };
                response.Content = new StringContent(JsonConvert.SerializeObject(error), Encoding.UTF8, "application/json");
                return response;
            }
        }

        // PUT api/<apiController>/5
        [HttpPut("{LeadId}")]
        public HttpResponseMessage Put(int LeadId, [FromBody] Lead lead)
        {
            Hashtable ht = new Hashtable();
            ht.Add("LeadId", LeadId);
            ht.Add("FirstName", lead.FirstName);
            ht.Add("LastName", lead.LastName);
            ht.Add("Email", lead.Email);
            ht.Add("PhoneNumber", lead.PhoneNumber);
            ht.Add("ProductId", lead.ProductId);
            ht.Add("AssignedAgentId", lead.AssignedAgentId);
            ht.Add("Source", lead.Source);
            ht.Add("Status", lead.Status);
            DataTable dt = da.ExecuteStoredProcedure("lead_update", ht);
            if (dt.Rows.Count > 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);//200
                response.Content = new StringContent(JsonConvert.SerializeObject(dt), Encoding.UTF8, "application/json");
                return response;
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);//404
                var error = new { Code = 500, Message = "Something went wrong!" };
                response.Content = new StringContent(JsonConvert.SerializeObject(error), Encoding.UTF8, "application/json");
                return response;
            }
        }
    }
}