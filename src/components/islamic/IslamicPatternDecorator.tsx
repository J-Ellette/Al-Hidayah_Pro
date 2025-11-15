/**
 * Islamic Pattern Decorator
 * Adds subtle Islamic geometric patterns as background decoration
 * Following UAE Design System visual language
 */

import { ReactNode } from 'react'

interface IslamicPatternDecoratorProps {
  children: ReactNode
  variant?: 'subtle' | 'visible'
  className?: string
}

export function IslamicPatternDecorator({ 
  children, 
  variant = 'subtle',
  className = '' 
}: IslamicPatternDecoratorProps) {
  return (
    <div className={`islamic-pattern-bg ${className}`}>
      {children}
      {variant === 'visible' && (
        <div className="absolute top-4 right-4 opacity-5 pointer-events-none">
          <svg width="120" height="120" viewBox="0 0 120 120" fill="none" xmlns="http://www.w3.org/2000/svg">
            {/* Islamic star pattern */}
            <circle cx="60" cy="60" r="50" stroke="currentColor" strokeWidth="0.5" className="text-aegreen-600"/>
            <path d="M60 10 L65 50 L90 30 L70 55 L110 60 L70 65 L90 90 L65 70 L60 110 L55 70 L30 90 L50 65 L10 60 L50 55 L30 30 L55 50 Z" 
                  fill="currentColor" 
                  className="text-aegold-400" 
                  opacity="0.6"/>
          </svg>
        </div>
      )}
    </div>
  )
}
