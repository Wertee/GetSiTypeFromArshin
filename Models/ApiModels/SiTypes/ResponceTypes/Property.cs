﻿namespace GetSiTypeFromArshin.Models.ApiModels.SiTypes.ResponceTypes
{
    public class Property
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool multiple { get; set; }
        public string title { get; set; }
        public string constraint { get; set; }
        public object value { get; set; }
        public string longValue { get; set; }
        public object link { get; set; }
        public string mime { get; set; }
    }
}
