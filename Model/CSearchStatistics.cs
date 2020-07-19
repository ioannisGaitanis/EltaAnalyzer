using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CSearchStatistics
    {
        #region Properties

        public int TotalPackages { get; set; }

        private int deliveredPackages;
        public int DeliveredPackages
        {
            get => this.deliveredPackages;
            set
            {
                if (this.deliveredPackages == value) return;
                this.deliveredPackages = value;
            }
        }

        private int returnedPackages;
        public int ReturnedPackages
        {
            get => this.returnedPackages;
            set
            {
                if (this.returnedPackages == value) return;
                this.returnedPackages = value;
            }
        }

        private int canceledPackages;
        public int CanceledPackages
        {
            get => this.canceledPackages;
            set
            {
                if (this.canceledPackages == value) return;
                this.canceledPackages = value;
            }
        }

        private int lostPackages;
        public int LostPackages
        {
            get => this.lostPackages;
            set
            {
                if (this.lostPackages == value) return;
                this.lostPackages = value;
            }
        }

        public int PendingPackages
        {
            get => this.TotalPackages - this.DeliveredPackages - this.ReturnedPackages - this.CanceledPackages - this.LostPackages;
        }

        #endregion Properties
    }
}
