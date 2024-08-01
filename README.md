# room3d-character

## Unity 3D Demo Project
This demo project showcases a 3D environment with a controllable character and interactive elements. The project demonstrates character movement, navigation, and camera control using Unity's NavMesh and Cinemachine.

### Key Features
#### 3D Environment and Character
- A fully 3D environment with a character model.
- The character can be controlled using either the arrow keys or the WASD keys.

### Character Controls
#####  Rotation:		
- A key or Left Arrow: Rotate the character to the left.
- D key or Right Arrow: Rotate the character to the right.
#### Movement:
- W key or Up Arrow: Move the character forward.
- S key or Down Arrow: Move the character backward.

#### Clickable Points
- The scene contains clickable points that the player can interact with.
- When a point is clicked, the character will navigate to the clicked location using Unity's AI Navigation system.

#### **Notes**: 
- The mouse is not used for camera movement; instead, it is used for interacting with view points in the scene.
- I disabled brotli compression to speed up the build process for testing purposes. it can be enabled for production builds.


### AI Navigation
- The project utilizes Unity's NavMesh system for AI navigation, allowing the character to move to the clicked points smoothly and avoid obstacles.
### Cinemachine Camera
- The project incorporates Cinemachine for advanced camera control, providing dynamic and smooth camera movements that follow the character.

## Getting Started
### Prerequisites
Unity 2022.3 LTS or later.
### Installation
1. Clone or download the project from the repository.
2. Open the project in Unity.
3. Open MainScene from scenes folder and hit run.

#### Usage
- Use the arrow keys or WASD keys to control the character.
- Click on the points in the scene to make the character navigate to them.

## Running the Project Locally

1. **Install WAMP or any other tool to create a local server:** 
   You can download WAMP from [this link](https://sourceforge.net/projects/wampserver/).

2. **Build the project for WebGL in Unity:**
   - Open your Unity project.
   - Go to `File > Build Settings`.
   - Select `WebGL` and click `Build`.
   - Choose the output directory for the build (e.g., `web-build` folder).

3. **Set up the local server:**
   - Once WAMP is up and running, create an alias and give the full path to the WebGL build folder.
   - For example, if your alias is `room3d`, your alias configuration might look like:
     ```
     Alias /room3d "C:/path/to/your/web-build"
     <Directory "C:/path/to/your/web-build">
         Options Indexes FollowSymLinks MultiViews
         AllowOverride all
         Require all granted
     </Directory>
     ```

4. **Access the demo:**
   - Open a web browser and go to `http://localhost/room3d/` (replace `room3d` with your alias name if different).
   - The demo should be up and running.