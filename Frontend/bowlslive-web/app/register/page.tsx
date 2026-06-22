"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";
import { Check } from "lucide-react";
import { startTransition, useState } from "react";

import { useAuth } from "@/components/auth/auth-provider";
import { ApiError } from "@/lib/api";

const inputClass =
  "h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20";

const benefits = [
  "Run unlimited singles, pairs, trips and fours tournaments",
  "Invite markers and members to score and follow along",
  "Open your events to visiting clubs",
] as const;

export default function RegisterPage() {
  const router = useRouter();
  const { register, status } = useAuth();
  const [email, setEmail] = useState("");
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setErrorMessage(null);
    setIsSubmitting(true);

    try {
      await register({ email, userName, password, confirmPassword });
      startTransition(() => {
        router.replace("/dashboard");
      });
    } catch (error) {
      setErrorMessage(error instanceof ApiError ? error.message : "Unable to create your account.");
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <div className="mx-auto flex min-h-[calc(100vh-5rem)] w-full max-w-6xl items-center px-4 py-12 sm:px-6 lg:px-8">
      <div className="grid w-full items-center gap-10 lg:grid-cols-[1fr_28rem]">
        <section className="hidden lg:block">
          <span className="inline-flex items-center gap-2 rounded-full border border-border/60 bg-card/70 px-3 py-1 text-xs font-medium text-muted-foreground backdrop-blur">
            <span className="size-1.5 rounded-full bg-primary" />
            Get your club started
          </span>
          <h1 className="mt-6 text-4xl font-semibold tracking-tight">
            Set up your club in minutes.
          </h1>
          <p className="mt-4 max-w-md text-base leading-7 text-muted-foreground">
            Create an account to host tournaments, manage your members and share live results with
            everyone who turns up to play.
          </p>
          <ul className="mt-8 space-y-3">
            {benefits.map((benefit) => (
              <li key={benefit} className="flex items-start gap-3 text-sm text-muted-foreground">
                <span className="mt-0.5 inline-flex rounded-full bg-primary/10 p-1 text-primary">
                  <Check className="size-4" aria-hidden="true" />
                </span>
                {benefit}
              </li>
            ))}
          </ul>
        </section>

        <section className="rounded-3xl border border-border/60 bg-card p-6 shadow-md sm:p-8">
          <h2 className="text-2xl font-semibold tracking-tight">Create your account</h2>
          <p className="mt-2 text-sm text-muted-foreground">
            You&apos;ll be signed in straight away and can set up your first tournament.
          </p>

          <form className="mt-6 space-y-5" onSubmit={handleSubmit}>
            <div className="space-y-2">
              <label className="text-sm font-medium" htmlFor="username">
                Display name
              </label>
              <input
                id="username"
                type="text"
                value={userName}
                onChange={(event) => setUserName(event.target.value)}
                autoComplete="username"
                className={inputClass}
                placeholder="Oakdale Bowling Club"
                required
              />
            </div>

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

            <div className="grid gap-5 sm:grid-cols-2">
              <div className="space-y-2">
                <label className="text-sm font-medium" htmlFor="password">
                  Password
                </label>
                <input
                  id="password"
                  type="password"
                  value={password}
                  onChange={(event) => setPassword(event.target.value)}
                  autoComplete="new-password"
                  className={inputClass}
                  placeholder="At least 8 characters"
                  required
                />
              </div>

              <div className="space-y-2">
                <label className="text-sm font-medium" htmlFor="confirmPassword">
                  Confirm password
                </label>
                <input
                  id="confirmPassword"
                  type="password"
                  value={confirmPassword}
                  onChange={(event) => setConfirmPassword(event.target.value)}
                  autoComplete="new-password"
                  className={inputClass}
                  placeholder="Repeat password"
                  required
                />
              </div>
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
              {isSubmitting ? "Creating account..." : "Create account"}
            </button>
          </form>

          <p className="mt-6 text-sm text-muted-foreground">
            Already registered?{" "}
            <Link href="/login" className="font-semibold text-primary hover:text-primary/80">
              Sign in
            </Link>
          </p>
        </section>
      </div>
    </div>
  );
}
