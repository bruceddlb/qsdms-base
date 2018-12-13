using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Data.IServices
{
    public interface ISalePlanDetaiService<T, Q, P> : IDAL<T, Q, P>
    {
        /// <summary>
        /// 处理到货状态
        /// </summary>
        /// <param name="keys"></param>
        bool DoDispose(string[] keys);
    }
}
