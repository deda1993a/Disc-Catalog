using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disc_Catalog
{
    class FileInfo
    {
        private string key;
        private ulong id;
        private string name;
        private string parent;
        private string labelInfo;


        public FileInfo(string key, string name, string parent, ulong id, string labelInfo)
        {
            this.key = key;
            this.name = name;
            this.parent = parent;
            this.id = id;
            this.labelInfo = labelInfo;
        }

        public string Key()
        {
            return key;
        }

        public string Parent()
        {
            return parent;
        }

        public string Name()
        {
            return name;
        }

        public ulong Id()
        {
            return id;
        }

        public string LabelInfo()
        {
            return labelInfo;
        }

    }
}
