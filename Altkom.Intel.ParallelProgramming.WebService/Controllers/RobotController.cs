using Altkom.Intel.ParallelProgramming.Service;
using Altkom.Intel.ParallelProgramming.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Altkom.Intel.ParallelProgramming.WebService.Controllers
{
    public class RobotController : ApiController
    {
        private readonly IRobotService robotService;

        public RobotController(IRobotService robotService)
        {
            this.robotService = robotService;
        }

        public RobotController()
            : this(new RobotService())
        {
        }

        // GET: api/Robot
        public IEnumerable<Robot> Get()
        {
            return this.robotService.Get();
        }

        // GET: api/Robot/5
        public Robot Get(int id)
        {
            return this.robotService.Get(id);
        }

        // POST: api/Robot
        public IHttpActionResult Post([FromBody]Robot value)
        {
            this.robotService.Add(value);
            return this.CreatedAtRoute("DefaultApi", new { id = value.Id }, value);
        }

        // PUT: api/Robot/5
        public void Put(int id, [FromBody]Robot value)
        {
        }

        // DELETE: api/Robot/5
        public void Delete(int id)
        {
            this.robotService.Remove(id);
        }
    }
}
