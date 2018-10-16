using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;

namespace TournamentSortSys.Common
{
    public class DeviceDetector
    {
        public enum ChassisTypes
        {
            Other = 1,
            Unknow,
            Desktop,
            LowProfileDesktop,
            PizzaBox,
            MiniTower,
            Tower,
            Portable,
            Laptop,
            Notebook,
            Handheld,
            DockingStation,
            AllInOne,
            SubNoteBook,
            SpaceSaving,
            MainSystemChassis,
            ExpansionChassis,
            SubChassis,
            BusExpansionChassis,
            PeripheralChassis,
            StorageChasis,
            RackMountChassis,
            SealedCasePC
        }

        public enum KnownHardwareKind
        {
            Unknown,
            SurfacePro,
            SurfacePro2,
            SurfacePro3,
            SurfacePro4,
            SurfaceBook2,
            DellPro8,
            DellPro10
        }

        static string[] dellModel = new String[] { "Venue 8 Pro 5830" };
        static KnownHardwareKind[] dellModelKind=new KnownHardwareKind[] {KnownHardwareKind.DellPro8};
        static string[] msModel = new string[] { "Surface with Windows 8 Pro", "Surface Pro 2", "Surface Pro 3" };
        static KnownHardwareKind[] msModelKind = new KnownHardwareKind[] { KnownHardwareKind.SurfacePro, KnownHardwareKind.SurfacePro2, KnownHardwareKind.SurfacePro3 };

        static bool ParseKindCore(HardwareInfo res, string[] model, KnownHardwareKind[] kind)
        {
            int i = Array.IndexOf<string>(model, res.Model);
            if (i < 0) return false;
            res.Kind = kind[i];
            return true;
        }

        static void ParseKindDell(HardwareInfo res) { ParseKindCore(res, dellModel, dellModelKind); }
        static void ParseKindMicrosoft(HardwareInfo res)
        {
            ParseKindCore(res, msModel, msModelKind);
        }


        public class HardwareInfo
        {
            public HardwareInfo()
            {
            }

            public KnownHardwareKind Kind { get; set; }
            public string Manufacturer { get; set; }
            public string Model { get; set; }

            public override string ToString()
            {
                if (Kind == KnownHardwareKind.Unknown)
                {
                    return $"Unknown:{Manufacturer}/{Model}";
                }

                return $"{Kind}: {Manufacturer}/{Model}";
            }
        }

        static HardwareInfo deviceHardwareInfo = null;
        static bool? hasBattery = null;
        static ChassisTypes? chassis = null;
        static bool? hasTouchSupport = null;
        static bool? isWindow8 = null;

        public static bool IsWindow8
        {
            get
            {
                if(isWindow8==null)
                {
                    isWindow8 = DetectWindows8();
                }

                return isWindow8.Value;
            }
        }

        public static HardwareInfo DetectHardwareInfo()
        {
            if (deviceHardwareInfo==null)
            {
                deviceHardwareInfo = ParseHardwareInfo();
            }

            return deviceHardwareInfo;
        }

        static bool DetectWindows8()
        {
            try
            {
                var win8version = new Version(6, 2, 9200, 0);

                if(Environment.OSVersion.Platform==PlatformID.Win32NT && Environment.OSVersion.Version>=win8version)
                {
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        public static bool IsTablet
        {
            get
            {
                if (!IsWindow8)
                {
                    return false;
                }
                if (!HasTouchSupport)
                {
                    return false;
                }

                return HasBattery;
            }
        }

        public static bool IsTableChassis
        {
            get
            {
                if (Chassis==ChassisTypes.Handheld|| Chassis==ChassisTypes.Portable)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool HasTouchSupport
        {
            get
            {
                if (hasTouchSupport==null)
                {
                    hasTouchSupport = CheckTouch();
                }

                return hasTouchSupport.Value;
            }
        }

        static bool CheckTouch()
        {
            var device = System.Windows.Input.Tablet.TabletDevices.Cast<System.Windows.Input.TabletDevice>().FirstOrDefault(dev => dev.Type == System.Windows.Input.TabletDeviceType.Touch);
            if(device == null)
            {
                return false;
            }

            return true;
        }

        public static ChassisTypes Chassis
        {
            get
            {
                if (chassis==null)
                {
                    chassis = GetCurrentChassisType();
                }
                return chassis.Value;
            }
        }

        private static ChassisTypes GetCurrentChassisType()
        {
            try
            {
                var systemEnclosures = new ManagementClass("Win32_SystemEnclosure");
                foreach (ManagementObject obj in systemEnclosures.GetInstances())
                {
                    foreach (int i in (UInt16[])(obj["ChassisTypes"]))
                    {
                        if (i>0 && i<25)
                        {
                            return (ChassisTypes)i;
                        }
                    }
                }
            }
            catch
            {
            }
            return ChassisTypes.Unknow;
        }

        public static bool HasBattery
        {
            get
            {
                if(hasBattery==null)
                {
                    hasBattery = CheckHasBattery();
                }

                return hasBattery.Value;
            }
        }

        static bool CheckHasBattery()
        {
            try
            {
                var query = new ObjectQuery("Select * From Win32_Battery");
                var searcher = new ManagementObjectSearcher(query);

                var collection = searcher.Get();
                return collection.Count > 0;
            }
            catch
            {
            }

            return false;
        }

        static HardwareInfo ParseHardwareInfo()
        {
            HardwareInfo res = new HardwareInfo();
            ParseHardwareInfoCore(res);
            return res;
        }

        static bool ParseHardwareInfoCore(HardwareInfo res)
        {
            try
            {
                var query = new ObjectQuery("Select * FROM Win32_ComputerSystem");
                var searcher = new ManagementObjectSearcher(query);
                var collection = searcher.Get();
                foreach (var c in collection)
                {
                    res.Manufacturer = c["Manufacturer"].ToString();
                    res.Model = c["Model"].ToString();
                }
            }
            catch
            {
                return false;
            }
            ParseKind(res);
            return true;
        }

        static void ParseKind(HardwareInfo res)
        {
            if (res.Manufacturer == "Microsoft Corporation")
            {
                ParseKindMicrosoft(res);
            }
            if (res.Manufacturer == "DellInc.")
            {
                ParseKindDell(res);
            }
        }

        public static bool SuggestHybridDemoParameters(out float touchScale, out float fontSize)
        {
            touchScale = 2f;
            fontSize = 11f;
            var h = DetectHardwareInfo();
            switch (h.Kind)
            {
                case KnownHardwareKind.DellPro8:
                    touchScale = 1.5f;
                    fontSize = 10;
                    return true;
                case KnownHardwareKind.DellPro10:
                case KnownHardwareKind.SurfacePro:
                case KnownHardwareKind.SurfacePro2:
                case KnownHardwareKind.SurfacePro3:
                    touchScale = 2.5f;
                    fontSize = 8.2f;
                    return true;


            }
            if (Screen.PrimaryScreen.WorkingArea.Width < 1500 || Screen.PrimaryScreen.WorkingArea.Height < 800)
            {
                touchScale = 1.5f;
                fontSize = 10;
            }
            return true;

        }
    }
}
