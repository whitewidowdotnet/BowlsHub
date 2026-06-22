"use client";

import Link from "next/link";
import { ArrowRight } from "lucide-react";

import { useAuth } from "@/components/auth/auth-provider";

const primaryCta =
  "inline-flex h-12 items-center justify-center gap-2 rounded-xl bg-primary px-6 text-sm font-semibold text-primary-foreground shadow-sm transition hover:bg-primary/90 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary/40";
const secondaryCta =
  "inline-flex h-12 items-center justify-center rounded-xl border border-border bg-background/70 px-6 text-sm font-semibold text-foreground transition hover:bg-muted focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary/30";

export function HomeAuthActions() {
  const { session, status, user } = useAuth();

  if (status === "loading") {
    return (
      <div className="flex flex-col gap-3 sm:flex-row">
        <div className="h-12 w-44 animate-pulse rounded-xl bg-muted" />
        <div className="h-12 w-32 animate-pulse rounded-xl bg-muted" />
      </div>
    );
  }

  if (user && session) {
    return (
      <div className="flex flex-col gap-4">
        <p className="text-sm text-muted-foreground">
          Signed in as <span className="font-medium text-foreground">{user.userName}</span>.
        </p>
        <div className="flex flex-col gap-3 sm:flex-row">
          <Link href="/dashboard" className={primaryCta}>
            Go to my dashboard
            <ArrowRight className="size-4" />
          </Link>
        </div>
      </div>
    );
  }

  return (
    <div className="flex flex-col gap-4">
      <div className="flex flex-col gap-3 sm:flex-row">
        <Link href="/register" className={primaryCta}>
          Register your club
          <ArrowRight className="size-4" />
        </Link>
        <Link href="/login" className={secondaryCta}>
          Member sign in
        </Link>
      </div>
      <p className="text-sm text-muted-foreground">
        Free to set up. No card needed to run your first tournament.
      </p>
    </div>
  );
}
