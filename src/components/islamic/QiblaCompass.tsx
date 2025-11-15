import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Compass } from "@phosphor-icons/react"
import { useState, useEffect } from "react"

export function QiblaCompass() {
  const [qiblaDirection, setQiblaDirection] = useState<number | null>(null)
  const [deviceHeading, setDeviceHeading] = useState<number>(0)
  const [location, setLocation] = useState("Detecting location...")
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    // In a real implementation, this would:
    // 1. Get user's geolocation
    // 2. Calculate Qibla direction based on location
    // 3. Use device orientation API to show compass
    
    // For now, using sample data
    setLocation("Current Location")
    // Qibla direction for a sample location (e.g., New York to Mecca is approximately 58°)
    setQiblaDirection(58)
  }, [])

  const rotationAngle = qiblaDirection !== null ? qiblaDirection - deviceHeading : 0

  return (
    <Card>
      <CardHeader>
        <CardTitle className="flex items-center gap-2">
          <Compass className="h-5 w-5 text-accent" />
          Qibla Direction
        </CardTitle>
        <p className="text-sm text-muted-foreground">{location}</p>
      </CardHeader>
      <CardContent className="flex flex-col items-center space-y-4">
        {error ? (
          <div className="text-center p-4">
            <p className="text-sm text-destructive">{error}</p>
          </div>
        ) : qiblaDirection !== null ? (
          <>
            <div className="relative w-48 h-48 flex items-center justify-center">
              {/* Compass background */}
              <div className="absolute inset-0 rounded-full bg-gradient-to-br from-accent/20 to-accent/5 border-2 border-accent/30" />
              
              {/* Compass marks */}
              <div className="absolute inset-4 rounded-full border border-border">
                {/* Cardinal directions */}
                <div className="absolute top-0 left-1/2 -translate-x-1/2 -translate-y-full text-xs font-semibold text-foreground">N</div>
                <div className="absolute bottom-0 left-1/2 -translate-x-1/2 translate-y-full text-xs font-semibold text-muted-foreground">S</div>
                <div className="absolute right-0 top-1/2 -translate-y-1/2 translate-x-full text-xs font-semibold text-muted-foreground">E</div>
                <div className="absolute left-0 top-1/2 -translate-y-1/2 -translate-x-full text-xs font-semibold text-muted-foreground">W</div>
              </div>
              
              {/* Qibla indicator */}
              <div 
                className="absolute w-1 h-20 bg-accent rounded-full transition-transform duration-300"
                style={{
                  transform: `rotate(${rotationAngle}deg)`,
                  transformOrigin: 'center bottom',
                  top: 'calc(50% - 5rem)'
                }}
              >
                <div className="absolute -top-2 left-1/2 -translate-x-1/2 w-4 h-4 bg-accent rounded-full border-2 border-background" />
              </div>
              
              {/* Center dot */}
              <div className="absolute w-3 h-3 bg-foreground rounded-full" />
            </div>
            
            <div className="text-center space-y-2">
              <p className="text-2xl font-bold text-accent">{qiblaDirection}°</p>
              <p className="text-sm text-muted-foreground">Direction to Mecca (Kaaba)</p>
            </div>
            
            <div className="text-xs text-muted-foreground text-center max-w-xs">
              Point your device north and align the green indicator with the physical direction shown
            </div>
          </>
        ) : (
          <div className="flex items-center justify-center h-48">
            <p className="text-sm text-muted-foreground">Calculating direction...</p>
          </div>
        )}
      </CardContent>
    </Card>
  )
}
