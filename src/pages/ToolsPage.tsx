import { Wrench } from "@phosphor-icons/react"
import { PrayerTimesCard } from "@/components/islamic/PrayerTimesCard"
import { QiblaCompass } from "@/components/islamic/QiblaCompass"
import { ScrollArea } from "@/components/ui/scroll-area"

export function ToolsPage() {
  return (
    <div className="flex-1 flex flex-col bg-background overflow-hidden">
      <div className="border-b border-border p-6">
        <div className="max-w-6xl mx-auto">
          <div className="flex items-center gap-4 mb-4">
            <Wrench className="h-8 w-8 text-accent" weight="duotone" />
            <h1 className="text-3xl font-semibold text-foreground">Islamic Tools</h1>
          </div>
          <p className="text-muted-foreground">
            Prayer times calculator, Qibla direction finder, and other useful Islamic tools.
          </p>
        </div>
      </div>
      
      <ScrollArea className="flex-1">
        <div className="max-w-6xl mx-auto p-6">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
            <PrayerTimesCard />
            <QiblaCompass />
          </div>
        </div>
      </ScrollArea>
    </div>
  )
}
