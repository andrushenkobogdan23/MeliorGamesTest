using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private UnitsManager unitsManager;
    [SerializeField] private Fortress fortress;
    [SerializeField] private Text coinCount;
    [SerializeField] private BuyUnitMenu buyUnitMenu;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;
    private int curCoinCount;

    void OnGUI()
    {
        coinCount.text = unitsManager.Coins.ToString();
        curCoinCount = unitsManager.Coins;
    }

    public void SwitchBuyMenu()
    {
       buyUnitMenu.Switch(curCoinCount);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main menu", LoadSceneMode.Single);
    }

    private void Update()
    {
        if(unitsManager.Coins>=30)
        {
            WinPanel.SetActive(true);
        }
        if (fortress.fortressHP <= 0)
        {
            LosePanel.SetActive(true);
        }

    }
}
