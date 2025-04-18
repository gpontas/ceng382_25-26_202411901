using System.Text.Json;

namespace razorProject.Helpers;

public sealed class Utils
{
    private static readonly Utils _instance = new();
    public static Utils Instance => _instance;
    private Utils() { }

    public string ExportToJson<T>(List<T> data, List<string> selectedColumns)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

            var allColumns = typeof(T).GetProperties()
        .Where(p => p.Name != "Id")
        .Select(p => p.Name)
        .ToList();

    if (selectedColumns.Count == 0 || !selectedColumns.Any(c => allColumns.Contains(c)))
    {
        return JsonSerializer.Serialize(data.Select(item => 
        {
            var obj = new Dictionary<string, object?>();
            foreach (var prop in allColumns)
            {
                var value = item?.GetType().GetProperty(prop)?.GetValue(item);
                obj.Add(prop, value);
            }
            return obj;
        }), options);
    }
        
        // Fix: Return all data if no columns selected
        if (selectedColumns.Count == 0)
            return JsonSerializer.Serialize(data, options);

        // Filter selected columns
        var filteredData = data.Select(item =>
        {
            var obj = new Dictionary<string, object?>();
            foreach (var prop in selectedColumns)
            {
                var value = item?.GetType().GetProperty(prop)?.GetValue(item);
                obj.Add(prop, value);
            }
            return obj;
        });

        return JsonSerializer.Serialize(filteredData, options);
    }
}