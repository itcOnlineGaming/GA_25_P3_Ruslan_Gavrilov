using UnityEngine;

public class UniversalSettings : MonoBehaviour
{
    public static UniversalSettings Instance { get; private set; }

    private const string VolumeKey = "GameVolume"; 
    private float volume = 100; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolume();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
        Debug.Log("Volume Set To: " + volume);
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(VolumeKey))
        {
            volume = PlayerPrefs.GetInt(VolumeKey);
        }
    }
    public float GetVolume()
    {
        return volume;
    }
}
