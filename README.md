# Al-Hidayah Pro

A modern Islamic resources application providing easy access to the Quran, Hadith, guides, atlas, and various Islamic tools with a clean, focused interface designed for study and learning.

## 🎨 Design System

This application uses the **UAE Government Design System** (@aegov/design-system-react) which provides:
- Standards-compliant components following UAE government guidelines
- RTL (Right-to-Left) support for Arabic content
- Arabic typography support with Noto Kufi Arabic and Alexandria fonts
- Accessible, responsive UI components
- Comprehensive theming with UAE-specific color palettes

Learn more about the UAE Design System at [https://designsystem.gov.ae/](https://designsystem.gov.ae/)

## 🚀 Features

- **Quran**: Read and study the Holy Quran with translations and tafsir
- **Hadith Collections**: Explore authenticated narrations from the Prophet Muhammad (peace be upon him)
- **Study Guides**: Comprehensive guides for Islamic practices, beliefs, and history
- **Islamic Atlas**: Interactive maps and timelines of Islamic history
- **Islamic Tools**: Prayer times calculator, Qibla direction finder, and more
- **Search**: Search across all Islamic resources

## 🛠️ Tech Stack

### Frontend
- React 19
- TypeScript
- Vite
- UAE Design System React Components
- Tailwind CSS
- Radix UI primitives

### Backend
- C# .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQLite Database
- SignalR for real-time features

## 📦 Installation

### Frontend
```bash
npm install
```

### Backend
```bash
cd backend
dotnet restore
dotnet build
```

## 🚀 Development

### Quick Start (Both Frontend & Backend)
```bash
./start-dev.sh
```

This will start:
- Backend API on http://localhost:5000
- Frontend on http://localhost:5173

### Manual Start

**Frontend:**
```bash
npm run dev
```

**Backend:**
```bash
cd backend/AlHidayahPro.Api
dotnet run --urls http://localhost:5000
```

## 📚 Documentation

- [Integration Guide](./INTEGRATION_GUIDE.md) - Frontend-Backend integration
- [Backend README](./backend/README.md) - Backend API documentation
- [Build Sheet](./build_sheet.md) - Implementation progress and roadmap

## 🗄️ Database

The backend uses SQLite with sample data automatically seeded on first run:
- Surah Al-Fatihah (all 7 verses)
- Sample Hadiths from Sahih Bukhari and Sahih Muslim
- Sample audio recitation metadata

## 🔌 API Endpoints

### Quran
- `GET /api/quran/surahs` - Get all Surahs
- `GET /api/quran/surah/{number}/ayahs` - Get verses for a Surah
- `POST /api/quran/search` - Search verses

### Hadith
- `GET /api/hadith/collections` - Get Hadith collections
- `GET /api/hadith/collection/{name}` - Get Hadiths by collection
- `POST /api/hadith/search` - Search Hadiths

### Audio
- `GET /api/audio/reciters` - Get available reciters
- `GET /api/audio/recitation/{reciter}/surah/{number}` - Get recitation

### SignalR Hub
- `ws://localhost:5000/hubs/study` - Real-time study group features

## 🏗️ Build

```bash
npm run build
```

## 🧪 Testing

API endpoints can be tested with:
```bash
# Get all Surahs
curl http://localhost:5000/api/quran/surahs

# Get Al-Fatihah verses
curl http://localhost:5000/api/quran/surah/1/ayahs
```

## 📄 License

MIT License - see LICENSE file for details.
