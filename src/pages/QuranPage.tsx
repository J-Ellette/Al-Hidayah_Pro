import { Book } from "@phosphor-icons/react"
import { QuranVerse } from "@/components/islamic/QuranVerse"
import { RecitationPlayer } from "@/components/islamic/RecitationPlayer"
import { Bismillah } from "@/components/islamic/Bismillah"
import { IslamicPatternDecorator } from "@/components/islamic/IslamicPatternDecorator"
import { ScrollArea } from "@/components/ui/scroll-area"

// Sample verses for demonstration
const sampleVerses = [
  {
    surahNumber: 1,
    ayahNumber: 1,
    arabicText: "بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ",
    translation: "In the name of Allah, the Entirely Merciful, the Especially Merciful."
  },
  {
    surahNumber: 1,
    ayahNumber: 2,
    arabicText: "الْحَمْدُ لِلَّهِ رَبِّ الْعَالَمِينَ",
    translation: "All praise is due to Allah, Lord of the worlds."
  },
  {
    surahNumber: 1,
    ayahNumber: 3,
    arabicText: "الرَّحْمَٰنِ الرَّحِيمِ",
    translation: "The Entirely Merciful, the Especially Merciful."
  },
  {
    surahNumber: 1,
    ayahNumber: 4,
    arabicText: "مَالِكِ يَوْمِ الدِّينِ",
    translation: "Sovereign of the Day of Recompense."
  },
  {
    surahNumber: 1,
    ayahNumber: 5,
    arabicText: "إِيَّاكَ نَعْبُدُ وَإِيَّاكَ نَسْتَعِينُ",
    translation: "It is You we worship and You we ask for help."
  },
  {
    surahNumber: 1,
    ayahNumber: 6,
    arabicText: "اهْدِنَا الصِّرَاطَ الْمُسْتَقِيمَ",
    translation: "Guide us to the straight path."
  },
  {
    surahNumber: 1,
    ayahNumber: 7,
    arabicText: "صِرَاطَ الَّذِينَ أَنْعَمْتَ عَلَيْهِمْ غَيْرِ الْمَغْضُوبِ عَلَيْهِمْ وَلَا الضَّالِّينَ",
    translation: "The path of those upon whom You have bestowed favor, not of those who have earned [Your] anger or of those who are astray."
  }
]

export function QuranPage() {
  return (
    <div className="flex-1 flex flex-col bg-background overflow-hidden">
      <IslamicPatternDecorator variant="subtle">
        <div className="border-b border-border p-6 border-l-4 border-l-aegreen-500">
          <div className="max-w-4xl mx-auto">
            <div className="flex items-center gap-4 mb-4">
              <Book className="h-8 w-8 text-aegreen-600" weight="duotone" />
              <h1 className="text-3xl font-semibold text-foreground gold-accent">القرآن الكريم</h1>
              <span className="text-2xl font-semibold text-foreground ml-2">The Holy Quran</span>
            </div>
            <p className="text-muted-foreground">
              Read and study the complete Quran with translations, tafsir, and recitations.
            </p>
          </div>
        </div>
      </IslamicPatternDecorator>
      
      <ScrollArea className="flex-1">
        <div className="max-w-4xl mx-auto p-6 space-y-4">
          {/* Bismillah Header */}
          <Bismillah showTranslation={true} size="lg" />
          
          <div className="mb-6 islamic-border">
            <div className="flex items-center justify-between">
              <div>
                <h2 className="text-xl font-semibold text-foreground mb-2">
                  <span className="font-arabic text-2xl text-aegreen-700 ml-2" dir="rtl">سورة الفاتحة</span>
                  <span className="ml-2">Surah Al-Fatihah</span>
                  <span className="text-muted-foreground ml-2">(The Opening)</span>
                </h2>
                <p className="text-sm text-muted-foreground">7 verses • Meccan • Juz 1</p>
              </div>
              <div className="verse-number text-lg">1</div>
            </div>
          </div>

          {/* Audio Player for the entire Surah */}
          <RecitationPlayer surahNumber={1} className="mb-6 islamic-card" />
          
          {sampleVerses.map((verse) => (
            <div key={`${verse.surahNumber}-${verse.ayahNumber}`} className="verse-card-islamic">
              <QuranVerse
                {...verse}
                showArabic={true}
              />
            </div>
          ))}
        </div>
      </ScrollArea>
    </div>
  )
}
