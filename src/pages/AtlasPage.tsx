import { MapTrifold, MapPin, Clock, Globe } from "@phosphor-icons/react"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { ScrollArea } from "@/components/ui/scroll-area"
import { Badge } from "@/components/ui/badge"

const historicalPeriods = [
  {
    id: 'prophetic',
    title: 'Prophetic Era',
    period: '570-632 CE',
    description: 'Life of Prophet Muhammad (ﷺ) and early Islamic community',
    locations: ['Mecca', 'Medina', 'Arabia'],
    events: ['Birth of the Prophet', 'First Revelation', 'Hijrah', 'Treaty of Hudaybiyyah', 'Conquest of Mecca']
  },
  {
    id: 'rashidun',
    title: 'Rashidun Caliphate',
    period: '632-661 CE',
    description: 'Rule of the four rightly-guided caliphs',
    locations: ['Arabia', 'Levant', 'Persia', 'Egypt'],
    events: ['Compilation of Quran', 'Expansion', 'Battle of Yarmouk', 'Conquest of Jerusalem']
  },
  {
    id: 'umayyad',
    title: 'Umayyad Dynasty',
    period: '661-750 CE',
    description: 'First hereditary Islamic caliphate',
    locations: ['Damascus', 'Spain', 'North Africa', 'Central Asia'],
    events: ['Dome of the Rock', 'Expansion to Spain', 'Battle of Tours', 'Islamic Art Development']
  },
  {
    id: 'abbasid',
    title: 'Abbasid Golden Age',
    period: '750-1258 CE',
    description: 'Era of scientific and cultural flourishing',
    locations: ['Baghdad', 'Cairo', 'Cordoba', 'Samarkand'],
    events: ['House of Wisdom', 'Translation Movement', 'Scientific Advances', 'Cultural Renaissance']
  },
  {
    id: 'ottoman',
    title: 'Ottoman Empire',
    period: '1299-1922 CE',
    description: 'Last major Islamic caliphate',
    locations: ['Istanbul', 'Jerusalem', 'Mecca', 'Balkans'],
    events: ['Conquest of Constantinople', 'Protection of Holy Sites', 'World War I', 'End of Caliphate']
  }
]

const significantSites = [
  { name: 'Masjid al-Haram (Mecca)', significance: 'Holiest site in Islam, direction of prayer', country: 'Saudi Arabia' },
  { name: "Al-Masjid an-Nabawi (Medina)", significance: "Prophet's Mosque", country: 'Saudi Arabia' },
  { name: 'Al-Aqsa Mosque (Jerusalem)', significance: 'Third holiest site', country: 'Palestine' },
  { name: 'Dome of the Rock (Jerusalem)', significance: 'Site of Night Journey', country: 'Palestine' },
  { name: 'Great Mosque of Cordoba', significance: 'Islamic architecture in Europe', country: 'Spain' },
  { name: 'Al-Azhar Mosque (Cairo)', significance: 'Center of Islamic learning', country: 'Egypt' }
]

export function AtlasPage() {
  return (
    <div className="flex-1 flex flex-col bg-background overflow-hidden">
      <div className="border-b border-border p-6">
        <div className="max-w-6xl mx-auto">
          <div className="flex items-center gap-4 mb-4">
            <MapTrifold className="h-8 w-8 text-accent" weight="duotone" />
            <h1 className="text-3xl font-semibold text-foreground">Islamic Atlas</h1>
          </div>
          <p className="text-muted-foreground">
            Explore the geography and history of the Islamic world through interactive maps and timelines.
          </p>
        </div>
      </div>
      
      <ScrollArea className="flex-1">
        <div className="max-w-6xl mx-auto p-6 space-y-8">
          {/* Historical Timeline */}
          <div>
            <div className="flex items-center gap-2 mb-4">
              <Clock className="h-5 w-5 text-accent" />
              <h2 className="text-xl font-semibold text-foreground">Historical Timeline</h2>
            </div>
            <div className="space-y-4">
              {historicalPeriods.map((period) => (
                <Card key={period.id} className="cursor-pointer hover:shadow-md transition-shadow">
                  <CardHeader>
                    <div className="flex items-start justify-between">
                      <div className="flex-1">
                        <CardTitle className="text-lg">{period.title}</CardTitle>
                        <CardDescription>{period.description}</CardDescription>
                      </div>
                      <Badge variant="outline" className="ml-4">
                        {period.period}
                      </Badge>
                    </div>
                  </CardHeader>
                  <CardContent>
                    <div className="space-y-3">
                      <div>
                        <p className="text-xs font-semibold text-muted-foreground uppercase mb-2">Key Locations</p>
                        <div className="flex flex-wrap gap-2">
                          {period.locations.map((location) => (
                            <Badge key={location} variant="secondary" className="gap-1">
                              <MapPin className="h-3 w-3" />
                              {location}
                            </Badge>
                          ))}
                        </div>
                      </div>
                      <div>
                        <p className="text-xs font-semibold text-muted-foreground uppercase mb-2">Major Events</p>
                        <ul className="text-sm text-muted-foreground space-y-1">
                          {period.events.slice(0, 3).map((event, idx) => (
                            <li key={idx}>• {event}</li>
                          ))}
                        </ul>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
          </div>

          {/* Significant Sites */}
          <div>
            <div className="flex items-center gap-2 mb-4">
              <Globe className="h-5 w-5 text-accent" />
              <h2 className="text-xl font-semibold text-foreground">Significant Islamic Sites</h2>
            </div>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              {significantSites.map((site, index) => (
                <Card key={index} className="cursor-pointer hover:shadow-md transition-shadow">
                  <CardContent className="p-4">
                    <div className="flex items-start gap-3">
                      <MapPin className="h-5 w-5 text-accent mt-1 flex-shrink-0" />
                      <div className="flex-1">
                        <h3 className="font-semibold text-foreground mb-1">{site.name}</h3>
                        <p className="text-sm text-muted-foreground mb-2">{site.significance}</p>
                        <Badge variant="outline" className="text-xs">
                          {site.country}
                        </Badge>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
          </div>

          <div className="bg-muted/30 rounded-lg p-6 text-center">
            <Globe className="h-12 w-12 mx-auto text-muted-foreground mb-3" weight="thin" />
            <p className="text-muted-foreground">
              Interactive map feature to be implemented with geographic visualization
            </p>
          </div>
        </div>
      </ScrollArea>
    </div>
  )
}
