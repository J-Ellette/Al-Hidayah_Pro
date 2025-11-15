namespace AlHidayahPro.Api.DTOs;

/// <summary>
/// Data transfer object for Quran verse
/// </summary>
public class QuranVerseDto
{
    public int SurahNumber { get; set; }
    public int AyahNumber { get; set; }
    public string ArabicText { get; set; } = string.Empty;
    public string? EnglishTranslation { get; set; }
    public string? Transliteration { get; set; }
    public string? AudioUrl { get; set; }
    public int JuzNumber { get; set; }
    public int PageNumber { get; set; }
}

/// <summary>
/// Data transfer object for Surah information
/// </summary>
public class SurahDto
{
    public int Number { get; set; }
    public string ArabicName { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string? EnglishTranslation { get; set; }
    public int NumberOfAyahs { get; set; }
    public string RevelationType { get; set; } = string.Empty;
}

/// <summary>
/// Search request for Quran verses
/// </summary>
public class QuranSearchRequest
{
    public string Query { get; set; } = string.Empty;
    public bool SearchArabic { get; set; } = true;
    public bool SearchTranslation { get; set; } = true;
    public int? SurahNumber { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// Search results for Quran verses
/// </summary>
public class QuranSearchResults
{
    public List<QuranVerseDto> Results { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
