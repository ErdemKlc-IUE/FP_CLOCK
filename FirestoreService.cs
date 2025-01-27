using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;


namespace FP_CLOCK
{
    public class FirestoreService
    {
        public readonly FirestoreDb _firestoreDb;
        public int counter = 0;

        // Constructor now accepts a reference to Form1
        public FirestoreService(string credentialsPath)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\EnGoPer\Data\fp-clock-firebase-adminsdk-ebft1-4f832f8f70.json");
            _firestoreDb = FirestoreDb.Create("fp-clock");
        }

        public async Task<bool> LoginUser(string password)
        {
            try
            {
                string filePath = Path.Combine("C:\\EnGoPer\\Kontrol", "user_data.txt");

                string companyName = null;

                // Dosyadan Company Name'i oku
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath).ToList();
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("Company:"))
                        {
                            companyName = line.Split(':')[1].Trim(); // Şirket ismini al
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(companyName))
                {
                    Console.WriteLine("Company name bulunamadı.");
                    return false;
                }

                // Firestore'da companyName ID'sine sahip bir belge var mı kontrol et
                var docRef = _firestoreDb.Collection("login").Document(companyName);
                var docSnapshot = await docRef.GetSnapshotAsync();

                if (!docSnapshot.Exists)
                {
                    Console.WriteLine($"Database'de '{companyName}' isimli bir belge bulunamadı.");
                    return false;
                }

                Console.WriteLine($"Document with ID '{companyName}' exists.");

                // Kullanıcının Firestore'daki bilgilerini al
                string storedPassword = docSnapshot.GetValue<string>("password");
                string deviceID = GetDeviceId();
                string storedDeviceID = docSnapshot.GetValue<string>("device");


                // Kullanıcı bilgilerini doğrula
                if (storedPassword != password)
                {
                    Console.WriteLine("Kullanıcı adı veya şifre hatalı.");
                    return false;
                }
                if (storedDeviceID != deviceID)
                {
                    Console.WriteLine("Cihaz bilgileri uyuşmuyor.");
                    MessageBox.Show("Cihaz bilgileri uyuşmuyor.");
                    return false;
                }
                int successfulLoginCounter = docSnapshot.GetValue<int>("loginCount");
                bool approved = docSnapshot.GetValue<bool>("approved"); // Kullanıcı onay durumu

                // Kullanıcı onaylı mı kontrol et
                if (!approved && successfulLoginCounter >= 3)
                {
                    MessageBox.Show("Kullanıcı onaylanmamış. Giriş izni verilmez. Lütfen satıcınız ile görüşünüz!",
                                    "Giriş Hakkınız Bitmiştir",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return false;
                }

                Console.WriteLine("Giriş başarılı!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
        }
        static string GetDeviceId()
        {
            string cpuId = GetCpuId();
            string diskId = GetDiskId();
            string macAddress = GetMacAddress();

            string uniqueString = cpuId + diskId + macAddress;
            return ComputeSha256Hash(uniqueString);
        }

        static string GetCpuId()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["ProcessorId"]?.ToString() ?? "UnknownCPU";
                    }
                }
            }
            catch { }
            return "UnknownCPU";
        }

        static string GetDiskId()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_DiskDrive"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["SerialNumber"]?.ToString().Trim() ?? "UnknownDisk";
                    }
                }
            }
            catch { }
            return "UnknownDisk";
        }

        static string GetMacAddress()
        {
            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    {
                        return nic.GetPhysicalAddress().ToString();
                    }
                }
            }
            catch { }
            return "UnknownMAC";
        }

        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
