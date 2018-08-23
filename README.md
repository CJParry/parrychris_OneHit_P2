# One Hit Prototype    
"One Hit" is a fast paced 2D fighting game, with a 3D background. The twist is that it only requires one attack to defeat the other player. 

This game, developed in Unity, is played by two people sharing one keyboard. Player one is controlled with the arrow keys and Player two is controlled with WASD. Each players moveset consists of a short attack, dash-attack, jump, move and block.  

### Architecture (Game loop, level structure)  
The players have rigidbodys, box colliders and trigger box colliders. There are head and body trigger colliders. The head colliders are so player can jump off other players head. This works by the trigger setting the top players 'grounded' boolean to true, allowing them to jump.  
The game uses one camera which updates and adjusts its position in response to both players positions. From a viewers perspective, tyh camera follows the players horizontally, zooming in and out as the players get closer or further away. This helps give the 3D background effect.   
The sky is displayed with clouds passing though. This was done by writing a dedicated 'CloudScroll' script. It simply inceremements the clouds position every frame, resetting it to the begining position once a pre-set value is reached.  
There is a serarate script for each player, wich are very similar. Although there is a lot of duplicated code, I kept it like this as there are plans to add a range of characters, with different stats and moves.   
I had a lot of trouble implementing a canvas with UI components, such as text. In the end I used a sprite on top of each player as an indicator. The players indicator dissapears when they lose. This should be improved in the next iteration.   
Most variables which may need tuning are public, so the designers can help fine tune the game without having to change code.
  

Level begins with both players on opposite sides. Round is over when one player scores successful damage on the other player. This makes the sccene restart.
P

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
