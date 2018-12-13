using QSDMS.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Data.IServices
{
    public interface ITestService<T, Q, P> : IDAL<T, Q, P>
    {
    }
}
