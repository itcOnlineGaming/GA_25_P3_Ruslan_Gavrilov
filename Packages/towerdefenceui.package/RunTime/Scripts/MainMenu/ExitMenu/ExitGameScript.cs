using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameScript : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Cancel()
    {
        StartCoroutine(CloseQuitMenu());
    }
    private IEnumerator CloseQuitMenu()
    {
        yield return StartCoroutine(UIController.WaitForAnimation("Close", gameObject.GetComponent<Animator>()));

        CanvasGroupController.DisableGroup(_canvasGroup);
    }
}
