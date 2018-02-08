using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EmemyManager : MonoBehaviour
{
    public Action EnemyKilled;

    [SerializeField] private float startSpawnRate = 3f;
    [SerializeField] private float spawnRateStep = 0.1f;
    [SerializeField] private float flyingSpaunPointUnits = 0.1f;
    [SerializeField] private float SpaunPointUnits = 0.1f;
    private float curspaunreittime;

    [SerializeField] private Transform landUnitSpawnPoint;
    [SerializeField] private Transform flyingUnitSpawnPoint;

    [SerializeField] private Enemy BomberPrefab;
    [SerializeField] private Enemy OnagerPrefab;
    [SerializeField] private Enemy BatArmySwordPrefab;

    List<Enemy> Bombers = new List<Enemy>();
    List<Enemy> BatArmySwords = new List<Enemy>();
    List<Enemy> Onagers = new List<Enemy>();

    void Start()
    {
        curspaunreittime = startSpawnRate;
        StartCoroutine(SpaupEnemuWhisdalete());
    }
	
	
	void Update () {  }


    Enemy GetButArmySword()
    {
        var ButArmySword = BatArmySwords.FirstOrDefault(a => !a.gameObject.activeInHierarchy);

        if (ButArmySword == null)
        {
            ButArmySword = GameObject.Instantiate(BatArmySwordPrefab);
            BatArmySwords.Add(ButArmySword);
            ButArmySword.Kiled += Kiled;
        }
        return ButArmySword;
    }

    private void Kiled()
    {
        if(EnemyKilled != null)
            EnemyKilled.Invoke();
    }

    Enemy GetOnager()
    {
        var Onager = Onagers.FirstOrDefault(a => !a.gameObject.activeInHierarchy);

        if (Onager == null)
        {
            Onager = GameObject.Instantiate(OnagerPrefab);
            Onagers.Add(Onager);
            Onager.Kiled += Kiled;

        }
        return Onager;
    }


    Enemy GetBomber()
    {
        var Bomber = Bombers.FirstOrDefault(a => !a.gameObject.activeInHierarchy);

        if (Bomber == null)
        {
            Bomber = Instantiate(BomberPrefab);
            Bombers.Add(Bomber);
            Bomber.Kiled += Kiled;

        }
        return Bomber;
    }

    void SpawnEnemy()
    {
        int randspawn = Random.Range(0, 3);
        switch (randspawn)
        {
            case 0:
            {
                GetButArmySword().Revite(flyingUnitSpawnPoint.position);
                break;
                
            }
            case 1:
            {
                GetOnager().Revite(landUnitSpawnPoint.position);
                break;

            }
            case 2:
            {
                GetBomber().Revite(landUnitSpawnPoint.position);
                break;

            }
        }

    }

    IEnumerator SpaupEnemuWhisdalete()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(curspaunreittime);
            curspaunreittime += spawnRateStep;
            
        }
      
    }  
}