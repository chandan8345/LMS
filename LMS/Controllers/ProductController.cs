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
    public class ProductController : ControllerBase
    {
        data_access da = new data_access();

        //GET: api/<LeadController>
        [HttpGet]
        public string Get()
        {
            string sql = "select * from Products";
            DataTable dt = da.GetDataTableByCommand(sql);
            return JsonConvert.SerializeObject(dt);
        }

        // GET api/<ProductController>/5
        [HttpGet("{ProductId}")]
        public string Get(int ProductId)
        {
            string sql = "select * from Products where ProductId='" + ProductId + "'";
            DataTable dt = da.GetDataTableByCommand(sql);
            return JsonConvert.SerializeObject(dt);
        }

        [HttpPost]
        public string Post([FromBody] Product product)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProductName", product.ProductName);
            ht.Add("PlanDetails", product.PlanDetails);
            ht.Add("InsuranceType", product.InsuranceType);
            DataTable dt = da.ExecuteStoredProcedure("product_insert", ht);
            return JsonConvert.SerializeObject(dt);
        }

        [HttpDelete("{ProductId}")]
        public string Delete(int ProductId)
        {
            string sql = "delete from Leads where ProductId='" + ProductId + "'";
            da.ExecuteScalar(sql);
            DataTable dt = null;
            return JsonConvert.SerializeObject(dt);
        }

        // PUT api/<apiController>/5
        [HttpPut("{ProductId}")]
        public string Put(int ProductId, [FromBody] Product product)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProductId", ProductId);
            ht.Add("ProductName", product.ProductName);
            ht.Add("PlanDetails", product.PlanDetails);
            ht.Add("InsuranceType", product.InsuranceType);
            DataTable dt = da.ExecuteStoredProcedure("product_update", ht);
            return JsonConvert.SerializeObject(dt);
        }
    }
}