using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disc_Catalog
{
    class FilesInfo
    {
        private string key;
        private ulong id;
        private string name;
        private string parent;
        private string labelInfo;

        private long size;
        private string description;
        private string attr;
        private string crtdate;
        private string mdfdate;
        private string fullpath;


        public FilesInfo(string key, string name, string parent, ulong id, string labelInfo,
            long size, string description, string attr, string crtdate,
            string mdfdate, string fullpath)
        {
            this.key = key;
            this.name = name;
            this.parent = parent;
            this.id = id;
            this.labelInfo = labelInfo;
            this.size = size;
            this.description = description;
            this.attr = attr;
            this.crtdate = crtdate;
            this.mdfdate = mdfdate;
            this.fullpath = fullpath;
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

        public long Size()
        {
            return size;
        }
        public string Description()
        {
            return description;
        }

        public string Attr()
        {
            return attr;
        }

        public string Crtdate()
        {
            return crtdate;
        }
        public string Mdfdate()
        {
            return mdfdate;
        }
        public string Fullpath()
        {
            return fullpath;
        }

    }
}
