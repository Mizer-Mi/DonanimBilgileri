using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;

namespace MizerDonanimSessizClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Environment.Exit(0);
        }

        private static string BYTEMIZERGB(long bytes)
        {
            string[] Suffix = { "", "", "", "", "" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
        private static string MHZMIZERGHZ(long bytes)
        {
            string[] Suffix = { "", "", "", "", "" };
            int i;
            Double GONDERME = (Double)bytes / (Double)1000;

            return String.Format("{0:0.##} {1}", GONDERME, "");
        }






        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {

           
            #region İŞLEMCİ
            string clockSpeed = "";
            string procName = "";
            string manufacturer = "";
            string version = "";
            using (ManagementObjectSearcher win32Proc = new ManagementObjectSearcher("select * from Win32_Processor"),
    win32CompSys = new ManagementObjectSearcher("select * from Win32_ComputerSystem"),
        win32Memory = new ManagementObjectSearcher("select * from Win32_PhysicalMemory"))
            {
                foreach (ManagementObject obj in win32Proc.Get())
                {
                    clockSpeed = obj["CurrentClockSpeed"].ToString();
                    procName = obj["Name"].ToString();
                    manufacturer = obj["Manufacturer"].ToString();
                    version = obj["Version"].ToString();
                }

            }
            #endregion
            #region EKRAN KARTI
            String EkranKartiModeli = "";
            string EkranKartiBoyut = "";
            ManagementObjectSearcher objvide = new ManagementObjectSearcher("select * from Win32_VideoController");

            foreach (ManagementObject obj in objvide.Get())
            {
                EkranKartiModeli = obj["Name"].ToString().Trim();
                EkranKartiBoyut = obj["AdapterRAM"].ToString().Trim();
            }
            EkranKartiModel.Text = EkranKartiModeli;
            EkranKartiHiz.Text = (Math.Round(Convert.ToDouble(BYTEMIZERGB(Convert.ToInt64(EkranKartiBoyut)).Trim()), 1, MidpointRounding.AwayFromZero).ToString()) + " GB";

            #endregion
            #region Ram 
            String Ram_Bilgileri = "";

            string RAM_Model = "";
            string RAM_HIZI = "";
            Boolean devam = true;
            Boolean devam2 = true;
            string Ram_DDR_Nedir = "Bilinmiyor";
            Ram_DDR_Nedir = MotherboardInfo.RamType;

            if (Ram_DDR_Nedir.Contains("Bilinmiyor"))
            {
                devam2 = true;
            }
            else
            {
                devam2 = false;
            }


            ConnectionOptions connection = new ConnectionOptions();
            connection.Impersonation = ImpersonationLevel.Impersonate;

            ManagementScope scope = new ManagementScope("\\root\\CIMV2", connection);
            scope.Connect();

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            string test = "";
            foreach (ManagementObject queryObj in searcher.Get())
            {
                test = test + "-----------------------------------";
                foreach (PropertyData data in queryObj.Properties)
                {
                    test = test + "\n" + data.Name + "\t" + data.Value;
                    if (data.Name.Trim() == "PartNumber".Trim() && devam)
                    {
                        RAM_Model = data.Value.ToString().Trim();
                        devam = false;


                    }
                    else if (data.Name.Trim() == "Speed".Trim() && devam2)
                    {
                        RAM_HIZI = data.Value.ToString().Trim();
                        devam2 = false;

                    }


                }


            }

            if (Ram_DDR_Nedir.Contains("Bilinmiyor"))
            {
                Ram_Bilgileri = RAM_Model + " - " + RAM_HIZI + " MHz";
            }
            else
            {
                Ram_Bilgileri = RAM_Model + " - " + Ram_DDR_Nedir;
            }





            #endregion
            #region Anakart 
            string AnakartUretici = "";
            if (MotherboardInfo.Manufacturer.Contains("Star International"))
            {
                AnakartUretici = "MSI";
            }
            else
            {
                AnakartUretici = MotherboardInfo.Manufacturer;
            }
            AnakartModelTXT.Text = AnakartUretici.Trim() + " - " + MotherboardInfo.Product.Trim();



            #endregion
            #region HDD
            string HDDMarka = "";
            long HDDBOYUTCEVIRICI = 0;
            WqlObjectQuery q = new WqlObjectQuery("SELECT * FROM Win32_DiskDrive");
            ManagementObjectSearcher res = new ManagementObjectSearcher(q);
            foreach (ManagementObject o in res.Get())
            {

                HDDMarka = o["Model"].ToString().Trim();
                break;


            }
            HDDMARKATXT.Text = HDDMarka;
            foreach (System.IO.DriveInfo label in System.IO.DriveInfo.GetDrives())
            {

                if (label.IsReady)
                {
                    HDDBOYUTCEVIRICI = Convert.ToInt64(label.TotalSize.ToString().Trim());
                    break;
                }
            }



            #endregion


            Computer MizerPc = new Computer();

            string RAM = MizerPc.Info.TotalPhysicalMemory.ToString();
            string OS = MizerPc.Info.OSFullName;
            string CPU = procName.Trim();
            string CPU_HIZ = (Math.Round(Convert.ToDouble(MHZMIZERGHZ(Convert.ToInt64(clockSpeed))), 2, MidpointRounding.AwayFromZero)).ToString() + " GHz";
            CPUTXT.Text = CPU;
            CPUHIZTXT.Text = CPU_HIZ;

            Isletim_SistemiTXT.Text = OS;
            RAMmarkaTXT.Text = Ram_Bilgileri;
            RAM_BoyutTXT.Text = (Math.Round(Convert.ToDouble(BYTEMIZERGB(Convert.ToInt64(RAM)).Trim()), 1, MidpointRounding.AwayFromZero).ToString()) + " GB";
            HDDBOYUTTXT.Text = (Math.Round(Convert.ToDouble(BYTEMIZERGB(Convert.ToInt64(HDDBOYUTCEVIRICI)).Trim()), 0, MidpointRounding.AwayFromZero).ToString()) + " GB";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

    }

}
