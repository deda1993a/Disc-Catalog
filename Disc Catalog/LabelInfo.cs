using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disc_Catalog
{
    class LabelInfo
    {
        private string key;
        private int id;
        private string name;
        private string label;
        private string parent;
        private long size;
        private string description;
        private string serial;
        private string type;
        private string filesystem;

        public LabelInfo(string key, int id, string name, string label,
        string parent, long size, string description, string serial, string type, string filesystem)
        {
            this.key = key;
            this.id = id;
            this.name = name;
            this.label = label;
            this.parent = parent;
            this.size = size;
            this.description = description;
            this.serial = serial;
            this.type = type;
            this.filesystem = filesystem;
        }

    }
}
