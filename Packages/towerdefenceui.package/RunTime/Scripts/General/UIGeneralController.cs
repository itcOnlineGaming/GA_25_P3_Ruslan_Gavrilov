using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
            Destroy(gameObject);
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

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(animation));

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
    }
    public static async Task CloseUI(CanvasGroup group)
    {
        if (_instance != null)
        {
            await _instance.WaitForAnimation(group, "Close");
        }
    }

    private async Task WaitForAnimation(CanvasGroup group, string name)
    {
        await WaitForAnimationCoroutine(name, group.GetComponent<Animator>());
    }

    public static void LoadScene(CanvasGroup group, string scene)
    {
        if (scene != "")
        {
            _instance.StartCoroutine(_instance.LoadAsynchronously(scene, group));
        }
    }

    IEnumerator LoadAsynchronously(string sceneName, CanvasGroup group)
    { 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        CanvasGroupController.EnableGroup(group);

        Slider loadingSlider = group.GetComponentInChildren<Slider>();
        TMP_Text progressText = group.GetComponentInChildren<TMP_Text>();

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .95f);
            loadingSlider.value = progress;

            if (operation.progress >= 0.9f)
            {
                progressText.text = "Tap to continue";
                loadingSlider.value = 1;

                if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

}



