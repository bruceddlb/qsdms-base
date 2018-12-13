using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Model
{
    public partial class ProductEntity
    {
        public List<AttachmentPicEntity> AttachmentPicList { get; set; }

        public string ProductTypeName { get; set; }
        public string CateName { get; set; }

        public string ProductStatusName { get; set; }
        public string AuditStatusName { get; set; }

        public List<ProductRuleEntity> RuleList { get; set; }

        public string RuleExtName { get; set; }
    }
}
