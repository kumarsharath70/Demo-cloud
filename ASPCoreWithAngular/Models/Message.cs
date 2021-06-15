using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MessageTitle { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool LogicalDelete { get; set; }
        public bool IsActive { get; set; }
    }
}
