namespace GetSiTypeFromArshin.Models
{
    public record DataToExcel
    {
        public string? Number { get; set; }
        public string? Name { get; set; }
        public string? TypeSi { get; set; }
        public string? Manufacturer { get; set; }
        public string? CheckPeriod { get; set; }
    }
}
