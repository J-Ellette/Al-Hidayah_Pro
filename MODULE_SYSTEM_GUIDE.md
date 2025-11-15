# Module System Guide - Al-Hidayah Pro

Complete guide to the module system for importing and managing Islamic content.

## Overview

The module system provides a flexible way to import and manage various types of Islamic content:
- **Quran Translations** - Multiple languages with footnotes
- **Hadith Collections** - Authenticated narrations
- **Islamic Books** - With chapter organization
- **Tafsir** - Quranic commentary
- **Audio Recitations** - Multiple reciters
- **Commentary** - Scholarly notes
- **Dictionary** - Islamic terms

## Architecture

### Database Models

```
ContentModule (Base)
├── ModuleId (unique identifier)
├── Name (display name)
├── Type (QuranTranslation, Book, Hadith, etc.)
├── Language
├── Author/Translator
├── Version
├── IsInstalled (status)
└── Metadata (JSON)

QuranTranslation
├── ModuleId (FK to ContentModule)
├── SurahNumber
├── AyahNumber
├── Translation
├── Language
├── Translator
└── Footnotes

Book
├── ModuleId (FK to ContentModule)
├── Title
├── Author
├── Category
├── Language
└── Chapters[]

BookChapter
├── BookId (FK to Book)
├── ChapterNumber
├── Title
├── Content
└── Summary
```

## API Reference

### Module Management

#### Get All Modules
```http
GET /api/module
```

Response:
```json
[
  {
    "id": 1,
    "moduleId": "quran-translation-en-sahih",
    "name": "Sahih International Translation (en)",
    "type": 1,
    "language": "en",
    "author": "Sahih International",
    "isInstalled": true,
    "installedDate": "2024-01-01T00:00:00Z"
  }
]
```

#### Get Installed Modules
```http
GET /api/module/installed
```

#### Get Modules by Type
```http
GET /api/module/type/{type}
```

Types:
- `1` = QuranTranslation
- `2` = HadithCollection
- `3` = Book
- `4` = Tafsir
- `5` = Audio
- `6` = Commentary
- `7` = Dictionary

#### Get Specific Module
```http
GET /api/module/{moduleId}
```

#### Register New Module
```http
POST /api/module
Content-Type: application/json

{
  "moduleId": "quran-translation-fr-hamidullah",
  "name": "Hamidullah Translation (French)",
  "type": 1,
  "language": "fr",
  "author": "Muhammad Hamidullah",
  "description": "French translation of the Quran",
  "version": "1.0.0",
  "sourceUrl": "https://example.com/translation.json",
  "fileSize": 1048576,
  "license": "Public Domain"
}
```

### Content Import

#### Import Quran Translation
```http
POST /api/module/import/translation
Content-Type: application/json

{
  "filePath": "../AlHidayahPro.Data/SampleData/translation-sample.json",
  "moduleId": "quran-translation-en-sahih",
  "language": "en",
  "translator": "Sahih International"
}
```

**Translation JSON Format:**
```json
[
  {
    "SurahNumber": 1,
    "AyahNumber": 1,
    "Translation": "In the name of Allah, the Most Gracious, the Most Merciful.",
    "Footnotes": null
  }
]
```

#### Import Islamic Book
```http
POST /api/module/import/book
Content-Type: application/json

{
  "filePath": "../AlHidayahPro.Data/SampleData/book-sample.json",
  "moduleId": "book-sealed-nectar"
}
```

**Book JSON Format:**
```json
{
  "Title": "The Sealed Nectar",
  "Author": "Safiur-Rahman al-Mubarakpuri",
  "Category": "Seerah",
  "Language": "en",
  "Description": "Biography of Prophet Muhammad",
  "ISBN": "978-9960899558",
  "Publisher": "Darussalam",
  "PublicationYear": 1979,
  "PageCount": 627,
  "License": "Public Domain",
  "Chapters": [
    {
      "ChapterNumber": 1,
      "Title": "Introduction",
      "Content": "Chapter content here...",
      "Summary": "Brief summary",
      "PageNumber": 1
    }
  ]
}
```

