# Roadmap

The MVP feature set is complete. The remaining work is a short release-readiness pass rather than additional feature development.

| Status | Item | Notes |
| --- | --- | --- |
| ☐ | Timed clipboard clearing | Clear copied passwords and other sensitive clipboard contents after a short delay rather than leaving them indefinitely. |
| ☐ | Application icon | Finalize the Ephoros Vault icon and make sure it is used consistently by the executable, window, and taskbar. |
| ☐ | Clean-state smoke test | Test the full flow from a clean first run: account creation, login, folders, credential CRUD, search/filter, generator/options, copy, lock/unlock, exports, recovery, restart, and login again. |
| ☐ | Recovery failure testing | Verify recovery with the correct key as well as wrong, corrupted, and unavailable-local-key scenarios. |
| ☐ | Build and repository cleanup | Run the full test suite and Release build, review warnings, and make sure generated databases, keys, exports, or debug artifacts are not tracked. |
| ☐ | Security-focused review | Review plaintext handling, key handling, clipboard behavior, exports, authentication paths, logging, and failure cases before considering the MVP finished. |
