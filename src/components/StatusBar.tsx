import { GitBranch, Warning, Info } from "@phosphor-icons/react"
import { Separator } from "@/components/ui/separator"

export function StatusBar() {
  return (
    <div className="flex items-center h-6 bg-primary text-primary-foreground px-2 text-xs border-t border-border">
      <div className="flex items-center gap-2">
        <GitBranch className="h-3.5 w-3.5" />
        <span>main</span>
      </div>
      
      <Separator orientation="vertical" className="mx-2 h-4 bg-primary-foreground/20" />
      
      <div className="flex items-center gap-2">
        <Warning className="h-3.5 w-3.5" />
        <span>0</span>
      </div>
      
      <div className="flex items-center gap-2 ml-2">
        <Info className="h-3.5 w-3.5" />
        <span>0</span>
      </div>
      
      <div className="flex-1" />
      
      <div className="flex items-center gap-4">
        <span>Ln 1, Col 1</span>
        <span>Spaces: 2</span>
        <span>UTF-8</span>
        <span>Plain Text</span>
      </div>
    </div>
  )
}
