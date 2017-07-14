using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Application.API.Controllers
{
    public class HomeController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "http://localhost:2088/";

        public async Task<ActionResult> Index()
        {
            try
            {
                List<Employee> Employees = new List<Employee>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = await client.GetAsync("api/employee");
                    if (Response.IsSuccessStatusCode)
                    {
                        var ResponseResult = Response.Content.ReadAsStringAsync().Result;
                        Employees = JsonConvert.DeserializeObject<List<Employee>>(ResponseResult);
                    }
                }

                return View(Employees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<ActionResult> Detail(string id)
        {
            try
            {
                Employee Employee = new Employee();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = await client.GetAsync("api/employee/" + id);
                    if (Response.IsSuccessStatusCode)
                    {
                        var ResponseResult = Response.Content.ReadAsStringAsync().Result;
                        Employee = JsonConvert.DeserializeObject<Employee>(ResponseResult);
                    } 
                }

                return View(Employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Create()
        {
            try
            {
                return View(new Employee());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        string stringData = JsonConvert.SerializeObject(employee);

                        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                        client.BaseAddress = new Uri(Baseurl);
                        HttpResponseMessage response = await client.PostAsync("api/employee", contentData);
                    }

                    return RedirectToAction("Index", "Home");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> Update(string id)
        {
            try
            {
                Employee Employee = new Employee();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Response = await client.GetAsync("api/employee/" + id);
                    if (Response.IsSuccessStatusCode)
                    {
                        var ResponseResult = Response.Content.ReadAsStringAsync().Result;
                        Employee = JsonConvert.DeserializeObject<Employee>(ResponseResult);
                    }
                }

                return View(Employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        string stringData = JsonConvert.SerializeObject(employee);

                        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                        client.BaseAddress = new Uri(Baseurl);
                        HttpResponseMessage response = await client.PutAsync("api/employee/" + employee.Code, contentData);
                    }

                    return RedirectToAction("Index", "Home");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Response = await client.DeleteAsync("api/employee/" + id); 
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
