using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = UniversalSettings.Instance.GetVolume();

        Debug.Log(volumeSlider.value);

        volumeSlider.onValueChanged.AddListener(UniversalSettings.Instance.SetVolume);
    }
}
