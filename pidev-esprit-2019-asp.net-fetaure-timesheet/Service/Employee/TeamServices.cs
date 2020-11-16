using Data;
using ServicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;

namespace Service.Employee
{
    public class TeamServices : Service<Team>, ITeamServices
    {
        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork utwk = new UnitOfWork(dbf);
        public TeamServices() : base(utwk)
        {
        }
    }
}
