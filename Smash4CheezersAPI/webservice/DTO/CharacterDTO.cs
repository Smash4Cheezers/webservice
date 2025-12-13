namespace webservice.DTO;

public class CharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string WeightCategory { get; set; }
    public int Weight { get; set; }
    public SerieDTO Serie { get; set; }
}