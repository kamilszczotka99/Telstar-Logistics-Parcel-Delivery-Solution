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
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SessionController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public string GetRouteRequest(Account account)
        {
            var acc = _context.Accounts.FirstOrDefault(x => x.Email == account.Email);
            {
                if (acc.Password == account.Password)
                {
                    var sess = _context.Sessions.FirstOrDefault(x => x.Email == acc.Email);
                    {
                        if (sess.Token.Length>0)
                        {
                            return sess.Token;
                        }
                        else
                        {
                            var tmpSess =
                            new Session

                            {
                                Email = acc.Email,
                                Token = "aasada"

                            };
                            _context.Add(tmpSess
                           
                            );
                            _context.SaveChanges();
                            return tmpSess.Token;
                        }
                       
                    }
                    
                }
                else return "wrong pass";

            }



            //var account = _context.Accounts.FirstOrDefault(x => x.Email == email);
            //if (account.Password == _)
            //{

            //}
            //return await _context.Sessions.ToListAsync();

            //        // GET: api/Session
            //        [HttpGet]
            //        public async Task<ActionResult<IEnumerable<Session>>> GetSession()
            //        {
            //            return await _context.Sessions.ToListAsync();
            //        }

            //        // GET: api/Session/5
            //        [HttpGet("{id}")]
            //        public async Task<ActionResult<Session>> GetSession(string id)
            //        {
            //            var session = await _context.Sessions.FindAsync(id);

            //            if (session == null)
            //            {
            //                return NotFound();
            //            }

            //            return session;
            //        }

            //        // PUT: api/Session/5
            //        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            //        [HttpPut("{id}")]
            //        public async Task<IActionResult> PutSession(string id, Session session)
            //        {
            //            if (id != session.Token)
            //            {
            //                return BadRequest();
            //            }

            //            _context.Entry(session).State = EntityState.Modified;

            //            try
            //            {
            //                await _context.SaveChangesAsync();
            //            }
            //            catch (DbUpdateConcurrencyException)
            //            {
            //                if (!SessionExists(id))
            //                {
            //                    return NotFound();
            //                }
            //                else
            //                {
            //                    throw;
            //                }
            //            }

            //            return NoContent();
            //        }

            //        // POST: api/Session
            //        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            //        [HttpPost]
            //        public async Task<ActionResult<Session>> PostSession(Session session)
            //        {
            //            _context.Sessions.Add(session);
            //            try
            //            {
            //                await _context.SaveChangesAsync();
            //            }
            //            catch (DbUpdateException)
            //            {
            //                if (SessionExists(session.Token))
            //                {
            //                    return Conflict();
            //                }
            //                else
            //                {
            //                    throw;
            //                }
            //            }

            //            return CreatedAtAction("GetSession", new { id = session.Token }, session);
            //        }

            //        // DELETE: api/Session/5
            //        [HttpDelete("{id}")]
            //        public async Task<IActionResult> DeleteSession(string id)
            //        {
            //            var session = await _context.Sessions.FindAsync(id);
            //            if (session == null)
            //            {
            //                return NotFound();
            //            }

            //            _context.Sessions.Remove(session);
            //            await _context.SaveChangesAsync();

            //            return NoContent();
            //        }

            //        private bool SessionExists(string id)
            //        {
            //            return _context.Sessions.Any(e => e.Token == id);
            //        }
        }
    }
}



