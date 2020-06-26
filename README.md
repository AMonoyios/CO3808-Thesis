# CO3808-Thesis
Games Development Final year project.

## Title
Snap out Farmer

## Project Context
For my Final year project, I have decided to make a game that uses unique ways to teach the player that things you love aren’t always what you hope for. In this project I will find a way to implement the game using all the things I have learned from years of experience in the Unity3D engine and C# language. I have landed on the idea of using a manager-based game that can be split into modules making it easy to remove or add features without changing much or any of the code, rather than just disabling a specific manager. This approach does have a lot of benefits as it becomes harder and more complex the more managers and controllers you add on to it, the reason is because one of the managers may interfere with the actions that another one also controls or modifies. This game will be set in a medieval time period with a low polygon themed inspired by the well-known game World of Warcraft, having quest and interactions with the environment in order to proceed the story of the game.

## Versions
### 20w16a:
Created player                                <br/> 
Movement manager                              <br/>
Camera manager                                <br/>
Intro to raycast manager (Interaction system) <br/>
#### bugs:
B1) Fixed - viewer Vector3 is zero      <br/>
B2) Fixed - rotation reseting unwanted  <br/>
B3) Fixed - zooming inverted            <br/>

### 20w23a:
Added on focus controller   <br/>
Intro to interaction point  <br/>
#### bugs:
B4) Fixed - Focus items do not defocus, they override <br/>

### 20w23b:
Worked on fixing bug in defocusing an interact point <br/>

### 20w23c:
Finished focus controller   <br/>
Finished interaction system <br/>
#### bugs:
B5) Fixed - When interacting with an object and leaving the interaction area the interaction doesn't "pause"  <br/>
B6) Fixed - Small Interaction items don't work                                                                  <br/>

### 20w23d:
Created model for coin and textures/materials for it  <br/>
re-orginized files in unity                           <br/>

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

### 20w24b:
Implemented the basics for an inventory manager                      <br/>
Added singletons and delegates to update inventory manager and items <br/>
Player has a semi working LootItem->Inventory connection             <br/>
#### bugs:
B8) Fixed - When inventory slots are all full the invoke points to a null item reference <br/>

### 20w24c:
Started convertion from LWRP to URP rendering pipeline                            <br/>
Created a shader for the hidden interactables                                     <br/>
Started creating a glow effect when the player hovers mouse over an interactable  <br/>
#### bugs:
B9) Fixed - some interactables do not glow when in a specific area/angle  <br/>

### 20w24d:
Changed method of indicating interactable material using PBR shaders  <br/>
Added PostProcessing volume and layer                                 <br/>
Fixed bugs and optimised code layout                                  <br/>
#### bugs:
B10) Fixed - Coin piles do not glow correctly, needs re-modeling    <br/>

### 20w25a:
Changed indicator with glowing cirle PBR shader <br/>
Implemented a flexible loot chest system        <br/>
Created animations for the chest interactions   <br/>
Fixed B5 and B10                                <br/>
Scripts need cleaning up                        <br/>

### 20w26a:
Implemented an inventory system with interaction support                      <br/>
Added temp items and combined interactable items with scriptable object items <br/>
Created grass shader graph behavior                                           <br/>
Re-arranged folders and files                                                 <br/>
#### bugs:
B11) Fixed - Grass collision does not react to the right axis <br/>

### 20w26b:
Added ability to stack specific items together in the inventory                                     <br/>
Edited currency models to fine tune pivot point rotation                                            <br/>
Worked on chest loot system, loot now spawns relative to the rotation of its parent chest rotation  <br/>
#### bugs:
B12) Fixed - fixed issue with relative rotation for chest         <br/>
B13) Pending - Small interactable items clip out of playable area <br/>

### 20w26c:
Worked on inventory system, started implementation of drag and drop fuctionality  <br/>
#### bugs:
B14) Working - UI item holder does not hold items <br/>
