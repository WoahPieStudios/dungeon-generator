# Wave Function Collapse Implementation

## Overview

### General

[Martin Donald](https://www.youtube.com/channel/UC8bYucAICXmYet8pZ5Ja9Dw) made a really good general overview for the Wave Function Collapse algorithm [here](https://youtu.be/2SuvO4Gi7uY). While I don't think my algorithm for this dungeon generator will be a 1:1 implementation to how this algorithm is [initially made](https://github.com/mxgmn/WaveFunctionCollapse), it is what I used as a reference for the generator.

### Dungeon Generator

For this dungeon generator, it will use the Wave Function Collapse algorithm in order to determine which doors and paths can be used by the rooms relative to a point. Similar to [Martin Donald's sudoku comparison for the algorithm](https://youtu.be/2SuvO4Gi7uY), once a room with doors pointing to a direction has been chosen, the other rooms must have doors that connect from the previous room and lead to another available room.

## Visualization

### Legend
U - Up

D - Down

L - Left

R - Right

E - Empty

### Implementation 

> As the tables for these document are for visualization purposes only, the actual implementation of the algorithm might generate different results. However, these are the rules I wish for it to follow. I will also be using a 3x3 grid for convenience.

To start, all coordinates in the grid will contain all possible directions for the doors of that room.

|               |               |               |
| ------------- | ------------- | ------------- |
| U, D, L, R, E | U, D, L, R, E | U, D, L, R, E |
| U, D, L, R, E | U, D, L, R, E | U, D, L, R, E |
| U, D, L, R, E | U, D, L, R, E | U, D, L, R, E |

Then, the rooms by the edges of the grid would have their directions trimmed to their possible outcomes.

|              |               |              |
| ------------ | ------------- | ------------ |
| *D, R, E*    | *D, L, R, E*  | *D, L, E*    |
| *U, D, R, E* | U, D, L, R, E | *U, D, L, E* |
| *U, D, E*    | *U, L, R, E*  | *U, L, E*    |

Then a random room from the grid will be selected and have its doors selected.

|            |             |            |
| ---------- | ----------- | ---------- |
| D, R, E    | D, L, R, E  | D, L, E    |
| U, D, R, E | **U, L, R** | U, D, L, E |
| U, D, E    | U, L, R, E  | U, L, E    |

This would result for the rooms next to it have its possible doors affected.

|              |              |              |
| ------------ | ------------ | ------------ |
| D, R, E      | *D, L, R, E* | D, L, E      |
| *U, D, R, E* | **U, L, R**  | *U, D, L, E* |
| U, D, E      | U, L, R, E   | U, L, E      |

Reserve the connecting direction of the adjacent rooms,

|                 |                  |                 |
| --------------- | ---------------- | --------------- |
| D, R, E         | **D,** *L, R, E* | D, L, E         |
| *U, D, E* **R** | **U, L, R**      | *U, D, E* **L** |
| U, D, E         | U, L, R, E       | U, L, E         |

Then randomize the remaining directions.

|             |             |         |
| ----------- | ----------- | ------- |
| D, R, E     | **D, R**    | D, L, E |
| **U, D, R** | **U, L, R** | **L**   |
| U, D, E     | U, L, R, E  | U, L, E |

The adjacent rooms would then have their directions evaluated and trimmed.

|               |             |         |
| ------------- | ----------- | ------- |
| **D**         | **D, R**    | **E**   |
| **U, D, R**   | **U, L, R** | **L**   |
| **U,** *R, E* | U, L, R, E  | U, L, E |

This would repeat until all rooms in the grid have been evaluated.

|             |              |         |
| ----------- | ------------ | ------- |
| **D**       | **D, R**     | **E**   |
| **U, D, R** | **U, L, R**  | **L**   |
| **U, E**    | *U, L, R, E* | U, L, E |


|             |             |           |
| ----------- | ----------- | --------- |
| **D**       | **D, R**    | **E**     |
| **U, D, R** | **U, L, R** | **L**     |
| **U, E**    | **E**       | *U, L, E* |

|             |             |       |
| ----------- | ----------- | ----- |
| **D**       | **D, R**    | **E** |
| **U, D, R** | **U, L, R** | **L** |
| **U, E**    | **E**       | **E** |

