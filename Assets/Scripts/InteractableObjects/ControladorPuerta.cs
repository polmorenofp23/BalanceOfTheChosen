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

    // MAGIA MULTIJUGADOR: Al ser 'static', esta variable es compartida por TODAS las puertas
    public static int puertasListas = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = puertaCerrada;

        // Es vital resetear esto a 0 al empezar el nivel por si se reinicia la partida
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
                    // El jugador ha cumplido y está en la puerta
                    jugadorEnPosicion = true;
                    puertasListas++; // Sumamos 1 al contador global
                    spriteRenderer.sprite = puertaAbierta;

                    Debug.Log("Jugador en la puerta " + colorPuerta + ". Puertas listas: " + puertasListas);

                    // Si ambas puertas están listas, iniciamos la carga
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

    // NUEVO: Si el jugador se aleja de la puerta, cancelamos su estado de "listo"
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Characters") && jugadorEnPosicion)
        {
            jugadorEnPosicion = false;
            puertasListas--; // Restamos 1 al contador global
            spriteRenderer.sprite = puertaCerrada;

            // Cancelamos la cuenta atrás de carga de nivel por si el otro jugador ya estaba dentro
            CancelInvoke(nameof(CargarSiguienteNivel));

            Debug.Log("Jugador abandonó la puerta " + colorPuerta + ". Puertas listas: " + puertasListas);
        }
    }

    void CargarSiguienteNivel()
    {
        // Doble comprobación de seguridad antes de cargar
        if (puertasListas >= 2)
        {
            SceneManager.LoadScene(nombreSiguienteEscena);
        }
    }
}