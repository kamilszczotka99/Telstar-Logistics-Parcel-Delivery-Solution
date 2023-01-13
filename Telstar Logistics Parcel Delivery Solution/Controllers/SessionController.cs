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
        [HttpPost]
        [Route("sessions/checkToken")]
        public ActionResult CheckUserToken([FromBody] Session session)
        {
            var sess = _context.Sessions.FirstOrDefault(x => x.Email == session.Email);
            {
                if (sess != null)
                {
                    if (sess.Token == session.Token)
                    {
                        return Ok(200);

                    }
                    else return null;
                }
                else return null;
            }


        }
        [HttpPost]
        [Route("sessions/checkCredentials")]
        public string GetUserCredentials([FromBody] Account account)
        {
            var acc = _context.Accounts.FirstOrDefault(x => x.Email == account.Email);
            {
                if (acc != null)
                {
                    if (acc.Password == account.Password)
                    {
                        var sess = _context.Sessions.FirstOrDefault(x => x.Email == acc.Email);
                        {
                            if (sess != null)
                            {
                                return "{\"token\":" + "\"" + sess.Token + "\"}";
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
                                return "{\"token\":" + "\"" + tmpSess.Token + "\"}";

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
        public ActionResult LogoutUser([FromBody] Session session)
        {
            var sess = _context.Sessions.FirstOrDefault(x => x.Email == session.Email);
            {
                if (sess != null)
                {
                    if (sess.Token == session.Token)
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



