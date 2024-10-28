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

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        data_access da = new data_access();

        //GET: api/<AgentController>
        [HttpGet]
        public string Get()
        {
            string sql = "select * from Agents";
            DataTable dt = da.GetDataTableByCommand(sql);
            return JsonConvert.SerializeObject(dt);
        }

        // GET api/<LeadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string sql = "select * from Agents where AgentId='" + id + "'";
            DataTable dt = da.GetDataTableByCommand(sql);
            return JsonConvert.SerializeObject(dt);
        }

        [HttpPost]
        public string Post([FromBody] Agent agent)
        {
            Hashtable ht = new Hashtable();
            ht.Add("FirstName", agent.FirstName);
            ht.Add("LastName", agent.LastName);
            ht.Add("Email", agent.Email);
            ht.Add("PhoneNumber", agent.PhoneNumber);
            ht.Add("Role", agent.Role);
            DataTable dt = da.ExecuteStoredProcedure("agent_insert", ht);
            return JsonConvert.SerializeObject(dt);
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string sql = "delete from Agents where AgentId='" + id + "'";
            da.ExecuteScalar(sql);
            DataTable dt = null;
            return JsonConvert.SerializeObject(dt);
        }

        // PUT api/<apiController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Agent agent)
        {
            Hashtable ht = new Hashtable();
            ht.Add("AgentId", id);
            ht.Add("FirstName", agent.FirstName);
            ht.Add("LastName", agent.LastName);
            ht.Add("Email", agent.Email);
            ht.Add("PhoneNumber", agent.PhoneNumber);
            ht.Add("Role", agent.Role);
            DataTable dt = da.ExecuteStoredProcedure("agent_update", ht);
            return JsonConvert.SerializeObject(dt);
        }
    }
}

