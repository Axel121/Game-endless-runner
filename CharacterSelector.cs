using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour {

    public bool CanSwitch = true;
    List<GameObject> characterBuyList = new List<GameObject>();
    ListCharaters listCharacters;
    public GameObject character;
    public int index ;
    private void OnEnable()
    {
        index = PlayerPrefs.GetInt("lastplay", 0);
        listCharacters = GetComponentInParent<ListCharaters>();
        Functions.AddIndex(0, "indexCharacter");
        int[] indexes = AEDatabase.GetIntArray("indexCharacter");
        characterBuyList.Clear();
        for(int i =0;i<indexes.Length;i++)
        {
            characterBuyList.Add(listCharacters.characters[indexes[i]]);
        }
        characterBuyList[index].SetActive(true);
        character = characterBuyList[index];
        PlayerPrefs.SetInt("lastplay", index);
        PlayerPrefs.SetString("currentCharacter", character.name);
        SetIndex();
    }

    public void Next()
    {
        if (CanSwitch == true)
        {
            CanSwitch = false;
            characterBuyList[index].SetActive(false);
                if (index >= characterBuyList.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;         
                }
            characterBuyList[index].SetActive(true);
            character = characterBuyList[index];
            CanSwitch = true;
            PlayerPrefs.SetInt("lastplay", index);
            PlayerPrefs.SetString("currentCharacter", character.name);
            SetIndex();
        }
    }

    public void Previous()
    {
        if (CanSwitch == true)
        {
            CanSwitch = false;
            characterBuyList[index].SetActive(false);
            if (index == 0)
            {
                index = characterBuyList.Count - 1;
            }
            else
            {
                index--;

            }
            character = characterBuyList[index];
            characterBuyList[index].SetActive(true);
            CanSwitch = true;
            PlayerPrefs.SetInt("lastplay", index);
            PlayerPrefs.SetString("currentCharacter", character.name);
            SetIndex();
        }
    }
    public void SetIndex()
    {
        PlayerPrefs.SetString("currentCharacter", character.name);
    }
}
