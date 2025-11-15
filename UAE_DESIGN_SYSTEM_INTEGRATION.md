# UAE Design System Integration - Al-Hidayah Pro

Complete guide to UAE Design System integration with Islamic-specific customizations.

## Overview

Al-Hidayah Pro leverages the **UAE Design System** (https://designsystem.gov.ae/) to provide government-grade accessibility, RTL excellence, and cultural sensitivity appropriate for Islamic applications in the MENA region.

## UAE Design System Strengths

### 1. Government-Grade Accessibility ✅

**WCAG 2.1 AA Compliance:**
- All components meet WCAG 2.1 Level AA standards
- Screen reader support (NVDA, JAWS, VoiceOver)
- Keyboard navigation for all interactive elements
- High contrast mode support
- Focus indicators on all focusable elements

**Implemented in Al-Hidayah Pro:**
```typescript
// Example: Accessible button with ARIA labels
<Button 
  aria-label="Play Quran recitation"
  className="button-islamic"
>
  <Play className="h-6 w-6" />
</Button>

// Example: Accessible form with proper labels
<Label htmlFor="surah-select">Select Surah</Label>
<Select id="surah-select" aria-describedby="surah-help">
  ...
</Select>
<span id="surah-help" className="sr-only">Choose a chapter from the Quran</span>
```

### 2. RTL Excellence ✅

**Bidirectional Text Support:**
- Automatic RTL layout for Arabic content
- LTR layout for English content
- Mixed RTL/LTR support in bilingual interfaces
- Proper text alignment and directionality

**Implemented in Al-Hidayah Pro:**
```css
/* Islamic Theme CSS */
[dir="rtl"] {
  text-align: right;
}

[lang="ar"] {
  font-family: 'Noto Kufi Arabic', 'Amiri', 'Scheherazade', serif;
}

.quranic-text {
  direction: rtl;
  text-align: right;
  font-family: 'Noto Kufi Arabic', 'Amiri', 'Scheherazade', serif;
}
```

**Usage:**
```tsx
// Automatic RTL for Arabic text
<div className="quranic-text" dir="rtl" lang="ar">
  بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ
</div>

// Bilingual headers
<h1>
  <span className="font-arabic" dir="rtl">القرآن الكريم</span>
  <span>The Holy Quran</span>
</h1>
```

### 3. Cultural Sensitivity ✅

**MENA-Appropriate Design:**
- Color schemes aligned with Islamic aesthetics
- Patterns and visual language suitable for religious content
- Respectful presentation of sacred texts
- Gender-neutral iconography where appropriate

**Islamic Color Palette (UAE DLS Colors):**
```typescript
// From UAE Design System
--islamic-green: #4A9D5C (aegreen-600)
--islamic-gold: #B68A35 (aegold-600)
--neutral-backgrounds: slate colors from UAE DLS

// Usage in components
className="bg-aegreen-50 text-aegreen-800 border-aegreen-300"
```

### 4. Professional Polish ✅

**Production-Ready Components:**
- Used in UAE government applications
- Thoroughly tested across browsers and devices
- Consistent visual language
- High-quality animations and transitions

## Islamic-Specific Customizations

### 1. Prayer Time Integration ✅

**Components:**
- `PrayerTimesCard.tsx` - Displays five daily prayer times
- Uses UAE DLS Card and Badge components
- Highlights next prayer with Islamic colors

```tsx
<Card className="prayer-active">
  <CardHeader>
    <CardTitle className="text-aegreen-700">Next Prayer: Dhuhr</CardTitle>
  </CardHeader>
  <CardContent>
    <Badge className="bg-aegold-500">12:30 PM</Badge>
  </CardContent>
</Card>
```

### 2. Islamic (Hijri) Calendar ✅

**Features:**
- Dual calendar display (Gregorian/Hijri)
- Islamic holidays and special dates
- Ramadan tracking
- Hijri date conversion utilities

**Components:**
- `IslamicCalendar.tsx` - Full calendar widget
- `src/lib/hijri-calendar.ts` - Date conversion utilities

**Usage:**
```tsx
import { IslamicCalendar } from '@/components/islamic/IslamicCalendar'
import { getCurrentHijriDate, formatHijriDate } from '@/lib/hijri-calendar'

// Display calendar
<IslamicCalendar />

// Get current Hijri date
const hijriDate = getCurrentHijriDate()
console.log(formatHijriDate(hijriDate, 'ar')) // "١ محرم ١٤٤٦ هـ"
```

**Hijri Calendar Utilities:**
```typescript
// Convert dates
const hijriDate = gregorianToHijri(new Date())
const gregorianDate = hijriToGregorian(1446, 1, 1)

// Check if Ramadan
const isRamadanNow = isRamadan(new Date())

// Get Islamic holidays
const holidays = getIslamicHolidays(1446)

// Days until Ramadan
const daysLeft = getDaysUntilRamadan()
```

### 3. Arabic Typography ✅

**Enhanced Typography System:**
- **Noto Kufi Arabic** - Primary Arabic font (modern, readable)
- **Amiri** - Traditional Quranic text
- **Scheherazade** - Alternative Arabic font
- Proper line-height and letter-spacing for Arabic

**Implementation:**
```css
/* Base Arabic font */
.font-arabic {
  font-family: 'Noto Kufi Arabic', 'Amiri', 'Scheherazade', serif;
  font-weight: 400;
  letter-spacing: 0.02em;
  line-height: 2;
}

/* Quranic text specific */
.quranic-text {
  font-family: 'Noto Kufi Arabic', 'Amiri', 'Scheherazade', serif;
  font-size: 1.75rem;
  line-height: 2.5;
  letter-spacing: 0.03em;
  text-align: right;
  direction: rtl;
}

/* Bold Arabic text */
.font-arabic-bold {
  font-family: 'Noto Kufi Arabic', 'Amiri', 'Scheherazade', serif;
  font-weight: 700;
}
```

### 4. Islamic Color Themes ✅

**Primary Color Palette:**

```typescript
// Islamic Green (UAE DLS aegreen)
--islamic-green-50: #F3FAF4
--islamic-green-600: #3F8E50  // Primary
--islamic-green-700: #2F663C
--islamic-green-900: #24432B

// Gold Accents (UAE DLS aegold)
--islamic-gold-50: #F9F7ED
--islamic-gold-600: #92722A    // Primary
--islamic-gold-400: #CBA344
--islamic-gold-300: #D7BC6D

// Neutral Backgrounds (UAE DLS slate)
--slate-50: #F8FAFC
--slate-100: #F1F5F9
--slate-200: #E2E8F0
```

**Usage Examples:**
```tsx
// Quran page with Islamic green
<div className="border-l-4 border-l-aegreen-500">
  <h1 className="text-aegreen-700">القرآن الكريم</h1>
</div>

// Hadith page with gold accents
<div className="border-l-4 border-l-aegold-500">
  <h1 className="text-aegold-600">الحديث النبوي</h1>
</div>

// Cards with Islamic styling
<Card className="islamic-card hover:shadow-aegreen-100">
  <CardContent className="bg-gradient-to-r from-aegreen-50 to-transparent">
    ...
  </CardContent>
</Card>
```

## Technical Benefits

### 1. Consistent UX ✅

**Unified Component Library:**
- All UI components follow UAE DLS patterns
- Consistent spacing, typography, and color usage
- Predictable behavior across the application

**Component Categories:**
- **Layout:** Card, Container, Grid, Flex
- **Navigation:** Sidebar, Tabs, Breadcrumbs
- **Input:** Button, Input, Select, Checkbox, Radio
- **Feedback:** Badge, Alert, Toast, Progress
- **Data Display:** Table, List, Card

### 2. Regular Maintenance ✅

**TDRA Support:**
- Official support from UAE Telecommunications & Digital Government Regulatory Authority
- Regular updates and security patches
- Bug fixes and improvements
- New component additions

**Version Management:**
```json
{
  "@aegov/design-system-react": "^1.1.2",
  "@radix-ui/react-*": "Latest compatible versions"
}
```

### 3. Comprehensive Documentation ✅

**Resources:**
- Official docs: https://designsystem.gov.ae/
- Component library: https://github.com/TDRA-ae/aegov-dls-react
- Design tokens: Colors, spacing, typography
- Accessibility guidelines
- Code examples and patterns

**Documentation Structure:**
```
- Getting Started
- Components
  - Buttons
  - Forms
  - Navigation
  - Data Display
- Patterns
  - Forms
  - Navigation
  - Page Layouts
- Accessibility
- Theming
- Best Practices
```

### 4. Growing Community ✅

**MENA Developer Community:**
- Active development in UAE and GCC countries
- Shared components and patterns
- Arabic-first development approach
- Cultural understanding built-in

**Community Resources:**
- GitHub discussions
- Stack Overflow tags
- Developer forums
- Code sharing platforms

## Implementation Examples

### Complete Page Example

```tsx
import { IslamicPatternDecorator } from '@/components/islamic/IslamicPatternDecorator'
import { Bismillah } from '@/components/islamic/Bismillah'
import { IslamicCalendar } from '@/components/islamic/IslamicCalendar'
import { PrayerTimesCard } from '@/components/islamic/PrayerTimesCard'

export function HomePage() {
  return (
    <div className="flex-1 flex flex-col bg-background overflow-hidden">
      {/* Header with Islamic styling */}
      <IslamicPatternDecorator variant="subtle">
        <div className="border-b border-border p-6 border-l-4 border-l-aegreen-500">
          <div className="max-w-7xl mx-auto">
            <h1 className="text-3xl font-semibold">
              <span className="font-arabic text-aegreen-700 ml-2">الهداية برو</span>
              <span className="ml-2">Al-Hidayah Pro</span>
            </h1>
            <p className="text-muted-foreground mt-2">
              Your comprehensive Islamic learning companion
            </p>
          </div>
        </div>
      </IslamicPatternDecorator>

      {/* Main content */}
      <ScrollArea className="flex-1">
        <div className="max-w-7xl mx-auto p-6 space-y-6">
          {/* Bismillah */}
          <Bismillah showTranslation={true} size="lg" />

          {/* Content grid */}
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
            <PrayerTimesCard />
            <IslamicCalendar />
          </div>
        </div>
      </ScrollArea>
    </div>
  )
}
```

### Accessible Form Example

```tsx
<form className="space-y-4" onSubmit={handleSubmit}>
  {/* Surah selection */}
  <div>
    <Label htmlFor="surah" className="text-aegreen-700">
      <span className="font-arabic ml-2">اختر السورة</span>
      <span>Select Surah</span>
    </Label>
    <Select id="surah" aria-describedby="surah-help">
      <SelectTrigger className="border-aegreen-300 focus:ring-aegreen-500">
        <SelectValue placeholder="Choose a Surah" />
      </SelectTrigger>
      <SelectContent>
        <SelectItem value="1">
          <span className="font-arabic ml-2">الفاتحة</span>
          <span>Al-Fatihah</span>
        </SelectItem>
      </SelectContent>
    </Select>
    <span id="surah-help" className="text-xs text-muted-foreground">
      Choose a chapter from the Holy Quran
    </span>
  </div>

  {/* Submit button */}
  <Button 
    type="submit" 
    className="button-islamic w-full"
    aria-label="Load selected Surah"
  >
    Load Surah
  </Button>
</form>
```

## Accessibility Checklist

### ✅ Keyboard Navigation
- [ ] All interactive elements are keyboard accessible
- [ ] Logical tab order throughout the application
- [ ] Visible focus indicators
- [ ] Escape key closes modals and dialogs
- [ ] Arrow keys navigate through lists and menus

### ✅ Screen Reader Support
- [ ] Semantic HTML elements used
- [ ] ARIA labels on all buttons and inputs
- [ ] ARIA live regions for dynamic content
- [ ] Alternative text for all images
- [ ] Proper heading hierarchy

### ✅ Visual Accessibility
- [ ] Minimum 4.5:1 contrast ratio for normal text
- [ ] Minimum 3:1 contrast ratio for large text
- [ ] Color not used as the only visual indicator
- [ ] Text resizable up to 200% without loss of content
- [ ] Focus indicators visible in high contrast mode

### ✅ Islamic Content Accessibility
- [ ] Arabic text properly marked with lang="ar"
- [ ] RTL direction set for Arabic content
- [ ] Quranic text distinguishable from regular text
- [ ] Prayer times announced to screen readers
- [ ] Hijri dates provided alongside Gregorian

## Performance Optimization

### Code Splitting
```typescript
// Lazy load heavy components
const IslamicCalendar = lazy(() => import('@/components/islamic/IslamicCalendar'))
const RecitationPlayer = lazy(() => import('@/components/islamic/RecitationPlayer'))
```

### Font Loading
```css
/* Preload critical fonts */
<link rel="preload" href="/fonts/NotoKufiArabic.woff2" as="font" type="font/woff2" crossorigin>

/* Font display swap for better performance */
@font-face {
  font-family: 'Noto Kufi Arabic';
  font-display: swap;
  src: url('/fonts/NotoKufiArabic.woff2') format('woff2');
}
```

## Testing Guidelines

### Manual Testing
1. Test with screen readers (NVDA, JAWS, VoiceOver)
2. Keyboard-only navigation test
3. RTL layout verification
4. High contrast mode testing
5. Mobile responsiveness check

### Automated Testing
```typescript
// Example accessibility test
import { render } from '@testing-library/react'
import { axe, toHaveNoViolations } from 'jest-axe'

expect.extend(toHaveNoViolations)

test('IslamicCalendar should have no accessibility violations', async () => {
  const { container } = render(<IslamicCalendar />)
  const results = await axe(container)
  expect(results).toHaveNoViolations()
})
```

## Resources

### Official UAE Design System
- **Website:** https://designsystem.gov.ae/
- **React Library:** https://github.com/TDRA-ae/aegov-dls-react
- **Design Tokens:** https://designsystem.gov.ae/design-tokens/
- **Accessibility:** https://designsystem.gov.ae/accessibility/

### Islamic Resources
- **Hijri Calendar:** Used for date calculations
- **Islamic Finder:** https://www.islamicfinder.org/ (Prayer times API)
- **Quran.com:** https://quran.com/ (Quran API)
- **Sunnah.com:** https://sunnah.com/ (Hadith API)

### Fonts
- **Noto Kufi Arabic:** https://fonts.google.com/noto/specimen/Noto+Kufi+Arabic
- **Amiri:** https://fonts.google.com/specimen/Amiri
- **Scheherazade:** https://software.sil.org/scheherazade/

## Support

For issues or questions:
- **UAE Design System:** https://github.com/TDRA-ae/aegov-dls-react/issues
- **Al-Hidayah Pro:** https://github.com/J-Ellette/Al-Hidayah_Pro/issues
