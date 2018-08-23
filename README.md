# COMP-MDDNGame  
"One Hit" is a fast paced 2D fighting game, with a 3D background. The twist is that it only requires one attack to defeat the other player. 

# Architecture (Game loop, level structure)  
Players have rigidbody, box colliders, trigger box colliders. Special head trigger collider, so player can jump off other players head.
Most variables which need tuning/refining are public, so the designers can help fine tune the game without having to change code.
  
Both players play on the same keyboard. Player one is contolled with the arrow keys and Player two is controlled with WASD.  
Each players moveset consists of a short attack, dash-attack, jump, move and block.
Level begins with both players on opposite sides. Round is over when one player scores successful damage on the other player. This makes the sccene restart.
Players use 2 scripts as in future iterations we are likely to give different characters different stats/moves. Camera follows both players pan & zoom. Background clouds loops past  
had a lot of trouble implemented a canvas with UI components, such as text. In the end I used a sprtie on top of each player as an indicator. The players indicator dissapears when they lose.  

### Most technically interesting/challenging parts of prototype  
- Merging designers 3d background and other designers sprites/animations into project
- Getting UI to work - only works in Unity. Settled for a coloured sprite indicator to show game state.
- Getting the camera working with 3D scene
- Team-members using different versions caused a range of issues, such as missing assets



### Controls && MoveSet:  
#### Blue Player:  
Movement: Left/Right Arrow Keys  
Jump: Up Arrow Key  
Attack: Right Shift  
Block: Right Alt  
Dash: Down Arrow  

#### Green Player:  
Movement: A / D  
Jump: W  
Attack: Left Shift  
Block: E  
Dash: S  

### Instructions to copy repository locally:  
* Download Git  
* Fork main repository into your own GitHub Account  
* Copy clone link from your Forked repository  

* In command line / terminal

```bash
$ git clone [clone link from your repository]
$ cd COMP-MDDNGame
```
