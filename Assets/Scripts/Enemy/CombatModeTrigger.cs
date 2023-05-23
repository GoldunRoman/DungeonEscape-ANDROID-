using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatModeTrigger : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("CombatModeOnTriggerEnter::Enemy, CM = false");
            _enemy = other.gameObject.GetComponent<Enemy>();

        }



        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("CombatModeOnTriggerEnter::Player, CM = true");
            _enemy.SwitchCombatMode(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("CombatModeOnTriggerExit::Player, CM = false");
            _enemy.SwitchCombatMode(false);
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("CombatModeOnTriggerExit::Enemy, CM = false");
            _enemy.SwitchCombatMode(false);
        }
    }

}
