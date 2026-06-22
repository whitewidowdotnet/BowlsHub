"use client";

import Link from "next/link";
import { useEffect, useState } from "react";
import { Building2, Plus, ShieldCheck, Trash2 } from "lucide-react";

import { useAuth } from "@/components/auth/auth-provider";
import { Button } from "@/components/ui/button";
import { ApiError, createClub, deleteClub, getClubs, type Club, type ClubSummary } from "@/lib/api";

const INTERNAL_ADMIN_ROLE = "InternalAdmin";

const initialForm = {
  name: "",
  shortName: "",
  slug: "",
  email: "",
  phoneNumber: "",
};

function slugify(value: string) {
  return value
    .toLowerCase()
    .trim()
    .replace(/[^a-z0-9]+/g, "-")
    .replace(/^-+|-+$/g, "");
}

function toSummary(club: Club): ClubSummary {
  return {
    id: club.id,
    name: club.name,
    shortName: club.shortName,
    slug: club.slug,
    isActive: club.isActive,
  };
}

export default function ClubsPage() {
  const { token, user } = useAuth();
  const [clubs, setClubs] = useState<ClubSummary[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [form, setForm] = useState(initialForm);
  const [isSlugAuto, setIsSlugAuto] = useState(true);
  const [formError, setFormError] = useState<string | null>(null);
  const [formSuccess, setFormSuccess] = useState<string | null>(null);
  const [isCreating, setIsCreating] = useState(false);
  const [removingClubId, setRemovingClubId] = useState<string | null>(null);

  const isInternalAdmin = user?.roles?.includes(INTERNAL_ADMIN_ROLE) ?? false;

  useEffect(() => {
    let isMounted = true;

    void getClubs()
      .then((nextClubs) => {
        if (!isMounted) {
          return;
        }

        setClubs(nextClubs);
      })
      .catch((nextError) => {
        if (!isMounted) {
          return;
        }

        setError(nextError instanceof ApiError ? nextError.message : "Failed to load clubs.");
      })
      .finally(() => {
        if (isMounted) {
          setIsLoading(false);
        }
      });

    return () => {
      isMounted = false;
    };
  }, []);

  async function handleCreateClub(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setFormError(null);
    setFormSuccess(null);

    if (!token) {
      setFormError("You need to sign in as an internal admin before creating clubs.");
      return;
    }

    setIsCreating(true);

    try {
      const createdClub = await createClub(
        {
          name: form.name.trim(),
          shortName: form.shortName.trim(),
          slug: form.slug.trim(),
          email: form.email.trim() || undefined,
          phoneNumber: form.phoneNumber.trim() || undefined,
        },
        token,
      );

      setClubs((currentClubs) =>
        [toSummary(createdClub), ...currentClubs].sort((left, right) => left.name.localeCompare(right.name)),
      );
      setForm(initialForm);
      setIsSlugAuto(true);
      setFormSuccess(`Created ${createdClub.name}.`);
    } catch (createError) {
      setFormError(createError instanceof ApiError ? createError.message : "Unable to create the club.");
    } finally {
      setIsCreating(false);
    }
  }

  async function handleRemoveClub(club: ClubSummary) {
    if (!token) {
      setError("You need to sign in as an internal admin before removing clubs.");
      return;
    }

    if (!window.confirm(`Remove ${club.name}? This will hide it from the public clubs directory.`)) {
      return;
    }

    setError(null);
    setFormSuccess(null);
    setRemovingClubId(club.id);

    try {
      await deleteClub(club.id, token);
      setClubs((currentClubs) => currentClubs.filter((currentClub) => currentClub.id !== club.id));
      setFormSuccess(`Removed ${club.name}.`);
    } catch (removeError) {
      setError(removeError instanceof ApiError ? removeError.message : "Unable to remove the club.");
    } finally {
      setRemovingClubId(null);
    }
  }

  return (
    <div className="mx-auto flex w-full max-w-6xl flex-col px-4 py-8 sm:px-6 lg:px-8">
      <div className="mb-8 flex flex-col gap-4 lg:flex-row lg:items-end lg:justify-between">
        <div>
          <p className="text-sm font-semibold uppercase tracking-[0.25em] text-primary">Directory</p>
          <h1 className="mt-3 text-4xl font-semibold tracking-tight">Clubs</h1>
          <p className="mt-3 max-w-2xl text-base leading-7 text-muted-foreground">
            Browse all registered bowls clubs on the BowlsLive platform.
          </p>
        </div>

        {isInternalAdmin ? (
          <div className="rounded-2xl border border-primary/20 bg-primary/8 px-4 py-3 text-sm text-foreground">
            <div className="flex items-center gap-2 font-medium text-primary">
              <ShieldCheck className="size-4" />
              Internal admin tools enabled
            </div>
            <p className="mt-1 text-muted-foreground">You can create and remove clubs from this page.</p>
          </div>
        ) : null}
      </div>

      {isInternalAdmin ? (
        <section className="mb-8 rounded-[2rem] border border-border/70 bg-card p-6 shadow-sm sm:p-8">
          <div className="flex items-center gap-3">
            <div className="inline-flex rounded-2xl bg-primary/12 p-3 text-primary">
              <Plus className="size-5" />
            </div>
            <div>
              <h2 className="text-xl font-semibold">Add a club</h2>
              <p className="text-sm text-muted-foreground">
                Internal admins can register new clubs and immediately publish them to the directory.
              </p>
            </div>
          </div>

          <form className="mt-6 grid gap-4 md:grid-cols-2" onSubmit={handleCreateClub}>
            <div className="space-y-2">
              <label className="text-sm font-medium" htmlFor="club-name">
                Club name
              </label>
              <input
                id="club-name"
                value={form.name}
                onChange={(event) => {
                  const nextName = event.target.value;
                  setForm((currentForm) => ({
                    ...currentForm,
                    name: nextName,
                    slug: isSlugAuto ? slugify(nextName) : currentForm.slug,
                  }));
                }}
                className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
                placeholder="Cape Town Bowling Club"
                required
              />
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium" htmlFor="club-short-name">
                Short name
              </label>
              <input
                id="club-short-name"
                value={form.shortName}
                onChange={(event) => setForm((currentForm) => ({ ...currentForm, shortName: event.target.value }))}
                className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
                placeholder="CTBC"
                required
              />
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium" htmlFor="club-slug">
                Slug
              </label>
              <input
                id="club-slug"
                value={form.slug}
                onChange={(event) => {
                  setIsSlugAuto(false);
                  setForm((currentForm) => ({ ...currentForm, slug: slugify(event.target.value) }));
                }}
                className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
                placeholder="cape-town-bowling-club"
                required
              />
              <p className="text-xs text-muted-foreground">Lowercase letters, numbers, and hyphens only.</p>
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium" htmlFor="club-email">
                Contact email
              </label>
              <input
                id="club-email"
                type="email"
                value={form.email}
                onChange={(event) => setForm((currentForm) => ({ ...currentForm, email: event.target.value }))}
                className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
                placeholder="info@club.co.za"
              />
            </div>

            <div className="space-y-2 md:col-span-2">
              <label className="text-sm font-medium" htmlFor="club-phone-number">
                Phone number
              </label>
              <input
                id="club-phone-number"
                value={form.phoneNumber}
                onChange={(event) => setForm((currentForm) => ({ ...currentForm, phoneNumber: event.target.value }))}
                className="h-12 w-full rounded-xl border border-input bg-background px-4 text-sm shadow-sm transition focus:border-primary focus:outline-none focus:ring-2 focus:ring-primary/20"
                placeholder="021 555 1234"
              />
            </div>

            {formError ? (
              <p className="rounded-xl border border-destructive/30 bg-destructive/10 px-4 py-3 text-sm text-destructive md:col-span-2">
                {formError}
              </p>
            ) : null}

            {formSuccess ? (
              <p className="rounded-xl border border-primary/20 bg-primary/8 px-4 py-3 text-sm text-primary md:col-span-2">
                {formSuccess}
              </p>
            ) : null}

            <div className="md:col-span-2">
              <Button type="submit" size="lg" disabled={isCreating}>
                {isCreating ? "Creating club..." : "Create club"}
              </Button>
            </div>
          </form>
        </section>
      ) : null}

      {error ? (
        <div className="mb-6 rounded-[1.5rem] border border-destructive/30 bg-destructive/10 p-6 text-sm text-destructive">
          {error}
        </div>
      ) : null}

      {isLoading ? (
        <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
          {Array.from({ length: 6 }).map((_, i) => (
            <div key={i} className="h-40 animate-pulse rounded-[1.5rem] border border-border/70 bg-card/50" />
          ))}
        </div>
      ) : clubs.length === 0 ? (
        <div className="rounded-[2rem] border border-border/70 bg-card/70 p-10 text-center">
          <div className="inline-flex rounded-2xl bg-primary/12 p-4 text-primary">
            <Building2 className="size-6" />
          </div>
          <h2 className="mt-5 text-xl font-semibold">No clubs yet</h2>
          <p className="mt-3 text-sm text-muted-foreground">
            {isInternalAdmin
              ? "Use the form above to register the first club on the platform."
              : "Clubs will appear here once they have been registered on the platform."}
          </p>
        </div>
      ) : (
        <ul className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
          {clubs.map((club) => (
            <li key={club.id}>
              <div className="flex h-full flex-col rounded-[1.5rem] border border-border/70 bg-card/70 p-6 shadow-sm backdrop-blur transition hover:border-primary/40 hover:bg-card">
                <Link href={`/clubs/${club.slug}`} className="group flex flex-1 flex-col">
                  <div className="inline-flex rounded-2xl bg-primary/12 p-3 text-primary">
                    <Building2 className="size-5" />
                  </div>
                  <h2 className="mt-4 text-lg font-semibold group-hover:text-primary">{club.name}</h2>
                  <p className="mt-1 text-sm text-muted-foreground">{club.shortName}</p>
                  <p className="mt-auto pt-4 text-xs text-muted-foreground/70">{club.slug}</p>
                </Link>

                {isInternalAdmin ? (
                  <div className="mt-4 border-t border-border/70 pt-4">
                    <Button
                      variant="destructive"
                      size="sm"
                      disabled={removingClubId === club.id}
                      onClick={() => void handleRemoveClub(club)}
                    >
                      <Trash2 className="size-4" />
                      {removingClubId === club.id ? "Removing..." : "Remove club"}
                    </Button>
                  </div>
                ) : null}
              </div>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

