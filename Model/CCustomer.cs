using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CCustomer
    {
        public string BrandName { get; set; }

        public List<CAddress> Address { get; set; }

        public List<CCommunication> Communication { get; set; }
    }
}
