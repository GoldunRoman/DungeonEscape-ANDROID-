using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {           
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                Destroy(this.gameObject);
                player.AddDiamonds(); //1 by default
            }
        }
    }

}
