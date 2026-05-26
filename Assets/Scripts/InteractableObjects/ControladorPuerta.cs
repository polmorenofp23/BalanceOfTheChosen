using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorPuerta : MonoBehaviour
{
    [Header("Configuración de la Puerta")]
    [Tooltip("Escribe 'Azul', 'Rojo' o 'Morado' según la puerta")]
    public string colorPuerta = "Azul";
    public int cristalesNecesarios = 4;
    public string nombreSiguienteEscena = "LevelCompleted"; // Ajustado a tu nueva escena

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
            // Buscamos si el que ha tocado la puerta es Anakin o Vader
            Anakin scriptAnakin = collision.GetComponent<Anakin>();
            Vader scriptVader = collision.GetComponent<Vader>();

            int cristalesDelJugador = 0;
            bool jugadorValido = false;

            // 1. Si es Anakin y la puerta es Azul
            if (scriptAnakin != null && colorPuerta == "Azul")
            {
                cristalesDelJugador = scriptAnakin.blueKyberCrystalsCollected;
                jugadorValido = true;
            }
            // 2. Si es Vader y la puerta es Roja
            else if (scriptVader != null && colorPuerta == "Rojo")
            {
                cristalesDelJugador = scriptVader.redKyberCrystalsCollected;
                jugadorValido = true;
            }
            // 3. (Opcional) Si usáis la puerta Morada para Anakin
            else if (scriptAnakin != null && colorPuerta == "Morado")
            {
                cristalesDelJugador = scriptAnakin.purpleKyberCrystalsCollected;
                jugadorValido = true;
            }
            // 4. (Opcional) Si usáis la puerta Morada para Vader
            else if (scriptVader != null && colorPuerta == "Morado")
            {
                cristalesDelJugador = scriptVader.purpleKyberCrystalsCollected;
                jugadorValido = true;
            }

            // Si el jugador que ha tocado no coincide con el color de su puerta, no hacemos nada
            if (!jugadorValido) return;

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