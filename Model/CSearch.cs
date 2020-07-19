using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CSearch
    {
        private CWebApplicationContext context;
        #region Properties
        
        public DateTime TimeStamp { get; set; }

        public CSearchStatistics PackagesInfo { get; set; }

        public CSearch ParentSearch { get; set; }

        public CSearch ChildSearch { get; set; }

        public string Description { get; set; }

        public int State
        {
            get
            {
                return this.PackagesInfo.PendingPackages > 0 ? (int)ESearchStatus.Open : (int)ESearchStatus.Completed;
            }
        }

        List<CPackage> packages;

        public List<CPackage> Packages
        {
            get => this.packages;
            set
            {
                this.packages = value;
                this.SetSearchInformation();
            }
        }

        #endregion Properties

        #region Methods

        public void SetSearchInformation()
        {
            int delivered = 0, returned = 0, canceled = 0, lost = 0;
            this.PackagesInfo.TotalPackages = this.Packages.Count;

            foreach (CPackage package in Packages)
            {
                if (package.State.MainState == EPackagesStates.Delivered) delivered++;
                if (package.State.MainState == EPackagesStates.Returned) returned++;
                if (package.State.MainState == EPackagesStates.Canceled) canceled++;
                if (package.State.MainState == EPackagesStates.Lost) lost++;

            }
            this.PackagesInfo.DeliveredPackages = delivered;
            this.PackagesInfo.ReturnedPackages = returned;
            this.PackagesInfo.CanceledPackages = canceled;
            this.PackagesInfo.LostPackages = lost;
        }

        #endregion Methods
    }
}
