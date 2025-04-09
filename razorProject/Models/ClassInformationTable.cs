public class ClassInformationTable
// class elements created with the assistance of DeepSeekLLM
{
    public List<ClassInformationModel> Classes { get; set; } = new();
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public string? ClassNameFilter { get; set; }
    public int? StudentCountFilter { get; set; }
}