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
    public class TicketServices : Service<Ticket> , ITicketServices
    {
        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork uow = new UnitOfWork(dbf);

        

        public TicketServices() : base (uow)
        {
            
        }

        


    }
}
