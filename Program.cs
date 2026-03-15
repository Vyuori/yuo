using System;

while (true)
{
    Console.Write("Введите первую строку (или 'exit' для выхода): ");
    string first = Console.ReadLine();
    if (first?.ToLower() == "exit") break;
    
    Console.Write("Введите вторую строку: ");
    string second = Console.ReadLine();
    
    int distance = DamerauLevenshtein.Distance(first, second);
    Console.WriteLine($"Расстояние: {distance}");
}

public static class DamerauLevenshtein
{
    public static int Distance(string s1, string s2)
    {
        if (s1 == null || s2 == null) return -1;
        s1 = s1.ToUpperInvariant();
        s2 = s2.ToUpperInvariant();
        int len1 = s1.Length, len2 = s2.Length;
        if (len1 == 0) return len2;
        if (len2 == 0) return len1;

        int[,] matrix = new int[len1 + 1, len2 + 1];
        for (int i = 0; i <= len1; i++) matrix[i, 0] = i;
        for (int j = 0; j <= len2; j++) matrix[0, j] = j;

        for (int i = 1; i <= len1; i++)
        {
            for (int j = 1; j <= len2; j++)
            {
                int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
                int insert = matrix[i, j - 1] + 1;
                int delete = matrix[i - 1, j] + 1;
                int replace = matrix[i - 1, j - 1] + cost;
                int min = Math.Min(Math.Min(insert, delete), replace);

                if (i > 1 && j > 1 && s1[i - 1] == s2[j - 2] && s1[i - 2] == s2[j - 1])
                {
                    int transpose = matrix[i - 2, j - 2] + cost;
                    min = Math.Min(min, transpose);
                }
                matrix[i, j] = min;
            }
        }
        return matrix[len1, len2];
    }
}