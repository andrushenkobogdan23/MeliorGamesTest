using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortress : MonoBehaviour {

   [SerializeField] Sprite[] FortressImage;
   public int fortressHP; 

    public void Start()
    {
        fortressHP = 1000;
    }

    void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
           
            GetDamage();
        }

        
    }

    void SetSprite(int fHP )
    {
        fHP = fortressHP;
       
        if(fHP<=600)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = FortressImage[0];
        }
        else if(fHP<=400)

        {
            gameObject.GetComponent<SpriteRenderer>().sprite = FortressImage[1];
        }
        else if(fHP<=0)
        {

            gameObject.GetComponent<SpriteRenderer>().sprite = FortressImage[2];
        }                                                           
    }
   
    void GetDamage()
    {
        fortressHP = fortressHP-100;

        SetSprite(fortressHP);
      
    }

    
}
