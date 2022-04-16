namespace Infrastructure.Models;

public class EmailSettings
{
    public string ApiKey { get; set; } = default!;
    public string FromAddress { get; set; } = default!;
    public string FromName { get; set; } = default!;
}