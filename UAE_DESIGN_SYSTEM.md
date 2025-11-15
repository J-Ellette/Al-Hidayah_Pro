# UAE Design System Integration

This document describes how the UAE Government Design System is integrated into Al-Hidayah Pro.

## Overview

Al-Hidayah Pro uses the [UAE Government Design System](https://designsystem.gov.ae/) React package (`@aegov/design-system-react`) which provides standards-compliant components, RTL support, Arabic typography, and theming following UAE government guidelines.

## Package Information

- **Package**: `@aegov/design-system-react`
- **Version**: ^1.1.2
- **Repository**: https://github.com/TDRA-ae/aegov-dls-react
- **Documentation**: https://designsystem.gov.ae/

## Features Provided

### 1. RTL (Right-to-Left) Support
The design system automatically supports RTL layout for Arabic content. All components are built to handle bidirectional text properly.

### 2. Arabic Typography
Configured fonts for Arabic support:
- **Noto Kufi Arabic**: Primary Arabic font
- **Alexandria**: Alternative Arabic font
- **Roboto**: Latin text
- **Inter**: UI elements

### 3. UAE Color Palettes

The following official UAE color palettes are available in Tailwind classes:

- **aegold**: UAE Gold colors (50-950)
- **aered**: Red variants (50-950)
- **aegreen**: Green variants (50-950)
- **aeblack**: Black/Grey variants (50-950)
- **whitely**: White variants (50-500)
- **camel**: Camel/Orange variants (50-950)
- **slate**: Slate variants (50-950)
- **fuchsia**: Fuchsia variants (50-950)
- **techblue**: Technology blue variants (50-950)
- **seablue**: Sea blue variants (50-950)
- **desert**: Desert/Brown variants (50-950)

#### Primary Palettes
- **primary**: UAE Gold (50-950) - Use for primary actions and branding
- **secondary**: Black/Grey (50-950) - Use for secondary elements
- **primary-support**: Supporting yellow/orange colors (50-400)
- **secondary-support**: Supporting blue colors (50-400)
- **aered-support**: Supporting red colors (50-500)

### 4. Typography Scale

UAE Design System provides specific typography sizes:
- `text-h1` through `text-h6`: Heading sizes
- `text-display`: Extra large display text
- Standard sizes: `xs`, `sm`, `base`, `lg`, `xl`, `2xl`, `3xl`

## Available Components

The following UAE Design System components are available for use:

- **Accordion**: Collapsible content sections
- **Alert**: Notification and alert messages
- **Avatar**: User profile images
- **Banner**: Full-width informational banners
- **Blockquote**: Quoted text blocks
- **Breadcrumbs**: Navigation breadcrumb trails
- **Button**: Primary action buttons
- **Card**: Content containers
- **Checkbox**: Checkbox inputs
- **Dropdown**: Dropdown menus
- **FileUpload**: File upload components
- **Hyperlink**: Styled links
- **Input**: Text input fields
- **Modal**: Dialog/modal windows
- **Navigation**: Navigation menus
- **Pagination**: Page navigation
- **Popover**: Popover tooltips (PopoverRoot, PopoverTrigger, PopoverContent)
- **Radio**: Radio button inputs (Radio, RadioItem)
- **RangeSlider**: Slider inputs
- **Select**: Select dropdown
- **Steps**: Step indicators
- **Tabs**: Tabbed content
- **Textarea**: Multi-line text input
- **Toast**: Toast notifications
- **Toggle**: Toggle switches
- **Tooltip**: Hover tooltips

## Usage Examples

### Using UAE Design System Components

```jsx
import { Button, Card, Alert } from '@aegov/design-system-react';

function MyComponent() {
  return (
    <Card>
      <Alert type="info">This uses UAE Design System components</Alert>
      <Button variant="primary">UAE Button</Button>
    </Card>
  );
}
```

### Using UAE Colors

```jsx
<div className="bg-aegold-500 text-white">
  UAE Gold Background
</div>

<div className="bg-primary-500 text-white">
  Primary Color (UAE Gold)
</div>

<button className="bg-seablue-600 hover:bg-seablue-700">
  Sea Blue Button
</button>
```

### Using UAE Typography

```jsx
<h1 className="text-h1 font-alexandria">
  Arabic Heading - العنوان بالعربية
</h1>

<p className="text-base font-notokufi">
  Arabic text content - نص المحتوى بالعربية
</p>
```

### RTL Layout

To enable RTL layout for Arabic content, add the `dir="rtl"` attribute:

```jsx
<div dir="rtl" className="font-notokufi">
  <h2 className="text-h3">العنوان</h2>
  <p>هذا النص بالعربية يظهر من اليمين إلى اليسار</p>
</div>
```

## Configuration

### Tailwind Configuration

The UAE Design System theme is integrated in `tailwind.config.js`:
- All UAE color palettes
- UAE font families
- UAE spacing and sizing conventions
- Container settings for proper layout

### Styles Import

The UAE Design System styles are imported in `src/main.css`:
```css
@import '@aegov/design-system-react/dist/styles/tailwind.css';
```

### Font Configuration

Fonts are loaded in `index.html`:
```html
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600&family=Roboto:wght@400;500;600&family=Noto+Kufi+Arabic:wght@400;500;600&display=swap" rel="stylesheet">
```

## Best Practices

1. **Use UAE Components First**: Prefer UAE Design System components over custom implementations for consistency with government standards.

2. **Follow Color Guidelines**: Use the primary (aegold) palette for main actions and branding.

3. **Support Arabic**: Always test with Arabic content and ensure RTL layout works properly.

4. **Accessibility**: UAE Design System components are built with accessibility in mind. Maintain WCAG compliance when adding custom elements.

5. **Responsive Design**: Use the UAE Design System's responsive utilities and container settings.

## Resources

- **Official Documentation**: https://designsystem.gov.ae/
- **Components Reference**: https://designsystem.gov.ae/docs/components
- **Guidelines**: https://designsystem.gov.ae/guidelines
- **Blocks & Patterns**: https://designsystem.gov.ae/docs/blocks
- **GitHub Repository**: https://github.com/TDRA-ae/aegov-dls-react
