# Program 1
For Program 1, I followed Unity's [Roll a Ball](https://learn.unity.com/course/roll-a-ball) tutorial course. In this game
the player rolls a ball to collect floating cube tokens while evading a chasing red enemy. The player wins the game
by collecting all the tokens and loses if they fall are hit by the enemy or fall off of the game board.

## Change made:
I added the ability to jump to the game using the spacebar.\
To do this I added additional collision checks to see
when the player is touching the ground, along with a jump input listener to increase the player's vertical velocity
when the spacebar is pressed. While the tutorial does not specify any placements of obstacles, I added a bar and floating
platform with tokens on them that require jumping to reach. Finally, since the player may now use the jump in order to 
clear the walls surrounding the game board, I added a field below the board that gives players a game over if they fall
off.

## Notable Files:
`/GameDemo.mp4` - Short Game Demo video.\
`/Builds/Program1.exe` - Executable for running the game. Note: You need to download all files for the executable to run.\
`/Assets/Scripts/` - Scripts I wrote for the game.


## Tutorial followed:
https://learn.unity.com/course/roll-a-ball

## Controls: 
Movement - Arrow keys\
Jump - Space bar\
Pause - P
