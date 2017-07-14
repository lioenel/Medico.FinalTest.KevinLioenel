using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Application.API.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET api/employee
        public IHttpActionResult Get()
        {
            try
            {
                MedicoFinalTestEntities dbcontext = new MedicoFinalTestEntities();
                var Employees = dbcontext.Employees.ToList();
                return Ok(Employees);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message.ToString()),
                    ReasonPhrase = "Critical Exception"
                });
            }
        }

        // GET api/employee/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                MedicoFinalTestEntities dbcontext = new MedicoFinalTestEntities();
                var Employee = dbcontext.Employees.Where(x => x.Code == id).FirstOrDefault();
                return Ok(Employee);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message.ToString()),
                    ReasonPhrase = "Critical Exception"
                });
            }
        }

        // POST api/employee
        public IHttpActionResult Post([FromBody]Employee employee)
        {
            try
            {
                MedicoFinalTestEntities dbcontext = new MedicoFinalTestEntities();

                employee.Code = Guid.NewGuid().ToString();
                employee.Name = string.IsNullOrEmpty(employee.Name) ? "" : employee.Name;
                employee.Phone = string.IsNullOrEmpty(employee.Phone) ? "" : employee.Phone;
                employee.Address = string.IsNullOrEmpty(employee.Address) ? "" : employee.Address;
                dbcontext.Employees.Add(employee);
                dbcontext.SaveChanges();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message.ToString()),
                    ReasonPhrase = "Critical Exception"
                });
            }
        }

        // PUT api/employee/5
        public IHttpActionResult Put(string id, [FromBody]Employee employee)
        {
            try
            {
                MedicoFinalTestEntities dbcontext = new MedicoFinalTestEntities();

                var Employee = dbcontext.Employees.Where(x => x.Code == id).FirstOrDefault();
                if (Employee == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Employee not fount"),
                        ReasonPhrase = "Critical Exception"
                    });
                }

                Employee.Name = string.IsNullOrEmpty(employee.Name) ? "" : employee.Name;
                Employee.Phone = string.IsNullOrEmpty(employee.Phone) ? "" : employee.Phone;
                Employee.Address = string.IsNullOrEmpty(employee.Address) ? "" : employee.Address;
                dbcontext.SaveChanges();

                return Ok(Employee);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message.ToString()),
                    ReasonPhrase = "Critical Exception"
                });
            }
        }

        // DELETE api/employee/5
        public IHttpActionResult Delete(string id)
        {
            try
            {
                MedicoFinalTestEntities dbcontext = new MedicoFinalTestEntities();

                var Employee = dbcontext.Employees.Where(x => x.Code == id).FirstOrDefault();
                if (Employee == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Employee not fount"),
                        ReasonPhrase = "Critical Exception"
                    });
                }

                dbcontext.Employees.Remove(Employee);
                dbcontext.SaveChanges();

                return Ok(Employee);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message.ToString()),
                    ReasonPhrase = "Critical Exception"
                });
            }

        }
    }
}
