using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneControler : MonoBehaviour
{
    //Совпадение карт
    private MemoryCard firstCardOne;
    private MemoryCard secondCardTwo;
    private int scoreMatchedCard = 0;
    public bool canRealeGet
    {
        get { return secondCardTwo == null; }
    }
    //Компоненты для ячеек
    private const int  cellRad = 3;
    private const int cellCols = 6;
    private const float offsetX = 2f;
    private const float offsetY = 2.5f;
    
    //Ссылки и массивы
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private Text textMeshScore;
    
   
    private void Start()
    {
         Vector3 startCardPosition = originalCard.transform.position;
         int[] numbersCard = { 0, 0, 1, 1, 2, 2, 3, 3 ,4,4,5,5,6,6,7,7,8,8};
         numbersCard = AlgTysCardArray(numbersCard);
         for(int i = 0;i < cellCols;i++)
         {
            for(int k = 0;k < cellRad;k++)
            {
                MemoryCard card;
                if(i == 0 && k == 0)
                {
                  card = originalCard;
                }
                else
                {
                  card = Instantiate(originalCard) as MemoryCard; 
                }
                int index = k * cellCols + i;
                int id = numbersCard[index];
                card.SetCard(id, images[id]);
                //Позиция карт 
                float posX = (offsetX * i) + startCardPosition.x;
                float posY = -(offsetY * k) + startCardPosition.y;
                card.transform.position = new Vector3(posX, posY, startCardPosition.y);
            }
         }    
    }
    //Реализация алгоритма тасования Кнута.
    private int[] AlgTysCardArray(int[] numbersCard)
    {
        int[] newArray = numbersCard.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }
    //Открытие карт
    public void CardRevale(MemoryCard card)
    {
        if(firstCardOne == null)
        {
            firstCardOne = card;
        }
        else
        {
            secondCardTwo = card;
            StartCoroutine(CheckMathCard());
        }
    }
    //Сравнение карт и введение счёта
    private IEnumerator CheckMathCard()
    {
        if(firstCardOne.id == secondCardTwo.id)
        {
            scoreMatchedCard++;
            textMeshScore.text = "СЧЁТ " + scoreMatchedCard;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            firstCardOne.OnRevale();//Обложка карты SetActive
            secondCardTwo.OnRevale();
        }
        firstCardOne = null;
        secondCardTwo = null;
    }
}
