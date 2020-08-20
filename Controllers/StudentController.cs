using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrudUsingReact.Models;
using Microsoft.Ajax.Utilities;

namespace CrudUsingReact.Controllers
{
    [RoutePrefix("Api/Student")]
    public class StudentController : ApiController
    {
        CrudDemoEntities DB = new CrudDemoEntities();
        
        [HttpPost]
        [Route("AddotrUpdatestudent")]     
        public object AddotrUpdatestudent(Student st)
        {
            try
            {
                if (st.Id == 0)
                {
                    studentmaster sm = new studentmaster();
                    sm.Name = st.Name;
                    sm.RollNo = st.RollNo;
                    sm.Address = st.Address;
                    sm.Class = st.Class;
                    DB.studentmasters.Add(sm);
                    DB.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Successfully"
                    };
                }
                else
                {
                    var obj = DB.studentmasters.Where(x => x.Id == st.Id).ToList().FirstOrDefault();
                    if (obj.Id > 0)
                    {
                        obj.Name = st.Name;
                        obj.RollNo = st.RollNo;
                        obj.Address = st.Address;
                        obj.Class = st.Class;
                        DB.SaveChanges();
                        return new Response
                        {
                            Status = "Updated",
                            Message = "Updated Successfully"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };
        }

        [HttpGet]
        [Route("Studentdetails")]

        public object Studentdetails()
        {
            var a = DB.studentmasters.ToList();
            return a;
        }

        [HttpGet]
        [Route("StudentdetailById")]
        public object StudentdetailById(int id)
        {
            var obj = DB.studentmasters.Where(x => x.Id == id).ToList().FirstOrDefault();
            return obj;
        }

        //=========== User Info ==============
        [HttpGet]
        [Route("UserByName")]
        public object UserByLogin(tblUser user )
        {
            //var obj = DB.studentmasters.Where(x => x.Id == id).ToList().FirstOrDefault();
            var obj = DB.tblUsers.Where(x => x.User_Login == user.User_Login).ToList().FirstOrDefault();

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                return obj;
            }
        }


        //====================================

        [HttpDelete]
        [Route("Deletestudent")]
        public object Deletestudent(int id)
        {
            var obj = DB.studentmasters.Where(x => x.Id == id).ToList().FirstOrDefault();
            DB.studentmasters.Remove(obj);
            DB.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }

    }
}

