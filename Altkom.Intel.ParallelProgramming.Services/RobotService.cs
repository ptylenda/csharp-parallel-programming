using Altkom.Intel.ParallelProgramming.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Altkom.Intel.ParallelProgramming.Service
{
    public class RobotService : IRobotService
    {
        private static readonly List<Robot> Robots = new List<Robot>
        {
            new Robot { Id = 1, Name = "Robot 1", SupportedActions = ActionType.Move | ActionType.Terminate },
            new Robot { Id = 2, Name = "Robot 2", SupportedActions = ActionType.Move },
            new Robot { Id = 3, Name = "Robot 3", SupportedActions = ActionType.Move | ActionType.Wait },
        };

        public void Add(Robot robot)
        {
            if (!robot.SupportedActions.HasFlag(ActionType.Move))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Robot must be moveable"
                };

                throw new HttpResponseException(resp);
            }

            Robots.Add(robot);
        }

        public IList<Robot> Get()
        {
            return Robots.ToList();
        }

        public Robot Get(int id)
        {
            return Robots.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            Robots.RemoveAll(x => x.Id == id);
        }
    }
}