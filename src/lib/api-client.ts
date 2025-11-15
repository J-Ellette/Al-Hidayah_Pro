/**
 * API Client for Al-Hidayah Pro Backend
 * Handles all communication with the C# .NET backend API
 */

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api'

interface ApiError {
  message: string
  status?: number
}

class ApiClient {
  private baseUrl: string

  constructor(baseUrl: string = API_BASE_URL) {
    this.baseUrl = baseUrl
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<T> {
    const url = `${this.baseUrl}${endpoint}`
    
    try {
      const response = await fetch(url, {
        ...options,
        headers: {
          'Content-Type': 'application/json',
          ...options.headers,
        },
      })

      if (!response.ok) {
        const error: ApiError = {
          message: `API Error: ${response.statusText}`,
          status: response.status,
        }
        throw error
      }

      return await response.json()
    } catch (error) {
      console.error('API Request failed:', error)
      throw error
    }
  }

  // Quran API Methods
  async getAllSurahs() {
    return this.request<any[]>('/quran/surahs')
  }

  async getSurah(surahNumber: number) {
    return this.request<any>(`/quran/surah/${surahNumber}`)
  }

  async getVerse(surahNumber: number, ayahNumber: number) {
    return this.request<any>(`/quran/surah/${surahNumber}/ayah/${ayahNumber}`)
  }

  async getVersesByRange(surahNumber: number, fromAyah: number, toAyah: number) {
    return this.request<any[]>(
      `/quran/surah/${surahNumber}/ayah-range?from=${fromAyah}&to=${toAyah}`
    )
  }

  async searchQuran(query: string, options?: { translation?: string }) {
    return this.request<any[]>('/quran/search', {
      method: 'POST',
      body: JSON.stringify({ query, ...options }),
    })
  }

  // Hadith API Methods
  async getHadithCollections() {
    return this.request<string[]>('/hadith/collections')
  }

  async getHadithsByCollection(collection: string, page: number = 1, pageSize: number = 10) {
    return this.request<any[]>(
      `/hadith/collection/${encodeURIComponent(collection)}?page=${page}&pageSize=${pageSize}`
    )
  }

  async searchHadith(query: string, options?: { collection?: string; grade?: string }) {
    return this.request<any[]>('/hadith/search', {
      method: 'POST',
      body: JSON.stringify({ query, ...options }),
    })
  }

  // Audio API Methods
  async getReciters() {
    return this.request<string[]>('/audio/reciters')
  }

  async getRecitation(reciterName: string, surahNumber: number, ayahNumber?: number) {
    const ayahParam = ayahNumber ? `?ayahNumber=${ayahNumber}` : ''
    return this.request<any>(
      `/audio/recitation/${encodeURIComponent(reciterName)}/surah/${surahNumber}${ayahParam}`
    )
  }

  async getRecitationsBySurah(reciterName: string, surahNumber: number) {
    return this.request<any[]>(
      `/audio/recitation/${encodeURIComponent(reciterName)}/surah/${surahNumber}/all`
    )
  }

  // Auth API Methods
  async login(username: string, password: string) {
    return this.request<any>('/auth/login', {
      method: 'POST',
      body: JSON.stringify({ username, password }),
    })
  }

  async register(username: string, email: string, password: string) {
    return this.request<any>('/auth/register', {
      method: 'POST',
      body: JSON.stringify({ username, email, password }),
    })
  }

  async getCurrentUser() {
    return this.request<any>('/auth/me')
  }

  // AI Settings API Methods
  async getAiSettings() {
    return this.request<AiSettings>('/aisettings')
  }

  async updateAiSettings(settings: AiSettingsDto) {
    return this.request<AiSettings>('/aisettings', {
      method: 'POST',
      body: JSON.stringify(settings),
    })
  }

  async testAiConnection() {
    return this.request<AiTestResult>('/aisettings/test', {
      method: 'POST',
    })
  }

  // Learning API Methods
  async getUserProgress(userId: number) {
    return this.request<UserProgress>(`/learning/progress/${userId}`)
  }

  async getUserLearningPaths(userId: number) {
    return this.request<LearningPath[]>(`/learning/paths/user/${userId}`)
  }

  async getLearningPath(pathId: number) {
    return this.request<LearningPath>(`/learning/paths/${pathId}`)
  }

  async createLearningPath(path: CreateLearningPathDto) {
    return this.request<LearningPath>('/learning/paths', {
      method: 'POST',
      body: JSON.stringify(path),
    })
  }

  async getAchievements() {
    return this.request<Achievement[]>('/learning/achievements')
  }

  async getUserAchievements(userId: number) {
    return this.request<UserAchievement[]>(`/learning/achievements/user/${userId}`)
  }

  async completeMilestone(milestoneId: number, userId: number) {
    return this.request<void>(`/learning/milestones/${milestoneId}/complete`, {
      method: 'POST',
      body: JSON.stringify({ userId }),
    })
  }

  async getRecommendedPaths(userId: number) {
    return this.request<LearningPath[]>(`/learning/paths/recommendations/${userId}`)
  }

  // Quiz API Methods
  async getQuizzes(category?: string, difficulty?: string) {
    const params = new URLSearchParams()
    if (category) params.append('category', category)
    if (difficulty) params.append('difficulty', difficulty)
    const query = params.toString() ? `?${params.toString()}` : ''
    return this.request<Quiz[]>(`/quiz${query}`)
  }

  async getQuiz(quizId: number) {
    return this.request<Quiz>(`/quiz/${quizId}`)
  }

  async startQuiz(quizId: number, userId: number) {
    return this.request<QuizAttempt>(`/quiz/${quizId}/start`, {
      method: 'POST',
      body: JSON.stringify({ userId }),
    })
  }

  async submitQuiz(attemptId: number, answers: Record<number, string>) {
    return this.request<QuizAttempt>(`/quiz/attempts/${attemptId}/submit`, {
      method: 'POST',
      body: JSON.stringify({ answers }),
    })
  }

  async getUserQuizHistory(userId: number) {
    return this.request<QuizAttempt[]>(`/quiz/history/${userId}`)
  }

  // FlashCard API Methods
  async getFlashCards(category?: string) {
    const query = category ? `?category=${encodeURIComponent(category)}` : ''
    return this.request<FlashCard[]>(`/flashcard${query}`)
  }

  async getDueFlashCards(userId: number, limit = 20) {
    return this.request<FlashCard[]>(`/flashcard/due/${userId}?limit=${limit}`)
  }

  async reviewFlashCard(cardId: number, userId: number, quality: number) {
    return this.request<UserFlashCardProgress>(`/flashcard/${cardId}/review`, {
      method: 'POST',
      body: JSON.stringify({ userId, quality }),
    })
  }
}

// Types for AI Settings
export interface AiSettings {
  id: number
  userId?: number
  isEnabled: boolean
  provider: string
  apiKey?: string
  modelName?: string
  useFallback: boolean
  updatedAt: string
}

export interface AiSettingsDto {
  isEnabled: boolean
  provider: string
  apiKey?: string
  modelName?: string
  useFallback: boolean
}

export interface AiTestResult {
  success: boolean
  message: string
  provider?: string
}

// Types for Learning Features
export interface UserProgress {
  id: number
  userId: number
  totalPoints: number
  level: number
  experiencePoints: number
  versesMemorized: number
  versesRead: number
  hadithsStudied: number
  lessonsCompleted: number
  quizzesCompleted: number
  averageQuizScore: number
  currentStreak: number
  longestStreak: number
  lastActivityDate?: string
  totalStudyMinutes: number
}

export interface LearningPath {
  id: number
  userId: number
  title: string
  description?: string
  pathType: string
  targetDate?: string
  startDate: string
  completedDate?: string
  progressPercentage: number
  isActive: boolean
  difficultyLevel: string
  dailyMinutes: number
  milestones?: LearningMilestone[]
}

export interface LearningMilestone {
  id: number
  learningPathId: number
  title: string
  description?: string
  orderIndex: number
  isCompleted: boolean
  completedDate?: string
  targetDate?: string
  points: number
}

export interface Achievement {
  id: number
  achievementId: string
  title: string
  description: string
  iconName: string
  category: string
  points: number
  tier: string
  isHidden: boolean
}

export interface UserAchievement {
  id: number
  userId: number
  achievementId: number
  earnedDate: string
  progress: number
  achievement?: Achievement
}

export interface CreateLearningPathDto {
  userId: number
  title: string
  description?: string
  pathType: string
  targetDate?: string
  difficultyLevel: string
  dailyMinutes: number
}

export interface Quiz {
  id: number
  title: string
  description?: string
  category: string
  difficultyLevel: string
  timeLimitMinutes: number
  passingScore: number
  isActive: boolean
  questions?: QuizQuestion[]
}

export interface QuizQuestion {
  id: number
  quizId: number
  questionText: string
  questionType: string
  orderIndex: number
  points: number
  explanation?: string
  options?: string
  correctAnswer: string
}

export interface QuizAttempt {
  id: number
  userId: number
  quizId: number
  startTime: string
  endTime?: string
  score: number
  totalQuestions: number
  correctAnswers: number
  passed: boolean
  timeTakenSeconds: number
  quiz?: Quiz
}

export interface FlashCard {
  id: number
  front: string
  back: string
  category: string
  difficultyLevel: string
  reference?: string
  notes?: string
  isActive: boolean
}

export interface UserFlashCardProgress {
  id: number
  userId: number
  flashCardId: number
  easeFactor: number
  intervalDays: number
  repetitions: number
  nextReviewDate: string
  lastReviewDate?: string
  totalReviews: number
  successRate: number
  isMastered: boolean
  flashCard?: FlashCard
}

// Export singleton instance
export const apiClient = new ApiClient()

// Export types for use in components
export type { ApiError }
