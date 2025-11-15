# Al-Hidayah Pro - Frontend-Backend Integration Guide

This guide explains how to integrate the React frontend with the C# .NET backend API.

## Architecture Overview

```
┌─────────────────────────────────────────┐
│  React Frontend (Vite Dev Server)      │
│  Port: 5173                             │
│  - UAE Design System Components        │
│  - Islamic UI (Quran, Hadith pages)    │
│  - SignalR Client for real-time        │
└─────────────────────────────────────────┘
                    ↓ HTTP/WebSocket
┌─────────────────────────────────────────┐
│  C# .NET Backend API                    │
│  Port: 5000                             │
│  - RESTful API Endpoints                │
│  - SignalR Hub for Study Groups         │
│  - SQLite Database                      │
└─────────────────────────────────────────┘
```

## Running the Application

### Start the Backend API

```bash
cd backend/AlHidayahPro.Api
dotnet run --urls http://localhost:5000
```

The API will:
- Create the SQLite database if it doesn't exist
- Seed sample data (Al-Fatihah and sample Hadiths)
- Listen on http://localhost:5000

### Start the Frontend

```bash
npm run dev
```

The frontend will be available at http://localhost:5173

## API Endpoints Available

### Quran Endpoints

```
GET  /api/quran/surahs                        - Get all Surahs
GET  /api/quran/surah/{number}                - Get specific Surah info
GET  /api/quran/surah/{number}/ayahs          - Get all verses for a Surah
GET  /api/quran/surah/{number}/ayah/{ayah}    - Get specific verse
POST /api/quran/search                        - Search verses
```

### Hadith Endpoints

```
GET  /api/hadith/collections                  - Get available collections
GET  /api/hadith/collection/{name}            - Get Hadiths by collection
GET  /api/hadith/collection/{name}/hadith/{number} - Get specific Hadith
POST /api/hadith/search                       - Search Hadiths
```

### Audio Endpoints

```
GET  /api/audio/reciters                      - Get available reciters
GET  /api/audio/recitation/{reciter}/surah/{number} - Get recitation
GET  /api/audio/recitation/{reciter}/surah/{number}/all - Get all verse recitations
```

### SignalR Hub

```
WebSocket: /hubs/study

Methods:
- JoinStudyGroup(groupId)
- LeaveStudyGroup(groupId)
- ShareVerse(groupId, surahNumber, ayahNumber, commentary)
- SyncReadingPosition(groupId, surahNumber, ayahNumber)
- SendMessage(groupId, message)
```

## Frontend Integration Examples

### 1. Fetching Quran Data

```typescript
// In your React component or service
const fetchSurahs = async () => {
  const response = await fetch('http://localhost:5000/api/quran/surahs');
  const surahs = await response.json();
  return surahs;
};

const fetchVerse = async (surahNumber: number, ayahNumber: number) => {
  const response = await fetch(
    `http://localhost:5000/api/quran/surah/${surahNumber}/ayah/${ayahNumber}`
  );
  const verse = await response.json();
  return verse;
};
```

### 2. Searching Quran Verses

```typescript
const searchVerses = async (query: string) => {
  const response = await fetch('http://localhost:5000/api/quran/search', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      query: query,
      searchArabic: true,
      searchTranslation: true,
      pageNumber: 1,
      pageSize: 20
    }),
  });
  const results = await response.json();
  return results;
};
```

### 3. Fetching Hadith Data

```typescript
const fetchHadithCollections = async () => {
  const response = await fetch('http://localhost:5000/api/hadith/collections');
  const collections = await response.json();
  return collections;
};

const fetchHadiths = async (collection: string) => {
  const response = await fetch(
    `http://localhost:5000/api/hadith/collection/${encodeURIComponent(collection)}`
  );
  const hadiths = await response.json();
  return hadiths;
};
```

### 4. SignalR Integration for Study Groups

First, install SignalR client:

```bash
npm install @microsoft/signalr
```

Then use it in your React component:

```typescript
import * as signalR from '@microsoft/signalr';

