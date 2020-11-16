using ServicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Employee
{
    public interface IEmployeeServices : IService<Data.user>
    {
    }
}
