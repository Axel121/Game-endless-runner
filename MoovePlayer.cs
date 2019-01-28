using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoovePlayer : MonoBehaviour
{
    Animator anim;
    public Rigidbody2D rigid;
    public GameObject paricl;
    MoveEnemy enemy;
    public int toggle = 0;
    public float horizvel = 0;
    public float y;
    public int x = 0;
    public float playerSpeed = 10;
    public bool canMove;
    float destinationy;
    float clickTmp;
    bool movingUp;

    void Awake()
    {
        enemy = FindObjectOfType<MoveEnemy>();
        canMove = true;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        paricl.SetActive(false);
    }

    public void EnableParticle(bool enable)
    {
        paricl.SetActive(enable);
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(0, horizvel);
    }
    private void Update()
    {
         if (canMove == true)
        {
            if (Input.GetKey(KeyCode.S))
            {
                Up();           
            }
        }

        if (canMove == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Down();            
            }
        }
    }
       
    public void Down()
    {
        if (canMove == true)
        {
            canMove = false;
            clickTmp = transform.position.y;
            if (Mathf.Approximately(clickTmp, -10.5f)) 
            {
                destinationy = -4.5f; 
            }
            else if (Mathf.Approximately(clickTmp, -4.5f)) 
            {
                destinationy = 2.5f; 
            }
            horizvel = 10;
            movingUp = true;
            StartCoroutine(stopSlide());          
        }
    }

    public void Up()
    {
        if (canMove == true)
        {
            canMove = false;
            clickTmp = transform.position.y;
            if (Mathf.Approximately(clickTmp, 2.5f))
            {
                destinationy = -4.5f;
            }
            else if (Mathf.Approximately(clickTmp, -4.5f))
            {
                destinationy = -10.5f;
            }
            horizvel = -10;
            movingUp = false;
            StartCoroutine(stopSlide());
        }
    }

    IEnumerator stopSlide()
    {
        while (movingUp && transform.position.y <= destinationy || !movingUp && transform.position.y >= destinationy )
        {
           yield return null;
        }
        horizvel = 0;
        Vector3 pos = transform.position;
        pos.y = destinationy;
        transform.position = pos;
        canMove = true;
    }
    public void isFreeze()
    {
        if (toggle == 0)
        {
            toggle = 1;
            canMove = false;
        }
        else
        {
            toggle = 0;
            canMove = true;
        }
    }
}
