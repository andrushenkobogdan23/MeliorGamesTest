using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyUnitMenu : MonoBehaviour
{

    [SerializeField] private Image icon;
    [SerializeField] private int unitCost = 10;

    public void Switch(int coins)
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);

        if (coins >= unitCost)
        {
            var iconColor = icon.color;
            iconColor.a = 1;
            icon.color = iconColor;
        }
        else
        {
            var iconColor = icon.color;
            iconColor.a = 0.2f;
            icon.color = iconColor;
        }

    }
}
