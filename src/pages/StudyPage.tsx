import { useState } from "react"
import { BookOpen, Note, BookBookmark, ListBullets } from "@phosphor-icons/react"
import { QuranVerse } from "@/components/islamic/QuranVerse"
import { RecitationPlayer } from "@/components/islamic/RecitationPlayer"
import { Bismillah } from "@/components/islamic/Bismillah"
import { IslamicPatternDecorator } from "@/components/islamic/IslamicPatternDecorator"
import { ScrollArea } from "@/components/ui/scroll-area"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Textarea } from "@/components/ui/textarea"
import { Button } from "@/components/ui/button"
import { ResizablePanelGroup, ResizablePanel, ResizableHandle } from "@/components/ui/resizable-panels"

// Sample verse data
const sampleVerse = {
  surahNumber: 1,
  ayahNumber: 1,
  arabicText: "بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ",
  translation: "In the name of Allah, the Entirely Merciful, the Especially Merciful."
}

// Sample commentary data
const sampleCommentary = [
  {
    source: "Tafsir Ibn Kathir",
    text: "The Bismillah is the opening of every Surah except Surah At-Tawbah. It is a declaration of seeking Allah's help and blessings before beginning any action."
  },
  {
    source: "Tafsir Al-Jalalayn",
    text: "Beginning with the name of Allah, the Most Gracious, the Most Merciful. This is the opening verse that precedes every Surah to remind believers to start every action with remembrance of Allah."
  }
]

// Sample cross-references
const sampleReferences = [
  { surah: 27, ayah: 30, preview: "Indeed, it is from Solomon, and indeed, it reads: 'In the name of Allah...'" },
  { surah: 11, ayah: 41, preview: "And he said, 'Embark therein; in the name of Allah is its course and its anchorage...'" }
]

