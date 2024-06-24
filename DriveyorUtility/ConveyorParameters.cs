using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveyorUtility
{
    public class ConveyorParameters
    {
        public int PalletLength { get; set; }
        public int StopPosition { get; set; }
        public int GapSize { get; set; }
        public int TravelSize { get; set; }
        public int TimeoutSteps { get; set; }
        public int Direction { get; set; }
        public int DoubleSided { get; set; }
        public int TravelCorrection { get; set; }
        public int RevExternal { get; set; }
        public int RevInternal { get; set; }
        public int InhExternal { get; set; }
        public int InhInternal { get; set; }
    }

}

