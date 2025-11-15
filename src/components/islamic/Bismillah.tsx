/**
 * Bismillah Component
 * Displays "بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ" (In the name of Allah, the Most Gracious, the Most Merciful)
 * Used as a header for Islamic pages
 */

interface BismillahProps {
  showTranslation?: boolean
  className?: string
  size?: 'sm' | 'md' | 'lg'
}

const sizeClasses = {
  sm: 'text-xl',
  md: 'text-2xl',
  lg: 'text-3xl'
}

export function Bismillah({ showTranslation = false, className = '', size = 'md' }: BismillahProps) {
  return (
    <div className={`bismillah-header ${className}`}>
      <div className={`font-arabic ${sizeClasses[size]} mb-2`} dir="rtl" lang="ar">
        بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ
      </div>
      {showTranslation && (
        <div className="text-sm text-muted-foreground italic">
          In the name of Allah, the Most Gracious, the Most Merciful
        </div>
      )}
      <div className="islamic-divider mt-4" />
    </div>
  )
}
