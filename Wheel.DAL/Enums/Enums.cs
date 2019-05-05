using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Wheel.DAL.Enums
{

    public class Enums
    {
        public enum Seasons
        {
            Winter = 0,
            Summer,
            FourSeason
        }

        public enum Status
        {
            Deaktive=0,
            Aktive
        }

        public enum UsageStatus
        {
            Used = 0,
            New
        }

        public enum MediaType
        {
            User = 0,
            Tyre
        }
    }
    
}