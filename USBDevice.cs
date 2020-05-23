using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryReader
{
    public class USBDevice
    {
        private const string DATE_FORMAT = "dd-MMM-yy h:mm:ss tt zzz";
            
        public string Serial { get; private set; }
        public string FriendlyName { get; set; }

        public DateTime FirstInsertion { get; private set; }
        public DateTime LastInsertion { get; private set; }
        public DateTime LastRemoval { get; private set; }

        public string Vendor { get; private set; }
        public string Product { get; private set; }
        public string DriveLetter { get; private set; }
        public string DeviceGUID { get; private set; }

        public void SetFirstInsertion(string firstInsertion)
        {
            FirstInsertion = ConvertToLocalTime(firstInsertion);
        }
        public void SetLastInsertion(string lastInsertion)
        {
            LastInsertion = ConvertToLocalTime(lastInsertion);
        }
        public void SetLastRemoval(string lastRemoval)
        {
            LastRemoval = ConvertToLocalTime(lastRemoval);
        }

        public void SetDescription(string description)
        {
            if(description != null)
            {
                string[] split = description.Split('&');

                if(split.Length > 1)
                {
                    Vendor = split[1].Split('_')[1];
                    Product = split[2].Split('_')[1];
                }
            }
        }
        public void SetSerial(string serial)
        {
            if(serial != null)
            {
                if (!serial.StartsWith("6&"))
                {
                    string[] split = serial.Split('&');

                    if(split.Length > 1)
                    {
                        Serial = split[split.Length - 2];
                    }
                }
                else
                {
                    Serial = serial;
                }
            }
        }
        public void SetDriveLetter(string driveString)
        {
            if(driveString != null)
            {
                var split = driveString.Split('\\');

                if(split.Length > 1)
                {
                    DriveLetter = split[split.Length - 1];
                }
            }
        }
        public void SetDeviceGUID(string guidString)
        {
            if(guidString != null)
            {
                var split = guidString.Split('{');

                if(split.Length > 1)
                {
                    DeviceGUID = split[1].TrimEnd('}');
                }
            }
        }


        /// <summary>
        /// Converts UTC time stored in the registry to local time using DateTime.ParseExact
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        private DateTime ConvertToLocalTime(string utcTime)
        {
            return utcTime == null ? DateTime.MinValue : DateTime.ParseExact(utcTime, DATE_FORMAT, CultureInfo.InvariantCulture);
        }
    }
}
