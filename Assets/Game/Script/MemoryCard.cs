using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private Sprite image;
    [SerializeField] private SceneControler sc;
    private int _id;
    public int id
    {
        get { return _id; }
    }

   

    private void OnMouseDown()
    {
        if(cardBack.activeSelf && sc.canRealeGet)
        {
            cardBack.SetActive(false);
            sc.CardRevale(this);
        }
    }
    public void SetCard(int id,Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnRevale()
    {
        cardBack.SetActive(true);
    }
}
