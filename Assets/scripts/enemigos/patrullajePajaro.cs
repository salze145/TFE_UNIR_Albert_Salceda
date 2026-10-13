using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class patrullajePajaro : MonoBehaviour
{
    
    // referencias a los scripts del jugador
    public Player_mov movPlayer;
    public Player_respawn vidaPlayer;
    //da�o que hace al jugador
    public int danyo;
    [SerializeField] private float velocidad;
    // detecta si tiene una pared delante
    [SerializeField] private Transform sensorPared;
    // [SerializeField] private float distancia;
    //[SerializeField] private bool moviendoDerecha;
    public bool mirandoHaciaIzquierda = true;
    [SerializeField] private bool tocandoSueloFrente;
    [SerializeField] private float distanciaRayoFrente = 0.3f;
    [SerializeField] private LayerMask capaParedes;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
       tocandoSueloFrente = Physics2D.Raycast(sensorPared.position, transform.right * -1, distanciaRayoFrente, capaParedes);

    }

    private void FixedUpdate()
    {
        // Movimiento constante en el eje X
        float direccion = mirandoHaciaIzquierda ? -1f : 1f;
        rb2D.linearVelocity = new Vector2(direccion * velocidad, 0f);

        // Detectar si hay pared en frente
        ComprobarPared();



    }

    private void ComprobarPared()
    {
        if (sensorPared == null) return;

        // Direcci�n hacia donde mira el enemigo
        Vector2 direccionRayo = mirandoHaciaIzquierda ? Vector2.left : Vector2.right;

        // Lanzamos un rayo horizontal desde el objeto sensor
        RaycastHit2D hayPared = Physics2D.Raycast(sensorPared.position, direccionRayo, distanciaRayoFrente, capaParedes);

        if (hayPared.collider != null)
        {
            Girar();
        }
    }

    // en este caso suelo es cualquier construcci�n como las paredes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("suelo"))
        {
            velocidad *= -1;
            Girar();
        }

        if (collision.CompareTag("Player"))
        {
            movPlayer.contadorRebote = movPlayer.tiempoTotalRebote;

            if (collision.transform.position.x <= transform.position.x)
            {
                movPlayer.reboteDesdeDerecha = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                movPlayer.reboteDesdeDerecha = false;
            }
           // vidaPlayer.PlayerDamage(danyo);
            GameManager.Instance.PerderVida();
        }

    }

    

    private void Girar()
    {
        mirandoHaciaIzquierda = !mirandoHaciaIzquierda;

        // Giramos el sprite horizontalmente (Eje Y = 180�)
        transform.Rotate(0f, 180f, 0f);
    }

  

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(sensorPared.position, sensorPared.position + distanciaRayoFrente * transform.right * -1);
    }
}


