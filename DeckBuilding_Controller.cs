using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DeckBuilding_Controller : MonoBehaviour
{
    public List<GameObject> card_Overlap = new List<GameObject>(); //카드의 종류를 중복없이 순서대로 저장할 리스트
    public GameObject[] card_Obj = new GameObject[8]; //프리팹에 저장된 카드를 저장할 배열
    public Text[] card_Count = new Text[8]; //남아있는 카드의 개수를 알려줄 텍스트 8개
    public Button[] card_Input = new Button[8]; //사용가능한 카드의 버튼을 저장할 배열(카드 종류가 8가지이므로 크기는 8)

    private void Awake()
    {
        //플레이어가 카드 종류당 사용가능한 카드 개수를 5장으로 설정
        for (int i = 0; i < PlayerController.having_Card.Length; i++)
        {
            PlayerController.having_Card[i] = 5;
        }
    }

    private void FixedUpdate()
    {
        Card_Count();
        Deck_Check();
    }

    public void Card_Count() //남아있는 카드 장수를 받아와 텍스트를 변경
    {
        for (int i = 0; i < PlayerController.having_Card.Length; i++)
        {
            card_Count[i].text = PlayerController.having_Card[i].ToString();
        }
    }

    public void Card_Input() //덱에 카드를 저장하는 함수
    {
        for(int i = 0; i < PlayerController.having_Card.Length; i++)
        {
            if(card_Input[i].tag == EventSystem.current.currentSelectedGameObject.tag && PlayerController.having_Card[i] > 0 && Card_Deck_Controller.deck.Count < 30)
            {
                Card_Deck_Controller.deck.Add(card_Obj[i]);
                if(card_Overlap.Count == 0)
                {
                    card_Overlap.Add(card_Obj[i]);
                }
                else if(card_Overlap.Count > 0)
                {
                    if(card_Overlap.Contains(card_Obj[i]) == false)
                    {
                        card_Overlap.Add(card_Obj[i]);
                    }
                }
                PlayerController.having_Card[i]--;
            }
        }
    }

    public void Deck_Output() //덱에 있는 카드를 빼는 함수
    {
        for(int i = 0; i < 8; i++)
        {
            if(EventSystem.current.currentSelectedGameObject.tag == card_Obj[i].tag)
            {
                Card_Deck_Controller.deck.Remove(card_Obj[i]);
                if (Card_Deck_Controller.deck.Contains(card_Obj[i]) == false)
                {
                    GameObject.Find("Deck_List").transform.GetChild(0).transform.GetChild(card_Overlap.Count).gameObject.SetActive(false);
                    card_Overlap.Remove(card_Obj[i]);
                }
                PlayerController.having_Card[i]++;
            }
        }
    }

    public void Deck_Check() //덱에 있는 카드를 체크하고 인게임에 나타내는 함수
    {
        for(int i = 1; i <= card_Overlap.Count; i++)
        {
            GameObject obj = GameObject.Find("Deck_List").transform.GetChild(0).transform.GetChild(i).gameObject;
            Image cd_img = card_Overlap[i-1].GetComponent<Image>();
            Image obj_img = obj.GetComponent<Image>();
            Text obj_txt = obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();

            obj.SetActive(true);
            obj.tag = card_Overlap[i - 1].tag;
            obj_img.color = cd_img.color;

            for(int j = 0; j < 8; j++)
            {
                if(obj.tag == card_Obj[j].tag)
                {
                    obj_txt.text = (5 - PlayerController.having_Card[j]).ToString();
                }
            }
        }
    }

    public void StartScene() //별거없는 함수
    {
        if (Card_Deck_Controller.deck.Count < 30)
            Debug.Log("카드가 부족합니다.");
        else
            SceneManager.LoadScene("GamePlay_Scene");
    }
}
