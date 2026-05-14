using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Exceptions
{
    public class BusinessRuleException:Exception
    {
        public BusinessRuleException(string ex):base(ex)
        { }
    }
}
