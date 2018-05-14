using Altkom.Intel.ParallelProgramming.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Service
{
    public interface IRobotService
    {
        void Add(Robot robot);

        IList<Robot> Get();

        Robot Get(int id);

        void Remove(int id);
    }
}
