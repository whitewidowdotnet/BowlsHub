"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";
import { CalendarDays, LineChart, Radio } from "lucide-react";
import { startTransition, useState } from "react";

import { useAuth } from "@/components/auth/auth-provider";
import { ApiError } from "@/lib/api";

const inputClass =
  "h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20";

const perks = [
  { icon: CalendarDays, text: "Manage entries and draws for every tournament" },
  { icon: Radio, text: "Follow live scores as games play out on the green" },
  { icon: LineChart, text: "See section logs and results update on their own" },
] as const;

export default function LoginPage() {
  const router = useRouter();
  const { login, status } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setErrorMessage(null);
    setIsSubmitting(true);

    try {
      await login({ email, password });
      startTransition(() => {
        router.replace("/dashboard");
      });
    } catch (error) {
      setErrorMessage(error instanceof ApiError ? error.message : "Unable to sign in right now.");
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <div className="mx-auto flex min-h-[calc(100vh-5rem)] w-full max-w-6xl items-center px-4 py-12 sm:px-6 lg:px-8">
      <div className="grid w-full items-center gap-10 lg:grid-cols-[1fr_26rem]">
        <section className="hidden lg:block">
          <span className="inline-flex items-center gap-2 rounded-full border border-border/60 bg-card/70 px-3 py-1 text-xs font-medium text-muted-foreground backdrop-blur">
            <span className="size-1.5 rounded-full bg-primary" />
            Welcome back
          </span>
          <h1 className="mt-6 text-4xl font-semibold tracking-tight">
            Back to the green.
          </h1>
          <p className="mt-4 max-w-md text-base leading-7 text-muted-foreground">
            Sign in to pick up where your club left off — entries, draws and live results, all in
            one place.
          </p>
          <ul className="mt-8 space-y-4">
            {perks.map((perk) => {
              const Icon = perk.icon;
              return (
                <li key={perk.text} className="flex items-start gap-3 text-sm text-muted-foreground">
                  <span className="mt-0.5 inline-flex rounded-lg bg-accent p-2 text-accent-foreground">
                    <Icon className="size-4" aria-hidden="true" />
                  </span>
                  {perk.text}
                </li>
              );
            })}
          </ul>
        </section>

        <section className="rounded-3xl border border-border/60 bg-card p-6 shadow-md sm:p-8">
          <h2 className="text-2xl font-semibold tracking-tight">Sign in</h2>
          <p className="mt-2 text-sm text-muted-foreground">
            Use the email and password you registered with your club.
          </p>

          <form className="mt-6 space-y-5" onSubmit={handleSubmit}>
            <div className="space-y-2">
              <label className="text-sm font-medium" htmlFor="email">
                Email
              </label>
              <input
                id="email"
                type="email"
                value={email}
                onChange={(event) => setEmail(event.target.value)}
                autoComplete="email"
                className={inputClass}
                placeholder="you@yourclub.co.za"
                required
              />
            </div>

            <div className="space-y-2">
              <div className="flex items-center justify-between">
                <label className="text-sm font-medium" htmlFor="password">
                  Password
                </label>
              </div>
              <input
                id="password"
                type="password"
                value={password}
                onChange={(event) => setPassword(event.target.value)}
                autoComplete="current-password"
                className={inputClass}
                placeholder="Enter your password"
                required
              />
            </div>

            {errorMessage ? (
              <p className="rounded-xl border border-destructive/30 bg-destructive/10 px-4 py-3 text-sm text-destructive">
                {errorMessage}
              </p>
            ) : null}

            <button
              type="submit"
              disabled={isSubmitting || status === "loading"}
              className="inline-flex h-12 w-full items-center justify-center rounded-xl bg-primary px-4 text-sm font-semibold text-primary-foreground shadow-sm transition hover:bg-primary/90 disabled:cursor-not-allowed disabled:opacity-60"
            >
              {isSubmitting ? "Signing in..." : "Sign in"}
            </button>
          </form>

          <p className="mt-6 text-sm text-muted-foreground">
            New club?{" "}
            <Link href="/register" className="font-semibold text-primary hover:text-primary/80">
              Create an account
            </Link>
          </p>
        </section>
      </div>
    </div>
  );
}
