using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
   
    [SerializeField] private InputApplier inputApplier;
    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private Unit[] untPrefab1;
    [SerializeField] private Vector2[] possiblePositions;
    [SerializeField] private EmemyManager enEmemyManager;
    [SerializeField] private int curOrderLay = 2;

    private int unitCount = 0;
    private Arrow.ArrowPool arrowPool;
    private List<Unit> units = new List<Unit>();
    private int coins;
    

    public int Coins
    {
        get { return coins; }
    }

    void Start()
    {
        if(inputApplier != null)
            inputApplier.OnPlayerClicked += OnPlayerClicked;
        
        arrowPool = new Arrow.ArrowPool(arrowPrefab);
        InstantiateUnit();
        enEmemyManager.EnemyKilled += EnemyKilled;
    }

    private void EnemyKilled() { coins++; }

    private void InstantiateUnit()
    {
        Unit u = GameObject.Instantiate(untPrefab1[unitCount], possiblePositions[unitCount], Quaternion.identity) as Unit;
        units.Add(u);
        u.ArrowPool = arrowPool;
        u.GetComponent<SpriteRenderer>().sortingOrder = curOrderLay;
        curOrderLay++;

        unitCount++;
    }

    private void OnPlayerClicked(Vector2 position)
    {
        if(position.x > 4)
            return;
        

        var activeUnit = units.FirstOrDefault(u => !u.InCoolDown);

        if (activeUnit != null)
        {
            activeUnit.Shoot(position);
        }
    }


    public void AddUnit()
    {
        if(unitCount >= 3 ||coins <5) 
            return;

        InstantiateUnit();
        coins -= 5;
    }
}
