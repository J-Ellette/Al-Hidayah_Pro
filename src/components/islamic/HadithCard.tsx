import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import { BookmarkSimple, Copy } from "@phosphor-icons/react"

interface HadithCardProps {
  collection: string
  book: string
  hadithNumber: number
  text: string
  arabicText?: string
  grade?: 'Sahih' | 'Hasan' | 'Daif' | 'Mawdu'
  narrator?: string
  showArabic?: boolean
}

const gradeColors = {
  'Sahih': 'bg-green-500/10 text-green-700 border-green-500/20',
  'Hasan': 'bg-blue-500/10 text-blue-700 border-green-500/20',
  'Daif': 'bg-yellow-500/10 text-yellow-700 border-yellow-500/20',
  'Mawdu': 'bg-red-500/10 text-red-700 border-red-500/20'
}

export function HadithCard({
  collection,
  book,
  hadithNumber,
  text,
  arabicText,
  grade = 'Sahih',
  narrator,
  showArabic = false
}: HadithCardProps) {
  const handleBookmark = () => {
    // TODO: Implement bookmark functionality
    console.log('Bookmark hadith:', collection, hadithNumber)
  }

  const handleCopy = () => {
    const copyText = showArabic && arabicText 
      ? `${arabicText}\n\n${text}\n\n${collection} - ${book}, Hadith ${hadithNumber}`
      : `${text}\n\n${collection} - ${book}, Hadith ${hadithNumber}`
    navigator.clipboard.writeText(copyText)
  }

  return (
    <Card className="hadith-card mb-4">
      <CardHeader>
        <div className="flex items-center justify-between">
          <CardTitle className="text-sm font-medium text-muted-foreground">
            {collection} - {book}
          </CardTitle>
          <Badge variant="outline" className={gradeColors[grade]}>
            {grade}
          </Badge>
        </div>
        {narrator && (
          <p className="text-xs text-muted-foreground mt-1">
            Narrated by {narrator}
          </p>
        )}
      </CardHeader>
      <CardContent className="space-y-4">
        {showArabic && arabicText && (
          <div 
            className="arabic-text text-right text-xl leading-loose font-arabic"
            dir="rtl"
            lang="ar"
          >
            {arabicText}
          </div>
        )}
        <div className="text-foreground leading-relaxed">
          {text}
        </div>
        <div className="flex items-center justify-between pt-2">
          <span className="text-xs text-muted-foreground">
            Hadith #{hadithNumber}
          </span>
          <div className="flex gap-2">
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
              onClick={handleCopy}
              className="gap-2"
            >
              <Copy className="h-4 w-4" />
              Copy
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}
