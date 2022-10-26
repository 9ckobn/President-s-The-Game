using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class ComponentLineInventory : MonoBehaviour
    {
        //[SerializeField] private ComponentInventoryItem[] componentItems;
        //[SerializeField] private MoveLineInventory moveLine;
        //[SerializeField] private ButtonLineMove rightArrow;
        //[SerializeField] private ButtonLineMove leftArrow;

        //private int counterItem = 0;
        //private int amountItems;

        //public void SetDataItems(List<Item> dataItems, TypeClotheItem typeClotheItem)
        //{
        //    if (componentItems[0].gameObject.activeSelf)
        //    {
        //        foreach (var component in componentItems)
        //        {
        //            component.gameObject.SetActive(false);
        //        }
        //    }

        //    amountItems = dataItems.Count;

        //    for (int i = 0; i < dataItems.Count; i++)
        //    {
        //        componentItems[i].gameObject.SetActive(true);
        //        componentItems[i].SetDataItem(dataItems[i], typeClotheItem);
        //    }

        //    CheckSwitchOnArrow();
        //}

        ///// <summary>
        ///// Передвинуть линию вправо на один элемент, если это возможно
        ///// </summary>
        //public void MoveRightLine()
        //{
        //    if (counterItem + 1 > amountItems) // Если первый элемент, то двигать нельзя
        //    {
        //        return;
        //    }

        //    moveLine.MoveRight();

        //    counterItem++;
        //    CheckSwitchOnArrow();
        //    CheckSwitchOffArrow();
        //}

        ///// <summary>
        ///// Передвинуть линию влево на один элемент, если это возможно
        ///// </summary>
        //public void MoveLeftLine()
        //{
        //    if (counterItem == 0) // Если находимся на предпоследнем элементе, то двигать нельзя
        //    {
        //        return;
        //    }

        //    moveLine.MoveLeft();

        //    counterItem--;
        //    CheckSwitchOnArrow();
        //    CheckSwitchOffArrow();
        //}

        ///// <summary>
        ///// Скрыть линии Items и сообщить о закрытии
        ///// </summary>
        //public void CloseLineInventory()
        //{
        //    foreach (var component in componentItems)
        //    {
        //        component.gameObject.SetActive(false);
        //    }

        //    moveLine.ResetStartPosition();
        //    counterItem = 0;
        //    CheckSwitchOffArrow();
        //}

        //private void CheckSwitchOnArrow()
        //{
        //    if (counterItem > 0)
        //    {
        //        leftArrow.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        leftArrow.gameObject.SetActive(false);
        //    }

        //    if (counterItem + 2 < amountItems)
        //    {
        //        rightArrow.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        rightArrow.gameObject.SetActive(false);
        //    }
        //}

        //private void CheckSwitchOffArrow()
        //{
        //    if (counterItem == 0)
        //    {
        //        leftArrow.gameObject.SetActive(false);
        //    }

        //    if (counterItem + 1 >= amountItems)
        //    {
        //        rightArrow.gameObject.SetActive(false);
        //    }
        //}
    }
}