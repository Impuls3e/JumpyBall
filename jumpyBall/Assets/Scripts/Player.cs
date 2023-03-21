using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float currentTime;
    private bool smash, invincible;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            smash = true;

        }

        if (Input.GetMouseButtonUp(0))
        {

            smash = false;
        }
        if (invincible)
        {
            currentTime -= Time.deltaTime * .35f;
        }
        else
        {
            if (smash)
                currentTime += Time.deltaTime * .8f;
            else
                currentTime -= Time.deltaTime * .5f;
        }

        if(currentTime >= 1)
        {
            currentTime = 1;
            invincible = true;
            print("Done");
        }
        else if(currentTime <= 0)
        {
            currentTime = 0;
            invincible = false;
        }
        print(currentTime);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {

            smash = true;
            rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
        }
        if (rb.velocity.y > 5)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
    }
    private void OnCollisionEnter(Collision target)
    {
        if (!smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        else
        {
            if (invincible)
            {
                if (target.gameObject.tag == "enemy" || target.gameObject.tag == "plane")
                {
                    target.transform.parent.GetComponent<StackController>().ShatterAllParts();
                }
            }
            else
            {

                if (target.gameObject.tag == "enemy")
                {
                    target.transform.parent.GetComponent<StackController>().ShatterAllParts();
                }
                if (target.gameObject.tag == "plane")
                {
                    Debug.Log("Game Over");
                }
            }

        }

    }
    private void OnCollisionStay(Collision target)
    {
        if (!smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}
