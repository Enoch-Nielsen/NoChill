using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(DestroySelf), 1.5f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
