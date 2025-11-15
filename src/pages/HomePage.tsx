import { Book, Books, BookOpen, Compass, Wrench } from "@phosphor-icons/react"
import { useNavigate } from "react-router-dom"

export function HomePage() {
  const navigate = useNavigate()

  const features = [
    {
      id: 'quran',
      icon: Book,
      title: 'Quran',
      description: 'Read and study the Holy Quran',
      path: '/quran'
    },
    {
      id: 'hadith',
      icon: BookOpen,
      title: 'Hadith',
      description: 'Explore Hadith collections',
      path: '/hadith'
    },
    {
      id: 'guides',
      icon: Compass,
      title: 'Guides',
      description: 'Learn Islamic practices',
      path: '/guides'
    },
    {
      id: 'tools',
      icon: Wrench,
      title: 'Tools',
      description: 'Prayer times and more',
      path: '/tools'
    }
  ]

  return (
    <div className="flex-1 flex flex-col items-center justify-center bg-background p-8 overflow-auto">
      <div className="max-w-2xl text-center space-y-6">
        <Books className="h-24 w-24 mx-auto text-accent/40" weight="thin" />
        <h1 className="text-4xl font-semibold text-foreground">Welcome to Al-Hidayah Pro</h1>
        <p className="text-lg text-muted-foreground leading-relaxed">
          Your gateway to comprehensive Islamic knowledge. Explore the Quran, Hadith collections, study guides, historical atlas, and essential tools for Islamic learning.
        </p>
        
        <div className="grid grid-cols-2 gap-4 mt-12">
          {features.map((feature) => {
            const Icon = feature.icon
            return (
              <div 
                key={feature.id}
                className="p-6 bg-card rounded-lg border border-border hover:border-accent/50 transition-colors cursor-pointer"
                onClick={() => navigate(feature.path)}
              >
                <Icon className="h-8 w-8 text-accent mb-3" />
                <h3 className="font-medium text-foreground mb-1">{feature.title}</h3>
                <p className="text-sm text-muted-foreground">{feature.description}</p>
              </div>
            )
          })}
        </div>
      </div>
    </div>
  )
}
