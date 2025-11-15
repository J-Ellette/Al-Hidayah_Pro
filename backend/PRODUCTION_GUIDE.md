# Al-Hidayah Pro Production Deployment Guide

This guide covers the production-ready features added to Al-Hidayah Pro backend.

## Features Implemented

### 1. Full Data Import System ✅

The backend now includes comprehensive data import services for Quran and Hadith data.

#### Quran Data Import

**Endpoint**: `POST /api/admin/import/quran`

**Request Body**:
```json
{
  "filePath": "Data/Import/quran.json"
}
```

**Expected JSON Format**:
```json
[
  {
    "number": 1,
    "arabicName": "الفاتحة",
    "englishName": "Al-Fatihah",
    "englishTranslation": "The Opening",
    "numberOfAyahs": 7,
    "revelationType": "Meccan",
    "verses": [
      {
        "ayahNumber": 1,
        "arabicText": "بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ",
        "englishTranslation": "In the name of Allah...",
        "transliteration": "Bismillahir Rahmanir Raheem",
        "audioUrl": "https://example.com/audio/001_001.mp3",
        "juzNumber": 1,
        "pageNumber": 1
      }
    ]
  }
]
```

#### Hadith Data Import

**Endpoint**: `POST /api/admin/import/hadith`

**Request Body**:
```json
{
  "filePath": "Data/Import/sahih_bukhari.json",
  "collection": "Sahih Bukhari"
}
```

**Expected JSON Format**:
```json
[
  {
    "book": "Book of Revelation",
    "bookNumber": 1,
    "hadithNumber": "1",
    "arabicText": "عَنْ عُمَرَ...",
    "englishText": "Actions are according to intentions...",
    "grade": "Sahih",
    "narrator": "Umar ibn Al-Khattab",
    "chapter": "How the Divine Inspiration started"
  }
]
```

#### Database Statistics

**Endpoint**: `GET /api/admin/stats`

Returns current database statistics:
```json
{
  "totalSurahs": 114,
  "totalVerses": 6236,
  "totalHadiths": 7563,
  "totalRecitations": 228,
  "collections": ["Sahih Bukhari", "Sahih Muslim", ...]
}
```

### 2. JWT Authentication ✅

Complete JWT-based authentication system with user management.

#### Register New User

**Endpoint**: `POST /api/auth/register`

**Request**:
```json
{
  "username": "john_doe",
  "email": "john@example.com",
  "password": "SecurePassword123!",
  "fullName": "John Doe"
}
```

**Response**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "john_doe",
  "email": "john@example.com",
  "fullName": "John Doe",
  "role": "User",
  "expiresAt": "2025-11-10T21:30:00Z"
}
```

#### Login

**Endpoint**: `POST /api/auth/login`

**Request**:
```json
{
  "username": "john_doe",
  "password": "SecurePassword123!"
}
```

**Response**: Same as register

#### Validate Token

**Endpoint**: `POST /api/auth/validate`

**Headers**: `Authorization: Bearer {token}`

**Response**:
```json
{
  "valid": true
}
```

#### Using Authentication in Frontend

```typescript
// Login
const login = async (username: string, password: string) => {
  const response = await fetch('http://localhost:5000/api/auth/login', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password })
  });
  
  const data = await response.json();
  localStorage.setItem('token', data.token);
  return data;
};

