using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        bullet=transform.parent.GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        transform.parent.gameObject.SetActive(false);
    }
}
