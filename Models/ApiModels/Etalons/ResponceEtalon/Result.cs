namespace GetSiTypeFromArshin.Models.ApiModels.Etalons.ResponceEtalon;

public class Result
{
    public string number { get; set; }
    public string mitype_num { get; set; }
    public string mitype { get; set; }
    public string minotation { get; set; }
    public string modification { get; set; }
    public string factory_num { get; set; }
    public int year { get; set; }
    public string schematype { get; set; }
    public string schematitle { get; set; }
    public string npenumber { get; set; }
    public string rankcode { get; set; }
    public string rankclass { get; set; }
    public bool applicability { get; set; }
    public List<Cresult> cresults { get; set; }
}