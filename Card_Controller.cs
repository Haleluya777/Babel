using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card_Controller : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static Vector2 DefaultPos;
    public static Card_Deck_Controller deck;
    public GameObject Deck_Position;
    RectTransform trans;
    public void Awake()
    {
        deck = GameObject.Find("Deck_Pos").GetComponent<Card_Deck_Controller>();
        trans = GetComponent<RectTransform>();
        Deck_Position = GameObject.Find("Deck_Pos");
    }

    public void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, trans.anchoredPosition.y + 34);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, trans.anchoredPosition.y - 34);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        DefaultPos = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = Input.mousePosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        this.transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (Input.mousePosition.y > 300)
        {
            //Card_Using();
            this.gameObject.SetActive(false);
            deck.size--;
            this.transform.parent.gameObject.SetActive(false);
            this.transform.SetParent(Deck_Position.transform);
            this.transform.position = Deck_Position.transform.position;
        }
        else
        {
            this.transform.position = DefaultPos;
            transform.rotation = Quaternion.Euler(0, 0, transform.parent.eulerAngles.z);
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
