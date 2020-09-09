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
B14) Dropped - UI item holder does not hold items <br/>

### 20w27a:
Dropped idea for drag and drop (no point of creating one) <br/>
Implemented a right click to inventory to drop item       <br/>
#### bugs:
B15) Fixed - Counter does not display correct item count              <br/>
B16) Fixed - Chest can not apply spawn force to specific game objects <br/>

### 20w27b: - Supervisor meeting No1
Optimizing grass spawn script <br/>
orginized files               <br/>

### 20w28a:
Added support for area specific post processing             <br/>
Implemented a developer console in game view (Outputs only) <br/>
Optimizing group bush spawning                              <br/>
#### bugs:
B17) Fixed - grass spawns only with specific amount of pre generated prefabs  <br/>
B18) Fixed - Using items action wont show in developer console                <br/>

### 20w28b:
Fixed github project cloning micro issues     <br/>
Worked on optimizing the group grass spawner  <br/>
Bug fixing (B16,B17,B18)                      <br/>
#### bugs:
B19) Fixed - Custom console does not show interactable logs  <br/>

### 20w28c:
Worked on orginizing scripts                              <br/>
Started working on custom keybinding methods using script <br/>
Bug fixing (B19)                                          <br/>

### 20w28d:
Worked on UIControl optimization    <br/>
Null and double signed key detection  <br/>

### 20w29a:
Worked on Custom console UI <br/>
Bug fixing (B20)            <br/>
#### bugs:
B20) Fixed - When Custom console was deactivated messeges threw null reference errors <br/>

### 20w29b:
Started convertion of equipment items to custom scriptable objects  <br/>
Re-orginized files                                                  <br/>
Started work on implementing a simple equipment properties menu     <br/>

### 20w30a:
Worked on equipment system                            <br/>
Added the ability to drop and equip items             <br/>
Implemented equipment swap when slot already occupied <br/>
Worked on placeholder textures                        <br/>
Bug fixing (B21)                                      <br/>
#### bugs:
B21) Fixed - Items clipping out of the world when dropped from inventory  <br/>

### 20w30b:
Added equipment UI                              <br/>
UI can now display current equipped items       <br/>
Converted all UI updates to delegate singletons <br/>
#### bugs:
B22) Fixed - Player can interact behind the equipment ui  <br/>
B23) Working - Un-Equip button doesn't work                 <br/>

### 20w37a:
Player can interact with Inventories  <br/>
Items have unique tool tips           <br/>
Removed Custom Console Box (Beta)     <br/>
#### bugs:
B24) Working - Player pointing does not update correctly  <br/>
