import { House, Books, Book, BookOpen, Compass, MapTrifold, Wrench, MagnifyingGlass, GraduationCap, Notebook, CaretLeft, CaretRight, Gear, Target } from "@phosphor-icons/react"
import { Button } from "@/components/ui/button"
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip"
import { Separator } from "@/components/ui/separator"

interface SidebarProps {
  isCollapsed: boolean
  activeView: string
  onViewChange: (view: string) => void
  onToggleCollapse: () => void
}

const sidebarItems = [
  { id: 'home', icon: House, label: 'Home' },
  { id: 'library', icon: Books, label: 'Library' },
  { id: 'quran', icon: Book, label: 'Quran' },
  { id: 'hadith', icon: BookOpen, label: 'Hadith' },
  { id: 'study', icon: Notebook, label: 'Study' },
  { id: 'learning', icon: GraduationCap, label: 'Learning' },
  { id: 'practice', icon: Target, label: 'Practice' },
  { id: 'guides', icon: Compass, label: 'Guides' },
  { id: 'atlas', icon: MapTrifold, label: 'Atlas' },
  { id: 'tools', icon: Wrench, label: 'Tools' },
  { id: 'search', icon: MagnifyingGlass, label: 'Search' },
  { id: 'settings', icon: Gear, label: 'Settings' },
]

export function Sidebar({ isCollapsed, activeView, onViewChange, onToggleCollapse }: SidebarProps) {
  return (
    <div 
      className={`flex flex-col bg-card border-r border-border sidebar-transition ${
        isCollapsed ? 'w-[60px]' : 'w-[240px]'
      }`}
    >
      <div className="flex items-center justify-between h-14 px-4 border-b border-border">
        {!isCollapsed && (
          <h1 className="text-lg font-semibold text-foreground">Al-Hidayah Pro</h1>
        )}
        <Button
          variant="ghost"
          size="icon"
          className="h-8 w-8 text-muted-foreground hover:text-foreground hover:bg-muted/50"
          onClick={onToggleCollapse}
        >
          {isCollapsed ? <CaretRight className="h-5 w-5" /> : <CaretLeft className="h-5 w-5" />}
        </Button>
      </div>

      <div className="flex-1 overflow-auto py-4">
        <TooltipProvider delayDuration={300}>
          <div className="space-y-1 px-2">
            {sidebarItems.slice(0, 8).map((item) => {
              const Icon = item.icon
              return (
                <Tooltip key={item.id}>
                  <TooltipTrigger asChild>
                    <Button
                      variant="ghost"
                      className={`w-full justify-start ${isCollapsed ? 'px-0 justify-center' : 'px-3'} ${
                        activeView === item.id 
                          ? 'bg-accent/10 text-accent hover:bg-accent/20 hover:text-accent' 
                          : 'text-foreground hover:bg-muted/50'
                      }`}
                      onClick={() => onViewChange(item.id)}
                    >
                      <Icon className="h-5 w-5 flex-shrink-0" />
                      {!isCollapsed && <span className="ml-3">{item.label}</span>}
                    </Button>
                  </TooltipTrigger>
                  {isCollapsed && (
                    <TooltipContent side="right">
                      <p>{item.label}</p>
                    </TooltipContent>
                  )}
                </Tooltip>
              )
            })}
            
            <div className="py-2">
              <Separator className="bg-border" />
            </div>
            
            {sidebarItems.slice(8).map((item) => {
              const Icon = item.icon
              return (
                <Tooltip key={item.id}>
                  <TooltipTrigger asChild>
                    <Button
                      variant="ghost"
                      className={`w-full justify-start ${isCollapsed ? 'px-0 justify-center' : 'px-3'} ${
                        activeView === item.id 
                          ? 'bg-accent/10 text-accent hover:bg-accent/20 hover:text-accent' 
                          : 'text-foreground hover:bg-muted/50'
                      }`}
                      onClick={() => onViewChange(item.id)}
                    >
                      <Icon className="h-5 w-5 flex-shrink-0" />
                      {!isCollapsed && <span className="ml-3">{item.label}</span>}
                    </Button>
                  </TooltipTrigger>
                  {isCollapsed && (
                    <TooltipContent side="right">
                      <p>{item.label}</p>
                    </TooltipContent>
                  )}
                </Tooltip>
              )
            })}
          </div>
        </TooltipProvider>
      </div>
    </div>
  )
}
