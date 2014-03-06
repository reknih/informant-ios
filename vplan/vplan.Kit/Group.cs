using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vplan
{
	public class Group
    {
        public Group() { }
        public Group(int id, string name) {
            ID = id;
            ClassName = name;
        }

        public string ClassName { get; set; }
        public override string ToString()
        {
            return ClassName;
        }
        public int ID { get; set; }
    }
}
