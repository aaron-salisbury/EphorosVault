# Ephoros Vault

Ephoros Vault is a small experimental password manager inspired by a simple question: what useful desktop software could reasonably have existed on a late-1990s PC, but generally didn't?

It is a Windows Forms side project built around that idea, with a deliberately straightforward desktop experience. The MVP supports local credential storage, folders, search, password generation, locking, recovery keys, and exports to Bitwarden JSON and KeePass 2 XML.

Vault credentials are encrypted at rest. A random vault key is protected locally with Windows DPAPI, with a separate recovery-key export for portability and recovery. Password-manager exports contain plaintext credentials and should be handled accordingly.

This is a fun experiment, not a security-audited password manager. I wouldn't recommend trusting it with important real-world credentials.
