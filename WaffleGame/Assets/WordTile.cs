using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordTile : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;

    public string startValue;

    public string currentValue;
    public Text textValue;
    private RectTransform rectTransform;
    Vector2 startPosition;
    Transform wordMatrixTransform;
    public int row;
    public int column;

    CanvasGroup canvasGroup;
    public Image backImage;
    public Sprite correctSprite;
    public Sprite almostCorrectSprite;
    public Sprite wrongSprite;

    public static int correctTileNumber;
    public bool draggable = true;
    private void Awake() {
        correctTileNumber = 0;
        wordMatrixTransform = transform.parent.parent;

        row = transform.parent.GetSiblingIndex();
        column = transform.GetSiblingIndex();
        rectTransform = GetComponent<RectTransform>();

        startPosition = rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetStartValue(string _value)
    {
        this.startValue = _value;
        textValue.text = startValue;
        currentValue = startValue;

        SetColorBaseOnCurrentValue();
    }
    public void SetCurrentValue(string _value)
    {
        textValue.text = _value;
        currentValue = _value;

        SetColorBaseOnCurrentValue();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!draggable) return;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(wordMatrixTransform);
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!draggable) return;
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        SetToStartPos();
    }

    public void SetToStartPos()
    {
        transform.SetParent(wordMatrixTransform.GetChild(row));
        transform.SetSiblingIndex(column);

        rectTransform.anchoredPosition = startPosition;
        canvasGroup.blocksRaycasts = true;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void SwapValue(WordTile other)
    {
        string tmp = other.currentValue;
        other.SetCurrentValue(this.currentValue);
        this.SetCurrentValue(tmp);

    }

    private void SetColorBaseOnCurrentValue()
    {
        if (currentValue == startValue)
        {
            backImage.sprite = correctSprite;
            draggable = false;
            correctTileNumber++;
            if (correctTileNumber == 21) {
                GameController.instance.WinGame();
            }
            return;
        }

        draggable = true;
        for (int i = 0; i < 5; i++)
        {
            WordTile wordInColumn = wordMatrixTransform.GetChild(i).GetChild(column).GetComponent<WordTile>();
            if (wordInColumn != null && wordInColumn.startValue == currentValue && column != 1 && column != 3)
            {
                backImage.sprite = almostCorrectSprite;
                return;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            WordTile wordInRow = wordMatrixTransform.GetChild(row).GetChild(i).GetComponent<WordTile>();
            if (wordInRow != null && wordInRow.startValue == currentValue && row != 1 && row != 3)
            {
                backImage.sprite = almostCorrectSprite;
                return;
            }
        }

        backImage.sprite = wrongSprite;
    }

    public void OnDrop(PointerEventData eventData)
    {
        WordTile otherWord = eventData.pointerDrag.GetComponent<WordTile>();
        if (!otherWord.draggable || !this.draggable) return;

        otherWord.SetToStartPos();
        if (!SwapCounter.instance.EnoughSwapToConsume()) return;

        SwapValue(otherWord);
        SwapCounter.instance.ConsumSwap();
        Debug.Log(currentValue + "      " + otherWord.currentValue);
    }
}