using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Model
{
    public partial class OrderEntity
    {
        public string OrderTypeName { get; set; }

        public string PayWayName { get; set; }

        public string OrderStatusName { get; set; }
        public MemberAddressEntity Address { get; set; }


    }
}
