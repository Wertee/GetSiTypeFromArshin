using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSiTypeFromArshin.Models
{
    public class Root
    {
        public int status { get; set; }
        public Result result { get; set; }
        public object message { get; set; }
        public object trace { get; set; }
    }
}
