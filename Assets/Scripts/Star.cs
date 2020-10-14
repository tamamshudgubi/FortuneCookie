using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Star : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _starNumber;

    public event Action<int> Rated;

    public void OnPointerClick(PointerEventData eventData)
    {
        Rated?.Invoke(_starNumber);
    }
}
