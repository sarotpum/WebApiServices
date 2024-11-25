namespace SharedService.Models.Breakfasts
{
    public record class BreakfastResponseModel(
        Guid Id,
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        DateTime LastModifiedDateTime,
        List<string> Savory,
        List<string> Sweet);
}
