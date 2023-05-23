using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopUI;
    [SerializeField]
    private Player _player;

    private int _itemSelected = -1;
    private int _itemCost;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_player != null)
        {
            int diamonds = _player.GetDiamonds();
            UIManager.Instance.UpdateDiamondsShopPanel(diamonds);
        }

        if(other.CompareTag("Player"))
            _shopUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _shopUI.SetActive(false);
    }

    public void SelectItem(int item)
    {
        //0 - flame sword
        //1 - boots
        //2 - key
        _itemSelected = item;
        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(108);
                _itemCost = 30;
                
                break;
            
            case 1:
                UIManager.Instance.UpdateShopSelection(-4);
                _itemCost = 20;
                break;

            case 2:
                UIManager.Instance.UpdateShopSelection(-109);
                _itemCost = 20;
                break;

            case -1:
                return;

        }       
    }

    public void BuyItem()
    {
        if(_player != null)
        {
            //Has money and selected an item
            if (_player.GetDiamonds() >= _itemCost && _itemSelected != -1)
            {
                _player.TakeDiamonds(_itemCost);
                UIManager.Instance.UpdateDiamondsShopPanel(_player.GetDiamonds());
                Debug.Log("You bought " + _itemSelected + " item");

                if(_itemSelected == 0) //flame sword
                {
                    GameManager.Instance.HasFlameSword = true;
                }

                else if(_itemSelected == 1) // boots
                {
                    GameManager.Instance.HasBootsOfFlight = true;
                } 

                if(_itemSelected == 2) //key
                {
                    GameManager.Instance.HasKeyToCastle = true;
                }
            }

            else
            {
                Debug.Log("No money - no honey babe!");
                return;
            }
        }
            
    }
}
