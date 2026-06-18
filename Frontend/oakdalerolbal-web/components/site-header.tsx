"use client";

import Link from "next/link";

import { useAuth } from "@/components/auth/auth-provider";
import { Button, buttonVariants } from "@/components/ui/button";

export function SiteHeader() {
  const { logout, user } = useAuth();

  return (
    <header className="relative z-10 border-b border-border/60 bg-background/70 backdrop-blur">
      <div className="mx-auto flex w-full max-w-6xl flex-col gap-4 px-4 py-4 sm:px-6 lg:flex-row lg:items-center lg:justify-between lg:px-8">
        <Link href="/" className="text-lg font-semibold tracking-tight text-foreground">
          Oakdale Rolbal
        </Link>

        <nav className="flex flex-col gap-3 sm:flex-row sm:items-center">
          <Link href="/" className="text-sm text-muted-foreground hover:text-foreground">
            Home
          </Link>
          {user ? (
            <>
              <Link href="/dashboard" className="text-sm text-muted-foreground hover:text-foreground">
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
              <Link href="/register" className={buttonVariants()}>
                Create account
              </Link>
            </>
          )}
        </nav>
      </div>
    </header>
  );
}
