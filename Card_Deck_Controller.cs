using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Deck_Controller : MonoBehaviour
{
    List<GameObject> cd_hand = new List<GameObject>();
    RectTransform rect;
    public GameObject[] deck = new GameObject[30];
    public GameObject card_Pos;
    public int size = 5;
    public int deck_count = 30;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        DeckSetting();
        Start_Setting();
    }

    // Update is called once per frame
    void Update()
    {
        Card_Pos_Rotate();
    }

    public void Start_Setting()
    {
        for(int i = 0; i < 5; i++)
        {
            Draw();
        }
    }   

    public void Draw()
    {
        int num = Random.Range(0,deck_count);
        int chid_num = 0;
        deck_count--;
        while(true)
        {
            if (card_Pos.transform.GetChild(chid_num).gameObject.activeSelf == false)
            {
                card_Pos.transform.GetChild(chid_num).gameObject.SetActive(true);
                break;
            }
            else chid_num++;   
        }

        GameObject rand_card = this.transform.GetChild(num).gameObject;
        rand_card.transform.SetParent(card_Pos.transform.GetChild(chid_num));
        rand_card.transform.position = card_Pos.transform.GetChild(chid_num).transform.GetChild(0).position;
        rand_card.transform.rotation = card_Pos.transform.GetChild(chid_num).rotation;
        rand_card.gameObject.SetActive(true);
    }

    public void Button_Draw()
    {
        if(size < 7)
        {
            size++;
            Draw();
        }
    }

    public void DeckSetting()
    {
        for (int i = 0; i < 30; i++)
        {
            //deck[i] = Resources.Load<GameObject>("Prefabs/Card");
            Instantiate(deck[i], this.transform);
            deck[i].gameObject.SetActive(false);
            deck[i].transform.position = this.transform.position;
        }
    }

    public void Card_Pos_Rotate()
    {
        float first_num = (11.25f * size - 1) + 12.25f;
        for(int i = 0; i < 7; i++)
        {
            if(card_Pos.transform.GetChild(i).gameObject.activeSelf == true)
            {
                card_Pos.transform.GetChild(i).rotation = Quaternion.Euler(0, 0, first_num -= 22.5f);
            }
        }
    }
}
