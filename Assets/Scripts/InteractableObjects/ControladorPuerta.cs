using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorPuerta : MonoBehaviour
{
    [Header("Configuración de la Puerta")]
    [Tooltip("Escribe 'Azul' o 'Morado' según la puerta")]
    public string colorPuerta = "Azul";
    public int cristalesNecesarios = 4;
    public string nombreSiguienteEscena = "MenuPrincipal";

    [Header("Gráficos")]
    public Sprite puertaCerrada;
    public Sprite puertaAbierta;

    private SpriteRenderer spriteRenderer;
    private bool jugadorEnPosicion = false;

    public static int puertasListas = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = puertaCerrada;
        puertasListas = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Characters") && !jugadorEnPosicion)
        {
            Anakin scriptJugador = collision.GetComponent<Anakin>();

            if (scriptJugador != null)
            {
                int cristalesDelJugador = 0;

                if (colorPuerta == "Azul")
                {
                    cristalesDelJugador = scriptJugador.blueKyberCrystalsCollected;
                }
                else if (colorPuerta == "Morado")
                {
                    cristalesDelJugador = scriptJugador.purpleKyberCrystalsCollected;
                }

                if (cristalesDelJugador >= cristalesNecesarios)
                {
                    jugadorEnPosicion = true;
                    puertasListas++;
                    spriteRenderer.sprite = puertaAbierta;

                    if (SoundManager.Instance != null)
                    {
                        SoundManager.Instance.PlayDoor();
                    }

                    Debug.Log("Jugador en la puerta " + colorPuerta + ". Puertas listas: " + puertasListas);

                    if (puertasListas >= 2)
                    {
                        Debug.Log("¡Ambos jugadores listos! Cargando siguiente nivel...");
                        Invoke(nameof(CargarSiguienteNivel), 1f);
                    }
                }
                else
                {
                    Debug.Log("Acceso denegado en puerta " + colorPuerta + ". Tienes " + cristalesDelJugador + " de " + cristalesNecesarios + " cristales.");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Characters") && jugadorEnPosicion)
        {
            jugadorEnPosicion = false;
            puertasListas--;
            spriteRenderer.sprite = puertaCerrada;
            CancelInvoke(nameof(CargarSiguienteNivel));

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayDoorClose();
            }

            Debug.Log("Jugador abandonó la puerta " + colorPuerta + ". Puertas listas: " + puertasListas);
        }
    }

    void CargarSiguienteNivel()
    {
        if (puertasListas >= 2)
        {
            SceneManager.LoadScene(nombreSiguienteEscena);
        }
    }
}