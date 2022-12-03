using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private Player player;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snowball"))
        {
            player.Damage(7, false);
        }

        if (other.CompareTag("Icicle"))
        {
            player.Damage(10, true);
        }
        
        Debug.Log(other.tag);
        
       // Debug.Log("Hit");
    }
}
