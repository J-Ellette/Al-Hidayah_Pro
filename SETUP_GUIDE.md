# Al-Hidayah Pro - Setup Guide

Complete guide for setting up and running Al-Hidayah Pro with full backend integration.

## Prerequisites

- **Node.js** 18+ and npm
- **.NET 8+ SDK**
- **SQL Server** or **SQLite** (for development)
- **Windows** (for WebView2 desktop app)

## Quick Start

### 1. Clone Repository

```bash
git clone https://github.com/J-Ellette/Al-Hidayah_Pro.git
cd Al-Hidayah_Pro
```

### 2. Frontend Setup

```bash
# Install dependencies
npm install

# Create environment file
cp .env.example .env

# Edit .env and set API URL (default: http://localhost:5000/api)
# VITE_API_BASE_URL=http://localhost:5000/api

# Start development server
npm run dev
```

The frontend will be available at `http://localhost:5173`

### 3. Backend Setup

```bash
cd backend/AlHidayahPro.Api

# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update --project ../AlHidayahPro.Data

# Start API server
dotnet run
```

The API will be available at `http://localhost:5000`

## Database Population

### Option 1: Import Sample Data (Quick Start)

```bash
# Import Surah Al-Fatihah
curl -X POST http://localhost:5000/api/admin/import/quran \
  -H "Content-Type: application/json" \
  -d '{"FilePath":"../AlHidayahPro.Data/SampleData/quran-sample.json"}'

# Import sample Hadiths
curl -X POST http://localhost:5000/api/admin/import/hadith \
  -H "Content-Type: application/json" \
  -d '{"FilePath":"../AlHidayahPro.Data/SampleData/hadith-sample.json","Collection":"Sahih Bukhari"}'

# Check database stats
curl http://localhost:5000/api/admin/stats
```

### Option 2: Full Quran & Hadith Data

For production deployment, download full Islamic data:

**Quran Data:**
1. Visit https://api.alquran.cloud/v1/quran/en.asad for English translation
2. Visit https://api.alquran.cloud/v1/quran/quran-uthmani for Arabic
3. Format as per `backend/AlHidayahPro.Data/SampleData/README.md`
4. Import using admin endpoint

**Hadith Data:**
1. Use https://sunnah.com/ API for authenticated collections
2. Format as per sample data structure
3. Import each collection separately

## WebView2 Desktop App

### Building Desktop Application

```bash
cd backend/AlHidayahPro.Desktop

# Build for Windows
dotnet publish -c Release -r win-x64 --self-contained

# Run desktop app
dotnet run
```

The desktop app will:
1. Start the backend API automatically
2. Open the frontend in a WebView2 window
3. Provide a native Windows application experience

### Note on WebView2
Currently, the desktop launcher opens the default browser. Full WebView2 integration requires:
- Microsoft.Web.WebView2 NuGet package
- WPF or WinUI3 host window
- WebView2 control configuration

## Architecture

```
Al-Hidayah Pro/
├── src/                          # React frontend
│   ├── components/               # UI components
│   │   ├── islamic/              # Islamic-specific components
│   │   └── ui/                   # Shadcn UI components
│   ├── pages/                    # Page components
│   ├── lib/                      # Utilities and API client
│   └── styles/                   # CSS and themes
├── backend/
│   ├── AlHidayahPro.Api/         # ASP.NET Core API
│   ├── AlHidayahPro.Data/        # EF Core data layer
│   └── AlHidayahPro.Desktop/     # Desktop host app
└── public/                       # Static assets
```

## API Endpoints

### Quran API
- `GET /api/quran/surahs` - Get all Surahs
- `GET /api/quran/surah/{number}` - Get specific Surah
- `GET /api/quran/surah/{surah}/ayah/{ayah}` - Get specific verse
- `POST /api/quran/search` - Search Quran

### Hadith API
- `GET /api/hadith/collections` - Get all collections
- `GET /api/hadith/collection/{name}` - Get Hadiths from collection
- `POST /api/hadith/search` - Search Hadiths

### Audio API
- `GET /api/audio/reciters` - Get available reciters
- `GET /api/audio/recitation/{reciter}/surah/{number}` - Get recitation

### Admin API
- `POST /api/admin/import/quran` - Import Quran data
- `POST /api/admin/import/hadith` - Import Hadith data
- `GET /api/admin/stats` - Database statistics

## Features

### ✅ Implemented
- Complete Quran reader with Arabic text and translations
- Hadith browser with authenticity grading
- Audio recitation player with multiple reciters
- Prayer times calculator
- Qibla compass
- Study workspace with commentary and notes
- Learning dashboard with progress tracking
- Search functionality
- Islamic UI theme (UAE Design System compliant)

### ⏳ Planned
- Full WebView2 desktop integration
- Offline mode
- Cloud sync
- Advanced search with filters
- Tafsir (commentary) integration
- Bookmark and notes synchronization
- Multi-language support

## Development

### Running Tests

```bash
# Frontend tests
npm test

# Backend tests
cd backend
dotnet test
```

### Code Quality

```bash
# Frontend linting
npm run lint

# Backend code analysis
dotnet format
```

## Troubleshooting

### Frontend can't connect to backend
- Ensure backend is running on `http://localhost:5000`
- Check `.env` file has correct `VITE_API_BASE_URL`
- Verify CORS is enabled in backend

### Database errors
- Run `dotnet ef database update` to apply migrations
- Check connection string in `appsettings.json`
- Ensure SQL Server is running

### Arabic text not displaying
- Check that Arabic fonts are installed (Noto Kufi Arabic, Amiri)
- Verify browser supports RTL text
- Clear browser cache

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

See LICENSE file for details.

## Support

For issues and questions:
- GitHub Issues: https://github.com/J-Ellette/Al-Hidayah_Pro/issues
- Documentation: See build_sheet.md

## Resources

- **UAE Design System**: https://designsystem.gov.ae/
- **Quran.com API**: https://api.quran.com/
- **Sunnah.com**: https://sunnah.com/
- **Islamic APIs**: See `backend/AlHidayahPro.Data/SampleData/README.md`
