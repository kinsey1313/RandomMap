//-- Project 1 - Randomly generated map using the Diamond Square algorithm --//

Kinsey Reeves - 695705





My implementation follows the diamond square algorithm to create a randomly generated 
height map. 
The map includes water cutsection which is shaded using a transparent shader.
The sun lights up 
vertices on the map using the phong illumination model. 

Firstly I created a 2d array with the 
randomly seeded values. This 2d array could be of size
2^k+1 due to the constraint of the diamond
 square algorithm. 



The MapDisplay script used by 
the MapGenerator object instantiates a DiamondSquare object, 
in which is defined the array
(dsArray).

MapDisplay is the core script of the map generation.
It handles coloring, diamond square, 
and mesh generation. This can be seen in the setup() 
function. 

Setup() sets up the diamond
square terrain as a 2D array, calculates the colors from each 
vertices y value, and creates 
a new MeshData type. 

MeshData contains all arrays necessary 
for generating a mesh in unity. The MeshGenerator class 
contains this sub class and handles 
all mesh work. GenMeshData() takes in the dsArray[,] and 
colors and returns a MeshData object
 containing all the relevant data to create the mesh. The
mesh is created and used inside of 
the MapDisplay script in setup(). 


The choice to separate 
the setup and drawmesh functions 
from a single function, say Start() was so that they could 
be implemented using buttons separately.
I removed this for submission.



There exists a smooth function which is in the MapGenerator gameobject
(MapDisplay script)
which will smooth the map each time clicked.

Boundary checking is done using a 
mesh collider and raycasting from the camera object in 
all movement directions. The edges of the map
have box colliders of width 1 to stop one 
moving outside of it.
 
I originally was using less vertices
(e.g. 4 for a square) but decided to use more so triangles 
did not share vertices. This was due to the
inability to shade vertices seperately. I wanted to give the map a retro look and did not 
like 
the mixing of colors.

NOTE* I used gitlab for the project so all history might not be seen in github
