using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    // "Public" variables
    [SerializeField] GameObject player;

    // Private variables
    private Rigidbody2D rBody;
    private GameObject playerScript;

    private void Start()
    {
        rBody = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rBody.gravityScale *= -1;
            player.GetComponent<PlayerController>().Flip();
        }
    }
}
