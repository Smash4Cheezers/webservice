using Microsoft.AspNetCore.Mvc;

namespace smash4cheezersWebservice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    public string[] characters = new[]
    {
        "Captain Falcon", "Ouisticram", "Lebron James"
    };

    [HttpGet(Name = "GetCharacters")]
    public IEnumerable<Characters> GetCharacters()
    {
        return Enumerable.Range(1, 5).Select(index => new Characters()
            {
                Character = characters
            })
            .ToArray();
    }
}