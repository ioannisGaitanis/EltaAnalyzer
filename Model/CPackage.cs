using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CPackage
    {
        #region Properties

        public string TrackingNumber { get; set; }

        public DateTime Creation { get; set; }

        public CCustomer Sender { get; set; }

        public CCustomer Receiver { get; set; }

        public CCustomer Agency { get; set; }

        public CPostalInfo PostalInfo { get; set; }

        public CPackageState State { get; set; }

        public List<CHistoryUnit> History { get; set; }

        public List<CSearch> Searches { get; set; }

        public List<CAction> Actions { get; set; }



        #endregion Properties
    }
}