// Make authenticated requests
const fetchProtectedData = async () => {
  const token = localStorage.getItem('token');
  const response = await fetch('http://localhost:5000/api/protected', {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return response.json();
};
```

#### Protecting Endpoints

Add `[Authorize]` attribute to controllers or actions:

```csharp
[Authorize]
[HttpGet("protected")]
public ActionResult GetProtectedData()
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var username = User.FindFirst(ClaimTypes.Name)?.Value;
    var role = User.FindFirst(ClaimTypes.Role)?.Value;
    
    return Ok(new { userId, username, role });
}

[Authorize(Roles = "Admin")]
[HttpDelete("admin/delete/{id}")]
public ActionResult DeleteData(int id)
{
    // Only admins can access this
    return Ok();
}
```

### 3. Production Database Support ✅

The application now supports both SQLite (development) and PostgreSQL (production).

#### Configuration

**Development (SQLite)**:
```json
{
  "Database": {
    "Provider": "SQLite"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=alhidayah.db"
  }
}
```

**Production (PostgreSQL)**:
```json
{
  "Database": {
    "Provider": "PostgreSQL"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=alhidayah_prod;Username=alhidayah_user;Password=SecurePassword"
  }
}
```

#### Setting Up PostgreSQL

1. **Install PostgreSQL** on your server

2. **Create database and user**:
```sql
CREATE DATABASE alhidayah_prod;
CREATE USER alhidayah_user WITH PASSWORD 'SecurePassword';
GRANT ALL PRIVILEGES ON DATABASE alhidayah_prod TO alhidayah_user;
```

3. **Run migrations**:
```bash
cd backend/AlHidayahPro.Api
dotnet ef database update --context IslamicDbContext
```

4. **Set environment**:
```bash
export ASPNETCORE_ENVIRONMENT=Production
dotnet run
```

#### Database Migration Commands

```bash
# Create a new migration
dotnet ef migrations add MigrationName --project ../AlHidayahPro.Data

# Apply migrations
dotnet ef database update

# Rollback to specific migration
dotnet ef database update PreviousMigrationName

# Generate SQL script
dotnet ef migrations script --output migration.sql
```

### 4. HTTPS Configuration ✅

The application is now configured for HTTPS in production.

#### Development HTTPS

Start with HTTPS profile:
```bash
dotnet run --launch-profile https
```

Access at: `https://localhost:5001`

#### Production HTTPS

1. **Configure certificate** in appsettings.Production.json:
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://*:80"
      },
      "Https": {
        "Url": "https://*:443",
        "Certificate": {
          "Path": "/path/to/certificate.pfx",
          "Password": "CertificatePassword"
        }
      }
    }
  }
}
```

2. **Using Let's Encrypt** (recommended):
```bash
# Install certbot
sudo apt install certbot

# Obtain certificate
sudo certbot certonly --standalone -d yourdomain.com

# Convert to PFX
openssl pkcs12 -export \
  -out /etc/letsencrypt/live/yourdomain.com/certificate.pfx \
  -inkey /etc/letsencrypt/live/yourdomain.com/privkey.pem \
  -in /etc/letsencrypt/live/yourdomain.com/cert.pem \
  -certfile /etc/letsencrypt/live/yourdomain.com/chain.pem
```

3. **Update CORS for HTTPS**:

Edit `Program.cs` to include your production domain:
```csharp
policy.WithOrigins(
    "http://localhost:5173",
    "https://localhost:5001",
    "https://yourdomain.com",
    "https://www.yourdomain.com"
)
```

#### HTTPS Redirection

The application automatically redirects HTTP to HTTPS in production. To disable:
```csharp
// Comment out in Program.cs
// app.UseHttpsRedirection();
```

## Deployment Checklist

### Pre-Deployment

- [ ] Update JWT secret key in production configuration
- [ ] Configure PostgreSQL connection string
- [ ] Set up SSL/TLS certificate
- [ ] Update CORS origins for production domain
- [ ] Review and update logging levels
- [ ] Test all API endpoints
- [ ] Run database migrations
- [ ] Import production data (Quran, Hadith)

### Deployment Steps

1. **Build the application**:
```bash
dotnet publish -c Release -o ./publish
```

2. **Copy files to server**:
```bash
scp -r ./publish user@server:/var/www/alhidayah
```

3. **Configure systemd service** (Linux):

Create `/etc/systemd/system/alhidayah.service`:
```ini
[Unit]
Description=Al-Hidayah Pro API
After=network.target

[Service]
Type=notify
WorkingDirectory=/var/www/alhidayah
ExecStart=/usr/bin/dotnet /var/www/alhidayah/AlHidayahPro.Api.dll
Restart=always
RestartSec=10
SyslogIdentifier=alhidayah
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

