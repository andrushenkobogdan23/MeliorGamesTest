using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float damage = 50;

    private Vector3 position;
    private Quaternion _lookRotation;
    private Vector3 _direction;

    private const float LerpSpeed = 0.05f;
    private const float Speed = 0.1f;
    const float RotationSpeed = 1f;
    float rotSpeed = 5f;

    private Vector3 prevPos;
    private const float ForceFactor = 80f;
    private Rigidbody rigidbody;

    void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.SetDamage(damage);
        }
          Hit();
       
        
       
    }

    private void Hit()
    {
        gameObject.SetActive(false);
    }

    public void Shoot(Vector2 position, Vector3 force)
    {
        gameObject.SetActive(true);
        this.position = position;
        transform.rotation = new Quaternion(0f,-90f,0f,90f);
        var force1 = force * ForceFactor;
        rigidbody.AddForce(force1);
    }

    void FixedUpdate()
    {
   
        if (transform.position.y < -1 )
            Hit();
        var fwd = transform.position -prevPos;
        var desireRot = Quaternion.LookRotation(fwd);

        transform.rotation = Quaternion.Lerp(transform.rotation, desireRot, 0.99989899f);
        prevPos = transform.position;
    }

    void Awake() { rigidbody = GetComponent<Rigidbody>(); }


    public class ArrowPool
    {
        private Arrow arrowPrefab;

        List<Arrow> arrows = new List<Arrow>();
        public ArrowPool(Arrow arrow) { this.arrowPrefab = arrow; }

        public Arrow GetArrow()
        {
            var arrow = arrows.FirstOrDefault(a => !a.gameObject.activeInHierarchy);

            if (arrow == null)
            {
                arrow = GameObject.Instantiate(arrowPrefab);
                arrows.Add(arrow);
            }
            arrow.GetComponent<Rigidbody>().velocity = Vector3.zero;
            arrow.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            return arrow;
        }
    }
}