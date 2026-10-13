using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            
            //ojo aqui si descomento peta y se queda bloqueado!!
            //other.transform.GetComponent<Player_respawn>().PlayerDamage(other.GetContact(0).normal);
            
        }
    }
}
