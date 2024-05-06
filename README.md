<h1 align="center">
  :video_game: Oops! All Bards :video_game:
</h1>

## Contributors ##

### Project Leads ###
1. [Kyle Mitchell](https://github.com/kdavidmitchell)
2. [Joshua McCoy](https://github.com/dr-jam)

### Active Development Team ###
3. [Sarah Crowe]()
4. [Henry Ng]()
5. [Preston Chan]()
6. [Grant Gelardi]()
7. [Fuhsin Liao]()

### Project Alumni ###

8. [Camden Pettijohn](https://github.com/Camden-png)
9. [Brandon Hillig]()

## About ##

<p align="center">
    <img src="img/TitleCard.png" alt="Main Menu render"
    height="50%" width="50%">
</p>

Broadly, this repository is a code base for an ongoing research project concerned with game agent believability, artificial intelligence for game NPCs, and leveraging heterogeneous AI systems to open new game design spaces. Within the ```abl``` folder of this repository, you will find source code for a Java proxy server that runs a game agent using [ABL](http://www.cs.cmu.edu/~michaelm/publications/AI-IE2002.pdf). Within the ```oops-all-bards``` folder, you will find source code for a game demo built with Unity that is intended to be run alongside the Java proxy server to showcase the game agent.

## The _Oops! All Bards_ Game Demo ##
Central to our current work is a testbed virtual world to realize new ideas. In our case, this takes the form of _Oops! All Bards_. _Oops! All Bards_ is a 3D roleplaying game in which the player can inhabit the persona of a bard living in a medieval fantasy world currently under siege by a mysterious and maleficent entity. We drew much inspiration from classic tabletop RPGs, in which it is common that the player gathers a party of NPCs to join them as they progress through the game. 

### Project Goals ###

We intend our system in its current state to be used to improve the believability of moments between player-driven events and NPC reaction. Thus, it made sense to showcase how an NPC can react and behave in different scenarios using our combined system. For the purposes of the demo shown here, we crafted an NPC called Quinton: a semi-retired, grizzled bard who manages entertainment at a tavern called The Thirsty Whale. Quinton is an NPC who can be recruited to the player’s party and will aid the player in one of the scenarios shown in the demo.

The scenarios we chose for this particular demo are slices of two central aspects of the game. The player is first introduced to an exploratory scenario, wherein they can engage in dialogue with NPCs like Quinton, learn about the kinds of characters they are, improve relationships with them, and generally learn more about the world of the game. After some conversation, the player will be introduced to a combat scenario alongside Quinton, during which they can experience how Quinton will react and respond to various things that the player may, or may not, do. Of particular note is what may happen when Quinton, a character with the Vengeful trait, asks for help in combat and the player does not do anything to aid him.

## Guided Video Tour of the Oops! All Bards Demo ##

For anyone interested in seeing a guided tour of the demo as played by one of the collaborators on this project, please navigate [here](https://drive.google.com/file/d/1FPHsX0k_L6ojVAS33owDis2Sa2_megJC/view?usp=sharing).

## Installation ##

You can get a copy of this project up and running in the following ways,
depending on how you wish to use it:

### Game Only ###

If you only wish to play the demo, please follow these steps:

1. Download the appropriate .zip file from the [Releases](https://github.com/singlab/oops-all-bards/releases/tag/aiide-2022-demo) hub according to your OS. There are executables for Windows and macOS. Each .zip file contains two directories: ```server``` and ```game```.
2. For Windows users: Run ```run_oab.bat``` from within the ```server``` directory. For macOS users: You may execute the ```run_oab.sh``` shell script from the command line. This will ultimately open both a command line prompt and an ABL debugger; you can click the "Continue" button at the bottom of the ABL debugger and forget about it.
3. Run the appropriate game executable from within the ```game``` directory. For Windows users, this will be ```oops-all-bards.exe```. For macOS users, this will be ```OAB.app```. You should now be able to play the demo in the intended way.

### Build Project from Source ###

If you wish to build this project from source, it is recommended you have
[Unity 2021.2.9f1](https://unity3d.com/unity/whats-new/2021.2.9) installed on
your machine.

1. Clone this repository:
```shell
git clone https://github.com/singlab/oops-all-bards.git
```
2. After cloning/downloading this repository, open a new project in
Unity with the ```oops-all-bards``` directory as the root project folder.
3. Using the Java [Eclipse IDE](https://www.eclipse.org/downloads/packages/release/kepler/sr1/eclipse-ide-java-developers), go to ```File -> Open Projects from File System```.
4. In the dialogue window, select the ```abl``` folder as the import source using the Directory option, and click Finish.
5. Expand the ```src``` folder in the outliner on the left side of the editor. Expand the ```server``` folder on the left side of the editor. R-click the file ```TCPServer.java``` and select Run As -> Java application. This will run the Java proxy server on localhost.
6. You may now play the game demo within the Unity editor.
