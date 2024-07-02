using PutInOrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().HitMeteo();
        }
    }

}
