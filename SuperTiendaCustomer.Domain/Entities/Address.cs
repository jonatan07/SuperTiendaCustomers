using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Domain.Entities
{
    public class Address:BaseEntity
    {
        public string Description { get; set; }
        public bool Preference { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