### Module Operations

#### Install Module
```http
POST /api/module/{moduleId}/install
```

Marks a module as installed (after content import).

#### Uninstall Module
```http
POST /api/module/{moduleId}/uninstall
```

Marks module as uninstalled (keeps data).

#### Delete Module
```http
DELETE /api/module/{moduleId}
```

Completely removes module and associated data.

#### Update Module Metadata
```http
PATCH /api/module/{moduleId}
Content-Type: application/json

{
  "name": "Updated Name",
  "description": "Updated description",
  "version": "1.1.0"
}
```

## Data Sources

### Quran Translations

**Free Sources:**
- **Tanzil Project**: http://tanzil.net/trans/
  - Multiple translations in various languages
  - XML and text formats available
  
- **Quran.com API**: https://api.quran.com/api/v4/
  - REST API with multiple translations
  - JSON format
  
- **The Clear Quran**: https://theclearquran.org/
  - Dr. Mustafa Khattab's translation
  - Available with permission

**Popular Translations:**
- Sahih International (English)
- Yusuf Ali (English)
- Pickthall (English)
- Hamidullah (French)
- Bubenheim & Elyas (German)
- Abdel Haleem (English)

### Islamic Books

**Public Domain Sources:**
- **Internet Archive**: https://archive.org/details/texts
  - Search: "Islamic books"
  - Public domain classical texts
  
- **IslamicStudies.info**: https://islamicstudies.info/
  - Free Islamic texts
  - Organized by topic

**Popular Books:**
- Sahih Bukhari & Muslim (Hadith collections)
- Riyadh as-Salihin (Hadith compilation)
- The Sealed Nectar (Seerah)
- Tafsir Ibn Kathir (Commentary)
- Fiqh us-Sunnah (Islamic Law)
- In the Footsteps of the Prophet (Biography)

### Hadith Collections

**Sources:**
- **Sunnah.com**: https://sunnah.com/
  - API available
  - Major hadith collections
  - Authenticity grading

- **Hadith API**: https://hadithapi.com/
  - REST API
  - Multiple collections

**Major Collections:**
- Sahih Bukhari
- Sahih Muslim
- Sunan Abu Dawood
- Jami' at-Tirmidhi
- Sunan an-Nasa'i
- Sunan Ibn Majah
- Muwatta Malik
- Musnad Ahmad

## Usage Examples

### Complete Import Workflow

```bash
# 1. Register module
curl -X POST http://localhost:5000/api/module \
  -H "Content-Type: application/json" \
  -d '{
    "moduleId": "quran-translation-ur-maududi",
    "name": "Tafhim al-Quran (Urdu)",
    "type": 1,
    "language": "ur",
    "author": "Abul Ala Maududi",
    "description": "Urdu translation and commentary",
    "version": "1.0.0"
  }'

# 2. Import translation data
curl -X POST http://localhost:5000/api/module/import/translation \
  -H "Content-Type: application/json" \
  -d '{
    "filePath": "../Data/translations/maududi-urdu.json",
    "moduleId": "quran-translation-ur-maududi",
    "language": "ur",
    "translator": "Abul Ala Maududi"
  }'

# 3. Verify installation
curl http://localhost:5000/api/module/quran-translation-ur-maududi
```

### Creating Translation Data

```javascript
// Convert from API response to module format
const verses = apiResponse.data.verses;
const translationData = verses.map(verse => ({
  SurahNumber: verse.chapter_id,
  AyahNumber: verse.verse_number,
  Translation: verse.text,
  Footnotes: verse.footnotes || null
}));

// Save to JSON
fs.writeFileSync(
  'translation.json',
  JSON.stringify(translationData, null, 2)
);
```

### Creating Book Data

