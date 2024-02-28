using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightPiece : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(0f, 1f)] public float defaultAlphaValue;
    [Range(0f, 1f)] public float highlightAlphaValue;

    private Color pieceColor;

    private void Awake()
    {
        Debug.Log("awake");
        pieceColor = GetComponent<Image>().color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
        pieceColor.a = highlightAlphaValue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pieceColor.a = defaultAlphaValue;
    }
}
