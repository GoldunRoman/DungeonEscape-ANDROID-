using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI Manager is NULL!");
            }
            return _instance;
        }
    }

    [SerializeField]
    private Text _diamondsCountTxt;
    [SerializeField]
    private Text _playerGemCountText;
    [SerializeField]
    private Image _selectionImage;
    [SerializeField]
    private List<GameObject> _healthHUD;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateDiamondsShopPanel(int playerGems)
    {
        _playerGemCountText.text = "" + playerGems + " G";
    }

    public void UpdateShopSelection(int yPos)
    {
        _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemsCount(int gems)
    {
        _diamondsCountTxt.text = gems.ToString();
    }

    public void UpdateHealth(int playerHealth)
    {
        if (playerHealth < 0) return;

        _healthHUD[playerHealth].SetActive(false);
    }
}
