using DeviceDetectorNET;
using Capex.Models.ResponseModel;
using System.Net;
using System.Net.NetworkInformation;

namespace Capex.Utilities.Common
{
    public static class DeviceInformation
    {
        public static DeviceInfoResponseModel GetDeviceInfo(string userAgent)
        {
            DeviceInfoResponseModel responseModel = new DeviceInfoResponseModel();
            var detector = new DeviceDetector(userAgent);
            detector.SetUserAgent(userAgent);
            detector.Parse();
            responseModel.BrowserName = detector.GetBrowserClient().Match.Name;
            responseModel.BrowserVersion = detector.GetBrowserClient().Match.Version;
            responseModel.DeviceName = detector.GetDeviceName();
            responseModel.OSName = detector.GetOs().Match.Name;
            responseModel.OSVersion = detector.GetOs().Match.Version;
            responseModel.BrandName = detector.GetBrandName();
            responseModel.ModelName = detector.GetModel();
            responseModel.DeviceType = GetDeviceType(userAgent);
            responseModel.IpAddress = GetIPAddress();
            responseModel.MacId = GetMacAddress();
            responseModel.HostName = Dns.GetHostName();
            return responseModel;
        }
        public static string GetMacAddress()
        {

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                //if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet || nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || nic.OperationalStatus==OperationalStatus.Up)
                if (nic.OperationalStatus == OperationalStatus.Up &&
                   nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                   nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                {
                    // Get the physical address (MAC address) of the network interface
                    byte[] macBytes = nic.GetPhysicalAddress().GetAddressBytes();

                    string macAddress = BitConverter.ToString(macBytes);
                    return macAddress;
                }
            }
            return "MAC Address Not Found";
        }
        public static string GetIPAddress()
        {
            string hostName = Dns.GetHostName();
            string IP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return IP;
        }
        public static string GetDeviceType(string userAgent)
        {
            var detector = new DeviceDetector(userAgent);
            detector.SetUserAgent(userAgent);
            detector.Parse();
            if (detector.IsDesktop())
            {
                return "Desktop";
            }
            else if (detector.IsTablet())
            {
                return "Tablet";
            }
            else if (detector.IsMobile())
            {
                return "Mobile";
            }
            else
            {
                return "Unknown";
            }
        }
    }
}
