using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public int PuntosTotales { get; private set; }
    //public HUD hud;
    public HUD hudActual;
    public Rigidbody2D rb2D;
    public int vidas = 3;
    private int puntosInicioEscena = 0;
    // PANEL MUERTE
    private bool esInvulnerable = false;
    [SerializeField] private float tiempoInvulnerabilidad = 1.0f; // Tiempo sin recibir daño

    // para el panel de Game Over
    public GameObject GameOver;
    public float tiempoEsperaReiniciar = 2.5f; // Segundos en reiniciar

    private bool estaMuerto = false;
    // variables para el efecto hit blanco
    public float duracionFlash = 0.15f;
    private SpriteRenderer spriteRenderer;
    private Material materialOriginal;
    private Material materialBlanco;

   

    //para la muerte
    public AudioClip sonidoMuerte;
    [Range(0.05f, 0.5f)] public float escalaCamaraLenta = 0.2f;
    public float duracionCamaraLenta = 1.2f;
    private AudioSource audioSource;
    private Collider2D col;



    void Awake()
    {

        // capturo los componentes
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        /*
         if(Instance == null){
             Instance = this;
             DontDestroyOnLoad(gameObject); 
         }else{
             Debug.Log("Mas de un Game manager!!!");
             Destroy(gameObject);
         }
        */
        //hudActual = Object.FindFirstObjectByType<HUD>();
        if (Instance != null && Instance != this)
        {
            // Usa DestroyImmediate o return INMEDIATAMENTE para que no siga ejecutando código
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    //para la persistencia en el cambio de escenas o reinicio del juego
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        // guarod los puntos al entrar a la nueva escena
        puntosInicioEscena = PuntosTotales;
        // Forzar la búsqueda fresca en la escena recién cargada
        hudActual = FindObjectOfType<HUD>();

        if (hudActual != null)
        {
            hudActual.ActualizarCorazones(vidas);
            hudActual.ActualizarPuntos(PuntosTotales);
        }

        //Re-vinculamos el PanelGameOver de la escena actual
        UIManager ui = FindObjectOfType<UIManager>();
        if (ui != null)
        {
            GameOver = ui.GameOver;
        }
    }
    void Start()
    {

        GameOver.SetActive(false);
        if (GameOver != null)
        {
            GameOver.SetActive(false);
        }


    }

    public void SumarPuntos(int puntosASumar)
    {
        PuntosTotales += puntosASumar;
        hudActual = Object.FindFirstObjectByType<HUD>();
        if (hudActual != null)
        {
            hudActual.ActualizarPuntos(PuntosTotales);
        }
    }

    // descuenta una vidad y llama al hud para que oculte uno de los corazones
    public void PerderVida()
    {

        if (esInvulnerable) return;
        if (vidas > 0)
        {
            vidas -= 1;


            //EfectoFlashBlanco();
            //Debug.Log("Instancia del GameManager que resta vida: " + this.GetInstanceID());

            if (hudActual == null)
            {
                hudActual = FindObjectOfType<HUD>();
            }

            if (hudActual != null)
            {
                hudActual.ActualizarCorazones(vidas);
            }

        }

        if (vidas <= 0)
        {
            // si las vidas son 0 se llama a muerte jugador sinó a la corutina que dá 1 segundo de invulnerabilidad

            MuerteJugador();
        }
        else
        {
            StartCoroutine(RutinaInvulnerabilidad());
        }
    }

    private void CheckLife()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        MuerteJugador();
    }


    private void MuerteJugador()
    {

        //estaMuerto = true;
        MostrarGameOver();

        
    }

    private IEnumerator EfectoFlashBlanco()
    {
        if (spriteRenderer != null && materialBlanco != null)
        {
            // Cambiamos el material al blanco puro
            spriteRenderer.material = materialBlanco;

            yield return new WaitForSeconds(duracionFlash);

            // Volvemos al material normal con sus colores originales
            spriteRenderer.material = materialOriginal;
        }
    }

    private IEnumerator RutinaInvulnerabilidad()
    {
        esInvulnerable = true;

        EfectoFlashBlanco();

        yield return new WaitForSeconds(tiempoInvulnerabilidad);
        esInvulnerable = false;
    }


    private IEnumerator RutinaReiniciarEscena()
    {
        // Espera los segundos indicados
        yield return new WaitForSeconds(tiempoEsperaReiniciar);

        // Vuelve a cargar la escena activa actual
        //Scene escenaActual = SceneManager.GetActiveScene();
        Debug.Log("Reiniciando escena desde: " + System.Environment.StackTrace);
        //Time.timeScale = 1f;
        //vidas = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GameOver.SetActive(false);
        //SceneManager.LoadScene("MenuPrincipal");
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // Restaurar el tiempo por si el juego estaba pausado
        vidas = 3;           // <<-- IMPORTANTE: Restablecer el contador a 3
        //PuntosTotales = 0;
        PuntosTotales = puntosInicioEscena;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReiniciarJuegoCompleto()
    {
        Time.timeScale = 1f;
        vidas = 3;
        PuntosTotales = 0; // Aquí sí vuelve a 0 la partida entera
        puntosInicioEscena = 0;

        SceneManager.LoadScene("MenuPrincipal"); // O tu primera escena
    }

    public void OnClickReiniciar()
    {
        Debug.Log("¡Boton Reiniciar pulsado!");
        // Reanudamos el tiempo ANTES de cargar o reiniciar
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarJuegoCompleto();
        }
    }

    public void MostrarGameOver()
    {
     
        //if (estaMuerto) return;

        // Arrancamos la secuencia visual a cámara lenta
        //StartCoroutine(MuerteCamaraLentaCorutina());

        // 1. Si no tenemos la referencia al PanelGameOver, la buscamos directamente en la escena
        if (GameOver == null)
        {
            // Buscamos cualquier objeto en la escena activo o inactivo que tenga la etiqueta o el script de GameOver,
            // o lo buscamos por nombre dentro de todos los Canvas activos
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                Transform panelTransform = canvas.transform.Find("GameOver");
                if (panelTransform != null)
                {
                    GameOver = panelTransform.gameObject;
                }
            }
        }

        // 2. Si lo encontramos, lo activamos y vinculamos el botón
        if (GameOver != null)
        {
            GameOver.SetActive(true);

            // Buscamos el botón de reiniciar dentro del panel
            Button botonReiniciar = GameOver.GetComponentInChildren<Button>(true);

            if (botonReiniciar != null)
            {
                // Limpiamos referencias previas y asignamos la función de reinicio completo
                botonReiniciar.onClick.RemoveAllListeners();
                botonReiniciar.onClick.AddListener(() =>
                {
                    Time.timeScale = 1f;
                    ReiniciarJuegoCompleto();
                });
            }
            else
            {
                Debug.LogError("[GameManager] No se encontró ningún componente Button dentro del PanelGameOver.");
            }

            // Pausamos el tiempo de juego
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("[GameManager] No se pudo encontrar el PanelGameOver en la escena. Asegúrate de que el objeto en el Canvas se llame exactamente 'PanelGameOver'.");
        }
    }

    private IEnumerator MuerteCamaraLentaCorutina()
    {
        estaMuerto = true;

        // 1. Sonido de muerte
        if (audioSource != null && sonidoMuerte != null)
        {
            audioSource.PlayOneShot(sonidoMuerte);
        }

        // 2. Desactivar colisionador para atravesar el suelo mientras cae
        if (col != null) col.enabled = false;

        // 3. CÁMARA LENTA (Reducimos la velocidad del tiempo)
        Time.timeScale = escalaCamaraLenta; // Ej: 0.2f
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // 4. Girar a la rana de espaldas
        transform.rotation = Quaternion.Euler(0f, 0f, -90f);

        // 5. Impulso físico de salto antes de caer
        if (rb2D != null)
        {
            rb2D.linearVelocity = Vector2.zero;
            rb2D.gravityScale = 2.5f;
            rb2D.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
        }

        // 6. Espera en tiempo real mientras la rana parpadea
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < duracionCamaraLenta)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }

            yield return new WaitForSecondsRealtime(0.08f);
            tiempoTranscurrido += 0.08f;
        }

        if (spriteRenderer != null) spriteRenderer.enabled = true;

        // 7. Restauramos la velocidad normal del tiempo ANTES de llamar al GameManager
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // 8. Ahora sí llamamos a tu lógica habitual (descontar vida / mostrar GameOver)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PerderVida(); // Recuerda que PerderVida llama a MostrarGameOver() si se quedan sin vidas
        }
    }

}
