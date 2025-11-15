# Al-Hidayah Pro Backend

This directory contains the C# .NET backend for Al-Hidayah Pro, implementing the API services, database layer, and desktop host application.

## Project Structure

```
backend/
├── AlHidayahPro.Api/         # ASP.NET Core Web API
│   ├── Controllers/          # API endpoints
│   ├── Services/             # Business logic
│   ├── DTOs/                 # Data transfer objects
│   └── Hubs/                 # SignalR hubs for real-time features
├── AlHidayahPro.Data/        # Data layer
│   └── Models/               # Entity models
├── AlHidayahPro.Desktop/     # Desktop host application
└── AlHidayahPro.sln          # Solution file
```

## Features Implemented

### API Endpoints

#### Quran Controller (`/api/quran`)
- `GET /surahs` - Get all Surahs list
- `GET /surah/{surahNumber}` - Get specific Surah info
- `GET /surah/{surahNumber}/ayah/{ayahNumber}` - Get specific verse
- `GET /surah/{surahNumber}/ayahs` - Get all verses for a Surah
- `POST /search` - Search verses by text

#### Hadith Controller (`/api/hadith`)
- `GET /collections` - Get available Hadith collections
- `GET /collection/{collection}` - Get Hadiths by collection
- `GET /collection/{collection}/hadith/{hadithNumber}` - Get specific Hadith
- `POST /search` - Search Hadiths

#### Audio Controller (`/api/audio`)
- `GET /reciters` - Get list of available reciters
- `GET /recitation/{reciterName}/surah/{surahNumber}` - Get recitation for surah/verse
- `GET /recitation/{reciterName}/surah/{surahNumber}/all` - Get all recitations for a surah

#### Auth Controller (`/api/auth`) **NEW!**
- `POST /login` - User login with JWT token
- `POST /register` - User registration
- `POST /validate` - Validate JWT token

#### Admin Controller (`/api/admin`) **NEW!**
- `POST /import/quran` - Import full Quran data from JSON
- `POST /import/hadith` - Import Hadith collection from JSON
- `GET /stats` - Get database statistics

### Real-time Features (SignalR)

#### Study Hub (`/hubs/study`)
- `JoinStudyGroup(groupId)` - Join a study group
- `LeaveStudyGroup(groupId)` - Leave a study group
- `ShareVerse(groupId, surahNumber, ayahNumber, commentary)` - Share a verse with commentary
- `SyncReadingPosition(groupId, surahNumber, ayahNumber)` - Sync reading position
- `SendMessage(groupId, message)` - Send message to study group

### Database

The application supports both SQLite (development) and PostgreSQL (production). Database models include:
- `Surah` - Quran chapters
- `QuranVerse` - Individual verses
- `Hadith` - Hadith collections
- `AudioRecitation` - Audio recitation metadata
- `User` - User accounts with authentication **NEW!**

## Setup & Running

### Prerequisites
- .NET 9.0 SDK or later
- SQLite (included) or PostgreSQL for production

### Build the Solution

```bash
cd backend
dotnet restore
dotnet build
```

### Run the API Server

```bash
cd AlHidayahPro.Api
dotnet run
```

The API will be available at `http://localhost:5000`

### Run the Desktop Application

```bash
cd AlHidayahPro.Desktop
dotnet run
```

This will start the API server and open the frontend in your default browser.

## Database Setup

The application will create a SQLite database (`alhidayah.db`) on first run. To seed the database with initial data, you'll need to:

1. Create a migration:
```bash
cd AlHidayahPro.Data
dotnet ef migrations add InitialCreate --startup-project ../AlHidayahPro.Api
```

2. Apply the migration:
```bash
cd AlHidayahPro.Api
dotnet ef database update
```

## Configuration

Edit `appsettings.json` in the `AlHidayahPro.Api` project to configure:
- Connection strings
- CORS origins
- Logging levels

## API Documentation

When running in development mode, the API endpoints can be tested using tools like:
- Postman
- cURL
- Browser (for GET endpoints)

Example requests:

```bash
# Get all Surahs
curl http://localhost:5000/api/quran/surahs

# Get a specific verse
curl http://localhost:5000/api/quran/surah/1/ayah/1

# Search verses
curl -X POST http://localhost:5000/api/quran/search \
  -H "Content-Type: application/json" \
  -d '{"query": "mercy", "searchTranslation": true}'
```

## Development Notes

- The API uses CORS to allow requests from the frontend (default: localhost:5173, 5001 for HTTPS)
- SignalR is configured for real-time study group features
- All services use dependency injection
- Logging is configured to console output
- JWT authentication is enabled for protected endpoints
- Supports both HTTP and HTTPS (HTTPS recommended for production)

## New Production Features

### 1. JWT Authentication ✅
Complete authentication system with user registration, login, and JWT token validation. See [PRODUCTION_GUIDE.md](./PRODUCTION_GUIDE.md) for details.

### 2. Data Import System ✅
Import full Quran and Hadith data from JSON files using admin endpoints. Supports bulk import and updates.

### 3. PostgreSQL Support ✅
Production-ready PostgreSQL database support alongside SQLite for development. Configure via `appsettings.json`.

### 4. HTTPS Configuration ✅
Full HTTPS support with configurable certificates for production deployments.

## Production Deployment

See [PRODUCTION_GUIDE.md](./PRODUCTION_GUIDE.md) for comprehensive production deployment instructions including:
- JWT authentication setup
- PostgreSQL configuration
- HTTPS/SSL setup
- Data import procedures
- Deployment checklist
- Security considerations

## Future Enhancements

- Implement WebView2 for true desktop experience
- Implement caching for performance (Redis)
- Add API versioning
- Add Swagger/OpenAPI documentation
- Add rate limiting
- Implement advanced search with Elasticsearch
