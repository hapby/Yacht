# Yacht
CS20200 Programming Principles - Term Project (Spring 2026)

## How to Build

You can compile the project by running the following command in your terminal:

    dotnet build

## How to Run

You can execute the game by running:

    dotnet run

## Start Menu

Upon execution, a start menu will be displayed where you can configure the number of players (2P to 4P) using the UI buttons.
- Left-click the **"-"** button to decrease the number of players.
- Left-click the **"+"** button to increase the number of players.
- Left-click the **"Start"** button to transition to the main game screen.

## Gameplay Instructions

- **Rolling Dice:** On your turn, you can roll the five dice up to three times. Press and hold the **SPACEBAR** to charge the power gauge, and release it to roll.
- **Fixing Dice:** Left-click a die to fix its value. Fixed dice will be highlighted with a red outline and remain unchanged during subsequent rolls in that turn. Left-click a fixed die again to unfix it.
- **Scoring:** After rolling, choose an available scoring category by left-clicking the corresponding cell in your highlighted (glowing) column to record your score.
- **Turn Management:** Once a score is recorded, the turn automatically passes to the next player. After all players complete their turns, the round counter increases. The game lasts for 12 rounds.

## Winning Conditions

After 12 rounds, the final scores are calculated. If the subtotal of the Aces through Sixes sections is 63 or higher, a **+35 bonus** is automatically awarded. The player with the highest total score wins the game.

## Added Features & Enhancements (What's New)

- **Immersive Shaking Animation:** Added a visual shaking effect to the dice that scales dynamically with the power gauge intensity, making the rolling experience feel tactile and realistic.
- **Game Over & Winner Announcement Screen:** Introduced a dedicated game over screen that dynamically determines and announces the winner along with their final score, complete with a button to easily return to the start menu. Announcing a winner provides a much more natural and rewarding game conclusion.
- **Smooth Scene Transitions:** Added a transitional prompt ("Press Enter to See the Result") to cleanly guide players from the final gameplay round to the results screen.

## LLM Usage

I used an LLM to get hints for using Raylib in F#. Specifically, I asked the LLM for guidance on accepting keyboard text input for player name customization, recognizing SPACEBAR hold/release events, generating a dice shuffling animation, and understanding the basic rendering logic for a gauge bar. I also used an LLM to proofread and refine the English grammar of this README document.

- **What I had to manually change or reprompt:** The LLM provided general, standalone code snippets that were mostly imperative. For example, when creating the gauge bar logic, the LLM suggested code that directly mutated local variables to update the UI. I had to manually rewrite and adapt these snippets to fit my strict functional pipeline architecture using the pipe operator (|>). I extracted the pure mathematical logic (calculating the power ratio based on state.ShakePower and state.AnimElapsed) and manually integrated it into my immutable State record and custom UI modules.
- **The main point that the LLM was not able to do correctly:** The LLM lacked context about the overall functional structure of the game and my custom domain types (such as ScoreCategory or Player). Because it could not comprehend the broader architecture, it was entirely incapable of writing the complex overarching game logic, such as the Yacht scoring rules, turn management, or full-house validation. It was only useful for isolated Raylib syntax and basic math; the actual game design, state management, and scoring engine had to be entirely designed and implemented by myself.