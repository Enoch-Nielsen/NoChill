using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject iceExplosion;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snowball"))
        {
            if (!player.IsInvincible())
            {
                Instantiate(iceExplosion, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject, 0.05f);
            }

            player.Damage(5, false, other.transform.position);
        }
        
        if (other.CompareTag("Icicle"))
        {
            
            if (!player.IsInvincible())
            {
                Instantiate(iceExplosion, other.transform.position, Quaternion.identity);

                Destroy(other.gameObject, 0.05f);
            }
            
            player.Damage(5, true, other.transform.position);
        }
    }
}
