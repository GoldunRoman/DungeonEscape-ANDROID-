using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    private float _acidSpeed = 1.5f;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    void Update()
    {
        this.gameObject.transform.Translate(Vector3.right * _acidSpeed * Time.deltaTime);       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IDamageable hit = other.gameObject.GetComponent<IDamageable>();
            hit.Dammage();

            Destroy(this.gameObject);
        }
    }
}
