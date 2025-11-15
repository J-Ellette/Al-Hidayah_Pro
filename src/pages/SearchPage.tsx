import { MagnifyingGlass } from "@phosphor-icons/react"
import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"
import { Checkbox } from "@/components/ui/checkbox"
import { Label } from "@/components/ui/label"
import { Card, CardContent } from "@/components/ui/card"
import { ScrollArea } from "@/components/ui/scroll-area"
import { useState } from "react"

export function SearchPage() {
  const [searchQuery, setSearchQuery] = useState("")
  const [filters, setFilters] = useState({
    quran: true,
    hadith: true,
    commentary: false
  })

  const handleSearch = () => {
    // TODO: Implement actual search functionality
    console.log("Searching for:", searchQuery, "with filters:", filters)
  }

  return (
    <div className="flex-1 flex flex-col bg-background overflow-hidden">
      <div className="border-b border-border p-6">
        <div className="max-w-4xl mx-auto">
          <div className="flex items-center gap-4 mb-4">
            <MagnifyingGlass className="h-8 w-8 text-accent" weight="duotone" />
            <h1 className="text-3xl font-semibold text-foreground">Search</h1>
          </div>
          <p className="text-muted-foreground mb-6">
            Search across all Islamic resources including Quran, Hadith, and scholarly works.
          </p>
          
          {/* Search Bar */}
          <div className="flex gap-2">
            <Input
              type="text"
              placeholder="Search Quran, Hadith, and Commentary..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              onKeyDown={(e) => e.key === 'Enter' && handleSearch()}
              className="flex-1"
            />
            <Button onClick={handleSearch} className="gap-2">
              <MagnifyingGlass className="h-4 w-4" />
              Search
            </Button>
          </div>
        </div>
      </div>
      
      <div className="flex-1 flex overflow-hidden">
        {/* Filters Sidebar */}
        <div className="w-64 border-r border-border p-6 space-y-6">
          <div>
            <h3 className="font-semibold text-foreground mb-3">Sources</h3>
            <div className="space-y-2">
              <div className="flex items-center space-x-2">
                <Checkbox 
                  id="quran"
                  checked={filters.quran}
                  onCheckedChange={(checked) => setFilters({...filters, quran: checked as boolean})}
                />
                <Label htmlFor="quran" className="text-sm font-normal cursor-pointer">
                  Quran
                </Label>
              </div>
              <div className="flex items-center space-x-2">
                <Checkbox 
                  id="hadith"
                  checked={filters.hadith}
                  onCheckedChange={(checked) => setFilters({...filters, hadith: checked as boolean})}
                />
                <Label htmlFor="hadith" className="text-sm font-normal cursor-pointer">
                  Hadith
                </Label>
              </div>
              <div className="flex items-center space-x-2">
                <Checkbox 
                  id="commentary"
                  checked={filters.commentary}
                  onCheckedChange={(checked) => setFilters({...filters, commentary: checked as boolean})}
                />
                <Label htmlFor="commentary" className="text-sm font-normal cursor-pointer">
                  Commentary
                </Label>
              </div>
            </div>
          </div>
          
          <div>
            <h3 className="font-semibold text-foreground mb-3">Languages</h3>
            <div className="space-y-2">
              <div className="flex items-center space-x-2">
                <Checkbox id="arabic" defaultChecked />
                <Label htmlFor="arabic" className="text-sm font-normal cursor-pointer">
                  Arabic
                </Label>
              </div>
              <div className="flex items-center space-x-2">
                <Checkbox id="english" defaultChecked />
                <Label htmlFor="english" className="text-sm font-normal cursor-pointer">
                  English
                </Label>
              </div>
            </div>
          </div>
        </div>
        
        {/* Results Area */}
        <ScrollArea className="flex-1">
          <div className="max-w-4xl mx-auto p-6">
            {searchQuery ? (
              <div className="space-y-4">
                <p className="text-sm text-muted-foreground">
                  Search functionality will be implemented with backend integration
                </p>
                <Card>
                  <CardContent className="p-6">
                    <p className="text-center text-muted-foreground">
                      Enter a search term and press Search to find relevant verses and hadiths
                    </p>
                  </CardContent>
                </Card>
              </div>
            ) : (
              <div className="flex items-center justify-center h-full">
                <div className="text-center space-y-4">
                  <MagnifyingGlass className="h-16 w-16 mx-auto text-muted-foreground/50" weight="thin" />
                  <p className="text-muted-foreground">
                    Start typing to search across Islamic resources
                  </p>
                </div>
              </div>
            )}
          </div>
        </ScrollArea>
      </div>
    </div>
  )
}
