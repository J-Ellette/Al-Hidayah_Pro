import { BookOpen } from "@phosphor-icons/react"
import { HadithCard } from "@/components/islamic/HadithCard"
import { Bismillah } from "@/components/islamic/Bismillah"
import { IslamicPatternDecorator } from "@/components/islamic/IslamicPatternDecorator"
import { ScrollArea } from "@/components/ui/scroll-area"

// Sample hadiths for demonstration
const sampleHadiths = [
  {
    collection: "Sahih Bukhari",
    book: "Book of Faith",
    hadithNumber: 1,
    text: "Allah's Messenger (ﷺ) said, \"Islam is based on five principles: To testify that none has the right to be worshipped but Allah and Muhammad is Allah's Messenger, to offer the (compulsory congregational) prayers dutifully and perfectly, to pay Zakat (i.e., obligatory charity), to perform Hajj (i.e., Pilgrimage to Mecca), and to observe fast during the month of Ramadan.\"",
    grade: "Sahih" as const,
    narrator: "Ibn Umar"
  },
  {
    collection: "Sahih Muslim",
    book: "Book of Faith",
    hadithNumber: 8,
    text: "The Messenger of Allah (ﷺ) said: \"I have been commanded to fight against people till they testify that there is no god but Allah, that Muhammad is the messenger of Allah, and they establish prayer and pay Zakat; and if they do it, their blood and property are guaranteed protection on my behalf except when justified by law, and their affairs rest with Allah.\"",
    grade: "Sahih" as const,
    narrator: "Ibn Umar"
  },
  {
    collection: "Sunan Abu Dawood",
    book: "Book of Prayer",
    hadithNumber: 425,
    text: "The Prophet (ﷺ) said: \"The key to Paradise is prayer, and the key to prayer is cleanliness.\"",
    grade: "Hasan" as const,
    narrator: "Jabir bin Abdullah"
  }
]

export function HadithPage() {
  return (
    <div className="flex-1 flex flex-col bg-background overflow-hidden">
      <IslamicPatternDecorator variant="subtle">
        <div className="border-b border-border p-6 border-l-4 border-l-aegold-500">
          <div className="max-w-4xl mx-auto">
            <div className="flex items-center gap-4 mb-4">
              <BookOpen className="h-8 w-8 text-aegold-600" weight="duotone" />
              <h1 className="text-3xl font-semibold text-foreground gold-accent">
                <span className="font-arabic ml-2">الحديث النبوي</span>
                <span className="ml-2">Hadith Collections</span>
              </h1>
            </div>
            <p className="text-muted-foreground">
              Explore authenticated narrations from the Prophet Muhammad (ﷺ) from various collections.
            </p>
          </div>
        </div>
      </IslamicPatternDecorator>
      
      <ScrollArea className="flex-1">
        <div className="max-w-4xl mx-auto p-6 space-y-4">
          {/* Bismillah Header */}
          <Bismillah showTranslation={false} size="md" />
          
          <div className="mb-6 islamic-border">
            <h2 className="text-xl font-semibold text-foreground mb-2">Featured Hadiths</h2>
            <p className="text-sm text-muted-foreground">Selected authentic narrations from major collections</p>
          </div>
          
          {sampleHadiths.map((hadith, index) => (
            <div key={`${hadith.collection}-${hadith.hadithNumber}-${index}`} className="hadith-card-islamic">
              <HadithCard
                {...hadith}
              />
            </div>
          ))}
        </div>
      </ScrollArea>
    </div>
  )
}
