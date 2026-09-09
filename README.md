# indtec-labz-processing-pipeline

A .NET lab exploring extensible processing workflows using commands, factories, builders, contextual validation rules, lifecycle transitions and persistence.

## Goal

Explore how a processing flow can stay explicit while still being reusable across different use cases and entity variants.

```text
Command
  ↓
Handler
  ↓
Factory
  ↓
Builder Pipeline
  ↓
Validation Pipeline
  ↓
Validation Policy
  ↓
Lifecycle / Persistence
  ↓
Processing Result
```

The sample domain uses `Order` only as a neutral example. The interesting part is the set of reusable contracts and how rules can be applied contextually without a large conditional tree.

## Processing intents

- `Save`: errors block persistence; warnings and hints are returned to the caller.
- `Release`: errors and warnings block release; hints are informational.
- `SaveAndRelease`: follows the same release policy because the operation requires release.

## Validation severities

- `Hint`: informational and never blocks processing.
- `Warning`: allows save but blocks operations that require release.
- `Error`: blocks every processing intent.

## Contextual rules

Rules decide whether they apply to the current entity and processing context through `IsApplicable`.

That allows independent dimensions such as type, side and intent to be composed without creating subclasses for every possible combination.

Examples included in the lab:

- common rule: customer is required;
- type rule: subscription minimum value;
- side rule: informational rule for sell orders;
- composed rule: pre-order + sell + release.

## Projects

- `src/Indtec.ProcessingPipeline`: building blocks and sample implementation.
- `tests/Indtec.ProcessingPipeline.Tests`: tests for severity policies and rule applicability.

## Notes

This repository is intentionally a learning lab rather than a framework. New abstractions should be introduced only when concrete examples justify them.
