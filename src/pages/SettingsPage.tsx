import { useState, useEffect } from 'react'
import { apiClient, type AiSettings, type AiSettingsDto } from '@/lib/api-client'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Switch } from '@/components/ui/switch'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { AlertCircle, CheckCircle2, Loader2, Settings as SettingsIcon } from 'lucide-react'
import { Alert, AlertDescription } from '@/components/ui/alert'

export function SettingsPage() {
  const [settings, setSettings] = useState<AiSettings | null>(null)
  const [loading, setLoading] = useState(true)
  const [saving, setSaving] = useState(false)
  const [testing, setTesting] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [success, setSuccess] = useState<string | null>(null)
  const [testResult, setTestResult] = useState<{ success: boolean; message: string } | null>(null)
  
  // Form state
  const [isEnabled, setIsEnabled] = useState(false)
  const [provider, setProvider] = useState('none')
  const [apiKey, setApiKey] = useState('')
  const [modelName, setModelName] = useState('')
  const [useFallback, setUseFallback] = useState(true)

  useEffect(() => {
    loadSettings()
  }, [])

  const loadSettings = async () => {
    try {
      setLoading(true)
      setError(null)
      const data = await apiClient.getAiSettings()
      setSettings(data)
      
      // Update form state
      setIsEnabled(data.isEnabled)
      setProvider(data.provider)
      setApiKey(data.apiKey === '***CONFIGURED***' ? '' : '')
      setModelName(data.modelName || '')
      setUseFallback(data.useFallback)
    } catch (err) {
      setError('Failed to load AI settings. Please try again.')
      console.error('Error loading settings:', err)
    } finally {
      setLoading(false)
    }
  }

  const handleSave = async () => {
    try {
      setSaving(true)
      setError(null)
      setSuccess(null)
      setTestResult(null)

      const dto: AiSettingsDto = {
        isEnabled,
        provider,
        apiKey: apiKey || undefined,
        modelName: modelName || undefined,
        useFallback,
      }

      const updated = await apiClient.updateAiSettings(dto)
      setSettings(updated)
      setSuccess('AI settings saved successfully!')
      
      // Clear API key field if it was saved
      if (apiKey) {
        setApiKey('')
      }
    } catch (err) {
      setError('Failed to save AI settings. Please try again.')
      console.error('Error saving settings:', err)
    } finally {
      setSaving(false)
    }
  }

  const handleTest = async () => {
    try {
      setTesting(true)
      setTestResult(null)
      const result = await apiClient.testAiConnection()
      setTestResult(result)
    } catch (err) {
      setTestResult({
        success: false,
        message: 'Failed to test connection. Please check your settings.',
      })
      console.error('Error testing connection:', err)
    } finally {
      setTesting(false)
    }
  }

  if (loading) {
    return (
      <div className="flex-1 overflow-auto p-8">
        <div className="max-w-4xl mx-auto">
          <div className="flex items-center justify-center py-12">
            <Loader2 className="h-8 w-8 animate-spin text-primary" />
          </div>
        </div>
      </div>
    )
  }

  return (
    <div className="flex-1 overflow-auto p-8">
      <div className="max-w-4xl mx-auto">
        <div className="mb-8">
          <div className="flex items-center gap-3 mb-2">
            <SettingsIcon className="h-8 w-8 text-primary" />
            <h1 className="text-3xl font-bold">Settings</h1>
          </div>
          <p className="text-muted-foreground">
            Configure AI features and preferences for Al-Hidayah Pro
          </p>
        </div>

        {error && (
          <Alert variant="destructive" className="mb-6">
            <AlertCircle className="h-4 w-4" />
            <AlertDescription>{error}</AlertDescription>
          </Alert>
        )}

        {success && (
          <Alert className="mb-6 border-green-500 bg-green-50">
            <CheckCircle2 className="h-4 w-4 text-green-600" />
            <AlertDescription className="text-green-600">{success}</AlertDescription>
          </Alert>
        )}

        <Card>
          <CardHeader>
            <CardTitle>AI Features</CardTitle>
            <CardDescription>
              Configure artificial intelligence features with automatic software fall-back support
            </CardDescription>
          </CardHeader>
          <CardContent className="space-y-6">
            {/* Enable AI Toggle */}
            <div className="flex items-center justify-between">
              <div className="space-y-0.5">
                <Label htmlFor="ai-enabled" className="text-base">Enable AI Features</Label>
                <p className="text-sm text-muted-foreground">
                  Turn on AI-powered features throughout the application
                </p>
              </div>
              <Switch
                id="ai-enabled"
                checked={isEnabled}
                onCheckedChange={setIsEnabled}
              />
            </div>

            {/* AI Provider Selection */}
            <div className="space-y-2">
              <Label htmlFor="provider">AI Provider</Label>
              <Select value={provider} onValueChange={setProvider}>
                <SelectTrigger id="provider">
                  <SelectValue placeholder="Select AI provider" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="none">None</SelectItem>
                  <SelectItem value="local">Local AI</SelectItem>
                  <SelectItem value="chatgpt">ChatGPT (OpenAI)</SelectItem>
                  <SelectItem value="claude">Claude (Anthropic)</SelectItem>
                </SelectContent>
              </Select>
              <p className="text-sm text-muted-foreground">
                Choose your preferred AI provider
              </p>
            </div>

            {/* API Key Input (only for cloud providers) */}
            {provider !== 'none' && provider !== 'local' && (
              <div className="space-y-2">
                <Label htmlFor="api-key">API Key</Label>
                <Input
                  id="api-key"
                  type="password"
                  value={apiKey}
                  onChange={(e) => setApiKey(e.target.value)}
                  placeholder={settings?.apiKey === '***CONFIGURED***' ? 'Already configured (leave empty to keep)' : 'Enter your API key'}
                />
                <p className="text-sm text-muted-foreground">
                  {provider === 'chatgpt' && 'Get your API key from https://platform.openai.com/api-keys'}
                  {provider === 'claude' && 'Get your API key from https://console.anthropic.com/settings/keys'}
                </p>
              </div>
            )}

            {/* Model Name (optional) */}
            {provider !== 'none' && (
              <div className="space-y-2">
                <Label htmlFor="model-name">Model Name (Optional)</Label>
                <Input
                  id="model-name"
                  value={modelName}
                  onChange={(e) => setModelName(e.target.value)}
                  placeholder={
                    provider === 'chatgpt' ? 'e.g., gpt-4, gpt-3.5-turbo' :
                    provider === 'claude' ? 'e.g., claude-3-opus-20240229' :
                    'Enter model name'
                  }
                />
                <p className="text-sm text-muted-foreground">
                  Specify the model to use (leave empty for default)
                </p>
              </div>
            )}

            {/* Software Fall-back Toggle */}
            <div className="flex items-center justify-between">
              <div className="space-y-0.5">
                <Label htmlFor="fallback" className="text-base">Software Fall-Back</Label>
                <p className="text-sm text-muted-foreground">
                  Use software fall-back when AI is unavailable (connection issues, offline mode, etc.)
                </p>
              </div>
              <Switch
                id="fallback"
                checked={useFallback}
                onCheckedChange={setUseFallback}
              />
            </div>

            {/* Test Connection Result */}
            {testResult && (
              <Alert variant={testResult.success ? 'default' : 'destructive'}>
                {testResult.success ? (
                  <CheckCircle2 className="h-4 w-4 text-green-600" />
                ) : (
                  <AlertCircle className="h-4 w-4" />
                )}
                <AlertDescription className={testResult.success ? 'text-green-600' : ''}>
                  {testResult.message}
                </AlertDescription>
              </Alert>
            )}

            {/* Action Buttons */}
            <div className="flex gap-3 pt-4">
              <Button
                onClick={handleSave}
                disabled={saving}
                className="flex-1"
              >
                {saving ? (
                  <>
                    <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                    Saving...
                  </>
                ) : (
                  'Save Settings'
                )}
              </Button>
              
              {provider !== 'none' && (
                <Button
                  onClick={handleTest}
                  disabled={testing || saving}
                  variant="outline"
                >
                  {testing ? (
                    <>
                      <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                      Testing...
                    </>
                  ) : (
                    'Test Connection'
                  )}
                </Button>
              )}
            </div>
          </CardContent>
        </Card>

        {/* Information Card */}
        <Card className="mt-6">
          <CardHeader>
            <CardTitle>About AI Features</CardTitle>
          </CardHeader>
          <CardContent className="space-y-3 text-sm text-muted-foreground">
            <p>
              <strong>Software Fall-Back:</strong> When enabled, the application will automatically use
              built-in software alternatives if AI services are unavailable due to connection issues,
              offline mode, user preference, or membership level restrictions.
            </p>
            <p>
              <strong>Local AI:</strong> Run AI models locally on your device (requires additional setup).
            </p>
            <p>
              <strong>ChatGPT:</strong> Use OpenAI's GPT models for AI features (requires API key and internet connection).
            </p>
            <p>
              <strong>Claude:</strong> Use Anthropic's Claude models for AI features (requires API key and internet connection).
            </p>
            <p className="text-xs">
              Note: Your API keys are stored securely and never shared with third parties.
              Always keep your API keys confidential.
            </p>
          </CardContent>
        </Card>
      </div>
    </div>
  )
}
