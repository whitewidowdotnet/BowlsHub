"use client";

import {
  createContext,
  startTransition,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";

import {
  apiRequest,
  ApiError,
  type AuthResponse,
  type AuthUser,
  type LoginRequest,
  type RegisterRequest,
} from "@/lib/api";

type AuthStatus = "loading" | "authenticated" | "anonymous";

type AuthContextValue = {
  login: (request: LoginRequest) => Promise<void>;
  logout: () => void;
  refresh: () => Promise<void>;
  register: (request: RegisterRequest) => Promise<void>;
  session: AuthResponse | null;
  status: AuthStatus;
  token: string | null;
  user: AuthUser | null;
};

const STORAGE_KEY = "bowlslive.auth";

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

function normalizeUser(user: AuthUser): AuthUser {
  return {
    ...user,
    roles: Array.isArray(user.roles) ? user.roles : [],
  };
}

function normalizeSession(session: AuthResponse): AuthResponse {
  return {
    ...session,
    user: normalizeUser(session.user),
  };
}

function persistSession(nextSession: AuthResponse | null) {
  if (typeof window === "undefined") {
    return;
  }

  if (nextSession) {
    window.localStorage.setItem(STORAGE_KEY, JSON.stringify(nextSession));
    return;
  }

  window.localStorage.removeItem(STORAGE_KEY);
}

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [session, setSession] = useState<AuthResponse | null>(null);
  const [status, setStatus] = useState<AuthStatus>("loading");

  const applySession = useCallback((nextSession: AuthResponse | null) => {
    const normalizedSession = nextSession ? normalizeSession(nextSession) : null;
    persistSession(normalizedSession);
    setSession(normalizedSession);
    setStatus(normalizedSession ? "authenticated" : "anonymous");
  }, []);

  const refreshWithToken = useCallback(
    async (accessToken: string) => {
      try {
        const user = normalizeUser(await apiRequest<AuthUser>("/api/auth/me", { token: accessToken }));

        setSession((currentSession) => {
          const refreshedSession = normalizeSession({
            accessToken,
            expiresAtUtc: currentSession?.expiresAtUtc ?? new Date().toISOString(),
            user,
          });

          persistSession(refreshedSession);
          return refreshedSession;
        });

        setStatus("authenticated");
      } catch {
        applySession(null);
      }
    },
    [applySession],
  );

  useEffect(() => {
    if (typeof window === "undefined") {
      return;
    }

    const serialized = window.localStorage.getItem(STORAGE_KEY);

    if (!serialized) {
      queueMicrotask(() => {
        applySession(null);
      });
      return;
    }

    try {
      const storedSession = normalizeSession(JSON.parse(serialized) as AuthResponse);
      queueMicrotask(() => {
        applySession(storedSession);
        void refreshWithToken(storedSession.accessToken);
      });
    } catch {
      queueMicrotask(() => {
        applySession(null);
      });
    }
  }, [applySession, refreshWithToken]);

  async function login(request: LoginRequest) {
    const nextSession = await apiRequest<AuthResponse>("/api/auth/login", {
      body: JSON.stringify(request),
      method: "POST",
    });

    startTransition(() => {
      applySession(nextSession);
    });
  }

  async function register(request: RegisterRequest) {
    const nextSession = await apiRequest<AuthResponse>("/api/auth/register", {
      body: JSON.stringify(request),
      method: "POST",
    });

    startTransition(() => {
      applySession(nextSession);
    });
  }

  function logout() {
    applySession(null);
  }

  async function refresh() {
    if (!session?.accessToken) {
      throw new ApiError("There is no active session to refresh.", 401);
    }

    await refreshWithToken(session.accessToken);
  }

  const value: AuthContextValue = {
    login,
    logout,
    refresh,
    register,
    session,
    status,
    token: session?.accessToken ?? null,
    user: session?.user ?? null,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider.");
  }

  return context;
}
