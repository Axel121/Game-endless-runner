using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

   
    public int index;
    public MoveEnemy moveEnemy;
    public ColiderDestroy coliderDestroy;
    public bool CanMove = false;
    public float globalDistance = 0;
    float startCounter = 0;
    float counter = 1;
    MoovePlayer moovePlayer;
    public string character;
    public List<GameObject> blocks = new List<GameObject>();
    public List<GameObject> blocksActive = new List<GameObject>();
    private static LevelManager _Instance;
    public static LevelManager Instance

    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<LevelManager>();
            }
            return _Instance;
        }
    }
    private void Start()
    {
        character = PlayerPrefs.GetString("currentCharacter", "0");
        GameObject background = Instantiate(Resources.Load<GameObject>("Prefab/" + GameData.Instance.currentBackground)) as GameObject;
        GameObject player =  Instantiate(Resources.Load<GameObject>("Prefab/Character" + character)) as GameObject;
        moovePlayer =  player.GetComponent<MoovePlayer>() ;
        GameData.Instance.currentDistance = 0;
        InvokeRepeating("CounterDistance", startCounter, counter);   
    }
    public void CounterDistance()
    {
        GameData.Instance.currentDistance++;
    }

    public void EnableCollectingParticle(bool enable)
    {
        moovePlayer.EnableParticle(enable);
    }

    public void SpeedUp()
    {
        moveEnemy.SpeedUp();
    }

    public void SpeedDown()
    {
        moveEnemy.SpeedDown();
    }

    public void MoveEyeLeft()
    {
        moveEnemy.Left();
    }

    public void MovePlayerDown()
    {
        moovePlayer.Down();
    }

    public void MovePlayerUp()
    {
        moovePlayer.Up();
    }
    public void FreezePlayerAndEnemyPositon()
    {
        moovePlayer.canMove = false;
        moovePlayer.rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
        moveEnemy.rbO.constraints = RigidbodyConstraints2D.FreezePositionY;
        moveEnemy.rbO.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void UnFreezePlayerAndEnemyPositon()
    {
        moovePlayer.canMove = true;
        moovePlayer.rigid.constraints = RigidbodyConstraints2D.None;
        moovePlayer.rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        moveEnemy.rbO.constraints = RigidbodyConstraints2D.None;
        moveEnemy.rbO.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
