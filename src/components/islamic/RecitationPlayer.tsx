import { useRef, useState, useEffect } from "react"
import { Card, CardContent } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Slider } from "@/components/ui/slider"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Play, Pause, SkipBack, SkipForward, SpeakerHigh, SpeakerLow } from "@phosphor-icons/react"

interface RecitationPlayerProps {
  surahNumber: number
  ayahNumber?: number
  autoPlay?: boolean
  className?: string
}

export function RecitationPlayer({ 
  surahNumber, 
  ayahNumber,
  autoPlay = false,
  className = "" 
}: RecitationPlayerProps) {
  const audioRef = useRef<HTMLAudioElement>(null)
  const [isPlaying, setIsPlaying] = useState(false)
  const [currentTime, setCurrentTime] = useState(0)
  const [duration, setDuration] = useState(0)
  const [volume, setVolume] = useState(1)
  const [selectedReciter, setSelectedReciter] = useState("Abdul Basit")
  const [isLoading, setIsLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  // Available reciters (will be fetched from API in future)
  const reciters = [
    "Abdul Basit",
    "Mishary Rashid Alafasy",
    "Saad Al-Ghamdi",
    "Ahmed Al-Ajmi",
    "Mahmoud Khalil Al-Hussary"
  ]

  // Load audio when reciter or verse changes
  useEffect(() => {
    loadAudio()
  }, [surahNumber, ayahNumber, selectedReciter])

  const loadAudio = async () => {
    setIsLoading(true)
    setError(null)

    try {
      // For now, using sample audio URL structure
      // In production, this would call the backend API
      const ayahParam = ayahNumber ? `/ayah/${ayahNumber}` : ''
      const audioUrl = `/api/audio/recitation/${encodeURIComponent(selectedReciter)}/surah/${surahNumber}${ayahParam}`
      
      if (audioRef.current) {
        audioRef.current.src = audioUrl
        if (autoPlay) {
          await audioRef.current.play()
        }
      }
    } catch (err) {
      setError("Unable to load recitation. Please try again.")
      console.error("Audio loading error:", err)
    } finally {
      setIsLoading(false)
    }
  }

  const togglePlayPause = async () => {
    if (!audioRef.current) return

    try {
      if (isPlaying) {
        audioRef.current.pause()
      } else {
        await audioRef.current.play()
      }
    } catch (err) {
      setError("Unable to play audio. Please check your connection.")
      console.error("Playback error:", err)
    }
  }

  const handleTimeUpdate = () => {
    if (audioRef.current) {
      setCurrentTime(audioRef.current.currentTime)
    }
  }

  const handleLoadedMetadata = () => {
    if (audioRef.current) {
      setDuration(audioRef.current.duration)
    }
  }

  const handleSeek = (value: number[]) => {
    if (audioRef.current) {
      audioRef.current.currentTime = value[0]
      setCurrentTime(value[0])
    }
  }

  const handleVolumeChange = (value: number[]) => {
    const newVolume = value[0]
    setVolume(newVolume)
    if (audioRef.current) {
      audioRef.current.volume = newVolume
    }
  }

  const skipBackward = () => {
    if (audioRef.current) {
      audioRef.current.currentTime = Math.max(0, audioRef.current.currentTime - 10)
    }
  }

  const skipForward = () => {
    if (audioRef.current) {
      audioRef.current.currentTime = Math.min(duration, audioRef.current.currentTime + 10)
    }
  }

  const formatTime = (seconds: number) => {
    if (isNaN(seconds)) return "0:00"
    const mins = Math.floor(seconds / 60)
    const secs = Math.floor(seconds % 60)
    return `${mins}:${secs.toString().padStart(2, '0')}`
  }

  return (
    <Card className={className}>
      <CardContent className="p-4 space-y-4">
        {/* Reciter Selection */}
        <div className="flex items-center justify-between">
          <span className="text-sm font-medium text-foreground">Reciter:</span>
          <Select value={selectedReciter} onValueChange={setSelectedReciter}>
            <SelectTrigger className="w-[200px]">
              <SelectValue placeholder="Select reciter" />
            </SelectTrigger>
            <SelectContent>
              {reciters.map((reciter) => (
                <SelectItem key={reciter} value={reciter}>
                  {reciter}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>

        {/* Progress Bar */}
        <div className="space-y-2">
          <Slider
            value={[currentTime]}
            max={duration || 100}
            step={0.1}
            onValueChange={handleSeek}
            className="w-full"
            disabled={isLoading || !duration}
          />
          <div className="flex justify-between text-xs text-muted-foreground">
            <span>{formatTime(currentTime)}</span>
            <span>{formatTime(duration)}</span>
          </div>
        </div>

        {/* Controls */}
        <div className="flex items-center justify-center gap-4">
          <Button
            variant="ghost"
            size="sm"
            onClick={skipBackward}
            disabled={isLoading || !duration}
          >
            <SkipBack className="h-5 w-5" />
          </Button>

          <Button
            variant="default"
            size="lg"
            onClick={togglePlayPause}
            disabled={isLoading}
            className="rounded-full w-12 h-12"
          >
            {isPlaying ? (
              <Pause className="h-6 w-6" weight="fill" />
            ) : (
              <Play className="h-6 w-6" weight="fill" />
            )}
          </Button>

          <Button
            variant="ghost"
            size="sm"
            onClick={skipForward}
            disabled={isLoading || !duration}
          >
            <SkipForward className="h-5 w-5" />
          </Button>
        </div>

        {/* Volume Control */}
        <div className="flex items-center gap-3">
          {volume > 0.5 ? (
            <SpeakerHigh className="h-4 w-4 text-muted-foreground" />
          ) : (
            <SpeakerLow className="h-4 w-4 text-muted-foreground" />
          )}
          <Slider
            value={[volume]}
            max={1}
            step={0.01}
            onValueChange={handleVolumeChange}
            className="w-full"
          />
        </div>

        {/* Error Message */}
        {error && (
          <div className="text-sm text-destructive text-center">
            {error}
          </div>
        )}

        {/* Loading State */}
        {isLoading && (
          <div className="text-sm text-muted-foreground text-center">
            Loading recitation...
          </div>
        )}

        {/* Audio Element */}
        <audio
          ref={audioRef}
          onTimeUpdate={handleTimeUpdate}
          onLoadedMetadata={handleLoadedMetadata}
          onPlay={() => setIsPlaying(true)}
          onPause={() => setIsPlaying(false)}
          onEnded={() => setIsPlaying(false)}
          onError={() => setError("Failed to load audio")}
        />
      </CardContent>
    </Card>
  )
}
