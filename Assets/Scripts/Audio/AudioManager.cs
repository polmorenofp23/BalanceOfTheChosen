using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource; // Para la música de fondo
    //[SerializeField] private AudioSource sfxSource;   // Para los efectos de sonido

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- Funciones para la Música ---
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // // --- Funciones para los Efectos de Sonido (SFX) ---
    // public void PlaySFX(AudioClip clip)
    // {
    //     sfxSource.PlayOneShot(clip);
    // }
}