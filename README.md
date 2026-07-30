# echo-horror-game
A psychological-horror vertical slice built in Unity 6. ECHO focuses on tension through environmental storytelling, perception-driven enemy AI, exploration, and puzzle-solving.

## Core experience
Navigate a hostile environment, uncover story clues, solve an environmental puzzle, and avoid an enemy that can see and hear the player.

## Planned features
- First-person controller
- Interaction and inspection system
- Inventory and key-item flow
- Enemy AI: patrol, investigate, chase, and search
- Vision and hearing detection
- Environmental puzzle
- Checkpoint/save system
- Main menu, pause menu, and settings
- URP lighting, post-processing, and atmospheric audio

- ## Controls
-WASD — Move
-Mouse — Look
-E — Interact
-Esc — Pause

- ## Project structure

- `Scripts/` contains gameplay systems, split by responsibility: Player, Interaction, Inventory, Enemy, Puzzle, UI, and Core systems.
- `Scenes/` contains the main menu, playable prototype, and isolated test scenes.
- `Prefabs/` contains reusable player, enemy, environment, interactable, puzzle, and UI objects.
- `Data/ScriptableObjects/` stores configurable item, enemy, audio, and settings data.
- `Audio/` holds ambience, music, sound effects, and voice logs.
- `Art/` contains visual assets such as models, materials, textures, shaders, and VFX.

## Tech stack
- Unity 6
- C#
- Universal Render Pipeline (URP)
- Git

## Project status
In development.

## Running locally
1. Clone this repository.
2. Open the project through Unity Hub using Unity 6.
3. Open the main scene from `Assets/Scenes`.
4. Press Play.

## Non-goals

- Multiple levels or enemy types
- Crafting or complex inventory systems
- Achievements, localization, or full voice acting
- Cinematic cutscenes
- A full-length game

## Definition of done

- A polished 10–15 minute playable horror vertical slice
- One enemy with patrol, investigate, chase, and search behaviours
- Vision and hearing detection
- One complete environmental puzzle and key-item flow
- Checkpoint save/load
- Functional menus and settings
- Windows build, screenshots, and a 60–90 second demo video

## Portfolio evidence
This project demonstrates gameplay programming, finite state machines, AI perception, UI systems, persistence, and Unity optimization awareness.
