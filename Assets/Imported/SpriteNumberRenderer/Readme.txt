Sprite Number Renderer

Developed by: Kendal Cormany (pufferfiz)
Pufferfiz.net

Version 1.0


Set Up:
1. Develope your number sheet in your favorite image editing software. All Numbers should be same size and lined up for easy cutting. 
2. In unity define that image as a sprite and put the sprite mode into multiple. In the sprite cutter cut out all the numbers trying to make it so that they are the same size( if not place origin points in the same spot). 
3. Make a empty game object and add a sprite renderer and a SpriteNumber script. Call this Genaric Number.
4. In the GenaricNumber's Sprite Number Script populate the Numbers Array with all 10 of your numbers you cut out earlier. 
5. Push this gameobject into your assets folder so that it will become a prefab. 
6. Create a new game object and add a NumberRenderer Script to it. Under that script populate the Genaric Number spot with your GenaricNumberPrefab. Do not touch the Numbers array. 
7. Place this gameobject in the scene where the numbers will be placed, the script will auto add and remove number objects as they are used. If you want to change the sprite render order or scale, modify the genaric number. This will apply to all numbers. 


In Script
1. Add a numberrender object to a GUI script or what ever you want. In that script simply call the object's .RenderNumber function. Passing in a Int.
 
Note: Right now there is no support for negatives or fractions. 

Feel free to modify the script but you CAN NOT redistribute with out permission.

No Credit necessary in your game, enjoy!