import { useEffect, useState } from 'react'
import { apiClient, type UserProgress, type UserAchievement } from '@/lib/api-client'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Progress } from '@/components/ui/progress'
import { Badge } from '@/components/ui/badge'
import { Award, BookOpen, Clock, Flame, Star, TrendingUp } from 'lucide-react'

interface ProgressDashboardProps {
  userId: number
}

export function ProgressDashboard({ userId }: ProgressDashboardProps) {
  const [progress, setProgress] = useState<UserProgress | null>(null)
  const [achievements, setAchievements] = useState<UserAchievement[]>([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    loadData()
  }, [userId])

  const loadData = async () => {
    try {
      setLoading(true)
      const [progressData, achievementsData] = await Promise.all([
        apiClient.getUserProgress(userId),
        apiClient.getUserAchievements(userId)
      ])
      setProgress(progressData)
      setAchievements(achievementsData)
    } catch (error) {
      console.error('Error loading progress:', error)
    } finally {
      setLoading(false)
    }
  }

  if (loading) {
    return <div className="text-center py-8">Loading progress...</div>
  }

  if (!progress) {
    return <div className="text-center py-8">No progress data available</div>
  }

  const levelProgress = (progress.experiencePoints % 1000) / 10 // Simple level calculation

  return (
    <div className="space-y-6">
      {/* Header Stats */}
      <div className="grid gap-4 md:grid-cols-4">
        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Level</CardTitle>
            <Star className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">{progress.level}</div>
            <Progress value={levelProgress} className="mt-2" />
            <p className="text-xs text-muted-foreground mt-1">
              {progress.experiencePoints} XP
            </p>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Total Points</CardTitle>
            <TrendingUp className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">{progress.totalPoints}</div>
            <p className="text-xs text-muted-foreground">
              Keep learning to earn more!
            </p>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Current Streak</CardTitle>
            <Flame className="h-4 w-4 text-orange-500" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">{progress.currentStreak} days</div>
            <p className="text-xs text-muted-foreground">
              Best: {progress.longestStreak} days
            </p>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Study Time</CardTitle>
            <Clock className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">
              {Math.floor(progress.totalStudyMinutes / 60)}h {progress.totalStudyMinutes % 60}m
            </div>
            <p className="text-xs text-muted-foreground">
              Total study time
            </p>
          </CardContent>
        </Card>
      </div>

      {/* Learning Stats */}
      <Card>
        <CardHeader>
          <CardTitle>Learning Statistics</CardTitle>
          <CardDescription>Your Islamic learning journey</CardDescription>
        </CardHeader>
        <CardContent>
          <div className="grid gap-4 md:grid-cols-3">
            <div className="space-y-2">
              <div className="flex items-center justify-between">
                <span className="text-sm font-medium">Verses Read</span>
                <BookOpen className="h-4 w-4 text-muted-foreground" />
              </div>
              <div className="text-2xl font-bold">{progress.versesRead}</div>
              <Progress value={Math.min((progress.versesRead / 6236) * 100, 100)} />
              <p className="text-xs text-muted-foreground">of 6,236 verses in Quran</p>
            </div>

            <div className="space-y-2">
              <div className="flex items-center justify-between">
                <span className="text-sm font-medium">Verses Memorized</span>
                <Star className="h-4 w-4 text-yellow-500" />
              </div>
              <div className="text-2xl font-bold">{progress.versesMemorized}</div>
              <p className="text-xs text-muted-foreground">Keep memorizing!</p>
            </div>

            <div className="space-y-2">
              <div className="flex items-center justify-between">
                <span className="text-sm font-medium">Hadiths Studied</span>
                <BookOpen className="h-4 w-4 text-muted-foreground" />
              </div>
              <div className="text-2xl font-bold">{progress.hadithsStudied}</div>
              <p className="text-xs text-muted-foreground">Prophetic guidance</p>
            </div>

            <div className="space-y-2">
              <div className="flex items-center justify-between">
                <span className="text-sm font-medium">Lessons Completed</span>
                <Award className="h-4 w-4 text-muted-foreground" />
              </div>
              <div className="text-2xl font-bold">{progress.lessonsCompleted}</div>
              <p className="text-xs text-muted-foreground">Learning progress</p>
            </div>

            <div className="space-y-2">
              <div className="flex items-center justify-between">
                <span className="text-sm font-medium">Quizzes Completed</span>
                <TrendingUp className="h-4 w-4 text-muted-foreground" />
              </div>
              <div className="text-2xl font-bold">{progress.quizzesCompleted}</div>
              <p className="text-xs text-muted-foreground">
                Avg: {progress.averageQuizScore.toFixed(1)}%
              </p>
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Achievements */}
      <Card>
        <CardHeader>
          <CardTitle>Recent Achievements</CardTitle>
          <CardDescription>
            {achievements.length} achievement{achievements.length !== 1 ? 's' : ''} earned
          </CardDescription>
        </CardHeader>
        <CardContent>
          {achievements.length === 0 ? (
            <p className="text-sm text-muted-foreground">No achievements yet. Keep learning!</p>
          ) : (
            <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
              {achievements.slice(0, 6).map((ua) => (
                <div key={ua.id} className="flex items-start space-x-3 p-3 rounded-lg border">
                  <Award className={`h-8 w-8 ${getTierColor(ua.achievement?.tier)} flex-shrink-0`} />
                  <div className="flex-1 min-w-0">
                    <p className="text-sm font-medium truncate">{ua.achievement?.title}</p>
                    <p className="text-xs text-muted-foreground line-clamp-2">
                      {ua.achievement?.description}
                    </p>
                    <div className="flex items-center gap-2 mt-1">
                      <Badge variant="outline" className="text-xs">
                        {ua.achievement?.points} pts
                      </Badge>
                      <Badge variant="secondary" className="text-xs">
                        {ua.achievement?.tier}
                      </Badge>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  )
}

function getTierColor(tier?: string): string {
  switch (tier) {
    case 'platinum':
      return 'text-purple-500'
    case 'gold':
      return 'text-yellow-500'
    case 'silver':
      return 'text-gray-400'
    case 'bronze':
      return 'text-orange-700'
    default:
      return 'text-muted-foreground'
  }
}
