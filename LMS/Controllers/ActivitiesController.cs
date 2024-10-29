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
    [Microsoft.AspNetCore.Cors.EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
        {
            data_access da = new data_access();

            //GET: api/<LeadController>
            [HttpGet]
            public string Get()
            {
                string sql = "select * from Activities";
                DataTable dt = da.GetDataTableByCommand(sql);
                return JsonConvert.SerializeObject(dt);
            }

            // GET api/<ProductController>/5
            [HttpGet("{LeadId}")]
            public string Get(int LeadId)
            {
                string sql = "select * from Activities where LeadId='" + LeadId + "'";
                DataTable dt = da.GetDataTableByCommand(sql);
                return JsonConvert.SerializeObject(dt);
            }

            [HttpPost]
            public string Post([FromBody] Activity activity)

            {
                Hashtable ht = new Hashtable();
                ht.Add("LeadId", activity.LeadId);
                ht.Add("AgentId", activity.AgentId);
                ht.Add("ActivityType", activity.ActivityType);
                ht.Add("ActivityNotes", activity.ActivityNotes);
                ht.Add("ActivityDate", activity.ActivityDate);
                DataTable dt = da.ExecuteStoredProcedure("activity_insert", ht);
                return JsonConvert.SerializeObject(dt);
            }

            [HttpDelete("{LeadId}")]
            public string Delete(int LeadId)
            {
                string sql = "delete from Activities where LeadId='" + LeadId + "'";
                da.ExecuteScalar(sql);
                DataTable dt = null;
                return JsonConvert.SerializeObject(dt);
            }

            // PUT api/<apiController>/5
            [HttpPut("{agentId}/{leadId}")]
            public string Put(int agentId,int leadId, [FromBody] Activity activity)
            {
                Hashtable ht = new Hashtable();
                ht.Add("AgentId", agentId);
                ht.Add("LeadId", leadId);
                ht.Add("ActivityType", activity.ActivityType);
                ht.Add("ActivityNotes", activity.ActivityNotes);
                ht.Add("ActivityDate", activity.ActivityDate);
                DataTable dt = da.ExecuteStoredProcedure("activity_update", ht);
                return JsonConvert.SerializeObject(dt);
            }
        }
    }
