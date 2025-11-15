import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Clock, MapPin } from "@phosphor-icons/react"
import { useState, useEffect } from "react"

interface PrayerTime {
  name: string
  time: string
  isPassed: boolean
}

export function PrayerTimesCard() {
  const [location, setLocation] = useState("Detecting location...")
  const [currentTime, setCurrentTime] = useState(new Date())
  
  // Sample prayer times - in a real implementation, these would be calculated based on location
  const prayerTimes: PrayerTime[] = [
    { name: "Fajr", time: "5:30 AM", isPassed: currentTime.getHours() >= 5 && currentTime.getMinutes() >= 30 },
    { name: "Sunrise", time: "6:45 AM", isPassed: currentTime.getHours() >= 6 && currentTime.getMinutes() >= 45 },
    { name: "Dhuhr", time: "12:30 PM", isPassed: currentTime.getHours() >= 12 && currentTime.getMinutes() >= 30 },
    { name: "Asr", time: "3:45 PM", isPassed: currentTime.getHours() >= 15 && currentTime.getMinutes() >= 45 },
    { name: "Maghrib", time: "6:15 PM", isPassed: currentTime.getHours() >= 18 && currentTime.getMinutes() >= 15 },
    { name: "Isha", time: "7:30 PM", isPassed: currentTime.getHours() >= 19 && currentTime.getMinutes() >= 30 },
  ]

  useEffect(() => {
    // Update current time every minute
    const timer = setInterval(() => {
      setCurrentTime(new Date())
    }, 60000)

    // Get location (simplified - in real implementation would use geolocation API)
    setLocation("Local Time Zone")

    return () => clearInterval(timer)
  }, [])

  const nextPrayer = prayerTimes.find(prayer => !prayer.isPassed) || prayerTimes[0]

  return (
    <Card>
      <CardHeader>
        <CardTitle className="flex items-center gap-2">
          <Clock className="h-5 w-5 text-accent" />
          Prayer Times
        </CardTitle>
        <div className="flex items-center gap-2 text-sm text-muted-foreground">
          <MapPin className="h-4 w-4" />
          {location}
        </div>
      </CardHeader>
      <CardContent className="space-y-3">
        <div className="mb-4 p-3 bg-accent/10 rounded-lg border border-accent/20">
          <p className="text-xs text-muted-foreground mb-1">Next Prayer</p>
          <p className="text-lg font-semibold text-accent">{nextPrayer.name}</p>
          <p className="text-sm text-foreground">{nextPrayer.time}</p>
        </div>
        
        <div className="space-y-2">
          {prayerTimes.map((prayer) => (
            <div 
              key={prayer.name}
              className={`flex items-center justify-between p-2 rounded ${
                prayer.name === nextPrayer.name 
                  ? 'bg-accent/5 border border-accent/20' 
                  : prayer.isPassed
                  ? 'opacity-50'
                  : ''
              }`}
            >
              <span className="font-medium text-foreground">{prayer.name}</span>
              <span className="text-sm text-muted-foreground">{prayer.time}</span>
            </div>
          ))}
        </div>
        
        <p className="text-xs text-muted-foreground mt-4">
          Times are calculated for your current location
        </p>
      </CardContent>
    </Card>
  )
}
