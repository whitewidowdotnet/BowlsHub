"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";
import { startTransition, useState } from "react";

import { useAuth } from "@/components/auth/auth-provider";
import { ApiError } from "@/lib/api";

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
    <div className="mx-auto flex min-h-[calc(100vh-5rem)] w-full max-w-6xl items-center px-4 py-10 sm:px-6 lg:px-8">
      <div className="grid w-full gap-8 lg:grid-cols-[1fr_28rem]">
        <section className="rounded-[2rem] border border-border/70 bg-card/70 p-8 shadow-sm backdrop-blur sm:p-10">
          <p className="text-sm font-semibold uppercase tracking-[0.25em] text-primary">
            Sign in
          </p>
          <h1 className="mt-4 text-4xl font-semibold tracking-tight">Welcome back to BowlsLive.</h1>
          <p className="mt-4 max-w-2xl text-base leading-7 text-muted-foreground">
            Use your email and password to access the protected web experience and JWT-backed API.
          </p>
        </section>

        <section className="rounded-[2rem] border border-border/70 bg-card p-6 shadow-sm sm:p-8">
          <form className="space-y-5" onSubmit={handleSubmit}>
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
                className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
                placeholder="member@bowlslive.co.za"
                required
              />
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium" htmlFor="password">
                Password
              </label>
              <input
                id="password"
                type="password"
                value={password}
                onChange={(event) => setPassword(event.target.value)}
                autoComplete="current-password"
                className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
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
              className="inline-flex h-12 w-full items-center justify-center rounded-xl bg-primary px-4 text-sm font-semibold text-primary-foreground transition hover:bg-primary/90 disabled:cursor-not-allowed disabled:opacity-60"
            >
              {isSubmitting ? "Signing in..." : "Sign in"}
            </button>
          </form>

          <p className="mt-6 text-sm text-muted-foreground">
            New here?{" "}
            <Link href="/register" className="font-semibold text-primary hover:text-primary/80">
              Create an account
            </Link>
          </p>
        </section>
      </div>
    </div>
  );
}
