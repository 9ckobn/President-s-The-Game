using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;

public class Factor_Slot : MonoBehaviour, IDropHandler 
{
    public bool _targetDragIsTrue;
    public Transform _selectPresident = null; 

    public void OnDrop(PointerEventData eventData) // при перетаскивании Фактора
    {
        _targetDragIsTrue = true;
    } 
}