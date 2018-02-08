using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Enemy : MonoBehaviour
{
    public Action Kiled;
     
    private AnimatorController controller;
    private const float Speed = 0.01f;
    [SerializeField] private float hP = 300;
    private float startHp;


    void Awake()
    {
        startHp = hP;
        controller = GetComponent<AnimatorController>();
    }


    void FixedUpdate()
    {
        transform.position+=Vector3.right*Speed;
    }

    public void SetDamage(float amount)
    {
        hP -= amount;
        if (hP <= 0)
            Die();
    }

    
    void ClearEnemy()
    {
        
        gameObject.SetActive(false);
    }

    private void Die()
    {
        if(Kiled != null)
           Kiled.Invoke();

        GetComponent<Animator>().SetTrigger("Death");
    }

     public void Revite(Vector3 spawnPoint)
     {
        hP = startHp;
        transform.position = spawnPoint;

        gameObject.SetActive(true);
     }



    void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Fortress>();

        if (enemy != null)
        {
           
            Die();
        }

        
    }
}
