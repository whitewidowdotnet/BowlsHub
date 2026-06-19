import type { Metadata, Viewport } from "next";
import { Geist, Geist_Mono } from "next/font/google";

import { AuthProvider } from "@/components/auth/auth-provider";
import { SiteHeader } from "@/components/site-header";

import "./globals.css";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "BowlsLive",
  description: "Manage tournaments, track scores, and view live leaderboards.",
  applicationName: "BowlsLive",
  manifest: "/manifest.webmanifest",
};

export const viewport: Viewport = {
  themeColor: "#1f7a3f",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" className={`${geistSans.variable} ${geistMono.variable} h-full antialiased`}>
      <body className="min-h-full">
        <AuthProvider>
          <div className="relative flex min-h-screen flex-col overflow-hidden bg-background text-foreground">
            <div className="pointer-events-none absolute inset-x-0 top-0 h-80 bg-[radial-gradient(circle_at_top,_color-mix(in_oklch,var(--primary),white_65%)_0%,transparent_70%)] opacity-70" />
            <SiteHeader />
            <main className="relative flex-1">{children}</main>
          </div>
        </AuthProvider>
      </body>
    </html>
  );
}
