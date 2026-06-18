import { Activity, ArrowRight, ShieldCheck, Trophy, Waves } from "lucide-react";
import Link from "next/link";

import { HomeAuthActions } from "@/components/home-auth-actions";
import { buttonVariants } from "@/components/ui/button";
import { cn } from "@/lib/utils";

const highlights = [
  {
    title: "Secure member access",
    description: "Identity-backed accounts and JWT-protected API requests keep club data private.",
    icon: ShieldCheck,
  },
  {
    title: "Responsive tournament views",
    description: "Scoreboards, schedules, and player dashboards stay usable from phones to clubhouse screens.",
    icon: Activity,
  },
  {
    title: "Live competition updates",
    description: "Track fixtures, standings, and match movement as events unfold.",
    icon: Trophy,
  },
  {
    title: "Built for bowls clubs",
    description: "A focused foundation for registrations, fixtures, and everyday club administration.",
    icon: Waves,
  },
] as const;

export default function Home() {
  const year = new Date().getFullYear();

  return (
    <div className="mx-auto flex w-full max-w-6xl flex-col px-4 py-8 sm:px-6 lg:px-8">
      <section className="grid gap-8 lg:grid-cols-[1.2fr_0.8fr] lg:items-stretch">
        <div className="rounded-[2rem] border border-border/70 bg-card/80 p-8 shadow-sm backdrop-blur sm:p-10 lg:p-12">
          <p className="text-sm font-semibold uppercase tracking-[0.25em] text-primary">
            Oakdale Rolbal Platform
          </p>
          <h1 className="mt-5 max-w-3xl text-4xl font-semibold tracking-tight text-balance sm:text-5xl lg:text-6xl">
            A secure, responsive home base for bowls tournaments and club operations.
          </h1>
          <p className="mt-6 max-w-2xl text-base leading-7 text-muted-foreground sm:text-lg">
            The first iteration is focused on the web foundation: account creation, sign-in,
            protected API access, and a layout that feels good on mobile and desktop from day one.
          </p>
          <div className="mt-8 flex flex-col gap-3 sm:flex-row">
            <Link
              href="/register"
              className={cn(buttonVariants({ size: "lg" }), "justify-center px-5")}
            >
              <span className="inline-flex items-center gap-2">
                Create an account
                <ArrowRight className="size-4" />
              </span>
            </Link>
            <Link
              href="/login"
              className={cn(buttonVariants({ size: "lg", variant: "outline" }), "justify-center px-5")}
            >
              Sign in
            </Link>
          </div>
        </div>

        <HomeAuthActions />
      </section>

      <section className="mt-8 grid gap-4 md:grid-cols-2 xl:grid-cols-4">
        {highlights.map((highlight) => {
          const Icon = highlight.icon;

          return (
            <article
              key={highlight.title}
              className="rounded-[1.5rem] border border-border/70 bg-card/70 p-6 shadow-sm backdrop-blur"
            >
              <div className="inline-flex rounded-2xl bg-primary/12 p-3 text-primary">
                <Icon className="size-5" aria-hidden="true" />
              </div>
              <h2 className="mt-5 text-lg font-semibold">{highlight.title}</h2>
              <p className="mt-3 text-sm leading-6 text-muted-foreground">
                {highlight.description}
              </p>
            </article>
          );
        })}
      </section>

      <footer className="mt-12 flex flex-col gap-2 border-t border-border/70 py-6 text-sm text-muted-foreground sm:flex-row sm:items-center sm:justify-between">
        <p>Oakdale Rolbal</p>
        <p>{year}</p>
      </footer>
    </div>
  );
}
