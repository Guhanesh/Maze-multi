using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowboy : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float jumpHeight = 5f;

    public Transform groundCheckTransform;
    public float groundCheckRadius = 0.2f;

    public Transform targetTransform;
    public LayerMask mouseAimMask;
    public LayerMask groundMask;

    public GameObject bulletPrefab;
    public Transform muzzleTransform;

    public AnimationCurve recoilCurve;
    public float recoilDuration = 0.25f;
    public float recoilMaxRotation = 45f;
    public Transform rightLowerArm;
    public Transform rightHand;

    private float inputMovementX, inputMovementZ;
    private Animator animator;
    private Rigidbody rbody;
    private bool isGrounded;
    private Camera mainCamera;
    private float recoilTimer;
    private Vector3 lastDir,lastVec;

    private int FacingSign
    {
        get
        {
            Vector3 perp = Vector3.Cross(transform.forward, Vector3.forward);
            float dir = Vector3.Dot(perp, transform.up);
            return dir > 0f ? -1 : dir < 0f ? 1 : 0;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        GameManager.EnemySpawned += EnemySpawned;
    }

    private void EnemySpawned(Transform enemyTrans)
    {
        targetTransform = enemyTrans;
    }


    void Update()
    {
        inputMovementX = Input.GetAxisRaw("Horizontal");
        inputMovementZ = Input.GetAxisRaw("Vertical");

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
        {
            targetTransform.position = hit.point;
        }

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //  rbody.velocity = new Vector3(rbody.velocity.x, 0, 0);
        // rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -1 * Physics.gravity.y), ForceMode.VelocityChange);
        //}

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        recoilTimer = Time.time;

        // var go = Instantiate(bulletPrefab);
        //go.transform.position = muzzleTransform.position;
        //var bullet = go.GetComponent<Bullet>();
        var bullet = ObjectPoolingScript.SharedInstance.GetPooledObject();
        print(bullet.transform.position);
        bullet.Fire(muzzleTransform.position, muzzleTransform.eulerAngles, gameObject.layer, transform.forward);
        bullet.gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        // Recoil Animation
        if (recoilTimer < 0)
        {
            return;
        }

        float curveTime = (Time.time - recoilTimer) / recoilDuration;
        if (curveTime > 1f)
        {
            recoilTimer = -1;
        }
        else
        {
            rightLowerArm.Rotate(Vector3.forward, recoilCurve.Evaluate(curveTime) * recoilMaxRotation, Space.Self);
        }


    }

    private void FixedUpdate()
    {
        // Movement
        rbody.velocity = new Vector3(inputMovementX * walkSpeed, 0, inputMovementZ * walkSpeed);
        animator.SetFloat("speed", (FacingSign * rbody.velocity.magnitude) / walkSpeed);

        // Facing Rotation
        Vector3 moveRot=new Vector3(0,0,0);
        if (inputMovementX==1)
        {
            moveRot=new Vector3(0,90,0);
        }
        else if (inputMovementX==-1)
        {
            moveRot=new Vector3(0,-90,0);
        }
        else if (inputMovementZ==1)
        {
             moveRot=new Vector3(0,0,0);
        }
         else if (inputMovementZ==-1)
        {
             moveRot=new Vector3(0,180,0);
        }
        else
        {
            moveRot=lastVec;
        }
        lastVec=moveRot;
        
         rbody.MoveRotation(Quaternion.Euler(moveRot));
       // rbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(targetTransform.position.x - transform.position.x), 0)));
       //rbody.MoveRotation(Quaternion.Euler(new Vector3(0, 0 , 0)));
       // rbody.MoveRotation(Quaternion.Euler(new Vector3(0, 0f, 0)));

        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnAnimatorIK()
    {
//        print(rbody.velocity+" Vel");
        // Weapon Aim at Target IK
         Vector3 targetPos;
if ((Mathf.Abs(inputMovementX)==1)||(Mathf.Abs(inputMovementZ)==1))
{
      targetPos= rbody.velocity*8f;
      lastDir=targetPos;
       print(targetPos+ " 1 "+rbody.velocity);
}
else
{
    
    targetPos=lastDir;
     print(targetPos+ " 2 "+rbody.velocity);
}
          animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, targetPos);
       
        // Look at target IK
        animator.SetLookAtWeight(1);
      
        animator.SetLookAtPosition(targetPos);

        // animator.SetBoneLocalRotation(HumanBodyBones.Spine,new Quaternion(30,0,0,0));
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag=="Bullet")
        {
            print("GameOver");
        }
        
    }

}
