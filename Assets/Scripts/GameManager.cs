using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is null!");
            
            return _instance;
        }
    }

    public bool HasKeyToCastle { get; set; }
    public bool HasBootsOfFlight { get; set; }
    public bool HasFlameSword { get; set; }

    public Player Player { get; private set; }

    private void Awake()
    {
        _instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(Player == null)
        {
            Debug.Log("GameManager null reference exception! The Player is NULL!");
        }
    }
}
