import { CalendarDays, LineChart, Radio, Target, Trophy, Users } from "lucide-react";

import { HomeAuthActions } from "@/components/home-auth-actions";

const features = [
  {
    title: "Entries & draws",
    description:
      "Open entries for singles, pairs, trips or fours, then generate the draw and publish fixtures in a couple of clicks.",
    icon: CalendarDays,
  },
  {
    title: "Live scoring",
    description:
      "Markers tap in ends and shots from the bank. Every rink updates in real time — no paper cards to chase.",
    icon: Radio,
  },
  {
    title: "Standings & results",
    description:
      "Section logs, shot differences and qualifiers recalculate the moment a game is confirmed.",
    icon: LineChart,
  },
  {
    title: "Clubs & members",
    description:
      "Keep your member roll in one place and let visiting clubs enter your opens with a single login.",
    icon: Users,
  },
] as const;

const steps = [
  { label: "Open entries", detail: "Set format, fees and closing date." },
  { label: "Publish the draw", detail: "Rinks and sections, sent to players." },
  { label: "Score live", detail: "Ends and shots, green-side." },
  { label: "Crown a winner", detail: "Logs and results, settled automatically." },
] as const;

export default function Home() {
  const year = new Date().getFullYear();

  return (
    <div className="mx-auto flex w-full max-w-6xl flex-col px-4 py-10 sm:px-6 lg:px-8 lg:py-16">
      <section className="grid items-center gap-10 lg:grid-cols-[1.05fr_0.95fr]">
        <div>
          <span className="inline-flex items-center gap-2 rounded-full border border-border/60 bg-card/70 px-3 py-1 text-xs font-medium text-muted-foreground backdrop-blur">
            <span className="size-1.5 rounded-full bg-primary" />
            For bowls clubs &amp; tournament organisers
          </span>

          <h1 className="mt-6 text-4xl font-semibold tracking-tight text-balance sm:text-5xl lg:text-6xl">
            Run your club&apos;s bowls, from the{" "}
            <span className="text-primary">first draw</span> to the{" "}
            <span className="text-primary">final shot</span>.
          </h1>

          <p className="mt-6 max-w-xl text-base leading-7 text-muted-foreground sm:text-lg">
            BowlsLive brings entries, draws, live scoring and standings into one place — so
            players, markers and organisers always know what&apos;s happening on the green.
          </p>

          <div className="mt-8">
            <HomeAuthActions />
          </div>
        </div>

        <ScoreboardPreview />
      </section>

      <section className="mt-20">
        <div className="max-w-2xl">
          <h2 className="text-2xl font-semibold tracking-tight sm:text-3xl">
            Everything a tournament needs, in one app
          </h2>
          <p className="mt-3 text-muted-foreground">
            Built around how bowls is actually run — by clubs, for their players and visitors.
          </p>
        </div>

        <div className="mt-8 grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
          {features.map((feature) => {
            const Icon = feature.icon;

            return (
              <article
                key={feature.title}
                className="group rounded-3xl border border-border/60 bg-card/70 p-6 shadow-sm backdrop-blur transition hover:-translate-y-0.5 hover:shadow-md"
              >
                <div className="inline-flex rounded-2xl bg-accent p-3 text-accent-foreground ring-1 ring-primary/10">
                  <Icon className="size-5" aria-hidden="true" />
                </div>
                <h3 className="mt-5 text-lg font-semibold">{feature.title}</h3>
                <p className="mt-2 text-sm leading-6 text-muted-foreground">{feature.description}</p>
              </article>
            );
          })}
        </div>
      </section>

      <section className="mt-16 rounded-3xl border border-border/60 bg-card/60 p-8 shadow-sm backdrop-blur sm:p-10">
        <h2 className="text-2xl font-semibold tracking-tight">From nomination to silverware</h2>
        <ol className="mt-8 grid gap-6 sm:grid-cols-2 lg:grid-cols-4">
          {steps.map((step, index) => (
            <li key={step.label} className="relative">
              <span className="text-sm font-semibold text-primary">
                {String(index + 1).padStart(2, "0")}
              </span>
              <h3 className="mt-2 text-base font-semibold">{step.label}</h3>
              <p className="mt-1 text-sm leading-6 text-muted-foreground">{step.detail}</p>
            </li>
          ))}
        </ol>
      </section>

      <footer className="mt-16 flex flex-col gap-2 border-t border-border/60 py-8 text-sm text-muted-foreground sm:flex-row sm:items-center sm:justify-between">
        <p className="inline-flex items-center gap-2">
          <Trophy className="size-4 text-primary" aria-hidden="true" />
          BowlsLive — tournament software for bowls clubs
        </p>
        <p>&copy; {year} BowlsLive</p>
      </footer>
    </div>
  );
}

function ScoreboardPreview() {
  return (
    <div className="relative">
      <div className="absolute -inset-4 -z-10 rounded-[2.5rem] bg-gradient-to-br from-accent/40 via-transparent to-primary/10 blur-xl" />
      <div className="rounded-3xl border border-border/60 bg-card/85 p-6 shadow-md backdrop-blur sm:p-7">
        <div className="flex items-center justify-between">
          <span className="inline-flex items-center gap-2 rounded-full bg-primary/10 px-3 py-1 text-xs font-semibold text-primary">
            <span className="relative flex size-2">
              <span className="absolute inline-flex size-full animate-ping rounded-full bg-primary/60" />
              <span className="relative inline-flex size-2 rounded-full bg-primary" />
            </span>
            Live
          </span>
          <span className="text-xs font-medium text-muted-foreground">Rink 3 · End 9 of 18</span>
        </div>

        <p className="mt-5 text-xs font-semibold uppercase tracking-[0.18em] text-muted-foreground">
          Midlands Open · Section A
        </p>

        <div className="mt-4 space-y-3">
          <ScoreRow club="Oakdale" badge="OAK" shots={7} leading />
          <ScoreRow club="Riverside" badge="RIV" shots={5} />
        </div>

        <div className="mt-5 flex items-center justify-between border-t border-border/60 pt-4 text-xs text-muted-foreground">
          <span className="inline-flex items-center gap-1.5">
            <Target className="size-3.5 text-primary" aria-hidden="true" />
            Marker: J. Pretorius
          </span>
          <span>Updated 4s ago</span>
        </div>
      </div>
    </div>
  );
}

function ScoreRow({
  club,
  badge,
  shots,
  leading = false,
}: {
  club: string;
  badge: string;
  shots: number;
  leading?: boolean;
}) {
  return (
    <div className="flex items-center justify-between rounded-2xl bg-muted/60 px-4 py-3">
      <div className="flex items-center gap-3">
        <span className="grid size-9 place-items-center rounded-xl bg-foreground text-xs font-semibold text-background">
          {badge}
        </span>
        <span className="font-medium">{club} BC</span>
      </div>
      <span
        className={`text-2xl font-semibold tabular-nums ${leading ? "text-primary" : "text-foreground"}`}
      >
        {shots}
      </span>
    </div>
  );
}
