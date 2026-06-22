"use client";

import { useParams } from "next/navigation";
import { useEffect, useState } from "react";
import Link from "next/link";
import { ArrowLeft, Building2, Mail, Phone, ShieldCheck, Trash2 } from "lucide-react";

import { useAuth } from "@/components/auth/auth-provider";
import { deleteClub, getClub, type Club, ApiError } from "@/lib/api";
import { Button, buttonVariants } from "@/components/ui/button";
import { cn } from "@/lib/utils";

const INTERNAL_ADMIN_ROLE = "InternalAdmin";

export default function ClubPage() {
  const { slug } = useParams<{ slug: string }>();
  const { token, user } = useAuth();
  const [club, setClub] = useState<Club | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [isRemoving, setIsRemoving] = useState(false);

  const isInternalAdmin = user?.roles?.includes(INTERNAL_ADMIN_ROLE) ?? false;

  useEffect(() => {
    if (!slug) return;
    getClub(slug)
      .then(setClub)
      .catch((err) => setError(err instanceof ApiError ? err.message : "Failed to load club."))
      .finally(() => setIsLoading(false));
  }, [slug]);

  async function handleRemoveClub() {
    if (!club) {
      return;
    }

    if (!token) {
      setError("You need to sign in as an internal admin before removing clubs.");
      return;
    }

    if (!window.confirm(`Remove ${club.name}? This will hide it from the public clubs directory.`)) {
      return;
    }

    setError(null);
    setIsRemoving(true);

    try {
      await deleteClub(club.id, token);
      window.location.href = "/clubs";
    } catch (removeError) {
      setError(removeError instanceof ApiError ? removeError.message : "Unable to remove the club.");
      setIsRemoving(false);
    }
  }

  if (isLoading) {
    return (
      <div className="mx-auto flex w-full max-w-4xl px-4 py-8 sm:px-6 lg:px-8">
        <div className="h-64 w-full animate-pulse rounded-[2rem] border border-border/70 bg-card/50" />
      </div>
    );
  }

  if (error || !club) {
    return (
      <div className="mx-auto flex w-full max-w-4xl flex-col px-4 py-8 sm:px-6 lg:px-8">
        <Link href="/clubs" className={cn(buttonVariants({ variant: "outline", size: "sm" }), "self-start")}>
          <ArrowLeft className="mr-2 size-4" />
          Back to clubs
        </Link>
        <div className="mt-6 rounded-[1.5rem] border border-destructive/30 bg-destructive/10 p-6 text-sm text-destructive">
          {error ?? "Club not found."}
        </div>
      </div>
    );
  }

  return (
    <div className="mx-auto flex w-full max-w-4xl flex-col gap-6 px-4 py-8 sm:px-6 lg:px-8">
      <Link href="/clubs" className={cn(buttonVariants({ variant: "outline", size: "sm" }), "self-start")}>
        <ArrowLeft className="mr-2 size-4" />
        Back to clubs
      </Link>

      <section className="rounded-[2rem] border border-border/70 bg-card p-8 shadow-sm sm:p-10">
        <div className="flex flex-col gap-6 lg:flex-row lg:items-start lg:justify-between">
          <div className="flex items-start gap-6">
            <div className="inline-flex rounded-2xl bg-primary/12 p-4 text-primary">
              <Building2 className="size-6" />
            </div>
            <div>
              <p className="text-sm font-semibold uppercase tracking-[0.25em] text-primary">
                {club.shortName}
              </p>
              <h1 className="mt-1 text-4xl font-semibold tracking-tight">{club.name}</h1>

              {isInternalAdmin ? (
                <div className="mt-4 inline-flex items-center gap-2 rounded-full border border-primary/20 bg-primary/8 px-3 py-1 text-xs font-medium text-primary">
                  <ShieldCheck className="size-3.5" />
                  Internal admin tools enabled
                </div>
              ) : null}
            </div>
          </div>

          {isInternalAdmin ? (
            <Button variant="destructive" size="lg" disabled={isRemoving} onClick={() => void handleRemoveClub()}>
              <Trash2 className="size-4" />
              {isRemoving ? "Removing..." : "Remove club"}
            </Button>
          ) : null}
        </div>

        {error ? (
          <div className="mt-6 rounded-[1.5rem] border border-destructive/30 bg-destructive/10 p-4 text-sm text-destructive">
            {error}
          </div>
        ) : null}

        <dl className="mt-8 grid gap-4 sm:grid-cols-2">
          {club.email ? (
            <div className="flex items-center gap-3 rounded-xl bg-muted/70 px-4 py-3">
              <Mail className="size-4 shrink-0 text-muted-foreground" />
              <div className="text-sm">
                <dt className="text-muted-foreground">Email</dt>
                <dd className="mt-0.5 font-medium">
                  <a href={`mailto:${club.email}`} className="hover:text-primary">
                    {club.email}
                  </a>
                </dd>
              </div>
            </div>
          ) : null}

          {club.phoneNumber ? (
            <div className="flex items-center gap-3 rounded-xl bg-muted/70 px-4 py-3">
              <Phone className="size-4 shrink-0 text-muted-foreground" />
              <div className="text-sm">
                <dt className="text-muted-foreground">Phone</dt>
                <dd className="mt-0.5 font-medium">{club.phoneNumber}</dd>
              </div>
            </div>
          ) : null}

          <div className="rounded-xl bg-muted/70 px-4 py-3 text-sm">
            <dt className="text-muted-foreground">Slug</dt>
            <dd className="mt-0.5 font-medium font-mono">{club.slug}</dd>
          </div>

          <div className="rounded-xl bg-muted/70 px-4 py-3 text-sm">
            <dt className="text-muted-foreground">Registered</dt>
            <dd className="mt-0.5 font-medium">
              {new Date(club.createdUtc).toLocaleDateString(undefined, {
                year: "numeric",
                month: "long",
                day: "numeric",
              })}
            </dd>
          </div>
        </dl>
      </section>
    </div>
  );
}

