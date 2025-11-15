import { useState, useEffect } from 'react'
import { apiClient, type FlashCard } from '@/lib/api-client'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Badge } from '@/components/ui/badge'
import { RotateCcw, Check } from 'lucide-react'

interface FlashCardReviewProps {
  userId: number
  category?: string
}

export function FlashCardReview({ userId, category }: FlashCardReviewProps) {
  const [dueCards, setDueCards] = useState<FlashCard[]>([])
  const [currentCardIndex, setCurrentCardIndex] = useState(0)
  const [showBack, setShowBack] = useState(false)
  const [loading, setLoading] = useState(true)
  const [reviewComplete, setReviewComplete] = useState(false)

  useEffect(() => {
    loadDueCards()
  }, [userId, category])

  const loadDueCards = async () => {
    try {
      setLoading(true)
      const cards = await apiClient.getDueFlashCards(userId, 20)
      setDueCards(cards)
      setReviewComplete(cards.length === 0)
    } catch (error) {
      console.error('Error loading flashcards:', error)
    } finally {
      setLoading(false)
    }
  }

  const handleReview = async (quality: number) => {
    const currentCard = dueCards[currentCardIndex]
    if (!currentCard) return

    try {
      await apiClient.reviewFlashCard(currentCard.id, userId, quality)
      
      // Move to next card
      if (currentCardIndex < dueCards.length - 1) {
        setCurrentCardIndex(prev => prev + 1)
        setShowBack(false)
      } else {
        setReviewComplete(true)
      }
    } catch (error) {
      console.error('Error reviewing card:', error)
    }
  }

  if (loading) {
    return <div className="text-center py-8">Loading flashcards...</div>
  }

  if (reviewComplete || dueCards.length === 0) {
    return (
      <Card>
        <CardHeader>
          <CardTitle>Review Complete!</CardTitle>
          <CardDescription>No more cards due for review right now</CardDescription>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="text-center py-8">
            <Check className="h-16 w-16 text-green-500 mx-auto mb-4" />
            <p className="text-lg font-medium">Great job!</p>
            <p className="text-muted-foreground">
              You've reviewed all your due cards. Come back later for more.
            </p>
          </div>
          <Button onClick={loadDueCards} variant="outline" className="w-full">
            Check Again
          </Button>
        </CardContent>
      </Card>
    )
  }

  const currentCard = dueCards[currentCardIndex]
  const progress = ((currentCardIndex + 1) / dueCards.length) * 100

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h2 className="text-2xl font-bold">Flashcard Review</h2>
        <div className="flex items-center gap-3">
          <Badge variant="outline">
            {currentCardIndex + 1} / {dueCards.length}
          </Badge>
          <Badge variant="secondary">{currentCard.category}</Badge>
        </div>
      </div>

      <div className="w-full bg-secondary rounded-full h-2">
        <div
          className="bg-primary h-2 rounded-full transition-all duration-300"
          style={{ width: `${progress}%` }}
        />
      </div>

      <Card className="min-h-[400px] flex flex-col">
        <CardHeader>
          <CardTitle className="text-sm text-muted-foreground">
            {showBack ? 'Answer' : 'Question'}
          </CardTitle>
          {currentCard.reference && (
            <CardDescription>{currentCard.reference}</CardDescription>
          )}
        </CardHeader>
        <CardContent className="flex-1 flex flex-col">
          <div className="flex-1 flex items-center justify-center text-center p-8">
            <div className="space-y-4">
              <p className="text-2xl font-medium leading-relaxed">
                {showBack ? currentCard.back : currentCard.front}
              </p>
              {showBack && currentCard.notes && (
                <p className="text-sm text-muted-foreground">{currentCard.notes}</p>
              )}
            </div>
          </div>

          {!showBack ? (
            <Button
              onClick={() => setShowBack(true)}
              className="w-full"
              size="lg"
            >
              <RotateCcw className="mr-2 h-4 w-4" />
              Show Answer
            </Button>
          ) : (
            <div className="space-y-3">
              <p className="text-sm text-center text-muted-foreground mb-2">
                How well did you remember this?
              </p>
              <div className="grid grid-cols-2 gap-3">
                <Button
                  onClick={() => handleReview(0)}
                  variant="destructive"
                  size="lg"
                >
                  Again
                  <span className="text-xs ml-1">(0)</span>
                </Button>
                <Button
                  onClick={() => handleReview(3)}
                  variant="outline"
                  size="lg"
                >
                  Hard
                  <span className="text-xs ml-1">(3)</span>
                </Button>
                <Button
                  onClick={() => handleReview(4)}
                  variant="secondary"
                  size="lg"
                >
                  Good
                  <span className="text-xs ml-1">(4)</span>
                </Button>
                <Button
                  onClick={() => handleReview(5)}
                  variant="default"
                  size="lg"
                  className="bg-green-600 hover:bg-green-700"
                >
                  Easy
                  <span className="text-xs ml-1">(5)</span>
                </Button>
              </div>
              <p className="text-xs text-center text-muted-foreground">
                Your rating helps schedule the next review using spaced repetition
              </p>
            </div>
          )}
        </CardContent>
      </Card>

      <div className="grid grid-cols-3 gap-4 text-center text-sm">
        <div>
          <div className="font-medium">New</div>
          <div className="text-muted-foreground">
            {dueCards.filter(c => !c.isActive).length}
          </div>
        </div>
        <div>
          <div className="font-medium">Learning</div>
          <div className="text-muted-foreground">
            {currentCardIndex}
          </div>
        </div>
        <div>
          <div className="font-medium">Remaining</div>
          <div className="text-muted-foreground">
            {dueCards.length - currentCardIndex - 1}
          </div>
        </div>
      </div>
    </div>
  )
}
