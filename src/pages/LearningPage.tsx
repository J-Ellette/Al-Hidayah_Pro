import { GraduationCap, CheckCircle, Circle, Clock, Star } from "@phosphor-icons/react"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Progress } from "@/components/ui/progress"
import { Button } from "@/components/ui/button"
import { ScrollArea } from "@/components/ui/scroll-area"
import { PrayerTimesCard } from "@/components/islamic/PrayerTimesCard"
import { QiblaCompass } from "@/components/islamic/QiblaCompass"
import { Bismillah } from "@/components/islamic/Bismillah"
import { IslamicPatternDecorator } from "@/components/islamic/IslamicPatternDecorator"
import { ProgressDashboard } from "@/components/learning/ProgressDashboard"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"

// Sample learning modules
const learningModules = [
  {
    id: 1,
    title: "Shahada & Belief",
    description: "Understanding the declaration of faith and core Islamic beliefs",
    lessons: 5,
    completed: 5,
    status: "completed" as const
  },
  {
    id: 2,
    title: "Prayer (Salah)",
    description: "Learning the five daily prayers and their significance",
    lessons: 8,
    completed: 3,
    status: "in-progress" as const
  },
  {
    id: 3,
    title: "Charity (Zakat)",
    description: "Understanding obligatory charity and its calculation",
    lessons: 4,
    completed: 0,
    status: "not-started" as const
  },
  {
    id: 4,
    title: "Fasting (Sawm)",
    description: "The month of Ramadan and the practice of fasting",
    lessons: 6,
    completed: 0,
    status: "not-started" as const
  },
  {
    id: 5,
    title: "Pilgrimage (Hajj)",
    description: "The journey to Mecca and its rituals",
    lessons: 7,
    completed: 0,
    status: "not-started" as const
  }
]

// Sample glossary items
const glossaryItems = [
  { term: "Allah", definition: "The Arabic word for God in Islam" },
  { term: "Salah", definition: "The five daily prayers performed by Muslims" },
  { term: "Quran", definition: "The holy book of Islam, revealed to Prophet Muhammad" },
  { term: "Hadith", definition: "Sayings and actions of Prophet Muhammad (peace be upon him)" }
]

