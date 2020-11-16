using Data;
using Data.Infrastructure;
using ServicePattern;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Service.Timesheet
{
    public class ProjectServices : Service<Project> , IProjectServices
    {
        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork uow = new UnitOfWork(dbf);
        

        public ProjectServices() : base (uow)
        {
            
        }

        


    }
}
