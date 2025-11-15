import { useState, useEffect } from 'react'
import { apiClient, type Quiz, type QuizQuestion, type QuizAttempt } from '@/lib/api-client'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group'
import { Label } from '@/components/ui/label'
import { Progress } from '@/components/ui/progress'
import { Badge } from '@/components/ui/badge'
import { CheckCircle2, XCircle, Clock, Award } from 'lucide-react'

interface QuizInterfaceProps {
  userId: number
  quizId?: number
}

export function QuizInterface({ userId, quizId }: QuizInterfaceProps) {
  const [quizzes, setQuizzes] = useState<Quiz[]>([])
  const [selectedQuiz, setSelectedQuiz] = useState<Quiz | null>(null)
  const [currentAttempt, setCurrentAttempt] = useState<QuizAttempt | null>(null)
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0)
  const [answers, setAnswers] = useState<Record<number, string>>({})
  const [submitted, setSubmitted] = useState(false)
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    if (quizId) {
      loadQuiz(quizId)
    } else {
      loadQuizzes()
    }
  }, [quizId])

  const loadQuizzes = async () => {
    try {
      setLoading(true)
      const data = await apiClient.getQuizzes()
      setQuizzes(data)
    } catch (error) {
      console.error('Error loading quizzes:', error)
    } finally {
      setLoading(false)
    }
  }

  const loadQuiz = async (id: number) => {
    try {
      setLoading(true)
      const data = await apiClient.getQuiz(id)
      setSelectedQuiz(data)
    } catch (error) {
      console.error('Error loading quiz:', error)
    } finally {
      setLoading(false)
    }
  }

  const startQuiz = async (quiz: Quiz) => {
    try {
      const attempt = await apiClient.startQuiz(quiz.id, userId)
      setCurrentAttempt(attempt)
      setSelectedQuiz(quiz)
      setCurrentQuestionIndex(0)
      setAnswers({})
      setSubmitted(false)
    } catch (error) {
      console.error('Error starting quiz:', error)
    }
  }

  const handleAnswerChange = (questionId: number, answer: string) => {
    setAnswers(prev => ({ ...prev, [questionId]: answer }))
  }

  const submitQuiz = async () => {
    if (!currentAttempt) return

    try {
      const result = await apiClient.submitQuiz(currentAttempt.id, answers)
      setCurrentAttempt(result)
      setSubmitted(true)
    } catch (error) {
      console.error('Error submitting quiz:', error)
    }
  }

  const resetQuiz = () => {
    setSelectedQuiz(null)
    setCurrentAttempt(null)
    setCurrentQuestionIndex(0)
    setAnswers({})
    setSubmitted(false)
    loadQuizzes()
  }

  if (loading) {
    return <div className="text-center py-8">Loading quizzes...</div>
  }

  // Quiz List View
  if (!selectedQuiz) {
    return (
      <div className="space-y-4">
        <div className="flex items-center justify-between">
          <h2 className="text-2xl font-bold">Available Quizzes</h2>
        </div>
        
        {quizzes.length === 0 ? (
          <p className="text-muted-foreground">No quizzes available at this time.</p>
        ) : (
          <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
            {quizzes.map((quiz) => (
              <Card key={quiz.id} className="hover:shadow-lg transition-shadow">
                <CardHeader>
                  <div className="flex items-start justify-between">
                    <CardTitle className="text-lg">{quiz.title}</CardTitle>
                    <Badge variant="outline">{quiz.difficultyLevel}</Badge>
                  </div>
                  <CardDescription>{quiz.description}</CardDescription>
                </CardHeader>
                <CardContent className="space-y-3">
                  <div className="flex items-center justify-between text-sm">
                    <span className="text-muted-foreground">Questions:</span>
                    <span className="font-medium">{quiz.questions?.length || 0}</span>
                  </div>
                  <div className="flex items-center justify-between text-sm">
                    <span className="text-muted-foreground">Passing Score:</span>
                    <span className="font-medium">{quiz.passingScore}%</span>
                  </div>
                  {quiz.timeLimitMinutes > 0 && (
                    <div className="flex items-center justify-between text-sm">
                      <span className="text-muted-foreground">Time Limit:</span>
                      <span className="font-medium flex items-center gap-1">
                        <Clock className="h-3 w-3" />
                        {quiz.timeLimitMinutes} min
                      </span>
                    </div>
                  )}
                  <Button onClick={() => startQuiz(quiz)} className="w-full">
                    Start Quiz
                  </Button>
                </CardContent>
              </Card>
            ))}
          </div>
        )}
      </div>
    )
  }

  // Quiz Results View
  if (submitted && currentAttempt) {
    const passed = currentAttempt.passed
    return (
      <Card>
        <CardHeader>
          <div className="flex items-center gap-3">
            {passed ? (
              <CheckCircle2 className="h-8 w-8 text-green-500" />
            ) : (
              <XCircle className="h-8 w-8 text-red-500" />
            )}
            <div>
              <CardTitle>{passed ? 'Quiz Passed!' : 'Quiz Not Passed'}</CardTitle>
              <CardDescription>Your results for {selectedQuiz.title}</CardDescription>
            </div>
          </div>
        </CardHeader>
        <CardContent className="space-y-6">
          <div className="grid gap-4 md:grid-cols-3">
            <div className="text-center p-4 rounded-lg border">
              <div className="text-3xl font-bold text-primary">{currentAttempt.score.toFixed(1)}%</div>
              <div className="text-sm text-muted-foreground">Your Score</div>
            </div>
            <div className="text-center p-4 rounded-lg border">
              <div className="text-3xl font-bold">{currentAttempt.correctAnswers}/{currentAttempt.totalQuestions}</div>
              <div className="text-sm text-muted-foreground">Correct Answers</div>
            </div>
            <div className="text-center p-4 rounded-lg border">
              <div className="text-3xl font-bold">{Math.floor(currentAttempt.timeTakenSeconds / 60)}:{(currentAttempt.timeTakenSeconds % 60).toString().padStart(2, '0')}</div>
              <div className="text-sm text-muted-foreground">Time Taken</div>
            </div>
          </div>

          {passed && (
            <div className="flex items-center gap-2 p-4 rounded-lg bg-green-50 border border-green-200">
              <Award className="h-5 w-5 text-green-600" />
              <span className="text-green-700 font-medium">
                Congratulations! You've passed this quiz.
              </span>
            </div>
          )}

          <div className="flex gap-3">
            <Button onClick={resetQuiz} variant="outline" className="flex-1">
              Back to Quizzes
            </Button>
            <Button onClick={() => startQuiz(selectedQuiz)} className="flex-1">
              Try Again
            </Button>
          </div>
        </CardContent>
      </Card>
    )
  }

  // Quiz Taking View
  const currentQuestion = selectedQuiz.questions?.[currentQuestionIndex]
  if (!currentQuestion) {
    return <div>No questions available</div>
  }

  const progress = ((currentQuestionIndex + 1) / (selectedQuiz.questions?.length || 1)) * 100
  const options = currentQuestion.options ? JSON.parse(currentQuestion.options) : []

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h2 className="text-2xl font-bold">{selectedQuiz.title}</h2>
        <Badge variant="outline">
          Question {currentQuestionIndex + 1} of {selectedQuiz.questions?.length}
        </Badge>
      </div>

      <Progress value={progress} className="h-2" />

      <Card>
        <CardHeader>
          <CardTitle className="text-lg">{currentQuestion.questionText}</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <RadioGroup
            value={answers[currentQuestion.id] || ''}
            onValueChange={(value) => handleAnswerChange(currentQuestion.id, value)}
          >
            {options.map((option: string, index: number) => (
              <div key={index} className="flex items-center space-x-2 p-3 rounded-lg border hover:bg-accent">
                <RadioGroupItem value={option} id={`option-${index}`} />
                <Label htmlFor={`option-${index}`} className="flex-1 cursor-pointer">
                  {option}
                </Label>
              </div>
            ))}
          </RadioGroup>

          <div className="flex gap-3 pt-4">
            {currentQuestionIndex > 0 && (
              <Button
                variant="outline"
                onClick={() => setCurrentQuestionIndex(prev => prev - 1)}
              >
                Previous
              </Button>
            )}
            
            {currentQuestionIndex < (selectedQuiz.questions?.length || 0) - 1 ? (
              <Button
                onClick={() => setCurrentQuestionIndex(prev => prev + 1)}
                className="flex-1"
                disabled={!answers[currentQuestion.id]}
              >
                Next Question
              </Button>
            ) : (
              <Button
                onClick={submitQuiz}
                className="flex-1"
                disabled={Object.keys(answers).length !== selectedQuiz.questions?.length}
              >
                Submit Quiz
              </Button>
            )}
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
