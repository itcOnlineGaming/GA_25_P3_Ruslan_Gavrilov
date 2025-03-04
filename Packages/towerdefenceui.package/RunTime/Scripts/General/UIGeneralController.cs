using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

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
    private Task WaitForAnimationCoroutine(string animation, Animator animator)
    {
        var tcs = new TaskCompletionSource<bool>();

        _instance.StartCoroutine(AnimationCoroutine(animation, animator, tcs));

        return tcs.Task;
    }

    private IEnumerator AnimationCoroutine(string animation, Animator animator, TaskCompletionSource<bool> tcs)
    {
        animator.Play(animation);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        tcs.SetResult(true);
    }

    public static async Task ToggleUI(CanvasGroup group)
    {
        if (_instance != null)
        {
            CanvasGroupController.EnableGroup(group);
            await _instance.WaitForAnimation(group, "Open");
        }
        else
        {
            Debug.LogError("UIGeneralController instance is missing in the scene. Add it to a GameObject.");
        }
    }
    public static async Task CloseUI(CanvasGroup group)
    {
        if (_instance != null)
        {
            await _instance.WaitForAnimation(group, "Close");
            CanvasGroupController.DisableGroup(group);
        }
        else
        {
            Debug.LogError("UIController instance is missing in the scene!");
        }
    }

    private async Task WaitForAnimation(CanvasGroup group, string animationName)
    {
        await WaitForAnimationCoroutine(animationName, group.GetComponent<Animator>());
    }
}



