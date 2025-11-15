import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { BookmarkSimple, SpeakerHigh, Copy } from "@phosphor-icons/react"

interface QuranVerseProps {
  surahNumber: number
  ayahNumber: number
  arabicText: string
  translation: string
  showArabic?: boolean
  translationLanguage?: string
}

export function QuranVerse({
  surahNumber,
  ayahNumber,
  arabicText,
  translation,
  showArabic = true,
  translationLanguage = "English"
}: QuranVerseProps) {
  const handleBookmark = () => {
    // TODO: Implement bookmark functionality
    console.log('Bookmark verse:', surahNumber, ayahNumber)
  }

  const handlePlayAudio = () => {
    // TODO: Implement audio playback
    console.log('Play audio for verse:', surahNumber, ayahNumber)
  }

  const handleCopy = () => {
    const text = showArabic ? `${arabicText}\n\n${translation}` : translation
    navigator.clipboard.writeText(text)
  }

  return (
    <Card className="verse-card mb-4">
      <CardHeader>
        <CardTitle className="text-sm font-medium text-muted-foreground">
          Surah {surahNumber}, Ayah {ayahNumber}
        </CardTitle>
      </CardHeader>
      <CardContent className="space-y-4">
        {showArabic && (
          <div 
            className="arabic-text text-right text-2xl leading-loose font-arabic"
            dir="rtl"
            lang="ar"
          >
            {arabicText}
          </div>
        )}
        <div className="text-foreground leading-relaxed">
          {translation}
        </div>
        <div className="flex gap-2 pt-2">
          <Button
            variant="ghost"
            size="sm"
            onClick={handleBookmark}
            className="gap-2"
          >
            <BookmarkSimple className="h-4 w-4" />
            Bookmark
          </Button>
          <Button
            variant="ghost"
            size="sm"
            onClick={handlePlayAudio}
            className="gap-2"
          >
            <SpeakerHigh className="h-4 w-4" />
            Listen
          </Button>
          <Button
            variant="ghost"
            size="sm"
            onClick={handleCopy}
            className="gap-2"
          >
            <Copy className="h-4 w-4" />
            Copy
          </Button>
        </div>
      </CardContent>
    </Card>
  )
}