export function LearningPage() {
  const totalLessons = learningModules.reduce((sum, module) => sum + module.lessons, 0)
  const completedLessons = learningModules.reduce((sum, module) => sum + module.completed, 0)
  const overallProgress = Math.round((completedLessons / totalLessons) * 100)

  const nextLesson = learningModules.find(m => m.status === "in-progress")

  return (
    <div className="flex-1 flex flex-col bg-background overflow-hidden">
      <IslamicPatternDecorator variant="subtle">
        <div className="border-b border-border p-6 border-l-4 border-l-aegreen-500">
          <div className="max-w-7xl mx-auto">
            <div className="flex items-center gap-4 mb-4">
              <GraduationCap className="h-8 w-8 text-aegreen-600" weight="duotone" />
              <h1 className="text-3xl font-semibold text-foreground">
                <span className="font-arabic text-aegreen-700 ml-2">التعليم</span>
                <span className="ml-2">Learning Dashboard</span>
              </h1>
            </div>
            <p className="text-muted-foreground">
              Structured learning path for new Muslims and Islamic students with progress tracking.
            </p>
          </div>
        </div>
      </IslamicPatternDecorator>

      <ScrollArea className="flex-1">
        <div className="max-w-7xl mx-auto p-6 space-y-6">
          {/* Bismillah */}
          <Bismillah showTranslation={false} size="md" />
          
          {/* Tabs for different views */}
          <Tabs defaultValue="curriculum" className="w-full">
            <TabsList className="grid w-full max-w-md grid-cols-2">
              <TabsTrigger value="curriculum">Curriculum</TabsTrigger>
              <TabsTrigger value="progress">My Progress</TabsTrigger>
            </TabsList>
            
            <TabsContent value="curriculum" className="space-y-6 mt-6">
          
          {/* Overall Progress Card */}
          <Card className="border-aegreen-300 bg-gradient-to-r from-aegreen-50 to-aegold-50 islamic-card">
            <CardHeader>
              <CardTitle className="flex items-center justify-between">
                <div className="flex items-center gap-3">
                  <Star className="h-6 w-6 text-aegold-600" weight="fill" />
                  <span className="text-aegreen-800">
                    <span className="font-arabic ml-2">أساسيات الإسلام</span>
                    <span className="ml-2">Islamic Basics Course</span>
                  </span>
                </div>
                <span className="text-3xl font-bold text-aegreen-700">{overallProgress}%</span>
              </CardTitle>
            </CardHeader>
            <CardContent className="space-y-4">
              <div className="progress-islamic h-3 rounded-full" style={{ width: `${overallProgress}%` }} />
              <div className="flex items-center justify-between text-sm">
                <span className="text-muted-foreground font-semibold">
                  {completedLessons} of {totalLessons} lessons completed
                </span>
                {nextLesson && (
                  <div className="flex items-center gap-2 text-aegreen-700 font-semibold">
                    <Clock className="h-4 w-4" />
                    <span>Next: {nextLesson.title}</span>
                  </div>
                )}
              </div>
            </CardContent>
          </Card>

          {/* Tools Grid */}
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
            <PrayerTimesCard />
            <QiblaCompass />
          </div>

          {/* Learning Modules */}
          <div>
            <h2 className="text-2xl font-semibold text-foreground mb-4">Learning Path</h2>
            <p className="text-muted-foreground mb-6">
              Follow this structured path to learn the Five Pillars of Islam
            </p>
            <div className="space-y-4">
              {learningModules.map((module) => (
                <Card 
                  key={module.id}
                  className={`cursor-pointer transition-all hover:shadow-md ${
                    module.status === "completed" 
                      ? "border-green-500/20 bg-green-500/5" 
                      : module.status === "in-progress"
                      ? "border-accent/20 bg-accent/5"
                      : ""
                  }`}
                >
                  <CardContent className="p-6">
                    <div className="flex items-start gap-4">
                      {/* Status Icon */}
                      <div className="mt-1">
                        {module.status === "completed" ? (
                          <CheckCircle className="h-6 w-6 text-green-600" weight="fill" />
                        ) : module.status === "in-progress" ? (
                          <Circle className="h-6 w-6 text-accent" weight="duotone" />
                        ) : (
                          <Circle className="h-6 w-6 text-muted-foreground" />
                        )}
                      </div>

                      {/* Module Content */}
                      <div className="flex-1">
                        <div className="flex items-start justify-between mb-2">
                          <div>
                            <h3 className="text-lg font-semibold text-foreground">
                              {module.title}
                            </h3>
                            <p className="text-sm text-muted-foreground mt-1">
                              {module.description}
                            </p>
                          </div>
                          <Button 
                            size="sm"
                            variant={module.status === "in-progress" ? "default" : "outline"}
                          >
                            {module.status === "completed" 
                              ? "Review" 
                              : module.status === "in-progress"
                              ? "Continue"
                              : "Start"}
                          </Button>
                        </div>

                        {/* Progress Bar for module */}
                        <div className="mt-3 space-y-2">
                          <Progress 
                            value={(module.completed / module.lessons) * 100} 
                            className="h-2"
                          />
                          <p className="text-xs text-muted-foreground">
                            {module.completed} of {module.lessons} lessons
                          </p>
                        </div>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
          </div>

          {/* Quick Access Glossary */}
          <Card>
            <CardHeader>
              <CardTitle>Islamic Terms Glossary</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                {glossaryItems.map((item, index) => (
                  <div key={index} className="border-l-2 border-accent pl-3">
                    <h4 className="font-semibold text-foreground">{item.term}</h4>
                    <p className="text-sm text-muted-foreground mt-1">{item.definition}</p>
                  </div>
                ))}
              </div>
              <Button variant="outline" className="mt-4 w-full">
                View Full Glossary
              </Button>
            </CardContent>
          </Card>

          {/* Daily Hadith */}
          <Card className="border-accent/20">
            <CardHeader>
              <CardTitle>Daily Hadith</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-3">
                <p className="text-foreground leading-relaxed">
                  "The best among you are those who have the best manners and character."
                </p>
                <p className="text-sm text-muted-foreground">
                  — Prophet Muhammad (ﷺ), Sahih Bukhari
                </p>
              </div>
            </CardContent>
          </Card>
          </TabsContent>
          
          <TabsContent value="progress" className="space-y-6 mt-6">
            {/* Progress Dashboard with mock user ID */}
            <ProgressDashboard userId={1} />
          </TabsContent>
        </Tabs>
        </div>
      </ScrollArea>
    </div>
  )
}
