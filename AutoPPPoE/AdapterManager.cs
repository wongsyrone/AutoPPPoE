using DotRas;
using System.Collections.Generic;
using System.Management;

namespace AutoPPPoE
{
    public class AdapterManager
    {
        public IList<string> adapterName { get; private set; }

        public IList<string> rasName { get; private set; }

        public AdapterManager()
        {
            adapterName = new List<string>();
            rasName     = new List<string>();
            loadAdapter();
            loadRAS(RasPhoneBookType.AllUsers);
            loadRAS(RasPhoneBookType.User);
        }

        private void loadAdapter()
        {
            ManagementScope scope       = new ManagementScope();
            ObjectQuery     objectQuery = new ObjectQuery("SELECT * FROM Win32_NetworkAdapter");
            using (ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher(scope, objectQuery))
            using (ManagementObjectCollection objectCollect = objectSearcher.Get())
            {
                foreach (ManagementObject result in objectCollect)
                {
                    var nicNetConnId = result.Properties["NetConnectionID"].Value;
                    var name         = result.Properties["Name"].Value;
                    if (nicNetConnId != null)
                    {
                        adapterName.Add($"{name}${nicNetConnId}");
                    }
                }
            }
        }

        private void loadRAS(RasPhoneBookType type)
        {
            string phoneBookPath = RasPhoneBook.GetPhoneBookPath(type);
            using (RasPhoneBook phoneBook = new RasPhoneBook())
            {
                phoneBook.Open(phoneBookPath);
                foreach (RasEntry entry in phoneBook.Entries)
                {
                    rasName.Add(entry.Name);
                }
            }
        }
    }
}