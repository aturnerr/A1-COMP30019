Implementation

The terrain generation in our project starts off with the Diamond-Square algorithm, which 
creates the base terrain mesh with our preferred input properties. This mesh is attached to 
a script which applies the Phong Illumination model, without the specular component. Our
implementation has our terrain generating at different y-positions in the world each time,
so using the max and min value of the terrain, the location of other objects like the camera,
sun and water are generated relative to the terrains world position. These values are also
used in the terrain colours, where certain percentages of the terrain have different colours
according to their respective height.

The water plane is generated with a custom number of vertices and uses the water shader from
the Unity Standard Assets and has a refractive surface. The position of the sun is orbiting
a sphere in the centre of our terrain.

Camera collision with the terrain bounds use the sizes set in the original terrain generation,
for the terrain, the camera is nested within a game object with a rigidbody attached to it.
This game object handles collision with the terrain, and input movement keys modify the rigidbody
itself, while the camera maintains it's relative position with the object.


Sources
	- Unity Standard Assets (water shader)
	- https://www.youtube.com/channel/UCG5XadFg6icC2TcF0I5DIig (camera control)
	- https://www.youtube.com/watch?v=1HV8GbFnCik (diamond square)
	- Workshop 2 solutions (sun rotation)
	- Workshop 5 solutions (phong illumination)