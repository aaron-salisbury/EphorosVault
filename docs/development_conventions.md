# EphorosVault Development Conventions

## Purpose and scope

These conventions apply to human contributors and AI coding assistants. Prefer clear,
simple implementations and deliberate contracts over speculative abstractions.

EphorosVault serves as a straightforward password vault for Windows 98.

## Platform and dependencies

- The shipped application must remain on **.NET Framework 2.0 / CLR 2.0**. Do not
  retarget it to modern .NET or raise consumers' runtime requirements.
- Modern C# syntax is welcome when the configured modern compiler can compile it
  against the actual net20 reference assemblies without newer runtime dependencies.
  Language version and framework version are separate decisions.
- File-scoped namespaces, target-typed construction, and collection expressions
  may be used where their lowering works on net20. Do not introduce records,
  Task-based APIs, LINQ, AsyncLocal, ExceptionDispatchInfo, or other newer framework
  requirements without a deliberate, net20-compatible implementation decision.
- Packages and their transitive runtime dependencies must support net20. Keep
  Unity and Enterprise Library details inside their adapters where practical.

## Presentation pattern

The desktop UI follows a lightweight **Model-View-Presenter (MVP)** pattern. WinForms
forms and controls are views: they expose user actions and display state, but avoid
owning application behavior. Presenters respond to view events, coordinate business
services, and update the view through interfaces such as `IVaultView`. Keep domain
and persistence logic out of the presentation layer, and keep views free of direct
dependencies on concrete data or integration implementations.

## Results, exceptions, and validation

- Use `ProcessResult<T, TError>` for expected outcomes callers should branch on.
  Reserve the enum's zero/default value for success; use `Failure(error)` for failures.
- Use `ProcessResult<T>` for exceptions deliberately captured at a boundary.
  Unexpected exceptions should otherwise propagate. Never manufacture an exception
  merely to communicate an ordinary NotFound or validation outcome.
- Use `ValidationResult<T>` for validation feedback. Return snapshots rather than
  mutable backing collections; mutations must preserve validity and notifications.
- Prefer `Value` after checking success or `TryGet(out value)`. Boolean conversion
  describes operation success: a successful result containing `false` is still successful.
- Preserve the original exception and stack. Since net20 lacks ExceptionDispatchInfo,
  failed value access wraps the original exception as an inner exception.

## C# naming and formatting

- Use file-scoped namespaces and alphabetically ordered using directives without
  a separate System-first group. Place using directives outside the namespace.
- Prefer explicit local types and target-typed `new` where clear. Use built-in
  aliases such as `int`, `string`, and `bool`.
- Private fields use `_camelCase`; constants use `UPPER_SNAKE_CASE`; public and
  internal members use `PascalCase`; parameters and locals use `camelCase`.
- Use ordinary constructors for classes rather than primary constructors. Keep
  parameter lists on one line where readable.
- Use `string.Empty` for an empty string. There is no built-in EditorConfig rule
  that reliably enforces this preference; enforce it during review.
- **Always use braces for `if`, `else`, loops, `using` statements, and `lock`,
  even when the body contains only one statement.**
- Put braces on separate lines (Allman style). Expand method, constructor,
  operator, accessor, try/catch/finally, and lambda blocks. Put each executable
  statement on its own line; never compress several statements into one line.
- Methods, constructors, operators, and local functions use block bodies.
  Simple expression-bodied properties/indexers/accessors are acceptable. Auto-properties
  may remain on one line because they contain no executable statements.
- Use four spaces, LF line endings, UTF-8, a final newline, and no trailing whitespace.

```csharp
public bool TryGet(out T value)
{
    if (!IsSuccessful)
    {
        value = default;
        return false;
    }

    value = Value;
    return true;
}
```

Root `.editorconfig` defines editor preferences and enables IDE0011 (missing braces)
as a warning. An editor/analyzer must support these settings for diagnostics to appear;
the legacy project does not automatically make every style diagnostic a CI build check.
