"use client";

import {
  createContext,
  startTransition,
  useContext,
  useEffect,
  useEffectEvent,
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

const STORAGE_KEY = "oakdalerolbal.auth";

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

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

  const applySession = useEffectEvent((nextSession: AuthResponse | null) => {
    persistSession(nextSession);
    setSession(nextSession);
    setStatus(nextSession ? "authenticated" : "anonymous");
  });

  const refreshWithToken = useEffectEvent(async (accessToken: string) => {
    try {
      const user = await apiRequest<AuthUser>("/api/auth/me", { token: accessToken });

      setSession((currentSession) => {
        if (!currentSession || currentSession.accessToken !== accessToken) {
          const refreshedSession = {
            accessToken,
            expiresAtUtc: currentSession?.expiresAtUtc ?? new Date().toISOString(),
            user,
          };

          persistSession(refreshedSession);
          return refreshedSession;
        }

        const refreshedSession = { ...currentSession, user };
        persistSession(refreshedSession);
        return refreshedSession;
      });

      setStatus("authenticated");
    } catch {
      applySession(null);
    }
  });

  useEffect(() => {
    if (typeof window === "undefined") {
      return;
    }

    const serialized = window.localStorage.getItem(STORAGE_KEY);

    if (!serialized) {
      setStatus("anonymous");
      return;
    }

    try {
      const storedSession = JSON.parse(serialized) as AuthResponse;
      setSession(storedSession);
      void refreshWithToken(storedSession.accessToken);
    } catch {
      applySession(null);
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
