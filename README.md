# CO3808-Thesis
Games Development Final year project.

## Title
Snap out Farmer

## Project Context
For my Final year project, I have decided to make a game that uses unique ways to teach the player that things you love aren’t always what you hope for. In this project I will find a way to implement the game using all the things I have learned from years of experience in the Unity3D engine and C# language. I have landed on the idea of using a manager-based game that can be split into modules making it easy to remove or add features without changing much or any of the code, rather than just disabling a specific manager. This approach does have a lot of benefits as it becomes harder and more complex the more managers and controllers you add on to it, the reason is because one of the managers may interfere with the actions that another one also controls or modifies. This game will be set in a medieval time period with a low polygon themed inspired by the well-known game World of Warcraft, having quest and interactions with the environment in order to proceed the story of the game.

## Versions
### 20w16a:
Created player, Movement manager, Camera manager, Intro to raycast manager (Interaction system)
#### bugs:
B1) Fixed - viewer Vector3 is zero      <br/>
B2) Fixed - rotation reseting unwanted  <br/>
B3) Fixed - zooming inverted            <br/>

### 20w23a:
Added on focus controller, Intro to interaction point
#### bugs:
B4) Fixed - Focus items do not defocus, they override <br/>

### 20w23b:
Worked on fixing bug in defocusing an interact point <br/>

### 20w23c:
Finished focus controller, Finished interaction system
#### bugs:
B5) Pending - When interacting with an object and leaving the interaction area the interaction doesn't "pause"  <br/>
B6) Fixed - Small Interaction items don't work                                                                  <br/>

### 20w23d:
Created model for coin and textures/materials for it, re-orginized files in unity <br/>

### 20w23e:
Started model for loot chest <br/>

### 20w24a:
Re-Modeled coins <br/>
Created a script for Custom Scriptable Objects  <br/>
Implemented a player stats system               <br/>
Implemented a Currency Loot script              <br/>
Player has a working Balance                    <br/>
#### bugs:
B7) Fixed - After picking up items the interaction in PlayerFocus is pointing to missing <br/>
