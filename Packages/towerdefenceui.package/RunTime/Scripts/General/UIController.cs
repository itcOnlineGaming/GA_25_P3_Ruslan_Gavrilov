using UnityEngine;
using System.Collections;

public class UIGeneralController : MonoBehaviour
{
    private static UIGeneralController _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    public static void ToggleExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Non-static coroutine function
    private IEnumerator WaitForAnimationCoroutine(string animation, Animator animator)
    {
        animator.Play(animation);
        yield return null; // Wait for one frame

        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);
    }

    // Static method to be used in other classes
    public static IEnumerator WaitForAnimation(string animation, Animator animator)
    {
        if (_instance != null)
        {
            yield return _instance.StartCoroutine(_instance.WaitForAnimationCoroutine(animation, animator));
        }
        else
        {
            Debug.LogError("UIGeneralController instance is missing in the scene. Add it to a GameObject.");
        }
    }

    public static void ToggleUI(CanvasGroup group)
    {
        CanvasGroupController.EnableGroup(group);

        if (_instance != null)
        {
            _instance.StartCoroutine(_instance.WaitForAnimationCoroutine("Open", group.GetComponent<Animator>()));
        }
        else
        {
            Debug.LogError("UIGeneralController instance is missing in the scene. Add it to a GameObject.");
        }
    }
}



