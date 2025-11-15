/**
 * Islamic Calendar Component
 * Displays Hijri calendar with Islamic holidays
 * Following UAE Design System accessibility and RTL standards
 */

import { useState, useEffect } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Badge } from '@/components/ui/badge'
import { CalendarBlank, CaretLeft, CaretRight } from '@phosphor-icons/react'
import { 
  gregorianToHijri, 
  hijriToGregorian, 
  formatHijriDate, 
  getIslamicHolidays,
  getCurrentHijriDate 
} from '@/lib/hijri-calendar'

export function IslamicCalendar() {
  const [currentDate, setCurrentDate] = useState(new Date())
  const [selectedDate, setSelectedDate] = useState(new Date())
  const [viewMode, setViewMode] = useState<'gregorian' | 'hijri'>('hijri')
  
  const hijriDate = gregorianToHijri(currentDate)
  const holidays = getIslamicHolidays(hijriDate.year)
  
  // Get days in current Hijri month (approximate)
  const getDaysInHijriMonth = (year: number, month: number): number => {
    // Hijri months alternate between 29 and 30 days
    return month % 2 === 1 ? 30 : 29
  }
  
  const daysInMonth = getDaysInHijriMonth(hijriDate.year, hijriDate.month)
  
  // Get first day of month
  const firstDayGregorian = hijriToGregorian(hijriDate.year, hijriDate.month, 1)
  const firstDayOfWeek = firstDayGregorian.getDay()
  
  // Generate calendar days
  const calendarDays: Array<{ day: number; isCurrentMonth: boolean; hijriDay: number; isHoliday: boolean; holidayName?: string }> = []
  
  // Previous month padding
  for (let i = 0; i < firstDayOfWeek; i++) {
    calendarDays.push({ day: 0, isCurrentMonth: false, hijriDay: 0, isHoliday: false })
  }
  
  // Current month days
  for (let day = 1; day <= daysInMonth; day++) {
    const holiday = holidays.find(h => h.month === hijriDate.month && h.day === day)
    calendarDays.push({
      day,
      isCurrentMonth: true,
      hijriDay: day,
      isHoliday: !!holiday,
      holidayName: holiday?.name
    })
  }
  
  const handlePrevMonth = () => {
    const newMonth = hijriDate.month === 1 ? 12 : hijriDate.month - 1
    const newYear = hijriDate.month === 1 ? hijriDate.year - 1 : hijriDate.year
    const newDate = hijriToGregorian(newYear, newMonth, 1)
    setCurrentDate(newDate)
  }
  
  const handleNextMonth = () => {
    const newMonth = hijriDate.month === 12 ? 1 : hijriDate.month + 1
    const newYear = hijriDate.month === 12 ? hijriDate.year + 1 : hijriDate.year
    const newDate = hijriToGregorian(newYear, newMonth, 1)
    setCurrentDate(newDate)
  }
  
  const handleToday = () => {
    setCurrentDate(new Date())
    setSelectedDate(new Date())
  }
  
  const isToday = (day: number): boolean => {
    const today = getCurrentHijriDate()
    return day === today.day && 
           hijriDate.month === today.month && 
           hijriDate.year === today.year
  }
  
  const weekDays = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']
  const weekDaysAr = ['الأحد', 'الاثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت']
  
  return (
    <Card className="islamic-card">
      <CardHeader>
        <div className="flex items-center justify-between">
          <CardTitle className="flex items-center gap-2">
            <CalendarBlank className="h-5 w-5 text-aegreen-600" weight="duotone" />
            <span className="text-aegreen-800">
              <span className="font-arabic ml-2">التقويم الهجري</span>
              <span className="ml-2">Islamic Calendar</span>
            </span>
          </CardTitle>
          <div className="flex gap-2">
            <Button 
              variant="outline" 
              size="sm"
              onClick={() => setViewMode(viewMode === 'hijri' ? 'gregorian' : 'hijri')}
              className="text-xs"
            >
              {viewMode === 'hijri' ? 'Gregorian' : 'Hijri'}
            </Button>
            <Button 
              variant="outline" 
              size="sm"
              onClick={handleToday}
            >
              Today
            </Button>
          </div>
        </div>
      </CardHeader>
      
      <CardContent className="space-y-4">
        {/* Month Navigation */}
        <div className="flex items-center justify-between">
          <Button
            variant="ghost"
            size="sm"
            onClick={handlePrevMonth}
            className="h-8 w-8 p-0"
          >
            <CaretLeft className="h-4 w-4" />
          </Button>
          
          <div className="text-center">
            <div className="font-semibold text-lg text-aegreen-700">
              <span className="font-arabic">{hijriDate.monthNameAr}</span>
              <span className="mx-2">·</span>
              <span>{hijriDate.monthName}</span>
            </div>
            <div className="text-sm text-muted-foreground">
              <span className="font-arabic">{hijriDate.year} هـ</span>
              <span className="mx-2">·</span>
              <span>{hijriDate.year} AH</span>
            </div>
          </div>
          
          <Button
            variant="ghost"
            size="sm"
            onClick={handleNextMonth}
            className="h-8 w-8 p-0"
          >
            <CaretRight className="h-4 w-4" />
          </Button>
        </div>
        
        {/* Calendar Grid */}
        <div className="grid grid-cols-7 gap-1">
          {/* Week day headers */}
          {weekDays.map((day, index) => (
            <div key={day} className="text-center text-xs font-semibold text-muted-foreground py-2">
              <div>{day}</div>
              <div className="font-arabic text-[10px]">{weekDaysAr[index]}</div>
            </div>
          ))}
          
          {/* Calendar days */}
          {calendarDays.map((dayInfo, index) => (
            <div
              key={index}
              className={`
                aspect-square flex items-center justify-center text-sm rounded-md
                transition-colors cursor-pointer
                ${!dayInfo.isCurrentMonth ? 'opacity-0 pointer-events-none' : ''}
                ${isToday(dayInfo.day) ? 'bg-aegreen-600 text-white font-bold' : ''}
                ${dayInfo.isHoliday && !isToday(dayInfo.day) ? 'bg-aegold-100 text-aegold-800 font-semibold' : ''}
                ${!isToday(dayInfo.day) && !dayInfo.isHoliday ? 'hover:bg-accent/10' : ''}
              `}
              title={dayInfo.holidayName}
            >
              {dayInfo.isCurrentMonth && dayInfo.day}
            </div>
          ))}
        </div>
        
        {/* Holidays in current month */}
        {holidays.filter(h => h.month === hijriDate.month).length > 0 && (
          <div className="pt-4 border-t border-border">
            <h4 className="text-sm font-semibold text-foreground mb-2">
              Islamic Events This Month
            </h4>
            <div className="space-y-2">
              {holidays
                .filter(h => h.month === hijriDate.month)
                .map((holiday, index) => (
                  <div key={index} className="flex items-start gap-2 text-sm">
                    <Badge variant="outline" className="bg-aegold-50 border-aegold-300 text-aegold-800">
                      {holiday.day}
                    </Badge>
                    <div>
                      <div className="font-semibold text-foreground">
                        <span className="font-arabic ml-2">{holiday.nameAr}</span>
                        <span>{holiday.name}</span>
                      </div>
                      <div className="text-xs text-muted-foreground">{holiday.description}</div>
                    </div>
                  </div>
                ))}
            </div>
          </div>
        )}
        
        {/* Current Date Display */}
        <div className="pt-4 border-t border-border text-center">
          <div className="text-xs text-muted-foreground mb-1">Today's Date</div>
          <div className="font-semibold text-aegreen-700">
            <div className="font-arabic text-base">{formatHijriDate(getCurrentHijriDate(), 'ar')}</div>
            <div className="text-sm mt-1">{formatHijriDate(getCurrentHijriDate(), 'en')}</div>
          </div>
          <div className="text-xs text-muted-foreground mt-1">
            {new Date().toLocaleDateString('en-US', { 
              weekday: 'long', 
              year: 'numeric', 
              month: 'long', 
              day: 'numeric' 
            })}
          </div>
        </div>
      </CardContent>
    </Card>
  )
}
