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
        [HttpGet("{AgentId}")]
        public string Get(int AgentId)
        {
            string sql = "select * from Agents where AgentId='" + AgentId + "'";
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

        [HttpDelete("{AgentId}")]
        public string Delete(int AgentId)
        {
            string sql = "delete from Agents where AgentId='" + AgentId + "'";
            da.ExecuteScalar(sql);
            DataTable dt = null;
            return JsonConvert.SerializeObject(dt);
        }

        // PUT api/<apiController>/5
        [HttpPut("{AgentId}")]
        public string Put(int AgentId, [FromBody] Agent agent)
        {
            Hashtable ht = new Hashtable();
            ht.Add("AgentId", AgentId);
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

