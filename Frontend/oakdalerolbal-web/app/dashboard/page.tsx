"use client";

import Link from "next/link";
import { ShieldCheck, UserRound } from "lucide-react";

import { useAuth } from "@/components/auth/auth-provider";
import { Button, buttonVariants } from "@/components/ui/button";
import { cn } from "@/lib/utils";

export default function DashboardPage() {
  const { logout, session, status, user } = useAuth();

  if (status === "loading") {
    return (
      <div className="mx-auto flex min-h-[calc(100vh-5rem)] w-full max-w-4xl items-center justify-center px-4 py-10 sm:px-6 lg:px-8">
        <div className="rounded-[2rem] border border-border/70 bg-card px-8 py-10 text-center shadow-sm">
          <p className="text-sm font-medium text-muted-foreground">Checking your session...</p>
        </div>
      </div>
    );
  }

  if (!user || !session) {
    return (
      <div className="mx-auto flex min-h-[calc(100vh-5rem)] w-full max-w-4xl items-center justify-center px-4 py-10 sm:px-6 lg:px-8">
        <div className="w-full rounded-[2rem] border border-border/70 bg-card p-8 text-center shadow-sm sm:p-10">
          <h1 className="text-3xl font-semibold tracking-tight">You need to sign in first.</h1>
          <p className="mt-4 text-muted-foreground">
            This page depends on the protected `/api/auth/me` endpoint.
          </p>
          <div className="mt-6 flex flex-col justify-center gap-3 sm:flex-row">
            <Link href="/login" className={buttonVariants({ size: "lg" })}>
              Go to sign in
            </Link>
            <Link
              href="/register"
              className={cn(buttonVariants({ size: "lg", variant: "outline" }), "justify-center")}
            >
              Create account
            </Link>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="mx-auto flex w-full max-w-5xl flex-col gap-6 px-4 py-10 sm:px-6 lg:px-8">
      <section className="rounded-[2rem] border border-border/70 bg-card p-8 shadow-sm sm:p-10">
        <div className="flex flex-col gap-6 lg:flex-row lg:items-end lg:justify-between">
          <div>
            <p className="text-sm font-semibold uppercase tracking-[0.25em] text-primary">
              Protected dashboard
            </p>
            <h1 className="mt-4 text-4xl font-semibold tracking-tight">Signed in successfully.</h1>
            <p className="mt-4 max-w-2xl text-base leading-7 text-muted-foreground">
              The API accepted your bearer token and returned your account details from the
              Identity-backed user store.
            </p>
          </div>

          <Button variant="outline" size="lg" onClick={logout}>
            Sign out
          </Button>
        </div>
      </section>

      <section className="grid gap-4 md:grid-cols-2">
        <article className="rounded-[1.5rem] border border-border/70 bg-card p-6 shadow-sm">
          <div className="inline-flex rounded-2xl bg-primary/12 p-3 text-primary">
            <UserRound className="size-5" />
          </div>
          <h2 className="mt-5 text-lg font-semibold">Current user</h2>
          <dl className="mt-4 space-y-3 text-sm">
            <div className="rounded-xl bg-muted/70 px-4 py-3">
              <dt className="text-muted-foreground">Username</dt>
              <dd className="mt-1 font-medium">{user.userName}</dd>
            </div>
            <div className="rounded-xl bg-muted/70 px-4 py-3">
              <dt className="text-muted-foreground">Email</dt>
              <dd className="mt-1 font-medium">{user.email}</dd>
            </div>
            <div className="rounded-xl bg-muted/70 px-4 py-3">
              <dt className="text-muted-foreground">User ID</dt>
              <dd className="mt-1 break-all font-medium">{user.id}</dd>
            </div>
          </dl>
        </article>

        <article className="rounded-[1.5rem] border border-border/70 bg-card p-6 shadow-sm">
          <div className="inline-flex rounded-2xl bg-primary/12 p-3 text-primary">
            <ShieldCheck className="size-5" />
          </div>
          <h2 className="mt-5 text-lg font-semibold">JWT session</h2>
          <dl className="mt-4 space-y-3 text-sm">
            <div className="rounded-xl bg-muted/70 px-4 py-3">
              <dt className="text-muted-foreground">Expires</dt>
              <dd className="mt-1 font-medium">
                {new Date(session.expiresAtUtc).toLocaleString()}
              </dd>
            </div>
            <div className="rounded-xl bg-muted/70 px-4 py-3">
              <dt className="text-muted-foreground">API base URL</dt>
              <dd className="mt-1 font-medium">{process.env.NEXT_PUBLIC_API_BASE_URL ?? "http://localhost:5108"}</dd>
            </div>
          </dl>
        </article>
      </section>
    </div>
  );
}
