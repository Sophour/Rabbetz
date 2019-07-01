using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RabbitController : MonoBehaviour
{
    float hunger;

    public float visionRadius = 10.0f;
    public float visionRadiusSpeed = 0.2f;

    [Header("Status bars")]
    public Image hungerBar;

    public Camera mc;

    private Rigidbody rb;
    private float coolDownTime;
    private float timer;
    public float rabbetStrenght = 100.0f;
    private Vector3 movement;
    private float face;

    private float distToGround;
    private GameObject objectOfInterest;

    private bool canJump;


    SphereCollider fov;

    public const float maxHunger = 20;
    public const float hungerIncrement = 0.01f;
    private float targetAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hunger = maxHunger;
        objectOfInterest = null;
        fov = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpCooldown();
        DecreaseHunger();
        DesideWhatToDo();
    }

    private void DesideWhatToDo()
    {
        if (hunger > maxHunger * 0.6f)
        {
            LookForBae();
            return;
        }
        else if (hunger <= maxHunger * 0.6f && hunger > 0)
        {
            LookForFood( );
            return;
        }
        else if (hunger <= 0)
        {
            Die();
        }
    }

    private void DecreaseHunger()
    {
        hunger = hunger - hungerIncrement;
        hungerBar.fillAmount = hunger / maxHunger;

        if (objectOfInterest)
        {
            return;
        }
        if (hunger <= maxHunger * 0.6f)
        {
            GetComponent<SphereCollider>().radius += visionRadiusSpeed;
            if (GetComponent<SphereCollider>().radius > visionRadius)
            {
                GetComponent<SphereCollider>().radius = 0.0f;
            }
        }
        else
        {
            GetComponent<SphereCollider>().radius = 0.0f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectOfInterest || other.gameObject.name == "ground")
        {
            return;
        }
        if (hunger > maxHunger * 0.6f)
        {
            return;
        }
        if (other.gameObject.name == "Aloe(Clone)")
        {
            objectOfInterest = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other != objectOfInterest)
        {
            return;
        }

        if (!objectOfInterest)
        {
            return;
        }

        objectOfInterest = null;
    }

    private void JumpCooldown()
    {
        bool isGround;

        if (objectOfInterest)
        {
            coolDownTime = 0.3f;
        }
        else
        {
            coolDownTime = 2.0f;
        }

        transform.rotation = Quaternion.EulerRotation(0, 0, 0);

        isGround = IsGrounded();
        if (isGround)
        {
            timer += Time.deltaTime;
            if (timer >= coolDownTime || coolDownTime == 0.0f)
            {
                canJump = true;
                timer = 0;
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);
        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MoveSomewhere( GameObject target )
    {
        MoveToTarget( target);
    }

    private void MoveToTarget( GameObject target)
    {
        float jumpVPowery;

        if (!canJump)
        {
            return;
        }
        jumpVPowery = 1f;

        Vector2 way = new Vector2( target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z);
        way.Normalize();
        movement = new Vector3(way.x, jumpVPowery, way.y);
        movement.Normalize();
        rb.AddForce(movement * rabbetStrenght);
        canJump = false;
    }

    private void MoveSomewhere()
    {
        TakeAStep();
    }
    
 
    private void TakeAStep()
    {
        float jumpVPowery;
        float jumpHPowerx;
        float jumpHPowerz;

        if(!canJump)
        {
            return;
        }

        jumpHPowerx = UnityEngine.Random.Range(-1f, 1f);
        jumpHPowerz = UnityEngine.Random.Range(-1f, 1f);
        jumpVPowery = 1f;
        
        Vector2 way = new Vector2(jumpHPowerx, jumpHPowerz);
        way.Normalize();
        movement = new Vector3(way.x, jumpVPowery, way.y);
        movement.Normalize();
        rb.AddForce(movement * rabbetStrenght);
        canJump = false;
    }

    private void LookForBae()
    {
        MoveSomewhere();
    }

    private void LookForFood()
    {
        if (objectOfInterest)
        {
            MoveSomewhere(objectOfInterest);
            Feed(objectOfInterest);
        }
        else
        {
            MoveSomewhere();
        }
    }

    private void Feed(GameObject objectOfInterest)
    {
        if (Vector3.Distance(objectOfInterest.transform.position, transform.position) < 2.2f)
        {
            Destroy(objectOfInterest);
            hunger = maxHunger;
        }
    }

    private void Die()
    {
        // Animation of death
        Destroy(gameObject);
    }
}
