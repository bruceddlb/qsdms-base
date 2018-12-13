using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Data.IServices
{
    public interface ISalePlanService<T, Q, P> : IDAL<T, Q, P>
    {
        /// <summary>
        /// 重启计划
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        bool DoAgin(string keyValue);
    }
}