export function StudyPage() {
  const [selectedVerse, setSelectedVerse] = useState(sampleVerse)
  const [notes, setNotes] = useState("")
  const [bookmarks, setBookmarks] = useState<any[]>([])

  const handleSaveNote = () => {
    // TODO: Implement note saving to backend
    console.log("Saving note:", notes)
  }

  const handleAddBookmark = () => {
    // TODO: Implement bookmark saving to backend
    const newBookmark = {
      ...selectedVerse,
      timestamp: new Date().toISOString()
    }
    setBookmarks([...bookmarks, newBookmark])
  }

  return (
    <div className="flex-1 flex flex-col bg-background overflow-hidden">
      <IslamicPatternDecorator variant="subtle">
        <div className="border-b border-border p-6 border-l-4 border-l-aegreen-500">
          <div className="max-w-full mx-auto">
            <div className="flex items-center gap-4 mb-4">
              <BookOpen className="h-8 w-8 text-aegreen-600" weight="duotone" />
              <h1 className="text-3xl font-semibold text-foreground">
                <span className="font-arabic text-aegreen-700 ml-2">مساحة الدراسة</span>
                <span className="ml-2">Study Workspace</span>
              </h1>
            </div>
            <p className="text-muted-foreground">
              Multi-panel study interface for in-depth Quran study with commentary, cross-references, and personal notes.
            </p>
          </div>
        </div>
      </IslamicPatternDecorator>

      <div className="flex-1 overflow-hidden">
        <ResizablePanelGroup direction="horizontal" className="h-full">
          {/* Left Panel - Quran Text */}
          <ResizablePanel defaultSize={40} minSize={30}>
            <div className="h-full flex flex-col">
              <div className="p-4 border-b border-border bg-gradient-to-r from-aegreen-50 to-transparent">
                <h2 className="text-lg font-semibold text-aegreen-800">
                  <span className="font-arabic ml-2">النص القرآني</span>
                  <span className="ml-2">Quran Text</span>
                </h2>
              </div>
              <ScrollArea className="flex-1">
                <div className="p-4 space-y-4">
                  {/* Bismillah */}
                  <Bismillah showTranslation={false} size="sm" />
                  
                  <div className="mb-4 islamic-border">
                    <h3 className="text-xl font-semibold text-foreground mb-2">
                      <span className="font-arabic text-2xl text-aegreen-700 ml-2" dir="rtl">سورة الفاتحة</span>
                      <span className="ml-2">Surah Al-Fatihah (The Opening)</span>
                    </h3>
                    <p className="text-sm text-muted-foreground">7 verses • Meccan • Juz 1</p>
                  </div>

                  <div className="verse-card-islamic">
                    <QuranVerse
                      {...selectedVerse}
                      showArabic={true}
                    />
                  </div>

                  {/* Audio Player */}
                  <RecitationPlayer
                    surahNumber={selectedVerse.surahNumber}
                    ayahNumber={selectedVerse.ayahNumber}
                    className="islamic-card"
                  />

                  {/* Quick Actions */}
                  <div className="flex gap-2">
                    <Button onClick={handleAddBookmark} variant="outline" size="sm" className="border-aegreen-300 hover:bg-aegreen-50">
                      Add Bookmark
                    </Button>
                  </div>
                </div>
              </ScrollArea>
            </div>
          </ResizablePanel>

          <ResizableHandle />

          {/* Right Panel - Study Tools */}
          <ResizablePanel defaultSize={60} minSize={40}>
            <div className="h-full flex flex-col">
              <Tabs defaultValue="commentary" className="flex-1 flex flex-col">
                <div className="border-b border-border">
                  <TabsList className="w-full justify-start rounded-none border-b-0 p-0 h-auto">
                    <TabsTrigger 
                      value="commentary" 
                      className="rounded-none border-b-2 border-transparent data-[state=active]:border-accent data-[state=active]:bg-transparent"
                    >
                      <Note className="h-4 w-4 mr-2" />
                      Commentary
                    </TabsTrigger>
                    <TabsTrigger 
                      value="references"
                      className="rounded-none border-b-2 border-transparent data-[state=active]:border-accent data-[state=active]:bg-transparent"
                    >
                      <BookBookmark className="h-4 w-4 mr-2" />
                      Cross References
                    </TabsTrigger>
                    <TabsTrigger 
                      value="notes"
                      className="rounded-none border-b-2 border-transparent data-[state=active]:border-accent data-[state=active]:bg-transparent"
                    >
                      <ListBullets className="h-4 w-4 mr-2" />
                      My Notes
                    </TabsTrigger>
                  </TabsList>
                </div>

                <TabsContent value="commentary" className="flex-1 m-0">
                  <ScrollArea className="h-full">
                    <div className="p-4 space-y-4">
                      {sampleCommentary.map((commentary, index) => (
                        <Card key={index}>
                          <CardHeader>
                            <CardTitle className="text-base">{commentary.source}</CardTitle>
                          </CardHeader>
                          <CardContent>
                            <p className="text-sm text-foreground leading-relaxed">
                              {commentary.text}
                            </p>
                          </CardContent>
                        </Card>
                      ))}
                    </div>
                  </ScrollArea>
                </TabsContent>

                <TabsContent value="references" className="flex-1 m-0">
                  <ScrollArea className="h-full">
                    <div className="p-4 space-y-4">
                      <p className="text-sm text-muted-foreground mb-4">
                        Related verses that mention similar themes or concepts:
                      </p>
                      {sampleReferences.map((ref, index) => (
                        <Card key={index} className="cursor-pointer hover:bg-accent/5 transition-colors">
                          <CardContent className="p-4">
                            <div className="flex items-start justify-between mb-2">
                              <span className="font-semibold text-accent">
                                Surah {ref.surah}, Ayah {ref.ayah}
                              </span>
                            </div>
                            <p className="text-sm text-foreground">
                              {ref.preview}
                            </p>
                          </CardContent>
                        </Card>
                      ))}
                    </div>
                  </ScrollArea>
                </TabsContent>

                <TabsContent value="notes" className="flex-1 m-0">
                  <div className="h-full flex flex-col">
                    <ScrollArea className="flex-1">
                      <div className="p-4 space-y-4">
                        <div>
                          <h3 className="text-sm font-semibold text-foreground mb-2">
                            Personal Notes for Surah {selectedVerse.surahNumber}, Ayah {selectedVerse.ayahNumber}
                          </h3>
                          <Textarea
                            value={notes}
                            onChange={(e) => setNotes(e.target.value)}
                            placeholder="Write your thoughts, reflections, or questions about this verse..."
                            className="min-h-[200px] resize-none"
                          />
                          <Button onClick={handleSaveNote} className="mt-2">
                            Save Note
                          </Button>
                        </div>

                        {/* Bookmarks List */}
                        {bookmarks.length > 0 && (
                          <div className="mt-6">
                            <h3 className="text-sm font-semibold text-foreground mb-3">
                              Bookmarks ({bookmarks.length})
                            </h3>
                            <div className="space-y-2">
                              {bookmarks.map((bookmark, index) => (
                                <Card key={index} className="cursor-pointer hover:bg-accent/5">
                                  <CardContent className="p-3">
                                    <p className="text-sm font-medium text-accent">
                                      Surah {bookmark.surahNumber}, Ayah {bookmark.ayahNumber}
                                    </p>
                                    <p className="text-xs text-muted-foreground mt-1">
                                      {bookmark.translation.substring(0, 80)}...
                                    </p>
                                  </CardContent>
                                </Card>
                              ))}
                            </div>
                          </div>
                        )}
                      </div>
                    </ScrollArea>
                  </div>
                </TabsContent>
              </Tabs>
            </div>
          </ResizablePanel>
        </ResizablePanelGroup>
      </div>
    </div>
  )
}
