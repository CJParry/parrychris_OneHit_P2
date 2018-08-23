# COMP-MDDNGame  
# Needed:

Playable exported Unity app (mac & windows)  
Git repository link  
Documented video demo - voice over showing moves, camera pan, gameplay  


Readme.md with:  
# Architecture (Game loop, level structure)  


"One Hit" is a fast paced 2D fighting game, with a 3D background. The twist is that it only requires one attack to defeat the other player.   
Both players play on the same keyboard. Player one is contolled with the arrow keys and Player two is controlled with WASD.  
Each players moveset consists of a short attack, dash-attack, jump, move and block.
Level begins with both players on opposite sides. Round is over when one player scores successful damage on the other player. This makes the sccene restart.
Players use 2 scripts as in future iterations we are likely to give different characters different stats/moves.

# Most technically interesting/challenging parts of prototype  
- Merging designers 3d background and other designers sprites/animatons into project

## Instructions to copy repository locally:  
* Download Git  
* Fork main repository into your own GitHub Account  
* Copy clone link from your Forked repository  

* In command line / terminal

```bash
$ git clone [clone link from your repository]
$ cd COMP-MDDNGame
```
### Dev Controls:  
#### Player One:  
Movement: Arrow Keys  
Attack: Right Shift  
Block: Left Alt  
Dash: Down Arrow  

#### Player Two:  
Movement: WASD  
Attack: Left Shift  
Block: E  
Dash: S  
