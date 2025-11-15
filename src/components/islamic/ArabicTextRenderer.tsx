interface ArabicTextRendererProps {
  text: string
  className?: string
  fontSize?: 'sm' | 'base' | 'lg' | 'xl' | '2xl'
  tajweedHighlight?: boolean
}

const fontSizeClasses = {
  'sm': 'text-sm',
  'base': 'text-base',
  'lg': 'text-lg',
  'xl': 'text-xl',
  '2xl': 'text-2xl'
}

export function ArabicTextRenderer({
  text,
  className = '',
  fontSize = 'xl',
  tajweedHighlight = false
}: ArabicTextRendererProps) {
  return (
    <div
      className={`
        font-arabic
        ${fontSizeClasses[fontSize]}
        leading-loose
        text-right
        ${className}
      `}
      dir="rtl"
      lang="ar"
    >
      {tajweedHighlight ? (
        <TajweedHighlighter text={text} />
      ) : (
        text
      )}
    </div>
  )
}

// Placeholder for Tajweed highlighting - to be implemented later
function TajweedHighlighter({ text }: { text: string }) {
  // TODO: Implement Tajweed rules highlighting
  // This will color-code different Arabic letters according to Tajweed rules
  return <span>{text}</span>
}
