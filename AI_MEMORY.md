# AI Developer Guidelines

This file defines the rules and boundaries for any AI working on this project.
These are non-negotiable and apply to every task, change, and decision made.

---

## 1. Local-Only Changes

All work is strictly confined to the local environment.

- You may only create, edit, or delete files within the local project directory.
- You may not execute any command that writes to, modifies, or communicates with a remote environment, server, or repository.
- This includes - but is not limited to - pushing code, deploying builds, triggering CI/CD pipelines, or writing to external databases.

**Why:** The AI operates as a local development assistant. Remote actions carry risk that cannot be undone and fall outside the scope of what an AI should action autonomously.

---

## 2. Git Is Read-Only

Git commands are permitted only for reading state. Writing to history or remotes is forbidden.

**Allowed:**
```
git status
git log
git diff
git branch
git show
git fetch --dry-run
```

**Never allowed:**
```
git commit
git push
git merge
git rebase
git reset
git checkout -b
git branch -d / -D
git tag
git stash (push)
```

**Why:** Version control history is the authoritative record of intent. The AI should never author commits, alter history, or influence what gets pushed. Those decisions belong to the developer.

---

## 3. No Over-Engineering

Only build what is explicitly needed right now.

- If a feature, abstraction, or utility is not required by the current task, do not add it.
- Do not add interfaces, base classes, extension points, or configuration options on the basis that they "might be useful later."
- Do not introduce design patterns unless the problem at hand genuinely requires them.

**Why:** Unused code is a liability. It increases maintenance burden, adds cognitive load, and introduces surface area for bugs - with zero benefit until the hypothetical future need arrives, if it ever does.

---

## 4. Always Explain Your Work

For every change made, provide a clear explanation covering:

1. **What** was changed.
2. **Why** it was necessary.
3. **Why** this approach is the most appropriate for the situation.

This applies to code changes, file edits, terminal commands, and architectural decisions.

**Why:** Developers need to understand and own the changes in their codebase. Unexplained changes erode trust and make review and debugging harder.

---

## 5. Simplicity Is a Requirement

The simplest working solution is always preferred.

- If two approaches solve the same problem, choose the one that is easier to read and reason about.
- Complexity must be justified by a concrete, present need - not anticipated future requirements.
- Clever code is not better code.

**Why:** Simple code is easier to test, debug, review, and maintain. Reliability follows directly from simplicity. Complexity is where bugs live.

---

## 6. No Quick Fixes or Workarounds

If something is broken, fix it properly.

- Do not patch around a problem to get things working temporarily.
- Do not suppress errors, add null checks to hide missing data, or comment out failing code.
- Identify the root cause and resolve it correctly before moving on.
- If the proper fix requires information or access you do not have, stop and ask - do not improvise.

**Why:** Workarounds mask real problems and create technical debt. They make future debugging significantly harder because the codebase no longer reflects reality.

---

## 7. Follow Best Practices at All Times

Write code as if a senior engineer will review it immediately.

- Follow the established conventions of the language, framework, and project.
- Apply SOLID principles where relevant and natural - not as a checklist.
- Structure code for readability and maintainability.
- Write only what is needed, and write it well.

**Why:** Consistency and quality in a codebase reduce errors and make collaboration easier. Best practices exist because they have been proven to reduce failure modes over time.

---

## 8. Security Is Always in Scope

Security is not a separate concern - it is part of every task.

- Never log, print, or expose sensitive values such as credentials, tokens, or personal data.
- Never hardcode secrets, API keys, or environment-specific values in code.
- Validate and sanitise all input at the boundary where it enters the system.
- Apply the principle of least privilege: request only the access or permissions a component genuinely needs.
- When in doubt about the security implications of an approach, raise it explicitly before proceeding.

**Why:** Security issues are significantly cheaper to prevent than to fix after the fact. They are also a professional and ethical responsibility.

---

## 9. Environment Files Are Confidential

`.env` files must never be read, printed, or accessed in any way.

- If you need to understand what environment variables the project uses, read the `.env.template` file instead.
- If a specific value from a `.env` file is needed to complete a task, stop and ask the developer to provide it explicitly.
- Never infer, guess, or reconstruct the contents of a `.env` file.

**Why:** `.env` files contain secrets. Reading them - even without logging - is a security risk. The template file exists precisely to communicate structure without exposing values.

---

## 10. Naming Over Comments

Code must be self-explanatory through naming. Comments must not be used to compensate for poor naming.

**Naming rules:**
- Names must describe what a thing *is* or *does*, not how it works internally.
- Avoid generic names: `data`, `info`, `value`, `temp`, `obj`, `time`, `result`.
- Use names that make the purpose unambiguous in context.

**Comment rules:**
- Do not add comments that restate what the code already says.
- The only acceptable comments are those explaining *why* a non-obvious decision was made - never *what* the code does.

**Examples:**

| Unacceptable | Acceptable |
|---|---|
| `time` | `currentTime`, `sessionExpiryTime` |
| `data` | `rawApiResponse`, `parsedUserProfile` |
| `flag` | `isEmailVerified`, `hasPendingChanges` |
| `DoStuff()` | `RecalculateInvoiceTotals()` |
| `// create employee` above `CreateEmployee()` | *(no comment needed)* |

**Why:** Comments that explain *what* code does go stale and are often wrong. A well-named codebase is always accurate because the names *are* the code. Descriptive naming is also the single highest-leverage readability improvement available.

---

## 11. Agent Operating Mode and Enforcement

These instructions apply to every AI session in this repository and override default assistant behavior when in conflict.

- The AI must respect the active mode constraints (e.g., Ask mode is strictly read-only).
- In Ask mode, the AI must never edit files, apply patches, run write operations, or execute state-changing commands.
- The AI may use read/search tools to inspect the codebase and answer questions.
- If a request requires code changes while in Ask mode, the AI must explain what should be changed but must not perform the change.
- The AI must follow repository-specific guidance before making architectural or implementation decisions.
- The AI must prioritize correctness, security, and simplicity over speed.
- If constraints conflict, the AI must choose the safer/non-destructive interpretation and ask for clarification.

**Enforcement priority (highest to lowest):**
1. System and environment safety constraints
2. Repository AI memory rules (`AI_MEMORY.md`)
3. Active mode constraints (e.g., Ask/Act)
4. Task-specific user instructions
5. Default assistant behavior

**Failure handling:**
- If the AI cannot comply safely, it must stop and explain why.
- The AI must propose a compliant alternative path.

---

## Summary

| Rule | One-line principle |
|---|---|
| Local only | Never touch anything outside the local project. |
| Git is read-only | Never commit, push, or alter git history. |
| No over-engineering | Build what is needed now, nothing more. |
| Explain everything | Every change needs a what, why, and why this way. |
| Simplicity first | The simplest correct solution is the right solution. |
| No workarounds | Fix root causes; never patch around problems. |
| Best practices | Write code a senior engineer would be proud to review. |
| Security always | Treat security as part of every task, not a separate step. |
| Env files are off-limits | Read `.env.template`; ask for values; never read `.env`. |
| Names over comments | Write names that make comments unnecessary. |
| Mode enforcement | Follow safe mode constraints and stop when non-compliant. |

