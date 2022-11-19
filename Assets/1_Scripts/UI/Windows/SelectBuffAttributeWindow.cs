using Cards;
using Core;
using EffectSystem;
using System.Collections.Generic;
using UI.Components;
using UnityEngine;

namespace UI
{
    public class SelectBuffAttributeWindow : Window
    {
        [SerializeField] private SelectedAttribute[] attributesObjects;
        [SerializeField] private Sprite diplomaticIcon, medicineIcon, economicIcon, rawMaterialsIcon;
        [SerializeField] private GameObject parentCards;

        private List<CardPresidentUI> cards = new List<CardPresidentUI>();

        protected override void AfterInitialization()
        {
            if(attributesObjects.Length != 4)
            {
                BoxController.GetController<LogController>().LogError("Count SelectedAttributes != 4");
            }
            else
            {
                attributesObjects[0].Init(diplomaticIcon, TypeAttribute.Diplomacy);
                attributesObjects[1].Init(medicineIcon, TypeAttribute.Medicine);
                attributesObjects[2].Init(economicIcon, TypeAttribute.Economic);
                attributesObjects[3].Init(rawMaterialsIcon, TypeAttribute.RawMaterials);
            }


        }


    }
}