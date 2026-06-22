"use client";

import Link from "next/link";

import { useAuth } from "@/components/auth/auth-provider";
import { Button } from "@/components/ui/button";

export function SiteHeader() {
  const { logout, user } = useAuth();

  return (
    <header className="sticky top-0 z-50 border-b border-border/50 bg-background/70 backdrop-blur-md">
      <div className="mx-auto flex w-full max-w-6xl items-center justify-between gap-4 px-4 py-3 sm:px-6 lg:px-8">
        <Link href="/" className="flex items-center gap-2.5 font-semibold tracking-tight text-foreground">
          {/* eslint-disable-next-line @next/next/no-img-element */}
          <img
            src="/icons/icon-192.svg"
            alt=""
            width={34}
            height={34}
            className="size-[34px] rounded-xl shadow-sm ring-1 ring-border/60"
          />
          <span className="text-lg">BowlsLive</span>
        </Link>

        <nav className="flex flex-col gap-3 sm:flex-row sm:items-center">
          <Link href="/" className="text-sm text-muted-foreground hover:text-foreground">
            Home
          </Link>
          <Link href="/clubs" className="text-sm text-muted-foreground hover:text-foreground">
            Clubs
          </Link>
          {user ? (
            <>
              <Link
                href="/dashboard"
                className="rounded-lg px-3 py-2 text-sm font-medium text-muted-foreground transition hover:bg-muted hover:text-foreground"
              >
                Dashboard
              </Link>
              <Button variant="outline" onClick={logout}>
                Sign out
              </Button>
            </>
          ) : (
            <>
              <Link href="/login" className="text-sm text-muted-foreground hover:text-foreground">
                Sign in
              </Link>
              <Link
                href="/register"
                className="inline-flex h-9 items-center justify-center rounded-lg bg-primary px-4 text-sm font-semibold text-primary-foreground shadow-sm transition hover:bg-primary/90"
              >
                Register club
              </Link>
            </>
          )}
        </nav>
      </div>
    </header>
  );
}
