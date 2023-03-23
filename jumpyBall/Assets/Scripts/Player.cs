using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float currentTime;
    private bool smash, invincible;

    private int brokenStacks, totalStacks;

    public enum PlayerState {
        Prepare,
        Playing,
        Died,
        Finish
    }
    [HideInInspector]
    public PlayerState playerState = PlayerState.Prepare;

    public AudioClip bounceSFX, deadSFX, winSFX, destroySFX, invincibleSFX;

    public GameObject invincibleG;
    public Image invincibleF;
    public GameObject fireFx;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        brokenStacks = 0;

    }
     void Start()
    {
        totalStacks = FindObjectsOfType<StackController>().Length;
    }
    // Update is called once per frame
    void Update()
    {
        #region Playing
        if (playerState == PlayerState.Playing)

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
                if (!fireFx.activeInHierarchy)
                    fireFx.SetActive(true);
        }
        else
        {
                if (fireFx.activeInHierarchy)
                    fireFx.SetActive(false);
            if (smash)
                currentTime += Time.deltaTime * .8f;
            else
                currentTime -= Time.deltaTime * .5f;
        }

            if (currentTime >= 0.15f || invincibleF.color == Color.red)
                invincibleG.SetActive(true);
            else
                invincibleG.SetActive(false);
        if(currentTime >= 1)
        {
            currentTime = 1;
            invincible = true;
                invincibleF.color = Color.red;
        }
        else if(currentTime <= 0)
        {
            currentTime = 0;
            invincible = false;
                invincibleF.color = Color.white;
        }
            if (invincibleG.activeInHierarchy)
                invincibleF.fillAmount = currentTime / 1;
            #endregion


        }
        #region Prepare
        if (playerState == PlayerState.Prepare) {

            if (Input.GetMouseButtonDown(0))
                playerState = PlayerState.Playing;
        }
        #endregion

        #region Finish
        if (playerState == PlayerState.Finish)
        {

            if (Input.GetMouseButtonDown(0))
                FindAnyObjectByType<Spawner>().NextLevel();
        }
        #endregion


    }
    public void IncreaseBrokenStacks() {
        brokenStacks++;
        if(!invincible)
        {
            ScoreManager.instance.AddScore(1);
            SFXManager.instance.PlaySFX(destroySFX, 0.6f);


        }
        else
        {
            ScoreManager.instance.AddScore(2);
            SFXManager.instance.PlaySFX(invincibleSFX, 0.6f);

        }

    }
    private void FixedUpdate()
    {if(playerState==PlayerState.Playing)
        {
            if (Input.GetMouseButton(0))
            {

                smash = true;
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }

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
            SFXManager.instance.PlaySFX(bounceSFX, 0.6f);
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
                    ScoreManager.instance.ResetScore();
                    SFXManager.instance.PlaySFX(deadSFX, 0.6f);
                }
            }

        }

        FindObjectOfType<GameUi>().LevelSliderF(brokenStacks / (float)totalStacks);

        if(target.gameObject.tag=="Finish" && playerState== PlayerState.Playing)
        {
            playerState = PlayerState.Finish;
            SFXManager.instance.PlaySFX(winSFX, 0.6f);
        }

    }
    private void OnCollisionStay(Collision target)
    {
        if (!smash || target.gameObject.tag =="Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}
