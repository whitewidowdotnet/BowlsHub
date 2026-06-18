"use client";

import Link from "next/link";
import { ArrowRight, ShieldCheck } from "lucide-react";

import { useAuth } from "@/components/auth/auth-provider";
import { Button, buttonVariants } from "@/components/ui/button";
import { cn } from "@/lib/utils";

export function HomeAuthActions() {
  const { logout, session, status, user } = useAuth();

  if (status === "loading") {
    return (
      <section className="rounded-[2rem] border border-border/70 bg-card p-8 shadow-sm">
        <p className="text-sm font-medium text-muted-foreground">Checking your saved session...</p>
      </section>
    );
  }

  if (!user || !session) {
    return (
      <section className="rounded-[2rem] border border-border/70 bg-card p-8 shadow-sm">
        <div className="inline-flex rounded-2xl bg-primary/12 p-3 text-primary">
          <ShieldCheck className="size-5" />
        </div>
        <h2 className="mt-5 text-2xl font-semibold tracking-tight">Identity and JWT are ready.</h2>
        <p className="mt-4 text-sm leading-6 text-muted-foreground">
          Register a user from the web app, receive an access token, and immediately call protected
          API endpoints from the browser.
        </p>
        <div className="mt-6 flex flex-col gap-3">
          <Link
            href="/register"
            className={cn(buttonVariants({ size: "lg" }), "justify-center")}
          >
            <span className="inline-flex items-center gap-2">
              Start with registration
              <ArrowRight className="size-4" />
            </span>
          </Link>
          <Link
            href="/login"
            className={cn(buttonVariants({ size: "lg", variant: "outline" }), "justify-center")}
          >
            I already have an account
          </Link>
        </div>
      </section>
    );
  }

  return (
    <section className="rounded-[2rem] border border-border/70 bg-card p-8 shadow-sm">
      <p className="text-sm font-semibold uppercase tracking-[0.25em] text-primary">Signed in</p>
      <h2 className="mt-4 text-3xl font-semibold tracking-tight">{user.userName}</h2>
      <p className="mt-2 text-sm text-muted-foreground">{user.email}</p>
      <div className="mt-6 space-y-3 rounded-[1.5rem] bg-muted/70 p-4 text-sm">
        <p className="font-medium text-foreground">Active bearer session</p>
        <p className="text-muted-foreground">
          Token expires {new Date(session.expiresAtUtc).toLocaleString()}.
        </p>
      </div>
      <div className="mt-6 flex flex-col gap-3">
        <Link
          href="/dashboard"
          className={cn(buttonVariants({ size: "lg" }), "justify-center")}
        >
          Open dashboard
        </Link>
        <Button variant="outline" size="lg" className="justify-center" onClick={logout}>
          Sign out
        </Button>
      </div>
    </section>
  );
}
