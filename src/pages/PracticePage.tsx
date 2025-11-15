import { Target } from 'lucide-react'
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs'
import { QuizInterface } from '@/components/learning/QuizInterface'
import { FlashCardReview } from '@/components/learning/FlashCardReview'

export function PracticePage() {
  // In a real app, this would come from authentication
  const userId = 1

  return (
    <div className="flex-1 overflow-auto p-8">
      <div className="max-w-5xl mx-auto">
        <div className="mb-8">
          <div className="flex items-center gap-3 mb-2">
            <Target className="h-8 w-8 text-primary" />
            <h1 className="text-3xl font-bold">Practice & Review</h1>
          </div>
          <p className="text-muted-foreground">
            Test your knowledge with quizzes and reinforce learning with flashcards
          </p>
        </div>

        <Tabs defaultValue="quizzes" className="w-full">
          <TabsList className="grid w-full max-w-md grid-cols-2">
            <TabsTrigger value="quizzes">Quizzes</TabsTrigger>
            <TabsTrigger value="flashcards">Flashcards</TabsTrigger>
          </TabsList>

          <TabsContent value="quizzes" className="mt-6">
            <QuizInterface userId={userId} />
          </TabsContent>

          <TabsContent value="flashcards" className="mt-6">
            <FlashCardReview userId={userId} />
          </TabsContent>
        </Tabs>
      </div>
    </div>
  )
}
