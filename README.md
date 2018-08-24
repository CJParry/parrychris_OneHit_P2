# One Hit Prototype    
"One Hit" is a fast paced 2D fighting game, with a 3D background. The twist is that it only requires one successful attack to defeat the other player, winning the round. 

## Architecture  
This game, developed in Unity, is played by two people sharing one keyboard. The blue player is controlled with the arrow keys and the green player is controlled with WASD. Each players moveset consists of a short attack, dash-attack, jump, move and block.  The dash attack is on a 3 second cooldown, and is seen as the 'Over Powered' move.  

#### Level structure  
The players have rigidbodys, box colliders and trigger box colliders. There are head and body trigger colliders. The head colliders are so player can jump off other players head. This works by the trigger setting the top players 'grounded' boolean to true, allowing them to use jump.  

The game has one camera which updates and adjusts its position in response to both players positions. From a viewers perspective, the camera follows the players horizontally. It also zooms in and out as the players get closer or further awayfrom each other. This camera movement allows the surrounding enviroment to appear 3D.   

The sky is displayed with clouds passing though. This was done by writing a dedicated 'CloudScroll' script. It simply incremements the clouds position every frame, resetting it to the begining position once a pre-set value is reached.  

There is a separate script for each player, which are very similar. Although there is a lot of duplicated code, I kept it like this as there are plans to add a range of characters, with different stats and moves.   

Most variables which may need tuning in the future are public. This is so the designers can help fine tune the game without having to change code.

I had a lot of trouble implementing a canvas with UI components, such as text. In the end I disabled the canvas and used a sprite on top of each player as an indicator. The players indicator dissapears when they lose. This should be improved in the next iteration.   

#### Game Loop
The game begins with players on opposite sides. The round is over when one player scores successful damage on the other player. This makes the scene restart, and essentially begins the next round.   

Each round take from less than 1 second, to several seconds long. It is a faced paced game, hence the short round times. The game is in the same state at the beginning of every round.   

Although there is no total wins so far count, this is to be implemented at a later date.  

### Most Technically Interesting/Challenging Parts of Prototype  
- Merging designers 3D background and other designers sprites/animations into project.
- Getting UI to work - only works in Unity. Settled for a coloured sprite indicator to show game state.
- Getting the camera working with 3D scene.
- Implementing the first attack move (close-range melee) was difficult, and the learning curve was steep. Once the first was done, however, the rest of the moves were done much faster.
- Team-members using different versions of Unity caused a range of issues, such as missing assets. Most of the issues were never solved.

### Controls && MoveSet:  
#### Blue Player:  
Movement: Left/Right Arrow Keys  
Jump: Up Arrow Key  
Attack: Right Shift  
Block: Right Alt  
Dash: Down Arrow  
Delete: Restart

#### Green Player:  
Movement: A / D  
Jump: W  
Attack: Left Shift  
Block: E  
Dash: S  
Delete: Restart

### Instructions to copy repository locally:  
* Download Git  
* Fork main repository into your own GitHub Account  
* Copy clone link from your Forked repository  

* In command line / terminal

```bash
$ git clone [clone link from your repository]
$ cd COMP-MDDNGame
```
