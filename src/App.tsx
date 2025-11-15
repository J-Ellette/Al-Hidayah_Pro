import { useState, useEffect } from 'react'
import { BrowserRouter as Router, Routes, Route, useLocation, useNavigate } from 'react-router-dom'
import { Sidebar } from '@/components/Sidebar'
import { HomePage } from '@/pages/HomePage'
import { QuranPage } from '@/pages/QuranPage'
import { HadithPage } from '@/pages/HadithPage'
import { LibraryPage } from '@/pages/LibraryPage'
import { GuidesPage } from '@/pages/GuidesPage'
import { AtlasPage } from '@/pages/AtlasPage'
import { ToolsPage } from '@/pages/ToolsPage'
import { SearchPage } from '@/pages/SearchPage'
import { StudyPage } from '@/pages/StudyPage'
import { LearningPage } from '@/pages/LearningPage'
import { PracticePage } from '@/pages/PracticePage'
import { SettingsPage } from '@/pages/SettingsPage'

function AppContent() {
  const location = useLocation()
  const navigate = useNavigate()
  
  const [isSidebarCollapsed, setIsSidebarCollapsed] = useState<boolean>(() => {
    const saved = localStorage.getItem('sidebar-collapsed')
    return saved ? JSON.parse(saved) : false
  })

  useEffect(() => {
    localStorage.setItem('sidebar-collapsed', JSON.stringify(isSidebarCollapsed))
  }, [isSidebarCollapsed])

  const handleViewChange = (view: string) => {
    navigate(`/${view === 'home' ? '' : view}`)
  }

  const handleToggleCollapse = () => {
    setIsSidebarCollapsed((current) => !current)
  }

  // Determine active view from current path
  const activeView = location.pathname === '/' ? 'home' : location.pathname.substring(1)

  return (
    <div className="h-screen flex overflow-hidden">
      <Sidebar
        isCollapsed={isSidebarCollapsed}
        activeView={activeView}
        onViewChange={handleViewChange}
        onToggleCollapse={handleToggleCollapse}
      />
      
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/library" element={<LibraryPage />} />
        <Route path="/quran" element={<QuranPage />} />
        <Route path="/hadith" element={<HadithPage />} />
        <Route path="/guides" element={<GuidesPage />} />
        <Route path="/atlas" element={<AtlasPage />} />
        <Route path="/tools" element={<ToolsPage />} />
        <Route path="/search" element={<SearchPage />} />
        <Route path="/study" element={<StudyPage />} />
        <Route path="/learning" element={<LearningPage />} />
        <Route path="/practice" element={<PracticePage />} />
        <Route path="/settings" element={<SettingsPage />} />
      </Routes>
    </div>
  )
}

function App() {
  return (
    <Router>
      <AppContent />
    </Router>
  )
}

export default App