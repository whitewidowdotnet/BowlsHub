import { Building2, Trophy, UserRoundPlus, Waves } from "lucide-react";

import { Button } from "@/components/ui/button";

const features = [
  {
    title: "Tournament Management",
    description: "Set up events, organize brackets, and keep each tournament on schedule.",
    icon: Trophy,
  },
  {
    title: "Live Leaderboards",
    description: "Share real-time standings so players and spectators can follow every update.",
    icon: Waves,
  },
  {
    title: "Player Registration",
    description: "Register members quickly and keep participant details accurate and centralized.",
    icon: UserRoundPlus,
  },
  {
    title: "Club Administration",
    description: "Manage club operations from one place with clear, reliable workflows.",
    icon: Building2,
  },
] as const;

export default function Home() {
  const year = new Date().getFullYear();

  return (
    <div className="bg-background text-foreground">
      <main className="mx-auto flex min-h-screen w-full max-w-6xl flex-col px-4 py-10 sm:px-6 lg:px-8">
        <section className="rounded-2xl border border-border bg-card p-8 shadow-sm sm:p-12">
          <p className="text-sm font-semibold uppercase tracking-widest text-primary">
            Bowls Club Platform
          </p>
          <h1 className="mt-4 text-4xl font-bold tracking-tight sm:text-5xl">Oakdale Rolbal</h1>
          <p className="mt-4 max-w-2xl text-base text-muted-foreground sm:text-lg">
            Manage tournaments, track scores, and view live leaderboards.
          </p>
          <div className="mt-8">
            <Button size="lg" className="px-6">
              Coming Soon
            </Button>
          </div>
        </section>

        <section className="mt-12">
          <h2 className="text-2xl font-semibold tracking-tight">Features</h2>
          <div className="mt-6 grid gap-4 sm:grid-cols-2">
            {features.map((feature) => {
              const Icon = feature.icon;

              return (
                <article
                  key={feature.title}
                  className="rounded-xl border border-border bg-card p-6 shadow-sm"
                >
                  <Icon className="size-6 text-primary" aria-hidden="true" />
                  <h3 className="mt-4 text-lg font-semibold">{feature.title}</h3>
                  <p className="mt-2 text-sm text-muted-foreground">{feature.description}</p>
                </article>
              );
            })}
          </div>
        </section>

        <footer className="mt-auto pt-12 text-sm text-muted-foreground">
          <p>Oakdale Rolbal</p>
          <p className="mt-1">{year}</p>
          <p className="mt-1">Built with React, Next.js and ASP.NET Core</p>
        </footer>
      </main>
    </div>
  );
}
