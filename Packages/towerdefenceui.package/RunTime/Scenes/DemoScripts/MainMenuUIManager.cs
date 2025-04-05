using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    enum MenuStage
    {
        MainMenu,
        ModesMenu,
        LevelSelection,
        DifficultySelection,
        Settings,
        QuitGame
    }
    // UI Elements
    public CanvasGroup MainMenuButtons;
    public CanvasGroup ModesMenu;
    public CanvasGroup LevelSelector;
    public CanvasGroup LevelsMenu;
    public CanvasGroup DiffucultyMenu;
    public CanvasGroup SettingMenu;
    public CanvasGroup QuitMenu;

    public CanvasGroup loadingScene;

    private Dictionary<MenuStage, CanvasGroup> menuDictionary;

    private MenuStage menuStage;

    private string LevelName;

    void Start()
    {
        menuDictionary = new Dictionary<MenuStage, CanvasGroup>
        {
            { MenuStage.MainMenu, MainMenuButtons },
            { MenuStage.ModesMenu, ModesMenu },
            { MenuStage.LevelSelection, LevelSelector },
            { MenuStage.DifficultySelection, DiffucultyMenu },
            { MenuStage.Settings, SettingMenu },
            { MenuStage.QuitGame, QuitMenu }
        };

        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.ModesMenu]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.DifficultySelection]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.LevelSelection]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.Settings]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.QuitGame]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.DifficultySelection]);
        CanvasGroupController.DisableGroup(loadingScene);

        menuStage = MenuStage.MainMenu;
    }
    public void OpenSettingsMenu()
    {
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.MainMenu]);
        UIGeneralController.ToggleUI(menuDictionary[MenuStage.Settings]);

        menuStage = MenuStage.Settings;
    }

    public void OpenLevelSelector()
    {
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.MainMenu]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.ModesMenu]);
        CanvasGroupController.EnableGroup(LevelsMenu);

        UIGeneralController.ToggleUI(menuDictionary[MenuStage.LevelSelection]);

        menuStage = MenuStage.LevelSelection;
    }

    public void OpenDifficultySelector(string _level)
    {
        LevelName = _level;

        CanvasGroupController.DisableGroup(LevelsMenu);
        UIGeneralController.ToggleUI(menuDictionary[MenuStage.DifficultySelection]);

        menuStage = MenuStage.DifficultySelection;
    }

    public void OpenExitGameUI()
    {
        UIGeneralController.ToggleUI(menuDictionary[MenuStage.QuitGame]);

        menuStage = MenuStage.QuitGame;
    }
    public void OpenModesMenu()
    {
        UIGeneralController.ToggleUI(menuDictionary[MenuStage.ModesMenu]);
        menuStage = MenuStage.ModesMenu;
    }

    public void OpenQuitMenu()
    {
        UIGeneralController.ToggleUI(menuDictionary[MenuStage.QuitGame]);
        menuStage = MenuStage.QuitGame;
    }

    public void StartGame()
    {
        UIGeneralController.LoadScene(loadingScene, "DemoGame");
    }

    public async void BackButton()
    {
        switch (menuStage)
        {
            case MenuStage.DifficultySelection:
                await UIGeneralController.CloseUI(menuDictionary[MenuStage.DifficultySelection]);
                CanvasGroupController.EnableGroup(LevelsMenu);
                menuStage = MenuStage.LevelSelection;
                break;

            case MenuStage.LevelSelection:
                await UIGeneralController.CloseUI(menuDictionary[MenuStage.LevelSelection]);
                CanvasGroupController.EnableGroup(menuDictionary[MenuStage.ModesMenu]);
                menuStage = MenuStage.ModesMenu;
                break;

            case MenuStage.ModesMenu:
                await UIGeneralController.CloseUI(menuDictionary[MenuStage.ModesMenu]);
                CanvasGroupController.EnableGroup(menuDictionary[MenuStage.MainMenu]);
                menuStage = MenuStage.MainMenu;
                break;

            case MenuStage.Settings:
                await UIGeneralController.CloseUI(menuDictionary[MenuStage.Settings]);
                CanvasGroupController.EnableGroup(menuDictionary[MenuStage.MainMenu]);
                menuStage = MenuStage.MainMenu;
                break;

            case MenuStage.QuitGame:
                await UIGeneralController.CloseUI(menuDictionary[MenuStage.QuitGame]);
                break;

            default:
                break;
        }
    }
}
