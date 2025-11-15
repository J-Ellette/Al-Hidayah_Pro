import { Books, Book, BookOpen, Scroll, GraduationCap } from "@phosphor-icons/react"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { ScrollArea } from "@/components/ui/scroll-area"
import { Button } from "@/components/ui/button"

const libraryCollections = [
  {
    id: 'quran',
    title: 'The Holy Quran',
    icon: Book,
    description: 'Complete Quran with multiple translations and tafsir',
    itemCount: '114 Surahs',
    color: 'text-green-600'
  },
  {
    id: 'hadith',
    title: 'Hadith Collections',
    icon: BookOpen,
    description: 'Major authentic hadith compilations',
    itemCount: '6 Collections',
    color: 'text-blue-600'
  },
  {
    id: 'tafsir',
    title: 'Quranic Commentary (Tafsir)',
    icon: Scroll,
    description: 'Classical and contemporary explanations',
    itemCount: '10+ Commentaries',
    color: 'text-purple-600'
  },
  {
    id: 'fiqh',
    title: 'Islamic Jurisprudence (Fiqh)',
    icon: GraduationCap,
    description: 'Legal rulings and scholarly works',
    itemCount: '4 Schools',
    color: 'text-orange-600'
  }
]

const recentlyViewed = [
  { title: 'Surah Al-Fatihah', type: 'Quran', lastAccessed: '2 hours ago' },
  { title: 'Sahih Bukhari - Book of Faith', type: 'Hadith', lastAccessed: '1 day ago' },
  { title: 'Tafsir Ibn Kathir - Surah Al-Baqarah', type: 'Tafsir', lastAccessed: '3 days ago' }
]

export function LibraryPage() {
  return (
    <div className="flex-1 flex flex-col bg-background overflow-hidden">
      <div className="border-b border-border p-6">
        <div className="max-w-6xl mx-auto">
          <div className="flex items-center gap-4 mb-4">
            <Books className="h-8 w-8 text-accent" weight="duotone" />
            <h1 className="text-3xl font-semibold text-foreground">Islamic Library</h1>
          </div>
          <p className="text-muted-foreground">
            Access a comprehensive collection of Islamic texts, scholarly works, and classical literature.
          </p>
        </div>
      </div>
      
      <ScrollArea className="flex-1">
        <div className="max-w-6xl mx-auto p-6 space-y-8">
          {/* Main Collections */}
          <div>
            <h2 className="text-xl font-semibold text-foreground mb-4">Main Collections</h2>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              {libraryCollections.map((collection) => {
                const Icon = collection.icon
                return (
                  <Card key={collection.id} className="cursor-pointer hover:shadow-md transition-shadow">
                    <CardHeader>
                      <div className="flex items-start gap-4">
                        <div className={`p-3 rounded-lg bg-accent/10 ${collection.color}`}>
                          <Icon className="h-6 w-6" weight="duotone" />
                        </div>
                        <div className="flex-1">
                          <CardTitle className="text-lg">{collection.title}</CardTitle>
                          <CardDescription>{collection.description}</CardDescription>
                        </div>
                      </div>
                    </CardHeader>
                    <CardContent>
                      <div className="flex items-center justify-between">
                        <span className="text-sm text-muted-foreground">{collection.itemCount}</span>
                        <Button variant="ghost" size="sm">
                          Browse →
                        </Button>
                      </div>
                    </CardContent>
                  </Card>
                )
              })}
            </div>
          </div>

          {/* Recently Viewed */}
          <div>
            <h2 className="text-xl font-semibold text-foreground mb-4">Recently Viewed</h2>
            <Card>
              <CardContent className="p-0">
                {recentlyViewed.map((item, index) => (
                  <div 
                    key={index}
                    className="flex items-center justify-between p-4 hover:bg-accent/5 transition-colors cursor-pointer border-b last:border-b-0"
                  >
                    <div>
                      <p className="font-medium text-foreground">{item.title}</p>
                      <p className="text-sm text-muted-foreground">{item.type}</p>
                    </div>
                    <span className="text-xs text-muted-foreground">{item.lastAccessed}</span>
                  </div>
                ))}
              </CardContent>
            </Card>
          </div>

          {/* Quick Access */}
          <div>
            <h2 className="text-xl font-semibold text-foreground mb-4">Quick Access</h2>
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              <Button variant="outline" className="h-auto flex-col gap-2 py-4">
                <Book className="h-6 w-6" />
                <span className="text-sm">Quran Reader</span>
              </Button>
              <Button variant="outline" className="h-auto flex-col gap-2 py-4">
                <BookOpen className="h-6 w-6" />
                <span className="text-sm">Hadith Search</span>
              </Button>
              <Button variant="outline" className="h-auto flex-col gap-2 py-4">
                <Scroll className="h-6 w-6" />
                <span className="text-sm">Tafsir Library</span>
              </Button>
              <Button variant="outline" className="h-auto flex-col gap-2 py-4">
                <GraduationCap className="h-6 w-6" />
                <span className="text-sm">Study Tools</span>
              </Button>
            </div>
          </div>
        </div>
      </ScrollArea>
    </div>
  )
}
