// ─── Clubs ───────────────────────────────────────────────────────────────────

export type ClubSummary = {
  id: string;
  name: string;
  shortName: string;
  slug: string;
  isActive: boolean;
};

export type Club = {
  id: string;
  name: string;
  shortName: string;
  slug: string;
  email: string | null;
  phoneNumber: string | null;
  isActive: boolean;
  createdUtc: string;
};

export type CreateClubRequest = {
  name: string;
  shortName: string;
  slug: string;
  email?: string;
  phoneNumber?: string;
};

export type UpdateClubRequest = {
  name: string;
  shortName: string;
  email?: string;
  phoneNumber?: string;
  isActive: boolean;
};

// ─── Auth ─────────────────────────────────────────────────────────────────────

export type AuthUser = {
  id: string;
  email: string;
  userName: string;
  roles: string[];
};

export type AuthResponse = {
  accessToken: string;
  expiresAtUtc: string;
  user: AuthUser;
};

export type LoginRequest = {
  email: string;
  password: string;
};

export type RegisterRequest = {
  email: string;
  userName: string;
  password: string;
  confirmPassword: string;
};

type ValidationErrors = Record<string, string[]>;

type RequestOptions = Omit<RequestInit, "headers"> & {
  headers?: HeadersInit;
  token?: string | null;
};

const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL ?? "http://localhost:5108";

export class ApiError extends Error {
  constructor(
    message: string,
    public readonly status: number,
    public readonly errors?: ValidationErrors,
  ) {
    super(message);
  }
}

export async function apiRequest<T>(path: string, options: RequestOptions = {}): Promise<T> {
  const { headers, token, ...rest } = options;

  const response = await fetch(`${API_BASE_URL}${path}`, {
    cache: "no-store",
    ...rest,
    headers: {
      "Content-Type": "application/json",
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...headers,
    },
  });

  const contentType = response.headers.get("content-type") ?? "";
  const payload = contentType.includes("application/json")
    ? ((await response.json()) as Record<string, unknown>)
    : null;

  if (!response.ok) {
    const validationErrors = isValidationErrors(payload?.errors) ? payload.errors : undefined;
    const message = getErrorMessage(payload) ?? "The request could not be completed.";
    throw new ApiError(message, response.status, validationErrors);
  }

  return payload as T;
}

export async function getClubs(): Promise<ClubSummary[]> {
  return apiRequest<ClubSummary[]>("/api/clubs");
}

export async function getClub(slug: string): Promise<Club> {
  return apiRequest<Club>(`/api/clubs/${slug}`);
}

export async function createClub(request: CreateClubRequest, token: string): Promise<Club> {
  return apiRequest<Club>("/api/clubs", {
    method: "POST",
    body: JSON.stringify(request),
    token,
  });
}

export async function updateClub(id: string, request: UpdateClubRequest, token: string): Promise<Club> {
  return apiRequest<Club>(`/api/clubs/${id}`, {
    method: "PUT",
    body: JSON.stringify(request),
    token,
  });
}

export async function deleteClub(id: string, token: string): Promise<void> {
  await apiRequest<unknown>(`/api/clubs/${id}`, {
    method: "DELETE",
    token,
  });
}

export async function refreshAuthSession(token: string): Promise<AuthResponse> {
  return apiRequest<AuthResponse>("/api/auth/refresh", {
    method: "POST",
    token,
  });
}

function getErrorMessage(payload: Record<string, unknown> | null) {
  if (!payload) {
    return null;
  }

  if (typeof payload.title === "string" && payload.title) {
    return payload.title;
  }

  if (payload && typeof payload.detail === "string" && payload.detail) {
    return payload.detail;
  }

  if (typeof payload.message === "string" && payload.message) {
    return payload.message;
  }

  if (isValidationErrors(payload.errors)) {
    return Object.values(payload.errors).flat().join(" ");
  }

  return null;
}

function isValidationErrors(value: unknown): value is ValidationErrors {
  if (!value || typeof value !== "object") {
    return false;
  }

  return Object.values(value).every(
    (entry) => Array.isArray(entry) && entry.every((item) => typeof item === "string"),
  );
}
