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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.Controllers
{
    [Microsoft.AspNetCore.Cors.EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        data_access da = new data_access();

        //GET: api/<LeadController>
        [HttpGet]
        public string Get()
        {
            string sql = "select * from Leads";
            DataTable dt = da.GetDataTableByCommand(sql);
            return JsonConvert.SerializeObject(dt);
        }

        // GET api/<LeadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string sql = "select * from Leads where LeadId='" + id + "'";
            DataTable dt = da.GetDataTableByCommand(sql);
            return JsonConvert.SerializeObject(dt);
        }

        [HttpPost]
        public string Post([FromBody] Lead lead)
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
            return JsonConvert.SerializeObject(dt);
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string sql = "delete from Leads where LeadId='" + id + "'";
            da.ExecuteScalar(sql);
            DataTable dt = null;
            return JsonConvert.SerializeObject(dt);
        }

        // PUT api/<apiController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Lead lead)
        {
            Hashtable ht = new Hashtable();
            ht.Add("LeadId", id);
            ht.Add("FirstName", lead.FirstName);
            ht.Add("LastName", lead.LastName);
            ht.Add("Email", lead.Email);
            ht.Add("PhoneNumber", lead.PhoneNumber);
            ht.Add("ProductId", lead.ProductId);
            ht.Add("AssignedAgentId", lead.AssignedAgentId);
            ht.Add("Source", lead.Source);
            ht.Add("Status", lead.Status);
            DataTable dt = da.ExecuteStoredProcedure("lead_update", ht);
            return JsonConvert.SerializeObject(dt);
        }
    }
}