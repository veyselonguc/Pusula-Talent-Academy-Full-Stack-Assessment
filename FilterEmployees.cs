using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text.Json;

public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
{
    var filtered = employees
    .Where(e => e.Age is >= 25 and <= 40
                && (e.Department == "IT" || e.Department == "Finance")
                && e.Salary is >= 5000 and <= 9000
                && e.HireDate > new DateTime(2017, 1, 1))
    .ToList();

    var result = new
    {
        Names = filtered.Select(e => e.Name)
                        .OrderByDescending(n => n.Length)
                        .ThenBy(n => n)
                        .ToList(),
        TotalSalary = filtered.Sum(e => e.Salary),
        AverageSalary = filtered.Any() ? Math.Round(filtered.Average(e => e.Salary), 2) : 0,
        MinSalary = filtered.Any() ? filtered.Min(e => e.Salary) : 0,
        MaxSalary = filtered.Any() ? filtered.Max(e => e.Salary) : 0,
        Count = filtered.Count
    };

    var options = new JsonSerializerOptions
    {
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    return JsonSerializer.Serialize(result, options);
}