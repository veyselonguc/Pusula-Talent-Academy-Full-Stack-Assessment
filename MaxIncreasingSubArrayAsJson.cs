using System.Linq; 
using System.Text.Json; 
using System.Collections.Generic; 

public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
{
    if (numbers == null || numbers.Count == 0)
        return JsonSerializer.Serialize(new List<int>()); // boş liste

    List<int> best = new List<int>();
    int bestSum = int.MinValue;

    List<int> current = new List<int>();
    int currentSum = 0;

    current.Add(numbers[0]);
    currentSum = numbers[0];

    for (int i = 1; i < numbers.Count; i++)
    {
        if (numbers[i] > numbers[i - 1])
        {
            current.Add(numbers[i]);
            currentSum += numbers[i];
        }
        else
        {
            if (currentSum > bestSum)
            {
                best = new List<int>(current);
                bestSum = currentSum;
            }
            current.Clear();
            current.Add(numbers[i]);
            currentSum = numbers[i];
        }
    }

    // son alt diziyi kontrol et
    if (currentSum > bestSum)
    {
        best = new List<int>(current);
        bestSum = currentSum;
    }

    return JsonSerializer.Serialize(best);
}