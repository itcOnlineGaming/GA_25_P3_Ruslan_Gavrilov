using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

enum MenuStage
{
    MainMenu,
    ModesMenu,
    LevelSelection,
    DifficultySelection,
    Settings,
    QuitGame
}

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CanvasGroup MainMenuButtons;
    public CanvasGroup ModesMenu;
    public CanvasGroup LevelSelector;
    public CanvasGroup LevelsMenu;
    public CanvasGroup DiffucultyMenu;
    public CanvasGroup SettingMenu;
    public CanvasGroup QuitMenu;

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

        menuStage = MenuStage.MainMenu;
    }
    public void OpenSettingsMenu()
    {
        ModesMenu.GetComponent<Animator>().Play("Default");
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.ModesMenu]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.MainMenu]);
        CanvasGroupController.EnableGroup(menuDictionary[MenuStage.Settings]);
        StartCoroutine(UIController.WaitForAnimation("Open", SettingMenu.GetComponent<Animator>()));

        menuStage = MenuStage.Settings;
    }

    public void OpenLevelSelector()
    {
        ModesMenu.GetComponent<Animator>().Play("Default");
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.ModesMenu]);
        CanvasGroupController.DisableGroup(menuDictionary[MenuStage.MainMenu]);
        CanvasGroupController.EnableGroup(LevelSelector);
        CanvasGroupController.EnableGroup(LevelsMenu);
        StartCoroutine(UIController.WaitForAnimation("Open", LevelSelector.GetComponent<Animator>()));

        menuStage = MenuStage.LevelSelection;
    }

    public void OpenDifficultySelector(string _level)
    {
        LevelName = _level;

        CanvasGroupController.DisableGroup(LevelsMenu);
        CanvasGroupController.EnableGroup(DiffucultyMenu);
        StartCoroutine(UIController.WaitForAnimation("Open", DiffucultyMenu.GetComponent<Animator>()));

        menuStage = MenuStage.DifficultySelection;
    }

    public void OpenExitGameUI()
    {
        CanvasGroupController.EnableGroup(menuDictionary[MenuStage.QuitGame]);
        StartCoroutine(UIController.WaitForAnimation("Open", QuitMenu.GetComponent<Animator>()));
    }

    public void BackButton()
    {
        switch (menuStage)
        {
            case MenuStage.DifficultySelection:
                DiffucultyMenu.GetComponent<Animator>().Play("Default");
                CanvasGroupController.DisableGroup(DiffucultyMenu);
                OpenLevelSelector();
                break;

            case MenuStage.LevelSelection:
                StartCoroutine(CloseLevelSelction());
                break;

            case MenuStage.Settings:
                StartCoroutine(CloseSettingsCoRoutine());
                break;
            default:
                break;
        }
    }

    public void CloseModesMenu()
    {
        StartCoroutine(CloseModesCoRoutine());
    }

    private IEnumerator CloseModesCoRoutine()
    {
        yield return StartCoroutine(UIController.WaitForAnimation("Close", ModesMenu.GetComponent<Animator>()));

        ModesMenu.GetComponent<Animator>().Play("Default");
        CanvasGroupController.DisableGroup(ModesMenu);
    }

    private IEnumerator CloseSettingsCoRoutine() {
        yield return StartCoroutine(UIController.WaitForAnimation("Close", SettingMenu.GetComponent<Animator>()));

        SettingMenu.GetComponent<Animator>().Play("Default");
        CanvasGroupController.DisableGroup(SettingMenu);
        CanvasGroupController.EnableGroup(MainMenuButtons);
    }


    private IEnumerator CloseLevelSelction()
    {
        yield return StartCoroutine(UIController.WaitForAnimation("Close", LevelSelector.GetComponent<Animator>()));

        LevelSelector.GetComponent<Animator>().Play("Default");
        CanvasGroupController.DisableGroup(LevelSelector);
        CanvasGroupController.EnableGroup(MainMenuButtons);
    }

    public void OpenModesMenu()
    {
        CanvasGroupController.EnableGroup(ModesMenu);
        StartCoroutine(UIController.WaitForAnimation("Open", ModesMenu.GetComponent<Animator>()));
    }

    public void OpenQuitMenu()
    {
        CanvasGroupController.EnableGroup(menuDictionary[MenuStage.QuitGame]);
        StartCoroutine(UIController.WaitForAnimation("Open", QuitMenu.GetComponent<Animator>()));
    }
}
