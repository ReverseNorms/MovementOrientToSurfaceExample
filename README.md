<h2>Unity engine 3D character surface orientation example</h2>

This is an example project in Unity 2021.2 showing how to orient a character to the surface below it while moving.
The code is laid out in very simple terms, with detailed descriptions and an included example scene to test out in.

The entire function is contained within [SimpleCharacterControllerExample.cs](Assets/Scripts/SimpleCharacterControllerExample.cs)
Place the script on a character and setup the appropriate reference in the Unity Inspector.
There needs to be a Root (where this script is located) and the character rig/model as a child of that with its pivot at the feet.