4. **Start service**:
```bash
sudo systemctl enable alhidayah
sudo systemctl start alhidayah
sudo systemctl status alhidayah
```

5. **Configure reverse proxy** (Nginx):

```nginx
server {
    listen 80;
    server_name yourdomain.com;
    return 301 https://$server_name$request_uri;
}

server {
    listen 443 ssl http2;
    server_name yourdomain.com;

    ssl_certificate /etc/letsencrypt/live/yourdomain.com/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/yourdomain.com/privkey.pem;

    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
    
    location /hubs/ {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection "upgrade";
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
```

### Post-Deployment

- [ ] Verify HTTPS is working
- [ ] Test authentication flow
- [ ] Verify database connections
- [ ] Check SignalR connectivity
- [ ] Monitor application logs
- [ ] Set up monitoring and alerts
- [ ] Configure backups for database
- [ ] Test all API endpoints in production

## Environment Variables

For production, use environment variables instead of appsettings:

```bash
export ASPNETCORE_ENVIRONMENT=Production
export ConnectionStrings__DefaultConnection="Host=localhost;Database=alhidayah_prod;Username=alhidayah_user;Password=SecurePassword"
export Jwt__Key="YourProductionSecretKey32CharsMinimum!"
export Jwt__Issuer="AlHidayahPro"
export Jwt__Audience="AlHidayahProUsers"
```

Or use a `.env` file with a process manager like PM2 or systemd.

## Security Considerations

1. **JWT Secret**: Change the default JWT key to a strong, random string (min 32 characters)
2. **Database Password**: Use strong passwords and rotate regularly
3. **HTTPS Only**: Always use HTTPS in production
4. **CORS**: Restrict origins to your actual frontend domains
5. **Rate Limiting**: Consider implementing rate limiting for API endpoints
6. **SQL Injection**: The application uses EF Core parameterized queries (already protected)
7. **Input Validation**: All endpoints validate input data
8. **Password Hashing**: Passwords are hashed using SHA256 (consider bcrypt for production)

## Monitoring and Logging

### Application Insights (Azure)

Add to `Program.cs`:
```csharp
builder.Services.AddApplicationInsightsTelemetry();
```

### Serilog

Install packages:
```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
```

Configure in `Program.cs`:
```csharp
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/alhidayah-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
```

## Performance Optimization

1. **Database Indexing**: Already configured for optimal queries
2. **Caching**: Consider adding Redis for API response caching
3. **Connection Pooling**: PostgreSQL connection pooling is enabled by default
4. **Static File Compression**: Enable gzip compression in Nginx
5. **CDN**: Use CDN for audio files and static assets

## Backup and Recovery

### Database Backups

**PostgreSQL**:
```bash
# Backup
pg_dump -U alhidayah_user -d alhidayah_prod > backup_$(date +%Y%m%d).sql

# Restore
psql -U alhidayah_user -d alhidayah_prod < backup_20251109.sql
```

**Automated daily backups**:
```bash
# Add to crontab
0 2 * * * /usr/bin/pg_dump -U alhidayah_user -d alhidayah_prod > /backups/alhidayah_$(date +\%Y\%m\%d).sql
```

## Support and Troubleshooting

### Common Issues

1. **"JWT validation failed"**: Check JWT configuration and expiry times
2. **"Database connection failed"**: Verify connection string and PostgreSQL is running
3. **"CORS error"**: Update allowed origins in Program.cs
4. **"SignalR not connecting"**: Check WebSocket support in reverse proxy

### Logs Location

- Development: Console output
- Production: `/var/log/alhidayah/` (if configured with Serilog)
- System logs: `journalctl -u alhidayah -f`

## Scaling Considerations

- **Horizontal Scaling**: Deploy multiple instances behind a load balancer
- **Database**: Use connection pooling, read replicas for heavy read workloads
- **SignalR**: Use Redis backplane for multi-server SignalR
- **Caching**: Implement distributed caching with Redis
- **File Storage**: Use object storage (S3, Azure Blob) for audio files

## License

MIT License - see LICENSE file for details.
