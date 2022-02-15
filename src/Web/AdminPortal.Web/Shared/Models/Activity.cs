using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPortal.Web.Shared.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string UniqueToken { get; set; }

        public string Sender { get; set; }

        public string Payload { get; set; }

        public string UserId { get; set; }

        public string Status { get; set; }

        public string CreatedAt { get; set; }
    }
}
