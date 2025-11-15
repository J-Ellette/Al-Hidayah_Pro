import { Compass, Heart, Mosque, Moon, Path, Globe } from "@phosphor-icons/react"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { ScrollArea } from "@/components/ui/scroll-area"
import { Badge } from "@/components/ui/badge"

const guides = [
  {
    id: 'five-pillars',
    title: 'The Five Pillars of Islam',
    icon: Mosque,
    description: 'Understanding the fundamental practices of Islam',
    topics: ['Shahada', 'Salah', 'Zakat', 'Sawm', 'Hajj'],
    level: 'Beginner',
    color: 'text-green-600'
  },
  {
    id: 'beliefs',
    title: 'Articles of Faith (Iman)',
    icon: Heart,
    description: 'Six pillars of Islamic belief and creed',
    topics: ['Allah', 'Angels', 'Books', 'Prophets', 'Day of Judgment', 'Divine Decree'],
    level: 'Beginner',
    color: 'text-blue-600'
  },
  {
    id: 'prayer',
    title: 'How to Pray (Salah)',
    icon: Compass,
    description: 'Step-by-step guide to performing Islamic prayer',
    topics: ['Wudu', 'Prayer Times', 'Movements', 'Recitations'],
    level: 'Beginner',
    color: 'text-purple-600'
  },
  {
    id: 'ramadan',
    title: 'Guide to Ramadan',
    icon: Moon,
    description: 'Fasting, worship, and spiritual growth during the holy month',
    topics: ['Fasting Rules', 'Taraweeh', 'Laylatul Qadr', 'Eid al-Fitr'],
    level: 'Intermediate',
    color: 'text-indigo-600'
  },
  {
    id: 'new-muslim',
    title: 'New Muslim Guide',
    icon: Path,
    description: 'Essential knowledge for those new to Islam',
    topics: ['Basics', 'Daily Practice', 'Community', 'Resources'],
    level: 'Beginner',
    color: 'text-orange-600'
  },
  {
    id: 'history',
    title: 'Islamic History Overview',
    icon: Globe,
    description: 'Journey through Islamic civilization and heritage',
    topics: ['Prophet Muhammad', 'Caliphates', 'Golden Age', 'Modern Era'],
    level: 'Intermediate',
    color: 'text-amber-600'
  }
]

const levelColors = {
  'Beginner': 'bg-green-500/10 text-green-700 border-green-500/20',
  'Intermediate': 'bg-blue-500/10 text-blue-700 border-blue-500/20',
  'Advanced': 'bg-purple-500/10 text-purple-700 border-purple-500/20'
}

export function GuidesPage() {
  return (
    <div className="flex-1 flex flex-col bg-background overflow-hidden">
      <div className="border-b border-border p-6">
        <div className="max-w-6xl mx-auto">
          <div className="flex items-center gap-4 mb-4">
            <Compass className="h-8 w-8 text-accent" weight="duotone" />
            <h1 className="text-3xl font-semibold text-foreground">Study Guides</h1>
          </div>
          <p className="text-muted-foreground">
            Comprehensive guides for learning about Islamic practices, beliefs, and history.
          </p>
        </div>
      </div>
      
      <ScrollArea className="flex-1">
        <div className="max-w-6xl mx-auto p-6">
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {guides.map((guide) => {
              const Icon = guide.icon
              return (
                <Card key={guide.id} className="cursor-pointer hover:shadow-md transition-shadow">
                  <CardHeader>
                    <div className="flex items-start justify-between mb-2">
                      <div className={`p-3 rounded-lg bg-accent/10 ${guide.color}`}>
                        <Icon className="h-6 w-6" weight="duotone" />
                      </div>
                      <Badge variant="outline" className={levelColors[guide.level]}>
                        {guide.level}
                      </Badge>
                    </div>
                    <CardTitle className="text-lg">{guide.title}</CardTitle>
                    <CardDescription>{guide.description}</CardDescription>
                  </CardHeader>
                  <CardContent>
                    <div className="space-y-2">
                      <p className="text-xs font-semibold text-muted-foreground uppercase">Topics Covered</p>
                      <div className="flex flex-wrap gap-2">
                        {guide.topics.map((topic) => (
                          <Badge key={topic} variant="secondary" className="text-xs">
                            {topic}
                          </Badge>
                        ))}
                      </div>
                    </div>
                  </CardContent>
                </Card>
              )
            })}
          </div>
        </div>
      </ScrollArea>
    </div>
  )
}
