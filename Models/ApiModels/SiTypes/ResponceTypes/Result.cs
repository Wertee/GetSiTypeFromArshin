﻿namespace GetSiTypeFromArshin.Models.ApiModels.SiTypes.ResponceTypes
{
    public class Result
    {
        public int totalCount { get; set; }
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public List<Item> items { get; set; }
    }
}
