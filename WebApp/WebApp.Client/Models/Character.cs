namespace WebApp.Client.Models;

public class Character
{
    public string? Id { get; set; }

    public string CharacterName { get; set; } = null!;

    public string AnimeTitel { get; set; }

    public string ImageUrl { get; set; } = null!;
}