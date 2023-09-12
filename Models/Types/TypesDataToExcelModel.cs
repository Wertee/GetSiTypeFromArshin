namespace GetSiTypeFromArshin.Models.Types
{
    public record TypesDataToExcelModel
    {
        public string? Number { get; set; }
        public string? Name { get; set; }
        public string? TypeSi { get; set; }
        public string? Manufacturer { get; set; }
        public string? CheckPeriod { get; set; }
    }
}
