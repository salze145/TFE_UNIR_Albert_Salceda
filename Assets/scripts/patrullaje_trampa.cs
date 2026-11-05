using UnityEngine;

public class patrullaje_trampa : MonoBehaviour
{
    public float velocidad;
    public LayerMask suelo;
    private Rigidbody2D RB2D;
  
    public bool direccionVertical;
  

    // capturo el rigid body al iniciar
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }

    // establecemos una velocidad horizontal al RB
    private void Update()
    {

       if (direccionVertical == true)
        {
            //RB2D.linearVelocity = new Vector2(RB2D.linearVelocity.x, RB2D.linearVelocity.y * velocidad);
            RB2D.linearVelocity = new Vector2(0f, velocidad);
        }
        else
         {
            RB2D.linearVelocity = new Vector2(velocidad, 0f);
        }


    }

    // si hay colisión con objeto con etiqueta "suelo" llamamos a la función Flip
    private void OnTriggerEnter2D(Collider2D other)
    {
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
                GameManager.Instance.MuerteJugador();

        }
        // El parámetro 'other' representa el Collider2D del objeto que ha entrado
        if (collision.gameObject.tag == "suelo")
        {
            Flip();
        }


    }

    // se invierte la velocidad vertical
    private void Flip()
    {
        velocidad *= -1;
    }



    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.red;
      //  Gizmos.DrawLine(ray.position, new Vector2(ray.position.x, ray.position.y - groundDistance));

    }

}

