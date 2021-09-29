using AdminPortal.Web.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPortal.Web.Shared.Models
{
    public class Member
    {

        /*        {
          "mappings": [],
          "balance": null,
          "lifetimePoints": null,
          "customerId": null,
          "createdAt": "0001-01-01T00:00:00",
          "updatedAt": "0001-01-01T00:00:00",
          "id": 1,
          "domainEvents": null
        }*/

        public int Id { get; set; }
        public string CustomerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
