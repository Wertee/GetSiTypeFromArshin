namespace GetSiTypeFromArshin.Models.ApiModels.Etalons.ResponceEtalon;

public class Response
{
    public int numFound { get; set; }
    public int start { get; set; }
    public bool numFoundExact { get; set; }
    public List<Doc> docs { get; set; }
}