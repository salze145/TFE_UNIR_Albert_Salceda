using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Player_mov_ : MonoBehaviour
{
    
    
    public float velocidad = 5;
    private Rigidbody2D rb2D;

    private float move;
    public float fuerzaSalto = 4;
    private bool estaEnSuelo;
    public Transform esSuelo;
    public LayerMask capaSuelo;
    public float radioSuelo = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        rb2D.linearVelocity = new Vector2(move*velocidad, rb2D.linearVelocity.y);
        //cambiamos a dónde mira el player según su dirección
        if(move != 0)
            transform.localScale =  new Vector3(Mathf.Sign(move), 1, 1);

        if(Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, fuerzaSalto);
        }

    }

    private void FixedUpdate()
    {
        estaEnSuelo = Physics2D.OverlapCircle(esSuelo.position, radioSuelo, capaSuelo);

    }
}
