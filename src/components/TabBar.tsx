import { X } from "@phosphor-icons/react"
import { Button } from "@/components/ui/button"

interface Tab {
  id: string
  title: string
  isDirty?: boolean
}

interface TabBarProps {
  tabs: Tab[]
  activeTab: string
  onTabChange: (id: string) => void
  onTabClose: (id: string) => void
}

export function TabBar({ tabs, activeTab, onTabChange, onTabClose }: TabBarProps) {
  if (tabs.length === 0) {
    return null
  }

  return (
    <div className="flex items-center bg-card border-b border-border overflow-x-auto">
      {tabs.map((tab) => (
        <div
          key={tab.id}
          className={`group flex items-center px-3 py-2 border-r border-border cursor-pointer min-w-fit ${
            activeTab === tab.id
              ? 'bg-background text-foreground border-b-2 border-b-accent'
              : 'text-muted-foreground hover:text-foreground hover:bg-muted/50'
          }`}
          onClick={() => onTabChange(tab.id)}
        >
          <span className="text-sm mr-2">
            {tab.title}
            {tab.isDirty && <span className="ml-1">●</span>}
          </span>
          <Button
            variant="ghost"
            size="icon"
            className="h-4 w-4 p-0 opacity-0 group-hover:opacity-100 hover:bg-muted"
            onClick={(e) => {
              e.stopPropagation()
              onTabClose(tab.id)
            }}
          >
            <X className="h-3 w-3" />
          </Button>
        </div>
      ))}
    </div>
  )
}
