using UnityEngine;

namespace MenuUI
{
    public class VolumeControl : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
            AudioListener.volume = savedVolume;
        }

        public void SetVolume(float volume)
        {
            AudioListener.volume = volume;
            PlayerPrefs.SetFloat("Volume", volume);
        }
    }
}
