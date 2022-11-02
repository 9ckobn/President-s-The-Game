using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIWindow : Window
    {
        [SerializeField] private Text currentCharacterText;

        public void SetCurrentCharacterText(string text)
        {
            currentCharacterText.text = text;
        }
    }
}