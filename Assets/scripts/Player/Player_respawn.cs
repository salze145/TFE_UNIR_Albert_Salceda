using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_respawn : MonoBehaviour
{
    public Animator animator;
    //public GameObject[] corazones;
    
    //vidas y corazones
   

    // para el panel de Game Over
    //public GameObject GameOver;  
    //public float tiempoEsperaReiniciar = 2.5f; // Segundos en reiniciar

    //private bool estaMuerto = false;

    [SerializeField] private Rigidbody2D rb2D;
    private Player_mov movimientoJugador;
    public float duracionFlash = 0.15f;
    private SpriteRenderer spriteRenderer;
    private Material materialOriginal;
    private Material materialBlanco;

   

    //fuerza del rebote al chocar con enemigo
    // private float fuerzaRebote = 2f;
  

    //esta variable se utiliza para saber si estamos saltando encima de un enemigo
    private float mediaAltura;


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            materialOriginal = spriteRenderer.material;
            // Usamos el mismo shader de texto blanco que en el enemigo
            materialBlanco = new Material(Shader.Find("GUI/Text Shader"));
        }
    }


    private void Start()
    {
        // asignamos las tres vidas de los corazones
        
        rb2D = GetComponent<Rigidbody2D>();
        movimientoJugador = GetComponent<Player_mov>();
        animator = GetComponent<Animator>();
        //Debug.Log(corazones.Length);
        mediaAltura = spriteRenderer.bounds.extents.y;
        
    }



/*
    // comprueba las vidas y va quitando los corazones; si tiene menos de uno reinicia la escena
    private void CheckLife()
    {
        if(vidas < 1)
        {
            
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            MuerteJugador();

        }
        else if(vidas < 2)
        {
            Destroy(corazones[1].gameObject);
            
        }
        else if (vidas < 3)
        {
            Destroy(corazones[2].gameObject);
           

        }
    }
*/

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("enemigo"))
        {
          ColisionEnemigo(other);
        }

        if (other.gameObject.CompareTag("boss"))
        {
            ColisionBoss(other);
        }
    }

  

    //tocamos al enemigo por encima, el player rebota, el enemigo muere
    private void ColisionEnemigo(Collision2D other)
    {
        enemigo_col enemigo = other.gameObject.GetComponent<enemigo_col>();

        if(Physics2D.Raycast(transform.position, Vector2.down, mediaAltura + 0.1f, LayerMask.GetMask("enemigo")))
        {
            enemigo.Muerte();
        }
       
    }

    private void ColisionBoss(Collision2D other)
    {
        Boss1_col boss1 = other.gameObject.GetComponent<Boss1_col>();

        if (Physics2D.Raycast(transform.position, Vector2.down, mediaAltura + 0.1f, LayerMask.GetMask("boss")))
        {
            boss1.Muerte();
        }

    }

/*
    public void PlayerDamage(int danyo)
    {

        vidas -= danyo;
        StartCoroutine(EfectoFlashBlanco());
        Debug.Log(vidas);
        CheckLife();
      
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

    private void MuerteJugador()
    {
      
        estaMuerto = true;

        // Mostrar el mensaje "Game Over"
        if (GameOver != null)
        {
            GameOver.SetActive(true); // muestra el panel de game over
        }

        // Desactiva los controles/scripts del jugador para que no se mueva
        MonoBehaviour[] scriptsJugador = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scriptsJugador)
        {
            if (script != this) script.enabled = false;
        }

        // Inicia la cuenta atr�s para reiniciar
        StartCoroutine(RutinaReiniciarEscena());
    }

    private IEnumerator RutinaReiniciarEscena()
    {
        // Espera los segundos indicados
        yield return new WaitForSeconds(tiempoEsperaReiniciar);

        // Vuelve a cargar la escena activa actual
        Scene escenaActual = SceneManager.GetActiveScene();
        //GameOver.SetActive(false);
        SceneManager.LoadScene(escenaActual.name);
    }
   */


}
