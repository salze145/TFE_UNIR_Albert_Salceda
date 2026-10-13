using UnityEngine;

public class jumpDamage : MonoBehaviour
{
    public Collider2D collider2d;

    //public Animator anim;
    
   // public SpriteRenderer spriteRenderer;

    public float jumpForce = 1.5f;

    public int vidas_enem = 1;

    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if(other.GetContact(0).normal.y <= -0.9)
                {
                    other.gameObject.GetComponent<Player_mov>().ReboteUP();
               }
            
            
            PierdeVida();
            //compruebaVida();

        }
    }
*/
    public void PierdeVida()

    {
        if (vidas_enem >= 1)
        {
            vidas_enem -= 1; 
        }

        if (vidas_enem < 1)
        {
            //GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
        }
    }
        


    
   
}
