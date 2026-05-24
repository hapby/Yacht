# Yacht
CS20200 Term Project

## Build?

You can compile the project by typing `dotnet build`.

## Run?

You can run the project by typing `dotnet run`. 

## Game Title

You can start number of players 2P-4P to play with at the title.
LEFT Click "-" button to decrease the number of players.
LEFT Click "+" button to increase the number of players.
LEFT Click "Start" button to start the game.

## Game Play

On your turn, you can roll the dice at most three times by pressing and releasing the SPACE_BAR.
LEFT click dices to fix its value. Its value will not change until you unfix it by LEFT clicking it again.
After rolling dice, pick one pattern and left click the corresponding entry of the table in your glowing column to fill the score.
Once all tables are filled, the game is done.

## Who is Winner?

The player having highest score wins the game.

## LLM Usuage

I used LLM to get hints for using Raylib in F#. 
I asked LLM to generate code accepting any keyboard typing from user when user choosed to change its name. 
I asked LLM the basic logic of recognition of SPACEBAR holding and releasing using Raylib.
I asked LLM to generate code for making an animation of suffling dice.
I asked LLM the basic logic of guage bar. 
I didn't change anything from my plan. 

## What's new?

I added shaking animation on dices to make users actually feel rolling of dices.
I added new scene after the game is over announcing who is the winner and introducing a button to the title.
This game has a winner and without announcing a winner is quite unnatural since most of game announces the winner.
I added a explanation text "Press Enter to See the Result" to add new scene I just said. 
