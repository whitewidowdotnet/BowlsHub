"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";
import { startTransition, useState } from "react";

import { useAuth } from "@/components/auth/auth-provider";
import { ApiError } from "@/lib/api";

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
    <div className="mx-auto flex min-h-[calc(100vh-5rem)] w-full max-w-6xl items-center px-4 py-10 sm:px-6 lg:px-8">
      <div className="grid w-full gap-8 lg:grid-cols-[1fr_30rem]">
        <section className="rounded-[2rem] border border-border/70 bg-card/70 p-8 shadow-sm backdrop-blur sm:p-10">
          <p className="text-sm font-semibold uppercase tracking-[0.25em] text-primary">
            Register
          </p>
          <h1 className="mt-4 text-4xl font-semibold tracking-tight">Create your club account.</h1>
          <p className="mt-4 max-w-2xl text-base leading-7 text-muted-foreground">
            Registration returns a JWT immediately, so the web app can move straight into an
            authenticated session after account creation.
          </p>
        </section>

        <section className="rounded-[2rem] border border-border/70 bg-card p-6 shadow-sm sm:p-8">
          <form className="space-y-5" onSubmit={handleSubmit}>
            <div className="space-y-2">
              <label className="text-sm font-medium" htmlFor="username">
                Username
              </label>
              <input
                id="username"
                type="text"
                value={userName}
                onChange={(event) => setUserName(event.target.value)}
                autoComplete="username"
                className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
                placeholder="oakdale.member"
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
                className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
                placeholder="member@bowlslive.co.za"
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
                  className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
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
                  className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
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
              className="inline-flex h-12 w-full items-center justify-center rounded-xl bg-primary px-4 text-sm font-semibold text-primary-foreground transition hover:bg-primary/90 disabled:cursor-not-allowed disabled:opacity-60"
            >
              {isSubmitting ? "Creating account..." : "Create account"}
            </button>
          </form>

          <p className="mt-6 text-sm text-muted-foreground">
            Already have an account?{" "}
            <Link href="/login" className="font-semibold text-primary hover:text-primary/80">
              Sign in
            </Link>
          </p>
        </section>
      </div>
    </div>
  );
}