const StudyGroupComponent = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);

  useEffect(() => {
    // Create connection
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/hubs/study')
      .withAutomaticReconnect()
      .build();

    // Set up event handlers
    newConnection.on('VerseShared', (data) => {
      console.log('Verse shared:', data);
      // Update UI with shared verse
    });

    newConnection.on('ReadingPositionUpdated', (data) => {
      console.log('Reading position updated:', data);
      // Sync reading position
    });

    newConnection.on('MessageReceived', (data) => {
      console.log('Message received:', data);
      // Display message in chat
    });

    // Start connection
    newConnection.start()
      .then(() => {
        console.log('Connected to Study Hub');
        setConnection(newConnection);
      })
      .catch(err => console.error('Connection error:', err));

    return () => {
      newConnection.stop();
    };
  }, []);

  const joinGroup = async (groupId: string) => {
    if (connection) {
      await connection.invoke('JoinStudyGroup', groupId);
    }
  };

  const shareVerse = async (groupId: string, surah: number, ayah: number, commentary?: string) => {
    if (connection) {
      await connection.invoke('ShareVerse', groupId, surah, ayah, commentary);
    }
  };

  return (
    // Your component JSX
  );
};
```

## Environment Configuration

### Frontend (.env file)

Create a `.env` file in the root directory:

```env
VITE_API_URL=http://localhost:5000
VITE_SIGNALR_HUB_URL=http://localhost:5000/hubs/study
```

### Backend (appsettings.json)

The backend is already configured with:
- SQLite database connection
- CORS for frontend origins (localhost:5173, localhost:5000, localhost:3000)
- SignalR endpoint at `/hubs/study`

## Data Models

### QuranVerseDto

```typescript
interface QuranVerseDto {
  surahNumber: number;
  ayahNumber: number;
  arabicText: string;
  englishTranslation?: string;
  transliteration?: string;
  audioUrl?: string;
  juzNumber: number;
  pageNumber: number;
}
```

### SurahDto

```typescript
interface SurahDto {
  number: number;
  arabicName: string;
  englishName: string;
  englishTranslation?: string;
  numberOfAyahs: number;
  revelationType: string;
}
```

### HadithDto

```typescript
interface HadithDto {
  collection: string;
  book: string;
  bookNumber: number;
  hadithNumber: string;
  arabicText?: string;
  englishText: string;
  grade: string;
  narrator?: string;
  chapter?: string;
}
```

## Next Steps

1. **Update Frontend Pages**: Modify the Quran and Hadith pages to fetch data from the API
2. **Add Loading States**: Implement loading indicators while fetching data
3. **Error Handling**: Add proper error handling for API calls
4. **Caching**: Implement caching strategy for frequently accessed data
5. **Real-time Features**: Integrate SignalR for study group functionality
6. **Audio Integration**: Connect audio player to the audio API endpoints

## Testing the Integration

### Test API Endpoints

```bash
# Get all Surahs
curl http://localhost:5000/api/quran/surahs

# Get a specific verse
curl http://localhost:5000/api/quran/surah/1/ayah/1

# Get Hadith collections
curl http://localhost:5000/api/hadith/collections

# Search verses
curl -X POST http://localhost:5000/api/quran/search \
  -H "Content-Type: application/json" \
  -d '{"query":"mercy","searchTranslation":true}'
```

## Troubleshooting

### CORS Issues

If you encounter CORS errors:
1. Ensure the backend is running on port 5000
2. Check that the frontend origin is listed in CORS policy in `Program.cs`
3. Verify the API URL in your frontend code

### Database Issues

If the database isn't created:
1. Delete the `alhidayah.db` file if it exists
2. Restart the API - it will recreate and seed the database

### SignalR Connection Issues

If SignalR won't connect:
1. Ensure the backend is running
2. Check the hub URL is correct
3. Verify CORS settings include credentials
4. Check browser console for connection errors

## Production Deployment

For production, you'll need to:
1. Update CORS origins to your production domain
2. Use a production-ready database (PostgreSQL, SQL Server, etc.)
3. Configure HTTPS for both frontend and backend
4. Set up proper authentication and authorization
5. Configure environment variables for sensitive data
