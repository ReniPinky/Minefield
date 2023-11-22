This is the Minefield challenge game.

The aim is to get from the starting point, bottom left point of the map to the top row.
There are randomly assigned mines throughout the map.
This is completely random so the game might be impossible depending on the randomness.
You are allowed to hit 2 bombs, but the third bomb hit will kill you.

Each point on the map can be shown by a couple icons,
*P for player
*M for a found and blown up mine
*? for any space that is yet to be travelled to
*/ for any clear space that has been travelled to

At the end of the game, you will either have won, or died and the 
top 10 high scores will be saved locally in your temp folder(for easy cleanup)

The best fit path was attempted but doesn't currently work, i attempted to implement a breadth first search through the map to get the path,
but haven't gotten to the point of it showing just the best path.

This was designed in a way that the Minefield class is the gateway to the game 
and that owns the Grid and Player so that they do not need to know about each other, decoupling them.
This also means that Program where the game is ran, is a very simple implementation.