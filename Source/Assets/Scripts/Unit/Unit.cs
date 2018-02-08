using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform muzzle;

    private const float CoolDown = 1;
    private const float UpOffset = 8;
    private const float Damage = 1f;

    private AnimatorController controller;

    public Arrow.ArrowPool ArrowPool;
    private Vector3 target;
    private float curCooldown = 1;
    private float cooldownStep = 0.005f;
    private const float BaseAlphaOffset = 0.3f;
    private float CoolDownAlfa = 0.15f;

    public bool InCoolDown
    {
        get { return curCooldown + BaseAlphaOffset < CoolDown - CoolDownAlfa; }
    }

    void Awake()
    {
        controller = GetComponent<AnimatorController>();
    }

    public void Shoot(Vector2 target)
    {
        if (InCoolDown)
            return;

        this.target = target;

        var midPoint = (new Vector3(target.x,target.y) - muzzle.position);
        var apogeyPoint = midPoint + Vector3.up * UpOffset;
        this.target = apogeyPoint;
        this.target = new Vector2(target.x,Mathf.Clamp(target.y,0,1)*5);
        Debug.Log(target);
        GetComponent<Animator>().SetTrigger("Shoot");       
        curCooldown = 0;


    }

    void SpawnArrow()
    {
        var arrow = ArrowPool.GetArrow();
        arrow.Shoot(target, target - muzzle.position);

        arrow.transform.position = muzzle.position;
        arrow.transform.rotation = Quaternion.LookRotation(target);

        arrow.gameObject.SetActive(true);
        StartCoroutine(SetCooldown());

    }

    IEnumerator SetCooldown()
    {
        while (curCooldown + BaseAlphaOffset < CoolDown) 
        {
            var c = GetComponent<SpriteRenderer>().color; 
            c.a = curCooldown + BaseAlphaOffset;
            GetComponent<SpriteRenderer>().color = c; 
            curCooldown += cooldownStep;
            yield return new WaitForFixedUpdate();          
        }
        
    }

}
