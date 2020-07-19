using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CPackageState
    {
        public EPackagesStates MainState { get; set; }

        public string SecondaryState { get; set; }
    }
}
