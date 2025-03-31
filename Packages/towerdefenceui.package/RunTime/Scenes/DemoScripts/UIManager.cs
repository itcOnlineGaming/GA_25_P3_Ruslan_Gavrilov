using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Enum for Menu stages
    enum MenuStage
    {
        TowersView,
        DetailsView,
        SettingsView,
        LoseView,
        WinView,
        GamePlay
    }

    // CanvasGroups for the UI menus
    public CanvasGroup FullMenu;
    public CanvasGroup TowersMenu;
    public CanvasGroup DetailsView;
    public CanvasGroup Settings;
    public CanvasGroup WinScreen;
    public CanvasGroup LoseScreen;

    //
    public CanvasGroup LoadingScreen;

    public CanvasGroup SettingsButton;
    public CanvasGroup TowersMenuButton;

    // Dictionary to manage the CanvasGroups based on MenuStage
    private Dictionary<MenuStage, CanvasGroup> menuDictionary;

    // Variable to track current menu stage
    private MenuStage menuStage;

    // Static instance to implement Singleton pattern
    private static UIManager _instance;

    // Public property to access the instance
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GamePlayUIController instance is missing in the scene!");
            }
            return _instance;
        }
    }

    // Singleton Pattern: Ensures only one instance exists
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);  // Destroy this object if there's already an instance
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the menu dictionary with CanvasGroups
        menuDictionary = new Dictionary<MenuStage, CanvasGroup>
        {
            { MenuStage.TowersView, TowersMenu },
            { MenuStage.DetailsView, DetailsView },
            { MenuStage.SettingsView, Settings },
            { MenuStage.WinView, WinScreen },
            { MenuStage.LoseView, LoseScreen }
        };

        CanvasGroupController.DisableGroup(LoadingScreen);

        // Disable the FullMenu and set up initial menus
        CanvasGroupController.DisableGroup(FullMenu);
        UIGeneralController.ToggleUI(menuDictionary[MenuStage.TowersView]);
        UIGeneralController.ToggleUI(menuDictionary[MenuStage.DetailsView]);

        // Disable all menus initially
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.TowersView]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.DetailsView]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.SettingsView]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.WinView]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.LoseView]);

        menuStage = MenuStage.GamePlay;
    }

    public void OpenGameLose()
    {
        UIGeneralController.ToggleUI(menuDictionary[MenuStage.LoseView]);

        menuStage = MenuStage.LoseView;
    }

    public void OpenGameWin()
    {
        UIGeneralController.ToggleUI(menuDictionary[MenuStage.WinView]);

        menuStage = MenuStage.WinView;
    }

    public void CloseGameWin()
    {
        UIGeneralController.CloseUI(menuDictionary[MenuStage.WinView]);

        menuStage = MenuStage.GamePlay;
    }

    public void CloseGameLose()
    {
        UIGeneralController.CloseUI(menuDictionary[MenuStage.LoseView]);

        menuStage = MenuStage.GamePlay;
    }

    // Open the Settings Menu
    public void OpenSettingsMenu()
    {
        UIGeneralController.CloseUI(FullMenu);
        CanvasGroupController.DisableGroup(SettingsButton);
        UIGeneralController.ToggleUI(menuDictionary[MenuStage.SettingsView]);

        menuStage = MenuStage.SettingsView;
    }

    // Open the Towers Menu
    public async void OpenTowersMenu()
    {
        if (menuStage != MenuStage.SettingsView)
        {
            CanvasGroupController.DisableGroup(menuDictionary[MenuStage.DetailsView]);
            UIGeneralController.CloseUI(menuDictionary[MenuStage.DetailsView]);
            CanvasGroupController.DisableGroup(TowersMenuButton);
            CanvasGroupController.EnableGroup(menuDictionary[MenuStage.TowersView]);
            await UIGeneralController.ToggleUI(FullMenu);

            menuStage = MenuStage.TowersView;
        }
    }

    // Open the Details Menu
    public void OpenDetailsMenu()
    {
        if (menuStage != MenuStage.SettingsView)
        {
            CanvasGroupController.DisableGroup(TowersMenuButton);
            CanvasGroupController.EnableGroup(menuDictionary[MenuStage.DetailsView]);
            UIGeneralController.ToggleUI(FullMenu);

            menuStage = MenuStage.DetailsView;
        }
    }

    // Switch between TowersView and DetailsView
    public async void SwitchMenuViews()
    {
        if (menuStage != MenuStage.SettingsView)
        {
            if (menuStage == MenuStage.TowersView)
            {
                await UIGeneralController.CloseUI(menuDictionary[MenuStage.TowersView]);
                await UIGeneralController.ToggleUI(menuDictionary[MenuStage.DetailsView]);
                menuStage = MenuStage.DetailsView;
            }
            else
            {
                CanvasGroupController.DisableGroup(TowersMenuButton);
                CanvasGroupController.DisableGroup(menuDictionary[MenuStage.TowersView]);
                CanvasGroupController.EnableGroup(menuDictionary[MenuStage.DetailsView]);
                await UIGeneralController.ToggleUI(FullMenu);
                menuStage = MenuStage.DetailsView;
            }
        }
    }

    // Handle back button functionality based on the current menu stage
    public async void BackButton()
    {
        switch (menuStage)
        {
            case MenuStage.SettingsView:
                await UIGeneralController.CloseUI(menuDictionary[MenuStage.SettingsView]);
                CanvasGroupController.EnableGroup(SettingsButton);
                CanvasGroupController.EnableGroup(TowersMenuButton);
                await UIGeneralController.ToggleUI(menuDictionary[MenuStage.TowersView]);
                await UIGeneralController.ToggleUI(menuDictionary[MenuStage.DetailsView]);
                menuStage = MenuStage.GamePlay;
                break;

            case MenuStage.DetailsView:
                await UIGeneralController.CloseUI(FullMenu);
                CanvasGroupController.DisableGroup(menuDictionary[MenuStage.DetailsView]);
                CanvasGroupController.EnableGroup(TowersMenuButton);
                await UIGeneralController.ToggleUI(menuDictionary[MenuStage.TowersView]);
                await UIGeneralController.ToggleUI(menuDictionary[MenuStage.DetailsView]);
                menuStage = MenuStage.GamePlay;
                break;
            case MenuStage.TowersView:
                await UIGeneralController.CloseUI(FullMenu);
                CanvasGroupController.EnableGroup(TowersMenuButton);
                await UIGeneralController.ToggleUI(menuDictionary[MenuStage.TowersView]);
                await UIGeneralController.ToggleUI(menuDictionary[MenuStage.DetailsView]);
                menuStage = MenuStage.GamePlay;
                break;
            default:
                break;
        }
    }
    public void ReturnHome()
    {
        UIGeneralController.LoadScene(LoadingScreen, "Menu");
    }

    public void GoToStats()
    {
        UIGeneralController.LoadScene(LoadingScreen, "GameOver");
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
