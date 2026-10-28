using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class patrullajeRhino : MonoBehaviour
{

    public float velocidad;
    public Transform ray;
    public Transform ray2;
    public float groundDistance;
    public float frontDistance;
    public LayerMask suelo;
    private bool mirandoDerecha = true;

    private Rigidbody2D RB2D;

    // Evita bloqueos y giros en los bordes
    public float cdGiro = 0.25f;
    private float tiempoProximoGiro;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RB2D.linearVelocity = new Vector2(transform.right.x * velocidad, RB2D.linearVelocity.y);
        if (!isGrounded())
        {
            Flip();

        }
        else if (Time.time >= tiempoProximoGiro && isFront())
        //if (isFront())
        {
            Flip();
            //velocidad *= -1;
        }


    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        tiempoProximoGiro = Time.time + cdGiro;
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(ray.position, Vector2.down, groundDistance, suelo);
    }

    private bool isFront()
    {
        Vector2 direccionPared = transform.right;
        return Physics2D.Raycast(ray2.position, direccionPared, frontDistance, suelo);
    }

    private void OnDrawGizmos()
    {
        if (ray != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.position, ray.position + (Vector3)(Vector2.down * groundDistance));
        }

        if (ray2 != null)
        {
            Gizmos.color = Color.blue;
            Vector2 direccionPared = Application.isPlaying ? (Vector2)transform.right : Vector2.right;
            Gizmos.DrawLine(ray2.position, (Vector2)ray2.position + (direccionPared * frontDistance));
        }
    }
}