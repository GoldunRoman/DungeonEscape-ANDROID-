using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canAttack = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();

        if(hit != null)
        {
            if (_canAttack)
            {
                //Attack::IDamagable interface::Dammage() method
                hit.Dammage();
                _canAttack = false;
            }
            StartCoroutine(CanAttackDelay());
        }       
    }

    IEnumerator CanAttackDelay()
    {
        yield return new WaitForSeconds(0.3f);
        _canAttack = true;
    }
}
