﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Data.IServices
{
    public interface IOrderService<T, Q, P> : IDAL<T, Q, P>
    {
    }
}
