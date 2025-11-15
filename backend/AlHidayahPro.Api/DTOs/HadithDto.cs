namespace AlHidayahPro.Api.DTOs;

/// <summary>
/// Data transfer object for Hadith
/// </summary>
public class HadithDto
{
    public string Collection { get; set; } = string.Empty;
    public string Book { get; set; } = string.Empty;
    public int BookNumber { get; set; }
    public string HadithNumber { get; set; } = string.Empty;
    public string? ArabicText { get; set; }
    public string EnglishText { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string? Narrator { get; set; }
    public string? Chapter { get; set; }
}

/// <summary>
/// Search request for Hadith
/// </summary>
public class HadithSearchRequest
{
    public string Query { get; set; } = string.Empty;
    public string? Collection { get; set; }
    public string? Grade { get; set; }
    public bool SearchArabic { get; set; } = false;
    public bool SearchEnglish { get; set; } = true;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// Search results for Hadith
/// </summary>
public class HadithSearchResults
{
    public List<HadithDto> Results { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
