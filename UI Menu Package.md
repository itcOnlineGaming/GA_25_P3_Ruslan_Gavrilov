# UI Menu Package

# This component provides the ability to set up and display different kinds of menu UI elements to create a standard menu system for your Tower Defence Game.

### **Prefabs**

#### **Buttons**

# These buttons are used for various user actions. A script of your making can be dragged onto them. They are purely cosmetic.

* # **BackButton**: Navigates to the previous screen.

* # **CancelButton**: Cancels the current action.

* # **CostButton**: Displays the cost of a selected item.

* # **DifficultyButton**: Allows the player to select difficulty level.

* # **IconButton**: Displays an icon in a button.

* # **IconPlainButton**: Displays a plain button with no icon.

* # **LevelButton**: Used to select a game level.

* # **QuitButton**: Quits the game.

* # **TextButton**: A general-purpose button with text.

#### **Sliders**

# Used to adjust values in the game UI.

* # **VolumeSlider**: Adjusts the audio volume.

### **Menus**

# These are the main menus used in the game.

* # **StartMenu**: The main menu where players can start the game.

* # **SettingsMenu**: A menu for adjusting game settings.

* # **QuitMenu**: A menu for quitting the game.

* # **ModeMenu**: A menu to select the game mode.

* # **LoadingScene**: Displays while the game is loading.

* # **LevelSelection**: Allows players to select a level.

* # **GameWin**: Displays when the player wins.

* # **GameLose**: Displays when the player loses.

**Tower Menus**  
This menu is used for a basic placement UI and shop / details view for any towers you would create.

* # **TowerMenu**: The main menu where players can buy Towers. Comes with 6 available slots, and view details of a tower.

  * BuyBuidlings: Menus displayed for purchasing towers.  
  * TowerInformation: A menu you can switch to in order to display certain tower information.

* # **TowerSlot**: Individual slot for a tower in case you want to add more slots.

### **How To Use the UI Menu System**

This guide outlines the steps to integrate the UI Menu system into your Tower Defence Game. Follow these instructions to set up and manage your menus, animations, and scene transitions.

#### **Step 1: Set Up Core Game Objects**

1. **Create a new game object** in your scene.  
2. **Attach the following scripts** to the newly created game object:  
   * **UIGeneralController**: This singleton handles enabling/disabling UI elements, managing animations, and loading scenes asynchronously.  
   * **UniversalSettings**: This script is responsible for saving the volume setting from the volume slider.  
3. **Do not destroy** these game objects when loading a new scene to maintain access to these scripts.

#### **Step 2: Create the Canvas and Add UI Elements**

1. **Create a canvas** in your scene.  
2. **Drag any desired UI elements** from the provided prefabs onto the canvas.

#### **Step 3: Manage UI Elements Through Script**

1. **Create a new script** in your project, name it appropriately (e.g., `MenuManager`).  
2. This script will be responsible for turning UI elements on/off and transitioning between menus.

#### **Step 4: Store UI Elements**

In your newly created script, declare variables to store references to the CanvasGroups for your UI menus:

| // CanvasGroups for the UI menuspublic CanvasGroup FullMenu;public CanvasGroup TowersMenu;public CanvasGroup DetailsView;public CanvasGroup Settings;public CanvasGroup WinScreen;public CanvasGroup LoseScreen; |
| :---- |

These variables will represent the UI menus and their visibility states.

#### **Step 5: Enable/Disable UI Elements**

Use the following functions to control the visibility of your UI elements:

* **Disable a UI group:**

| CanvasGroupController.DisableGroup({canvasGroup}); |
| :---- |

* **Enable a UI group:**

| CanvasGroupController.EnableGroup({canvasGroup}); |
| :---- |

#### **Step 6: Play Animations**

Use the **UIGeneralController** to play animations when transitioning between menus:

* **Toggle UI (Play Opening Animation):**

| UIGeneralController.ToggleUI({canvasGroup}); |
| :---- |

* **Close UI (Play Closing Animation):**

| UIGeneralController.CloseUI({canvasGroup}); |
| :---- |

#### **Step 7: Open and Close Menus**

For clarity, create separate functions to open and close each menu. For example, the following function opens the Details Menu:

| public void OpenDetailsMenu(){    // Disable the button used to open the details menu    CanvasGroupController.DisableGroup(TowersMenuButton);    // Enable the DetailsView menu and make it visible    CanvasGroupController.EnableGroup(DetailsMenu);    // Play opening animation for the FullMenu    UIGeneralController.ToggleUI(FullMenu);    // Track which menu is currently active    menuStage \= MenuStage.DetailsView;} |
| :---- |

To **close a menu**, use an asynchronous function to wait for the closing animation to finish before re-enabling any other elements. Example:

| public async void BackButton(){    // Wait for the closing animation to finish    await UIGeneralController.CloseUI(menuDictionary\[MenuStage.SettingsView\]);    // Re-enable the buttons for opening menus    CanvasGroupController.EnableGroup(SettingsButton);    CanvasGroupController.EnableGroup(TowersMenuButton);    // Update the enum to track the current screen    menuStage \= MenuStage.GamePlay;} |
| :---- |

#### **Step 8: Load a Scene**

To load a scene while displaying a loading screen, use the following method. First, make sure you have a **loading screen prefab** to mask the scene transition.

| public void ReturnHome(){    // Display loading screen while loading the scene    UIGeneralController.LoadScene(LoadingScreen, "Menu");} |
| :---- |

**Note:** Replace `"Menu"` with the name of the scene you want to load. Attach this function to a button, and the scene will load when clicked.

**Notes:**

You are free to create your own UI menus based on what suits you games, you can add UI elements to the menus that already exist, example: Tooltips on the loading screen. These won’t override the prefab but add onto it inside of the scene you are using. 

By following these steps, you will be able to set up a fully functional UI menu system, handle animations, and manage scene transitions effectively.

