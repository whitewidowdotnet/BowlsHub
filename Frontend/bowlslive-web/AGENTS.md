<!-- BEGIN:nextjs-agent-rules -->

# This is NOT the Next.js you know

This version has breaking changes — APIs, conventions, and file structure may all differ from your training data. Read the relevant guide in `node_modules/next/dist/docs/` before writing any code. Heed deprecation notices.

<!-- END:nextjs-agent-rules -->

# Brand & theme — apply to ALL front-end work

The app's visual identity comes from the BowlsLive icon (a speeding navy bowls ball
with maroon + gold motion trails on a warm cream field). Source of truth:
`app/icon.svg` and `public/icons/icon-{192,512}.svg`. Keep these in sync if the mark changes.

Palette (defined as CSS variables in `app/globals.css`; never hardcode hex/RGB in components):

- **Background** — warm cream (`--background`, `oklch(0.965 0.028 92)`)
- **Foreground / ink** — deep navy (`--foreground`, `oklch(0.27 0.03 264)`)
- **Primary (calls to action)** — deep maroon (`--primary`, `#7b2e3b` ≈ `oklch(0.44 0.11 16)`)
- **Accent / highlights** — gold (`--accent`, `oklch(0.86 0.07 90)`)
- **Dark mode** — navy surfaces with the same maroon primary + gold accent.

Rules for new pages/components:
- Style only through the semantic Tailwind/shadcn tokens: `bg-background`, `text-foreground`,
  `bg-primary`/`text-primary-foreground`, `bg-card`, `border-border`, `bg-muted`, `text-accent-foreground`, etc.
  This keeps light/dark and any future palette change automatic.
- Use the existing card idiom: `rounded-[1.5rem]/[2rem] border border-border/70 bg-card/70 shadow-sm backdrop-blur`.
- Maroon = primary action; gold = accents/decorative; navy = headings/body ink; cream = surfaces.
- PWA `theme_color` is maroon `#7b2e3b`, `background_color` is cream `#faf1d6` (`public/manifest.webmanifest`).
