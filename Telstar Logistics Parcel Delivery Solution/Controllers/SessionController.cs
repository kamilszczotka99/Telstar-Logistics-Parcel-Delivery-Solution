using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Controllers
{
    
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SessionController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("sessions/checkToken")]
        public OkObjectResult CheckUserToken(string email, string token)
        {
            var sess = _context.Sessions.FirstOrDefault(x => x.Email == email);
            {
                if (sess != null)
                {
                    if (sess.Token == token)
                    {
                        return Ok(200);
                       
                    }
                    else return null;
                }
                else return null;
            }





        }
        [HttpGet]
        [Route("sessions/checkCredentials")]
        public string GetUserCredentials(string email, string password)
        {
            var acc = _context.Accounts.FirstOrDefault(x => x.Email == email);
            {
                if (acc != null)
                {
                    if (acc.Password == password)
                    {
                        var sess = _context.Sessions.FirstOrDefault(x => x.Email == acc.Email);
                        {
                            if (sess != null)
                            {
                                return sess.Token;
                            }
                            else
                            {
                                var tmpSess =
                                new Session
                                {
                                    Email = acc.Email,
                                    Token = System.Guid.NewGuid().ToString()
                                };
                                _context.Add(tmpSess
                                );
                                _context.SaveChanges();
                                return tmpSess.Token;
                            }
                        }
                    }
                    else return null;
                }
                else return null;
            }
        }


        [HttpDelete]
        [Route("sessions/logoutUser")]
        public OkObjectResult LogoutUser(string email, string token)
        {
            var sess = _context.Sessions.FirstOrDefault(x => x.Email == email);
            {
                if (sess != null)
                {
                    if (sess.Token == token)
                    {
                        _context.Remove(sess);
                        
                        _context.SaveChanges();
                        return Ok(200);
                        
                    }
                    else return null;
                }
                else return null;
            }
        }
    }


}



