using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity = 20f;
    public float life = 1f;

    private int firedByLayer;
    private float lifeTimer;

    private Vector3 directionBullet;


    void Update()
    {
        RaycastHit hit;
      
        if (Physics.Raycast(transform.position, transform.forward, out hit, velocity * Time.deltaTime, ~(1 << firedByLayer)))
        {
            transform.position = hit.point;
            Vector3 reflected = Vector3.Reflect(transform.forward, hit.normal);
            Vector3 direction = transform.forward;
            Vector3 vop = Vector3.ProjectOnPlane(reflected, Vector3.forward);
            transform.forward = vop;
            transform.rotation = Quaternion.LookRotation(vop, Vector3.forward);
              Hit(transform.position, direction, reflected, hit.collider);
        }
        else
        {
            transform.Translate(directionBullet * velocity * Time.deltaTime, Space.World);
        }
        


        if (Time.time > lifeTimer + life)
        {
//            print("dis2");
            gameObject.SetActive(false);
        }
    }

    private void Hit(Vector3 position, Vector3 direction, Vector3 reflected, Collider collider)
    {
        print("dis2");
        // Do something here with the object that was hit (collider), e.g. collider.gameObject 
        gameObject.SetActive(false);
    }

    public void Fire(Vector3 position, Vector3 euler, int layer,Vector3 direction)
    {
        lifeTimer = Time.time;
        transform.position = position;
        transform.eulerAngles = euler;
        // transform.position = new Vector3(transform.position.x, transform.position.y, 0);
       // Vector3 vop = Vector3.ProjectOnPlane(transform.forward, Vector3.forward);
        directionBullet = direction;
         Debug.DrawRay(transform.position,directionBullet,Color.red,5f);
       // transform.rotation = Quaternion.LookRotation(vop, Vector3.forward);

    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        print("Coll in bullet");
    }
}
