using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuController : MonoBehaviour
{
    public CanvasGroup settingsMenu;
    public CanvasGroup settingsPopOutButton;

    private RectTransform canvasRectTransform;
    private RectTransform objectRectTransform;

    public bool isActive;

    private void Start()
    {
        isActive = false;
        CanvasGroupController.DisableGroup(settingsMenu);

        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        objectRectTransform = GetComponent<RectTransform>();
    }

    public void OpenSettingsMenu()
    {
        CanvasGroupController.DisableGroup(settingsPopOutButton);
        CanvasGroupController.EnableGroup(settingsMenu);

        StartCoroutine(UIController.WaitForAnimation("Open", GetComponent<Animator>()));
    }

    public void CloseMenu(string animation)
    {
        StartCoroutine(CloseMenuAfterAnimation(animation));
    }

    private IEnumerator CloseMenuAfterAnimation(string animation)
    {
        yield return StartCoroutine(UIController.WaitForAnimation(animation, GetComponent<Animator>()));

        CanvasGroupController.DisableGroup(settingsMenu);
        CanvasGroupController.EnableGroup(settingsPopOutButton);

        settingsMenu.GetComponent<Animator>().Play("Default");
    }

}
