﻿namespace GetSiTypeFromArshin.Models.ApiModels.SiTypes.ResponceTypes
{
    public class Root
    {
        public int status { get; set; }
        public Result result { get; set; }
        public object message { get; set; }
        public object trace { get; set; }
    }
}
