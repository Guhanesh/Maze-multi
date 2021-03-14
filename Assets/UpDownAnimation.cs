using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
 
//adjust this to change speed
[SerializeField]float speed = 5f;
//adjust this to change how high it goes
[SerializeField]float height = 0.5f;
 
void Update() {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);
        //set the object's Y to the new calculated Y
        transform.localPosition = new Vector3(0, newY, 0) * height;
}
}
