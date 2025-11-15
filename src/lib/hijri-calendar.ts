/**
 * Hijri Calendar Utilities
 * Provides conversion between Gregorian and Hijri dates
 * Following UAE Design System date handling patterns
 */

interface HijriDate {
  year: number
  month: number
  day: number
  monthName: string
  monthNameAr: string
}

const HIJRI_MONTHS = [
  { name: "Muharram", arabic: "المحرم" },
  { name: "Safar", arabic: "صفر" },
  { name: "Rabi' al-Awwal", arabic: "ربيع الأول" },
  { name: "Rabi' al-Thani", arabic: "ربيع الثاني" },
  { name: "Jumada al-Awwal", arabic: "جمادى الأولى" },
  { name: "Jumada al-Thani", arabic: "جمادى الثانية" },
  { name: "Rajab", arabic: "رجب" },
  { name: "Sha'ban", arabic: "شعبان" },
  { name: "Ramadan", arabic: "رمضان" },
  { name: "Shawwal", arabic: "شوال" },
  { name: "Dhu al-Qi'dah", arabic: "ذو القعدة" },
  { name: "Dhu al-Hijjah", arabic: "ذو الحجة" }
]

/**
 * Convert Gregorian date to Hijri date
 * Using the Umm al-Qura calendar (used in Saudi Arabia and UAE)
 */
export function gregorianToHijri(date: Date): HijriDate {
  // This is a simplified algorithm
  // For production, use a library like moment-hijri or @js-temporal/polyfill
  
  const year = date.getFullYear()
  const month = date.getMonth() + 1
  const day = date.getDate()
  
  // Julian Day Number calculation
  const a = Math.floor((14 - month) / 12)
  const y = year + 4800 - a
  const m = month + 12 * a - 3
  
  let jd = day + Math.floor((153 * m + 2) / 5) + 365 * y + 
           Math.floor(y / 4) - Math.floor(y / 100) + 
           Math.floor(y / 400) - 32045
  
  // Convert JD to Hijri
  const l = jd - 1948440 + 10632
  const n = Math.floor((l - 1) / 10631)
  const l2 = l - 10631 * n + 354
  const j = (Math.floor((10985 - l2) / 5316)) * 
           (Math.floor((50 * l2) / 17719)) + 
           (Math.floor(l2 / 5670)) * 
           (Math.floor((43 * l2) / 15238))
  const l3 = l2 - (Math.floor((30 - j) / 15)) * 
            (Math.floor((17719 * j) / 50)) - 
            (Math.floor(j / 16)) * 
            (Math.floor((15238 * j) / 43)) + 29
  const hijriMonth = Math.floor((24 * l3) / 709)
  const hijriDay = l3 - Math.floor((709 * hijriMonth) / 24)
  const hijriYear = 30 * n + j - 30
  
  return {
    year: hijriYear,
    month: hijriMonth,
    day: hijriDay,
    monthName: HIJRI_MONTHS[hijriMonth - 1].name,
    monthNameAr: HIJRI_MONTHS[hijriMonth - 1].arabic
  }
}

/**
 * Convert Hijri date to Gregorian date
 */
export function hijriToGregorian(hijriYear: number, hijriMonth: number, hijriDay: number): Date {
  // Simplified conversion
  // For production, use a proper library
  
  const l = hijriYear - 1
  const n = Math.floor(l / 30)
  const l2 = l % 30
  const j = Math.floor((l2 + 1) / 2)
  
  let jd = 1948440 - 10632 + 10631 * n + 354 * l2 + 
           Math.floor((3 + 11 * l2) / 30) + 
           Math.floor((hijriMonth - 1) / 2) * 59 + 
           (hijriMonth % 2) * 30 + hijriDay
  
  // JD to Gregorian
  const a = jd + 32044
  const b = Math.floor((4 * a + 3) / 146097)
  const c = a - Math.floor((146097 * b) / 4)
  const d = Math.floor((4 * c + 3) / 1461)
  const e = c - Math.floor((1461 * d) / 4)
  const m = Math.floor((5 * e + 2) / 153)
  
  const day = e - Math.floor((153 * m + 2) / 5) + 1
  const month = m + 3 - 12 * Math.floor(m / 10)
  const year = 100 * b + d - 4800 + Math.floor(m / 10)
  
  return new Date(year, month - 1, day)
}

/**
 * Format Hijri date for display
 */
export function formatHijriDate(hijriDate: HijriDate, locale: 'en' | 'ar' = 'en'): string {
  if (locale === 'ar') {
    return `${hijriDate.day} ${hijriDate.monthNameAr} ${hijriDate.year} هـ`
  }
  return `${hijriDate.day} ${hijriDate.monthName} ${hijriDate.year} AH`
}

/**
 * Get current Hijri date
 */
export function getCurrentHijriDate(): HijriDate {
  return gregorianToHijri(new Date())
}

/**
 * Check if a date is in Ramadan
 */
export function isRamadan(date: Date = new Date()): boolean {
  const hijri = gregorianToHijri(date)
  return hijri.month === 9
}

/**
 * Get Islamic special dates for a Hijri year
 */
export function getIslamicHolidays(hijriYear: number): Array<{
  name: string
  nameAr: string
  month: number
  day: number
  description: string
}> {
  return [
    {
      name: "Islamic New Year",
      nameAr: "رأس السنة الهجرية",
      month: 1,
      day: 1,
      description: "Beginning of the Islamic calendar year"
    },
    {
      name: "Ashura",
      nameAr: "عاشوراء",
      month: 1,
      day: 10,
      description: "Day of remembrance"
    },
    {
      name: "Mawlid al-Nabi",
      nameAr: "المولد النبوي",
      month: 3,
      day: 12,
      description: "Birthday of Prophet Muhammad (ﷺ)"
    },
    {
      name: "Isra and Mi'raj",
      nameAr: "الإسراء والمعراج",
      month: 7,
      day: 27,
      description: "Night Journey and Ascension"
    },
    {
      name: "Beginning of Ramadan",
      nameAr: "بداية رمضان",
      month: 9,
      day: 1,
      description: "Start of the fasting month"
    },
    {
      name: "Laylat al-Qadr",
      nameAr: "ليلة القدر",
      month: 9,
      day: 27,
      description: "Night of Power (last 10 nights of Ramadan)"
    },
    {
      name: "Eid al-Fitr",
      nameAr: "عيد الفطر",
      month: 10,
      day: 1,
      description: "Festival of Breaking the Fast"
    },
    {
      name: "Hajj (Day of Arafah)",
      nameAr: "يوم عرفة",
      month: 12,
      day: 9,
      description: "Day of Arafah during Hajj"
    },
    {
      name: "Eid al-Adha",
      nameAr: "عيد الأضحى",
      month: 12,
      day: 10,
      description: "Festival of Sacrifice"
    }
  ]
}

/**
 * Get days remaining until next Ramadan
 */
export function getDaysUntilRamadan(): number {
  const today = new Date()
  const currentHijri = gregorianToHijri(today)
  
  let targetYear = currentHijri.year
  if (currentHijri.month >= 9) {
    targetYear++
  }
  
  const ramadanStart = hijriToGregorian(targetYear, 9, 1)
  const diffTime = ramadanStart.getTime() - today.getTime()
  const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24))
  
  return diffDays
}
