using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour {

   
    public int baseCapacity =10;
    public int baseSpeedCollecting = 1;
    public Text Tx_capaity;
    public Text Tx_money;
    public GameObject handlerSpawn;
    public GameObject handlerMovePlayer;
    public GameObject handlerMoveEnemy;
    public GameObject handlerMoveObject;
    Spawn spawn;
    MoovePlayer moveplayer;
    MoveEnemy moveenemy;
    MoveObject moveobject;
    public bool clicked;
    public bool canclick;
    public bool stopcollecting = false;
    public bool toggle = true;
    bool collecting;

    private void Awake()
    {          
        spawn = handlerSpawn.GetComponent<Spawn>();
        moveplayer = handlerMovePlayer.GetComponent<MoovePlayer>();
        moveenemy = handlerMoveEnemy.GetComponent<MoveEnemy>();
        moveobject = handlerMoveObject.GetComponent<MoveObject>();
        toggle = true; 
        Tx_money.text = "" + GameData.Instance.currentScore;
        Tx_capaity.text = " " + GameData.Instance.capacity;
        canclick = true;
    }
    private void Start()
    {      
        StartCoroutine(FadeEffect());
    }

    IEnumerator FadeEffect()
    {
        yield return StartCoroutine(Fade.Instance.FadeIn());
        yield return new WaitForSeconds(2);
    }
    public void collect()
    {
        if (canclick)
        {
            if (toggle)
            {
                clicked = true;
                if (GameData.Instance.currentScore < GameData.Instance.capacity)
                {
                    LevelManager.Instance.EnableCollectingParticle(true);
                }
                LevelManager.Instance.SpeedUp();
                toggle = !toggle;   
                LevelManager.Instance.FreezePlayerAndEnemyPositon();
                spawn.freze();
                moveenemy.block = false;
                collecting = true;
                InvokeRepeating("Collecting", 0, GameData.Instance.collectSpeed);
            }

            else 
            {
                clicked = false;
                LevelManager.Instance.EnableCollectingParticle(false);
                toggle = !toggle; ;
                LevelManager.Instance.UnFreezePlayerAndEnemyPositon();
                LevelManager.Instance.SpeedDown();
                spawn.unfreze();
                collecting = false;
                CancelInvoke("Collecting");
                PlayerPrefs.SetFloat("CurrentScore", GameData.Instance.currentScore);
            }
        }
    }

    public void StopCollecting()
    {
        if (stopcollecting == false)
        {
            CancelInvoke("Collecting");
            if (clicked == false)
            {
                LevelManager.Instance.SpeedUp();
            }
            LevelManager.Instance.EnableCollectingParticle(false);
        }
        stopcollecting = true;
        PlayerPrefs.SetInt("CurrentScore", GameData.Instance.currentScore);
    }

    public void Collecting()
    { 
        if (GameData.Instance.currentScore < GameData.Instance.capacity)
        {     
            GameData.Instance.currentScore +=  1;
           Tx_money.text = "" + GameData.Instance.currentScore;         
        }

        if (GameData.Instance.currentScore >= GameData.Instance.capacity)
        {
            LevelManager.Instance.EnableCollectingParticle(false);
        }
    }
}
