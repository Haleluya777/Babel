using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Deck_Controller : MonoBehaviour
{
    List<GameObject> cd_hand = new List<GameObject>(); //손에 가지고 있는 카드 리스트
    public static List<GameObject> deck = new List<GameObject>(); //덱 리스트
    public GameObject card_Pos; //카드의 위치를 지정할 빈 오브젝트
    public int size = 5; //손에 가지고 있는 카드의 개수
    public int deck_count = 30; //덱이 가지고 있는 초기 카드 개수

    public void Awake()
    {
        Start_Setting();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) //디버깅용 함수. 없어도 무관
        {
            for (int i = 0; i < 25; i++)
            {
                Debug.Log(deck[i].name);
            }
        }
    }

    public void Start_Setting()//씬 시작시 실행할 함수, 카드를 뽑는 함수인 Draw함수를 5번 실행한다.
    {
        for(int i = 0; i < 5; i++)
        {
            Draw();
        }
    }   

    public void Draw() //덱에서 카드를 뽑는 함수
    {
        int num = Random.Range(0,deck_count); //랜덤으로 리스트 위치를 지정할 int형 변수 deck_count를 쓰는 이유는 리스트 범위 밖을 벗어나면 안되기 때문
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

        GameObject rand_card = Instantiate(deck[num].gameObject); //카드 소환
        deck.Remove(deck[num]); //뽑은 카드는 덱에서 삭제
        rand_card.transform.SetParent(card_Pos.transform.GetChild(chid_num));
        rand_card.transform.position = card_Pos.transform.GetChild(chid_num).position;
    }

    public void Button_Draw() //카드뽑기 버튼을 누르면 실행
    {
        if(size < 7)
        {
            size++;
            Draw();
        }
    }
}
