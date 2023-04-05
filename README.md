
# Angry Birds clone

A clone of the popular mobile game "Angry birds", developed in Unity Engine.
Unity 2021.3.22f1

The game requires to choose 2 out of 3 birds to play.

Player loses every time they get out of birds while there are enemies remaining. Three hearts to recover lost birds.

If player destroys all the enemies with one bird, it gets recovered.



## Changes

- Renaming of Scriptable Objects to CharacterSO from BirdSO, due to the inclusion of the type Enemy that contains similar data.
- Addition of particle effects to birds when special ability is activated, and the enemy before gets destroyed.
- Unity Editor maintains data on ScriptableObjects, so to fix a bug, a list is cleared on runtime. The bug appears only on Unity Editor
- There was a bug related to the overlaping of panels at the end of the match. It got fixed by excluding the statements in LevelManager.cs
- Deletion of unused variables, organization of missing elements on the folders
