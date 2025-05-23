# Conway's Game of Life
---

## 1. **Summary**
Conway’s Game of Life is a type of computer simulation known as a cellular automaton (automaton is the singular form, automata the plural.) Cellular automata are essentially mathematical models of complex systems important to many fields of study including mathematics, biology, chemistry and physics.
The “game” was originally devised by British mathematician John Horton Conway in 1970. Conway was building upon work done in the 1940s by Hungarian-American mathematician John Von Neumann. Von Neumann was attempting to modal a type of machine that could make copies of its self (or self-replicate) and he eventually found such a machine could be represented by a 2 dimensional array of cells.

This was a project that was completed as a University assignment as part of my Project and Portfolio class.
Worked in the .NET framework using C# in order to reproduce a replica of Conway's Game of Life. I used version control using git, classes, and the windows form application.

## 2. **Tasks Completed**
	- Rendered a grid representing the individual cells.
	- Turn on/off cells with a click of the mouse
	- Implemented game rules
		- Living cells with less than 2 living neighbors die in the next generation
		- Living cells with more than 3 living neighbors die in the next generation
		- Living cells with 2 or 3 living neighbors live in the next generation
		- Dead cells with exactly 3 living neighbors live in the next generation
	- Added Start, Pause, and Next menu items and tool strip buttons
		- If the game is paused, the next button advances the game by 1 generation
	- Universe could be randomly populated from a time or from a seed variable
	- New or clear menu item emptied the universe of all living cells
	- The current state and size of the universe could be saved into a text file and later opened
	- The current generation is displayed in a status strip, with the current number of living cells
	- The universe attributes could be manipulated by
		- Controlling how many milliseconds there is between new generations
		- Adjusting the width and height of the universe
	- Menu items could be toggled on/off, such as the neighbor count, grid, and HUD
	- Gave two different methods to determine the bounds of the universe
		- Toroidal, wraps around the other side of the universe
		- Finite, once the cell goes out of bounds the cell is considered dead