using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Model
{
    public partial class DeliverOrderEntity
    {

        public string OrderStatusName { get; set; }
        public MemberAddressEntity Address { get; set; }

        public OrderEntity Order { get; set; }
    }
}
