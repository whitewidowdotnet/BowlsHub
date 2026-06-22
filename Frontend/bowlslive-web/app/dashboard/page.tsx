"use client";

import Link from "next/link";
import { CalendarPlus, Mail, ShieldCheck, Trophy, UserRound } from "lucide-react";

import { useAuth } from "@/components/auth/auth-provider";
import { Button } from "@/components/ui/button";

export default function DashboardPage() {
  const { logout, session, status, user } = useAuth();

  if (status === "loading") {
    return (
      <div className="mx-auto flex min-h-[calc(100vh-5rem)] w-full max-w-4xl items-center justify-center px-4 py-10 sm:px-6 lg:px-8">
        <div className="rounded-3xl border border-border/60 bg-card px-8 py-10 text-center shadow-sm">
          <p className="text-sm font-medium text-muted-foreground">Loading your dashboard…</p>
        </div>
      </div>
    );
  }

  if (!user || !session) {
    return (
      <div className="mx-auto flex min-h-[calc(100vh-5rem)] w-full max-w-4xl items-center justify-center px-4 py-10 sm:px-6 lg:px-8">
        <div className="w-full rounded-3xl border border-border/60 bg-card p-8 text-center shadow-sm sm:p-10">
          <h1 className="text-3xl font-semibold tracking-tight">Please sign in to continue.</h1>
          <p className="mt-4 text-muted-foreground">
            Your club dashboard is only available once you&apos;re signed in.
          </p>
          <div className="mt-6 flex flex-col justify-center gap-3 sm:flex-row">
            <Link
              href="/login"
              className="inline-flex h-12 items-center justify-center rounded-xl bg-primary px-6 text-sm font-semibold text-primary-foreground shadow-sm transition hover:bg-primary/90"
            >
              Sign in
            </Link>
            <Link
              href="/register"
              className="inline-flex h-12 items-center justify-center rounded-xl border border-border bg-background px-6 text-sm font-semibold text-foreground transition hover:bg-muted"
            >
              Register your club
            </Link>
          </div>
        </div>
      </div>
    );
  }

  const signedInUntil = new Date(session.expiresAtUtc).toLocaleString(undefined, {
    dateStyle: "medium",
    timeStyle: "short",
  });

  return (
    <div className="mx-auto flex w-full max-w-5xl flex-col gap-6 px-4 py-10 sm:px-6 lg:px-8">
      <section className="rounded-3xl border border-border/60 bg-card p-8 shadow-sm sm:p-10">
        <div className="flex flex-col gap-6 lg:flex-row lg:items-end lg:justify-between">
          <div>
            <p className="text-sm font-medium text-muted-foreground">Club dashboard</p>
            <h1 className="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">
              Welcome, {user.userName}.
            </h1>
            <p className="mt-3 max-w-2xl text-base leading-7 text-muted-foreground">
              This is home base for your club. Set up tournaments, manage your members and keep an
              eye on live results — all from here.
            </p>
          </div>

          <Button variant="outline" size="lg" onClick={logout}>
            Sign out
          </Button>
        </div>
      </section>

      <section className="rounded-3xl border border-dashed border-border bg-card/60 p-8 text-center shadow-sm sm:p-10">
        <div className="mx-auto inline-flex rounded-2xl bg-accent p-3 text-accent-foreground ring-1 ring-primary/10">
          <Trophy className="size-6" aria-hidden="true" />
        </div>
        <h2 className="mt-5 text-xl font-semibold tracking-tight">No tournaments yet</h2>
        <p className="mx-auto mt-2 max-w-md text-sm leading-6 text-muted-foreground">
          When you create your first tournament, entries, draws and live scoring will show up here.
          Tournament setup is rolling out soon.
        </p>
        <span className="mt-6 inline-flex h-11 cursor-not-allowed items-center justify-center gap-2 rounded-xl bg-muted px-5 text-sm font-semibold text-muted-foreground">
          <CalendarPlus className="size-4" aria-hidden="true" />
          Create tournament (coming soon)
        </span>
      </section>

      <section className="grid gap-4 md:grid-cols-2">
        <article className="rounded-3xl border border-border/60 bg-card p-6 shadow-sm">
          <div className="inline-flex rounded-2xl bg-primary/10 p-3 text-primary">
            <UserRound className="size-5" aria-hidden="true" />
          </div>
          <h2 className="mt-5 text-lg font-semibold">Club profile</h2>
          <dl className="mt-4 space-y-3 text-sm">
            <div className="rounded-xl bg-muted/60 px-4 py-3">
              <dt className="text-muted-foreground">Display name</dt>
              <dd className="mt-1 font-medium">{user.userName}</dd>
            </div>
            <div className="rounded-xl bg-muted/60 px-4 py-3">
              <dt className="inline-flex items-center gap-1.5 text-muted-foreground">
                <Mail className="size-3.5" aria-hidden="true" /> Email
              </dt>
              <dd className="mt-1 font-medium">{user.email}</dd>
            </div>
            <div className="rounded-xl bg-muted/70 px-4 py-3">
              <dt className="text-muted-foreground">User ID</dt>
              <dd className="mt-1 break-all font-medium">{user.id}</dd>
            </div>
            <div className="rounded-xl bg-muted/70 px-4 py-3">
              <dt className="text-muted-foreground">Roles</dt>
              <dd className="mt-1 font-medium">{user.roles?.length ? user.roles.join(", ") : "No roles assigned"}</dd>
            </div>
          </dl>
        </article>

        <article className="rounded-3xl border border-border/60 bg-card p-6 shadow-sm">
          <div className="inline-flex rounded-2xl bg-primary/10 p-3 text-primary">
            <ShieldCheck className="size-5" aria-hidden="true" />
          </div>
          <h2 className="mt-5 text-lg font-semibold">Sign-in</h2>
          <dl className="mt-4 space-y-3 text-sm">
            <div className="rounded-xl bg-muted/60 px-4 py-3">
              <dt className="text-muted-foreground">Status</dt>
              <dd className="mt-1 inline-flex items-center gap-2 font-medium">
                <span className="size-2 rounded-full bg-primary" />
                Signed in
              </dd>
            </div>
            <div className="rounded-xl bg-muted/60 px-4 py-3">
              <dt className="text-muted-foreground">Signed in until</dt>
              <dd className="mt-1 font-medium">{signedInUntil}</dd>
            </div>
          </dl>
        </article>
      </section>
    </div>
  );
}
