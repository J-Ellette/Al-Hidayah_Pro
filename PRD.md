# Planning Guide

Al-Hidayah Pro: A modern Islamic resources application providing easy access to the Quran, Hadith, guides, atlas, and various Islamic tools with a clean, focused interface designed for study and learning.

**Experience Qualities**:
1. **Serene** - Conveys peace and focus, creating a calm environment for spiritual learning and reflection
2. **Accessible** - Clear navigation that makes Islamic knowledge readily available to all users
3. **Reverent** - Treats sacred texts with appropriate dignity while maintaining usability

**Complexity Level**: Light Application (multiple features with basic state)
  - Multiple navigation sections (home, library, Quran, hadith, guides, atlas, tools, search) with collapsible sidebar and state management for navigation and content display

## Essential Features

### Navigation Sidebar
- **Functionality**: Left sidebar with collapsible navigation showing icons when collapsed, full labels when expanded
- **Purpose**: Provides quick access to all major sections of Islamic resources (Home, Library, Quran, Hadith, Guides, Atlas, Tools, Search)
- **Trigger**: Always visible; click on toggle to expand/collapse
- **Progression**: User views sidebar → clicks toggle or icon → sidebar expands to show labels → user clicks navigation item → content area updates
- **Success criteria**: Smooth animation between collapsed/expanded states, clear active state indication, icons remain visible when collapsed

### Content Display Area
- **Functionality**: Main content area that displays selected section content
- **Purpose**: Shows detailed content based on navigation selection (Quran verses, Hadith collections, guides, etc.)
- **Trigger**: Navigation selection change
- **Progression**: User selects navigation item → content area transitions → relevant content displays
- **Success criteria**: Clear content hierarchy, proper text rendering, scrollable when needed

### Search Functionality
- **Functionality**: Dedicated search section accessible from navigation
- **Purpose**: Allows users to search across all Islamic resources
- **Trigger**: Click on Search navigation item
- **Progression**: User selects Search → search interface appears → user enters query → results display
- **Success criteria**: Fast search interface, clear results presentation

## Edge Case Handling
- **Empty States**: Show welcoming home content when app first loads
- **Long Text**: Handle Arabic and English text with proper RTL/LTR support where needed
- **Collapsed Sidebar**: Maintain icon visibility and tooltips when sidebar is collapsed
- **Responsive Sizing**: Adapt layout gracefully on smaller screens, prioritize content area

## Design Direction

The design should feel serene, focused, and respectful - creating a peaceful environment for studying Islamic texts and resources. A clean interface with generous whitespace, using soft colors and subtle transitions that encourage contemplation and learning. The aesthetic should be modern yet timeless, emphasizing clarity and readability.

## Color Selection

Warm, earthy tones with green accents - inspired by traditional Islamic aesthetics and promoting calmness for reading and study.

- **Primary Color**: Deep Teal Green (oklch(0.45 0.08 180)) - Communicates growth, peace, and Islamic tradition
- **Secondary Colors**: Warm cream and beige tones (oklch(0.95 0.01 80) to oklch(0.90 0.02 75)) for backgrounds
- **Accent Color**: Rich Green (oklch(0.55 0.12 165)) - For active states, highlights, and important elements
- **Foreground/Background Pairings**:
  - Background (Warm Cream oklch(0.97 0.01 85)): Dark Brown text (oklch(0.25 0.02 60)) - Ratio 14.8:1 ✓
  - Card/Panel (Light Beige oklch(0.94 0.015 80)): Dark Brown text (oklch(0.25 0.02 60)) - Ratio 13.2:1 ✓
  - Primary (Deep Teal oklch(0.45 0.08 180)): White text (oklch(1 0 0)) - Ratio 6.1:1 ✓
  - Accent (Rich Green oklch(0.55 0.12 165)): White text (oklch(1 0 0)) - Ratio 4.9:1 ✓
  - Muted (Soft Beige oklch(0.85 0.02 75)): Dark text (oklch(0.30 0.02 60)) - Ratio 9.2:1 ✓

## Font Selection

Should convey clarity and elegance, using clean sans-serif fonts for UI and proper support for Arabic text. Inter provides excellent readability for English UI elements while supporting Arabic numerals.

- **Typographic Hierarchy**:
  - H1 (Welcome Title): Inter SemiBold/36px/normal letter spacing
  - H2 (Section Headers): Inter Medium/20px/normal letter spacing
  - H3 (Subsection Headers): Inter Medium/16px/normal letter spacing
  - Body (UI Text): Inter Regular/14px/normal letter spacing
  - Navigation Labels: Inter Medium/14px/normal letter spacing
  - Arabic Text: System Arabic fonts/18px/generous line height for readability

## Animations

Animations should be gentle and purposeful - creating a sense of calm while maintaining responsiveness. Transitions should feel natural and peaceful, never jarring or rushed.

- **Purposeful Meaning**: Smooth, gentle transitions that communicate state changes (sidebar expanding, content loading) with grace
- **Hierarchy of Movement**: Sidebar expansion deserves smooth 250ms ease-in-out transitions; content transitions should fade gently (200ms); hover states should be subtle and instant

## Component Selection

- **Components**:
  - **Button**: For navigation items with ghost variant, icon-only when collapsed
  - **Separator**: For divider line in navigation menu
  - **ScrollArea**: For content area with smooth scrolling
  - **Tooltip**: To show full navigation labels when sidebar is collapsed

- **Customizations**:
  - Custom sidebar component with collapsible navigation (icons + labels)
  - Custom content area for displaying section-specific information
  - Navigation items with smooth hover and active states

- **States**:
  - Navigation Items: Ghost variant with hover showing subtle green tint, active state with accent background
  - Sidebar: Collapsed (60px wide, icons only) vs Expanded (240px wide, icons + labels)
  - Content: Smooth fade-in when switching between sections

- **Icon Selection**:
  - Home: House icon from Phosphor
  - Library: Books icon
  - Quran: Book icon
  - Hadith: BookOpen icon
  - Guides: Compass icon
  - Atlas: MapTrifold icon
  - Tools: Wrench icon
  - Search: MagnifyingGlass icon
  - Toggle: CaretLeft/CaretRight for collapse/expand

- **Spacing**:
  - Sidebar items: py-3 px-4 with generous spacing for comfortable interaction
  - Content padding: p-8 for generous reading space
  - Section spacing: space-y-6 between major content blocks

- **Mobile**:
  - Sidebar becomes overlay drawer on mobile
  - Content area takes full width
  - Navigation items remain accessible through hamburger menu
