﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Management;



static public class MotherboardInfo
{


    public static string RamType
    {
        get
        {
            int type = 0;

            ConnectionOptions connection = new ConnectionOptions();
            connection.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2", connection);
            scope.Connect();
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            foreach (ManagementObject queryObj in searcher.Get())
            {
                type = Convert.ToInt32(queryObj["MemoryType"]);
            }

            return TypeString(type);
        }
    }

    private static string TypeString(int type)
    {
        string outValue = string.Empty;

        switch (type)
        {
            case 0x0: outValue = "Bilinmiyor"; break;
            case 0x1: outValue = "Bilinmiyor"; break;
            case 0x2: outValue = "DRAM"; break;
            case 0x3: outValue = "Synchronous DRAM"; break;
            case 0x4: outValue = "Cache DRAM"; break;
            case 0x5: outValue = "EDO"; break;
            case 0x6: outValue = "EDRAM"; break;
            case 0x7: outValue = "VRAM"; break;
            case 0x8: outValue = "SRAM"; break;
            case 0x9: outValue = "RAM"; break;
            case 0xa: outValue = "ROM"; break;
            case 0xb: outValue = "Flash"; break;
            case 0xc: outValue = "EEPROM"; break;
            case 0xd: outValue = "FEPROM"; break;
            case 0xe: outValue = "EPROM"; break;
            case 0xf: outValue = "CDRAM"; break;
            case 0x10: outValue = "3DRAM"; break;
            case 0x11: outValue = "SDRAM"; break;
            case 0x12: outValue = "SGRAM"; break;
            case 0x13: outValue = "RDRAM"; break;
            case 0x14: outValue = "DDR"; break;
            case 0x15: outValue = "DDR2"; break;
            case 0x16: outValue = "DDR2 FB-DIMM"; break;
            case 0x17: outValue = "Undefined 23"; break;
            case 0x18: outValue = "DDR3"; break;
            case 0x19: outValue = "FBD2"; break;
            case 0x1a: outValue = "DDR4"; break;
            default: outValue = "Bilinmiyor"; break;
        }

        return outValue;
    }


    private static ManagementObjectSearcher baseboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
    private static ManagementObjectSearcher motherboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice");

    static public string Availability
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in motherboardSearcher.Get())
                {
                    return GetAvailability(int.Parse(queryObj["Availability"].ToString()));
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public bool HostingBoard
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    if (queryObj["HostingBoard"].ToString() == "True")
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    static public string InstallDate
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    return ConvertToDateTime(queryObj["InstallDate"].ToString());
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string Manufacturer
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    return queryObj["Manufacturer"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string Model
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    return queryObj["Model"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string PartNumber
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    return queryObj["PartNumber"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string PNPDeviceID
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in motherboardSearcher.Get())
                {
                    return queryObj["PNPDeviceID"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string PrimaryBusType
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in motherboardSearcher.Get())
                {
                    return queryObj["PrimaryBusType"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string Product
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    return queryObj["Product"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public bool Removable
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    if (queryObj["Removable"].ToString() == "True")
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    static public bool Replaceable
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    if (queryObj["Replaceable"].ToString() == "True")
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    static public string RevisionNumber
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in motherboardSearcher.Get())
                {
                    return queryObj["RevisionNumber"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string SecondaryBusType
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in motherboardSearcher.Get())
                {
                    return queryObj["SecondaryBusType"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string SerialNumber
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    return queryObj["SerialNumber"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string Status
    {
        get
        {
            try
            {
                foreach (ManagementObject querObj in baseboardSearcher.Get())
                {
                    return querObj["Status"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string SystemName
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in motherboardSearcher.Get())
                {
                    return queryObj["SystemName"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    static public string Version
    {
        get
        {
            try
            {
                foreach (ManagementObject queryObj in baseboardSearcher.Get())
                {
                    return queryObj["Version"].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }

    private static string GetAvailability(int availability)
    {
        switch (availability)
        {
            case 1: return "Other";
            case 2: return "Unknown";
            case 3: return "Running or Full Power";
            case 4: return "Warning";
            case 5: return "In Test";
            case 6: return "Not Applicable";
            case 7: return "Power Off";
            case 8: return "Off Line";
            case 9: return "Off Duty";
            case 10: return "Degraded";
            case 11: return "Not Installed";
            case 12: return "Install Error";
            case 13: return "Power Save - Unknown";
            case 14: return "Power Save - Low Power Mode";
            case 15: return "Power Save - Standby";
            case 16: return "Power Cycle";
            case 17: return "Power Save - Warning";
            default: return "Unknown";
        }
    }

    private static string ConvertToDateTime(string unconvertedTime)
    {
        string convertedTime = "";
        int year = int.Parse(unconvertedTime.Substring(0, 4));
        int month = int.Parse(unconvertedTime.Substring(4, 2));
        int date = int.Parse(unconvertedTime.Substring(6, 2));
        int hours = int.Parse(unconvertedTime.Substring(8, 2));
        int minutes = int.Parse(unconvertedTime.Substring(10, 2));
        int seconds = int.Parse(unconvertedTime.Substring(12, 2));
        string meridian = "AM";
        if (hours > 12)
        {
            hours -= 12;
            meridian = "PM";
        }
        convertedTime = date.ToString() + "/" + month.ToString() + "/" + year.ToString() + " " +
        hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString() + " " + meridian;
        return convertedTime;
    }
}