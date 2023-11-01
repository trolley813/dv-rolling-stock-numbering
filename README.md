## DV Rolling Stock Numbering mod

A mod which allows to customize the numbering of locomotives on nameplates (like L-001 etc.). These numbers will be seen in the Career Manager etc.

### Settings

#### Prefixing scheme:
- Classic (always L-xxx, regardless of the model)
- Model-based (e.g. DE2-001, DE6-002 etc.)

#### Numbering scheme:
- Classic (000 to 099 for all engine models, like in the base game)
- Classic 3-digit (same but 000 to 999)
- Unified range (customized (e. g. 100 to 299), the same range for all locos)
- Model-based range (different range per each model, best to use in conjunction with classic prefixes)

All range ends (min and max) can be configured between 0 and 9999 (but be careful to use 4-digit numbers on steamers with model-based prefixes, since IDs like S282-9999 are a bit (actually, a *byte*) too long).

#### Default number ranges
- 1 to 99: S060 (Note the 0 is excluded, unlike in the base game)
- 100 to 299: DE2
- 300 to 399: DM3
- 400 to 599: DH4
- 600 to 799: DE6 (*try to drive a 777-767 pair of DE6s*)
- 800 to 999: S282

### Using with other mods
This mod is compatible with Number Manager, but you probably would like to disable random numbering and disallow offsets for better experience (otherwise you probably won't need this mod at all).