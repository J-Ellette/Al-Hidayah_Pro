# Islamic Data Population

This directory contains sample Islamic data for Al-Hidayah Pro, including support for the module system.

## Data Files

### Quran Data
- `quran-sample.json` - Sample data containing Surah Al-Fatihah (7 verses)
- `translation-sample.json` - Sample Quran translation (Surah Al-Fatihah)

For full Quran data, you can use these trusted sources:
- https://api.alquran.cloud/v1/quran/en.asad (English translation)
- https://api.alquran.cloud/v1/quran/quran-uthmani (Arabic Uthmani script)
- http://api.quran.com/ (Quran.com API)

### Hadith Data
- `hadith-sample.json` - Sample hadiths from Sahih Bukhari

For full Hadith collections, use:
- https://sunnah.com/ (Comprehensive hadith database)
- https://ahadith.co.uk/ (Multiple collections with authenticity grading)
- https://hadithapi.com/ (RESTful hadith API)

### Books
- `book-sample.json` - Sample Islamic book (The Sealed Nectar - Biography of the Prophet)

For Islamic books:
- https://archive.org/ (Internet Archive - Public domain Islamic books)
- https://islamicstudies.info/ (Free Islamic texts)

## Data Import Process

### Option 1: Using Admin API Endpoint
Once the backend is running, you can import data via HTTP POST:

```bash
# Import Quran data
curl -X POST http://localhost:5000/api/admin/import/quran \
  -H "Content-Type: application/json" \
  -d '{"FilePath":"../AlHidayahPro.Data/SampleData/quran-sample.json"}'

# Import Hadith data
curl -X POST http://localhost:5000/api/admin/import/hadith \
  -H "Content-Type: application/json" \
  -d '{"FilePath":"../AlHidayahPro.Data/SampleData/hadith-sample.json","Collection":"Sahih Bukhari"}'

# Import Quran Translation (Module System)
curl -X POST http://localhost:5000/api/module/import/translation \
  -H "Content-Type: application/json" \
  -d '{"FilePath":"../AlHidayahPro.Data/SampleData/translation-sample.json","ModuleId":"quran-translation-en-sahih","Language":"en","Translator":"Sahih International"}'

# Import Book (Module System)
curl -X POST http://localhost:5000/api/module/import/book \
  -H "Content-Type: application/json" \
  -d '{"FilePath":"../AlHidayahPro.Data/SampleData/book-sample.json","ModuleId":"book-sealed-nectar"}'
```

### Option 2: Using DbSeeder
The `DbSeeder` class can be used to populate sample data during development:

```csharp
// In Program.cs or a setup script
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.SeedAsync();
}
```

### Option 3: Manual Database Population
You can also manually populate the database using SQL scripts or Entity Framework migrations.

## Data Structure

### Quran Verse Format
```json
{
  "Number": 1,
  "ArabicName": "الفاتحة",
  "EnglishName": "Al-Fatihah",
  "EnglishTranslation": "The Opening",
  "NumberOfAyahs": 7,
  "RevelationType": "Meccan",
  "Verses": [
    {
      "AyahNumber": 1,
      "ArabicText": "بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ",
      "EnglishTranslation": "In the name of Allah...",
      "Transliteration": "Bismillahi ar-rahmani ar-rahim",
      "JuzNumber": 1,
      "PageNumber": 1
    }
  ]
}
```

### Hadith Format
```json
{
  "Book": "Book of Faith",
  "BookNumber": 1,
  "HadithNumber": "1",
  "ArabicText": "Arabic hadith text",
  "EnglishText": "English translation",
  "Grade": "Sahih",
  "Narrator": "Ibn Umar",
  "Chapter": "Chapter name"
}
```

## Important Notes

1. **Data Authenticity**: Always use verified and authenticated Islamic sources
2. **Arabic Text**: Ensure proper Unicode encoding for Arabic text
3. **Translations**: Multiple translations can be supported by extending the data model
4. **Audio URLs**: Audio recitation URLs can be added to the AudioRecitations table separately
5. **Database Size**: The full Quran is ~6,236 verses across 114 Surahs
6. **Hadith Collections**: Major collections include Sahih Bukhari (~7,000 hadiths), Sahih Muslim (~7,500), and others

## Recommended Full Data Sources

For production deployment, consider using:
- **Tanzil Project**: http://tanzil.net/docs/download (Various Quran texts)
- **Quran.com**: API with translations, audio, and tafsir
- **Sunnah.com**: Comprehensive hadith collections with chains of narration
- **IslamicFinder**: Prayer times and Qibla direction data
