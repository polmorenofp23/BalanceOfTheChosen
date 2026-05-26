using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Configuración de Audio")]
    public AudioSource sfxSource;
    public AudioSource walkSource;

    [Header("Clips de Sonido (SFX)")]
    public AudioClip walkSound;
    public AudioClip landSound;
    public AudioClip crystalSound;
    public AudioClip hologramSound;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound; // NUEVO: Sonido de cerrar

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

    // --- MÉTODOS PARA REPRODUCIR SONIDOS PUNTUALES ---
    public void PlayCrystal() => sfxSource.PlayOneShot(crystalSound);
    public void PlayHologram() => sfxSource.PlayOneShot(hologramSound);
    public void PlayLand() => sfxSource.PlayOneShot(landSound);
    public void PlayDoor() => sfxSource.PlayOneShot(doorOpenSound);
    public void PlayDoorClose() => sfxSource.PlayOneShot(doorCloseSound); // NUEVO

    // --- MÉTODOS PARA SONIDO CONTINUO (PASOS) ---
    public void PlayWalk()
    {
        if (walkSound != null && !walkSource.isPlaying)
        {
            walkSource.clip = walkSound;
            walkSource.Play();
        }
    }

    public void StopWalk()
    {
        if (walkSource.isPlaying)
        {
            walkSource.Stop();
        }
    }
}