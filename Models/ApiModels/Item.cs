using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSiTypeFromArshin.Models.ApiModels
{
    public class Item
    {
        public object values { get; set; }
        public List<Property> properties { get; set; }
        public string id { get; set; }
        public string alfrescoId { get; set; }
        public object nodeRef { get; set; }
        public string type { get; set; }
        public object permissions { get; set; }
    }
}
