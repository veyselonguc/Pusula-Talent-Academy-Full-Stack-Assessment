using System.Linq; 
using System.Text.Json; 
using System.Collections.Generic; 

public static string LongestVowelSubsequenceAsJson(List<string> words)
{
    var options = new JsonSerializerOptions
    {
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    return JsonSerializer.Serialize(
        words.Select(word =>
        {
            var matches = Regex.Matches(word, "[aeiouAEIOU]+")
                                .Select(m => m.Value);

            var longest = matches.OrderByDescending(s => s.Length)
                                    .FirstOrDefault() ?? "";

            return new
            {
                word,
                sequence = longest,
                length = longest.Length
            };
        }).ToList(),
        options
    );
}