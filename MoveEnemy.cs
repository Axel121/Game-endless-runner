using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    Collect collect;
    public GameObject handlerCollect;
    public Rigidbody2D rbO;
    Animator anim;
    Coroutine lastRoutine = null;
    Transform bar;
    public bool toggle = false;
    public float horizvel = 0;
    public float force = 10;
    public bool block;
    public bool canMove;
    float destinationy;
    float clickTmp;
    bool movingUp;
    public string character;

    private void Awake()
    { 
        collect = handlerCollect.GetComponent<Collect>();
        GameData.Instance.speedOpponent = 1;
        anim = GetComponent<Animator>();
        canMove = true; 
        rbO = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        character = PlayerPrefs.GetString("currentCharacter");
        GameData.Instance.speedOpponent = GameData.Instance.speedOpponentBasic;
        bar = GameObject.Find("Character"+character+"(Clone)").transform;   
    }

    private void FixedUpdate()
    {
        rbO.velocity = new Vector2(-GameData.Instance.speedOpponent, horizvel);
        transform.position = new Vector2(rbO.position.x, bar.position.y);        
    }

    public void SpeedUp()
    {   
        GameData.Instance.speedOpponent = GameData.Instance.speedOpponentBasic *3;  
    }

    public void SpeedDown()
    {
        GameData.Instance.speedOpponent = GameData.Instance.speedOpponent / 3;     
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "block")
        {
            SoundManager.Instance.PlayClip(SoundManager.Instance.EnemyToObject);
            if (lastRoutine != null)
            {
                StopCoroutine(lastRoutine);
            }
                lastRoutine = StartCoroutine(ReduceSpeed());       
        }
    }

    public void Left()
    {
        if (lastRoutine != null)
        {
            StopCoroutine(lastRoutine);
        }
        lastRoutine = StartCoroutine(LeftSpeed()); 
    }

    IEnumerator ReduceSpeed()
    {
        GameData.Instance.speedOpponent = -7.5f;
        yield return new WaitForSecondsRealtime(0.75f);
        if (collect.clicked == true)
            GameData.Instance.speedOpponent = GameData.Instance.speedOpponentBasic * 3;
        else
        {
            GameData.Instance.speedOpponent = GameData.Instance.speedOpponentBasic;
        }
    }
    public IEnumerator LeftSpeed()
    {
        SoundManager.Instance.PlayClip(SoundManager.Instance.PlayerToObject);
        GameData.Instance.speedOpponent = 15f;
        yield return new WaitForSecondsRealtime(0.25f);
        GameData.Instance.speedOpponent = GameData.Instance.speedOpponentBasic;      
    }
    public void isFreeze()
    {
        if (!toggle)
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
