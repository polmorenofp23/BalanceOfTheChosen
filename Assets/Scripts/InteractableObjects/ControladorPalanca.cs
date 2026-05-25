using UnityEngine;

public class ControladorPalanca : MonoBehaviour
{
    [Header("Conexiones")]
    public GameObject rayoLaser;

    [Header("Gráficos")]
    public Sprite palancaApagada;
    public Sprite palancaEncendida;

    private SpriteRenderer spriteRenderer;
    private bool trampaDesactivada = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Nos aseguramos de que empiece en la posición correcta
        spriteRenderer.sprite = palancaApagada;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Usamos el tag "Characters"
        if (collision.CompareTag("Characters"))
        {
            // Invertimos el valor. Si era 'false' pasa a 'true', y viceversa.
            trampaDesactivada = !trampaDesactivada;

            if (trampaDesactivada)
            {
                // El personaje ha APAGADO la trampa
                spriteRenderer.sprite = palancaEncendida;
                if (rayoLaser != null)
                {
                    rayoLaser.SetActive(false);
                }
            }
            else
            {
                // El personaje ha vuelto a ENCENDER la trampa
                spriteRenderer.sprite = palancaApagada;
                if (rayoLaser != null)
                {
                    rayoLaser.SetActive(true);
                }
            }
        }
    }
}