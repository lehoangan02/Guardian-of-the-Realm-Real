# 0003 — Keep hand interaction separate from gameplay

- Status: accepted
- Date: 2026-10-10
- Decider: Tran Duc An

## Context

The hand interaction work must support a two-hand crossbow arrow, hammer-like thunder, open-hand meteor, hero bench-to-road placement, and hand-only building/barracks controls. It must remain useful before map, combat, economy, and unit logic exist. Meta XR SDK APIs and gameplay code will evolve independently.

## Decision

Place Meta SDK calls in a hand adapter. Put gesture and interaction state in action objects coordinated by one service. Expose semantic previews and typed, exactly-once action requests through interfaces. Keep coordinates in board-local space. Gameplay supplies validity, trajectory prediction, cooldown availability, and effect resolution through ports. Editor-only fake input and fake gameplay ports use the same contracts.

## Consequences

Hand gestures can be tested without a map. Gameplay can change damage, paths, or projectile rules without changing Meta hand code. Preview providers must eventually share gameplay's actual rules or the landing marker will be wrong. Local fakes enable standalone development; future gameplay systems implement or adapt the stable ports. See [../design/hand-interaction-plan.md](../design/hand-interaction-plan.md).
