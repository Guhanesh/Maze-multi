using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
   [SerializeField] private GameObject bullet;
   [SerializeField] private LayerMask playerLayer;
   private float recoilTimer=-1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if  (recoilTimer==0)
        {
RaycastHit hit;
      
        if (Physics.Raycast(transform.position, transform.forward, out hit,10f,playerLayer))
        {
              var bullet =ObjectPoolingScript.SharedInstance.GetPooledObject();
        print( bullet.transform.position);
        bullet.Fire( transform.position, transform.eulerAngles, gameObject.layer,transform.forward);
        bullet.gameObject.SetActive(true);
        recoilTimer=-1;
          
        }
        
                if (Physics.Raycast(transform.position, -transform.forward, out hit,10f,playerLayer))
        {
               var bullet =ObjectPoolingScript.SharedInstance.GetPooledObject();
        print( bullet.transform.position);
        bullet.Fire( transform.position, transform.eulerAngles, gameObject.layer,-transform.forward);
        bullet.gameObject.SetActive(true);
          recoilTimer=-1;
        }

         if (Physics.Raycast(transform.position, transform.right, out hit,10f,playerLayer))
        {
            var bullet =ObjectPoolingScript.SharedInstance.GetPooledObject();
        print( bullet.transform.position);
        bullet.Fire( transform.position, transform.eulerAngles, gameObject.layer, transform.right);
        bullet.gameObject.SetActive(true);
          recoilTimer=-1;
        }

         if (Physics.Raycast(transform.position, -transform.right, out hit,10f,playerLayer))
        {
        var bullet =ObjectPoolingScript.SharedInstance.GetPooledObject();
        print( bullet.transform.position);
        bullet.Fire( transform.position, transform.eulerAngles, gameObject.layer, -transform.right);
        bullet.gameObject.SetActive(true);
         recoilTimer=-1;
        }
        
        }
          
        if  (recoilTimer<0)
        {
        recoilTimer+=Time.deltaTime;
        }
        else
        {
            recoilTimer=0;
        }
    }
}
