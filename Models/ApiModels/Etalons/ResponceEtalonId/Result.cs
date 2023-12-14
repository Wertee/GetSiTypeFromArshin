namespace GetSiTypeFromArshin.Models.ApiModels.Etalons.ResponceEtalonId;

public class Result
{
    public int count { get; set; }
    public int start { get; set; }
    public int rows { get; set; }
    public List<Item> items { get; set; }
}