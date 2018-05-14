using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Altkom.Intel.ParallelProgramming.Service.Models
{
    public class Robot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ActionType SupportedActions { get; set; }

        public override string ToString()
        {
            return $"ID={this.Id}, Name={this.Name}, SupportedActions={this.SupportedActions}";
        }
    }

    [Flags]
    public enum ActionType
    {
        Terminate = 1,
        Move,
        Wait
    }
}