```javascript
// Structure book data
const bookData = {
  Title: "Book Title",
  Author: "Author Name",
  Category: "Seerah", // or "Fiqh", "Tafsir", "Hadith"
  Language: "en",
  Description: "Book description",
  ISBN: "978-XXXXXXXXXX",
  Publisher: "Publisher Name",
  PublicationYear: 2024,
  PageCount: 500,
  License: "Creative Commons",
  Chapters: []
};

// Add chapters
bookData.Chapters.push({
  ChapterNumber: 1,
  Title: "Introduction",
  Content: "Full chapter text...",
  Summary: "Brief summary",
  PageNumber: 1
});

// Save to JSON
fs.writeFileSync(
  'book.json',
  JSON.stringify(bookData, null, 2)
);
```

## Best Practices

### Module Naming Convention

```
{type}-{content}-{language}-{author}

Examples:
- quran-translation-en-sahih
- hadith-sahih-bukhari
- book-sealed-nectar
- tafsir-ibn-kathir-en
- audio-mishary-full
```

### Version Management

Use semantic versioning: `MAJOR.MINOR.PATCH`

- **MAJOR**: Incompatible changes
- **MINOR**: New features (backward compatible)
- **PATCH**: Bug fixes

### Data Quality

1. **Verify Source**: Use authenticated Islamic sources
2. **Check Accuracy**: Cross-reference with scholarly works
3. **Include Metadata**: Author, date, source URL
4. **Add Footnotes**: Include explanatory notes where helpful
5. **Proper Encoding**: Use UTF-8 for Arabic text

### Performance

1. **Batch Import**: Import large datasets in batches
2. **Index Optimization**: Database indexes on frequently queried fields
3. **Lazy Loading**: Load module content on demand
4. **Caching**: Cache frequently accessed translations
5. **Compression**: Compress large text content

## Frontend Integration

### Using Installed Modules

```typescript
// Get installed translations
const translations = await apiClient.request('/module/type/1?installed=true');

// Display translation selector
<Select>
  {translations.map(module => (
    <SelectItem value={module.moduleId}>
      {module.name} ({module.language})
    </SelectItem>
  ))}
</Select>

// Fetch translation for specific verse
const translation = await apiClient.request(
  `/quran/translation/${moduleId}/${surahNumber}/${ayahNumber}`
);
```

### Module Management UI

```typescript
import { ModuleCard } from '@/components/modules/ModuleCard'

function ModuleManager() {
  const [modules, setModules] = useState([]);
  
  useEffect(() => {
    fetch('/api/module')
      .then(res => res.json())
      .then(setModules);
  }, []);
  
  const handleInstall = async (moduleId) => {
    await fetch(`/api/module/${moduleId}/install`, { method: 'POST' });
    // Refresh modules list
  };
  
  return (
    <div className="grid grid-cols-3 gap-4">
      {modules.map(module => (
        <ModuleCard 
          key={module.id}
          module={module}
          onInstall={handleInstall}
        />
      ))}
    </div>
  );
}
```

## Troubleshooting

### Common Issues

**Import Fails with "File not found"**
- Check file path is relative to ContentRootPath
- Verify file exists and has correct permissions
- Use forward slashes in path

**Translation not displaying**
- Verify module is marked as installed
- Check moduleId matches between import and query
- Ensure SurahNumber and AyahNumber are correct

**Module already exists error**
- Use a unique moduleId
- Or delete existing module first
- Or update existing module instead

**Database errors**
- Run migrations: `dotnet ef database update`
- Check connection string
- Verify SQL Server is running

## Security Considerations

1. **Input Validation**: Validate all import data
2. **File Path Security**: Restrict file access to approved directories
3. **SQL Injection**: Use parameterized queries (handled by EF Core)
4. **XSS Prevention**: Sanitize HTML content in books
5. **Authentication**: Add auth to admin endpoints in production

## Future Enhancements

- [ ] Module marketplace/directory
- [ ] Automatic updates
- [ ] Module dependencies
- [ ] Content verification/checksums
- [ ] Multi-user module sharing
- [ ] Cloud storage integration
- [ ] Offline mode support
- [ ] Module export functionality

## Support

For questions or issues:
- **GitHub Issues**: https://github.com/J-Ellette/Al-Hidayah_Pro/issues
- **Documentation**: See README.md and SETUP_GUIDE.md
