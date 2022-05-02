using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card_Controller : MonoBehaviour, IPointerClickHandler
{
    RectTransform trans; //카드 위치를 지정할 컴포넌트, 카드는 UI로 만들었기 때문에 Transform이 아닌 RectTransform사용

    public bool onClick = false; //카드를 선택했는지 확일할때 쓸 Bool 함수
    public void Awake()
    {
        trans = GetComponent<RectTransform>();
    }

    public void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData) //클릭함수
    {
        if(this.onClick == true)
        {
            this.trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, trans.anchoredPosition.y - 34);
            GameManager.card_Ready.Remove(this.gameObject);
            onClick = false;
        }
        else if(this.onClick == false && GameManager.card_Ready.Count < 3)
        {
            this.trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, trans.anchoredPosition.y + 34);
            GameManager.card_Ready.Add(this.gameObject);
            onClick = true;
        }
    }

    //카드 효과발동
    //public void Card_Using()
    //{
    //    string tg = this.tag;
    //    switch (tg)
    //    {
    //        case "Move_Right":
    //            break;
    //
    //        case "Move_Left":
    //            break;
    //
    //        case "Move_Up":
    //            break;
    //
    //        case "Move_Down":
    //            break;
    //    }
    //}
}
