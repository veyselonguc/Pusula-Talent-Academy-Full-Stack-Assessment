using System; 
using System.Linq; 
using System.Xml.Linq; 
using System.Text.Json; 

public static string FilterPeopleFromXml(string xmlData)
{
    var people = XDocument.Parse(xmlData)
        .Descendants("Person")
        .Select(p => new
        {
            Name = (string)p.Element("Name"),
            Age = (int)p.Element("Age"),
            Department = (string)p.Element("Department"),
            Salary = (decimal)p.Element("Salary"),
            HireDate = DateTime.Parse((string)p.Element("HireDate"))
        })
        .Where(p =>
            p.Age > 30 &&
            p.Department == "IT" &&
            p.Salary > 5000 &&
            p.HireDate < new DateTime(2019, 1, 1)
        )
        .ToList();

    var result = new
    {
        Names = people.Select(p => p.Name).OrderBy(n => n).ToList(),
        TotalSalary = people.Sum(p => (decimal?)p.Salary) ?? 0,
        AverageSalary = people.Any() ? people.Average(p => p.Salary) : 0,
        MaxSalary = people.Any() ? people.Max(p => p.Salary) : 0,
        Count = people.Count
    };

    return JsonSerializer.Serialize(result,
        new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
}