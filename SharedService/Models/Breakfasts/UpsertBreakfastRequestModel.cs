namespace SharedService.Models.Breakfasts
{
    public record UpsertBreakfastRequestModel(
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        List<string> Savory,
        List<string> Sweet);
}
