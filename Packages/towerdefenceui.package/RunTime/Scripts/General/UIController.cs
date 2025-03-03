using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static void ToggleExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the Unity Editor
        #else
        Application.Quit(); // Quit the application in a built game
        #endif
    }

    public static IEnumerator WaitForAnimation(string animation, Animator animator)
    {
        animator.Play(animation);
        yield return null;

        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);
    }

    public void ToggleGameOverUI()
    {
        CanvasGroupController.EnableGroup(gameLoseUI);
        StartCoroutine(WaitForAnimation("Open", gameLoseUI.GetComponent<Animator>()));
    }
}
