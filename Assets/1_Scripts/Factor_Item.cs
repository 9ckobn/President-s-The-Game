using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using System.IO;
using UnityEngine.UI;

public class Factor_Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private CanvasGroup _canvasGroup;
    private Canvas _mainCanvas;
    private RectTransform _rectTransform;
    private Transform _parent; // изначальный родитель карточки 
    public Transform _factorFolder; // папка с факторами 

    public JSONController jSONController;
    JSONController.ItemList myItemList;
    public string _name = "";
    public int _level = 0;
    public string _climate = ""; // климат президента 
    public int _buff_diplomation = 0;
    public int _buff_diplomation_delta = 0;
    public int _buff_fortune = 0;
    public int _buff_fortune_delta = 0;
    public int _buff_protection = 0;
    public int _buff_protection_delta = 0;
    public int _buff_attack = 0;
    private int _BUFF_Initial = 10; // Первоначально все атрибуты равны 10 
    public int _buff_attack_delta = 0;

    private Transform[] childrenFactors;
    private bool _materialOK = false;
    private bool _economicOK = false;
    private bool _healthOK = false;
    private bool _foodOK = false; 

    private Transform[] _presidentsArray = new Transform[3];
    private Transform[] _placePresidents = new Transform[3];

    public string _factor_materials = "";
    public int _materials_ability_protect = 0;
    public string _factor_economic = "";
    public int _economic_ability_protect = 0;
    public int _economic_ability_attack = 0;
    public string _factor_health = "";
    public int _health_ability_protect = 0;
    public string _factor_food = "";
    public int _food_ability_protect = 0;
    public int _food_ability_attack = 0;

    bool _triggerFactorOnDrop = false;

    public string _enterFactor = ""; // фактор, который оставляет игрок на президенте 

    private int counter_player = 0; // передаём номер президента из массива презадентов (j в цикле) 
    public int _BUFFmaterials;
    public int _BUFFeconomic;
    public int _BUFFhealth;
    public int _BUFFfood;

    private GameObject _prohibitMaterials;
    private GameObject _prohibitEconomic;
    private GameObject _prohibitHealth;
    private GameObject _prohibitFood;

    public Image[] ImageBuf = new Image[3];

    Transform _selectFactor = null;

    private void Start() // Старт игры, отображаем изначальные параметры карт президентов
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>(); // канвас групп у карточки каждого президента 
        _parent = transform.parent; // это родитель карточки, Place1,2,3 
        _mainCanvas = transform.parent.GetComponentInParent<Canvas>(); // канвас 

        childrenFactors = new Transform[_factorFolder.childCount]; // факторы, на которые кидаем президентов 

        for (int i = 0; i < childrenFactors.Length; i++) // забиваем массив childrenFactors факторами сцены. Можно было использовать foreach
        {
            childrenFactors[i] = _factorFolder.GetChild(i).transform;
        }

        _placePresidents[0] = _mainCanvas.transform.Find("PlacePresident1"); // пока так засунем 
        _presidentsArray[0] = _placePresidents[0].GetChild(0).transform;

        _placePresidents[1] = _mainCanvas.transform.Find("PlacePresident2");
        _presidentsArray[1] = _placePresidents[1].GetChild(0).transform;

        _placePresidents[2] = _mainCanvas.transform.Find("PlacePresident3");
        _presidentsArray[2] = _placePresidents[2].GetChild(0).transform;


        // занесли в переменные наши кружкИ 
        _prohibitMaterials = _factorFolder.transform.Find("Materials").Find("Prohibit").gameObject;
        _prohibitEconomic = _factorFolder.transform.Find("Economic").Find("Prohibit").gameObject;
        _prohibitHealth = _factorFolder.transform.Find("Health").Find("Prohibit").gameObject;
        _prohibitFood = _factorFolder.transform.Find("Food").Find("Prohibit").gameObject;

        ProhibitDisable(); // выключили все кружкИ 

        ////////// Обрабатываем JSON и все, что с ним связано 
        myItemList = jSONController.myItemList; // вытягиваем лист со скрипта 
        for (int j = 0; j < myItemList.player.Length; j++) // пробежались по всем президентам из JSON-файла 
        {
            if (name == myItemList.player[j].id) // нашли совпадения обладателя этого скрипта с президентами и JSON
            {
                counter_player = j; // запомнили, в какой ячейке массива нужный player (президент)

                // пишем в переменные всё из JSON 
                _name = myItemList.player[j].name;
                _level = myItemList.player[j].level;
                _climate = myItemList.player[j].climate;

                _buff_diplomation_delta = myItemList.player[j].buff_diplomation;
                _buff_protection_delta = myItemList.player[j].buff_protection;
                _buff_attack_delta = myItemList.player[j].buff_attack;
                _buff_fortune_delta = myItemList.player[j].buff_fortune; 

                _buff_diplomation = _BUFF_Initial + _buff_diplomation_delta;
                _buff_fortune = _BUFF_Initial + _buff_fortune_delta;
                _buff_protection = _BUFF_Initial + _buff_protection_delta;
                _buff_attack = _BUFF_Initial + _buff_attack_delta;
                _factor_materials = myItemList.player[j].factor_materials; // описание фактора
                _factor_economic = myItemList.player[j].factor_economic; // описание фактора
                _factor_health = myItemList.player[j].factor_health; // описание фактора
                _factor_food = myItemList.player[j].factor_food; // описание фактора
                _BUFFmaterials = (_buff_fortune + _buff_attack) / 2;
                _BUFFeconomic = (_buff_attack + _buff_diplomation) / 2;
                _BUFFhealth = (_buff_protection + _buff_diplomation) / 2;
                _BUFFfood = (_buff_protection + _buff_fortune) / 2;
            }
        }
        CalcClimate(); // расчет воздействия климата 
        WriteVariables(); // запись на карту текущих значений 
    }

    void ProhibitDisable()
    {
        _prohibitMaterials.SetActive(false);
        _prohibitEconomic.SetActive(false);
        _prohibitHealth.SetActive(false);
        _prohibitFood.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData) // начали перетаскивать президента 
    {
        transform.SetParent(_parent); // вернули в изначального родителя (в случае нахождения на факторе), чтобы при перемещении быть сверху факторов 
        ProhibitDisable(); // выключаем кружочки 
        Reset(); // сбрасываем все ранее выбранные ХП 
        ResetText(); // и текст 

        //Debug.Log("_selectPresident " + _targetPlace.GetComponent<Factor_Slot>()._selectPresident);
        //Debug.Log("this.transform " + this.transform);

        for (int i = 0; i < childrenFactors.Length; i++)
        {
            if (childrenFactors[i].GetComponent<Factor_Slot>()._selectPresident != null && childrenFactors[i].GetComponent<Factor_Slot>()._selectPresident != this.transform) // если там кто-то есть, но не наша карта, а мы начали перетаскивать, то всех остальных "домой" 
            {
                for (int j = 0; j < _presidentsArray.Length; j++)
                {
                    if (this.transform != _presidentsArray[j])
                    {
                        _presidentsArray[j].gameObject.SetActive(false);
                    }
                }
            }
        }
        
        _canvasGroup.blocksRaycasts = false; // убрали блокировку рейкаста 

        VariationText(counter_player); // выводим текст в блоки 
        _triggerFactorOnDrop = false; // по-умолчанию false 
        for (int i = 0; i < _factorFolder.childCount; i++) // обнуляем статус у всех факторов-таргетов 
        {
            childrenFactors[i].GetComponent<Factor_Slot>()._targetDragIsTrue = false; 
        } 
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor; // передвигаем президента 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        for (int j = 0; j < _presidentsArray.Length; j++) // включили всех президентов 
        {
            _presidentsArray[j].gameObject.SetActive(true);
        }

        for (int i = 0; i < _factorFolder.childCount; i++) 
        {
            if (childrenFactors[i].GetComponent<Factor_Slot>()._targetDragIsTrue == true) // Если Фактор в скрипте говорит, что на него пришел pointerDrag 
            {

                if (childrenFactors[i].name == "Materials" && _materialOK ||
                    childrenFactors[i].name == "Economic" && _economicOK ||
                    childrenFactors[i].name == "Health" && _healthOK ||
                    childrenFactors[i].name == "Food" && _foodOK) 
                { 
                    _triggerFactorOnDrop = true;
                    _selectFactor = childrenFactors[i];

                    childrenFactors[i].GetComponent<Factor_Slot>()._selectPresident = this.transform; // в Factor_Slot пишем, какой президент выбран 
                    transform.SetParent(childrenFactors[i]); // меняем родителя - поместили в _targetPlace 
                    transform.localPosition = new Vector3(0, -20); // обнулили позицию 

                    _canvasGroup.blocksRaycasts = true; // включили блокировку рейкаста 


                    if (childrenFactors[i].name == "Materials") Materials(counter_player); // дополняем баф от выбранного фактора 
                    else if (childrenFactors[i].name == "Economic") Economic(counter_player);
                    else if (childrenFactors[i].name == "Health") Health(counter_player);
                    else if (childrenFactors[i].name == "Food") Food(counter_player);

                    foreach (Transform child in _selectFactor.GetComponentsInChildren<Transform>()) // убираем президента, который сидел на нашем месте 
                    {
                        if (child != transform && child.tag == "Card")
                        {
                            if (child == _presidentsArray[0]) ResetTransformPresident(child.transform, _placePresidents[0]); // пока по-деревянному 
                            if (child == _presidentsArray[1]) ResetTransformPresident(child.transform, _placePresidents[1]);
                            if (child == _presidentsArray[2]) ResetTransformPresident(child.transform, _placePresidents[2]);
                        }
                    }
                }
            }
        }

        if (_triggerFactorOnDrop == false) // если ни по кому не попали, то сброс домой 
        {
            ResetTransformPresident(this.transform, _parent);
            Reset();
            if (_selectFactor != null) _selectFactor.GetComponent<Factor_Slot>()._selectPresident = null;
        }

        ResetText(); // и в любом случае сброс текста 
        ProhibitDisable(); // и кружочков 
    }

    void ResetTransformPresident(Transform x, Transform Place)
    {
        x.transform.SetParent(Place); // меняем родителя обратно, на Place1,2,3 
        x.transform.localPosition = Vector3.zero; // обнулили позицию 
        x.GetComponent<CanvasGroup>().blocksRaycasts = true; // включили блокировку рейкаста 
    } 
    
    void VariationText(int k)
    {
        if (myItemList.player[k].factor_materials != "") // если описание у сырья не пустое, то обрабатываем его  
        {
            _factorFolder.transform.Find("Materials").Find("Text").GetComponent<Text>().text = _factor_materials;
            _prohibitMaterials.SetActive(false);
            _materialOK = true;
        }
        else 
        {
            _factorFolder.transform.Find("Materials").Find("Text").GetComponent<Text>().text = "";
            _prohibitMaterials.SetActive(true);
            _materialOK = false; 
        }

        if (myItemList.player[k].factor_economic != "") // аналогично 
        {
            _factorFolder.transform.Find("Economic").Find("Text").GetComponent<Text>().text = _factor_economic;
            _prohibitEconomic.SetActive(false);
            _economicOK = true;
        }
        else
        {
            _factorFolder.transform.Find("Economic").Find("Text").GetComponent<Text>().text = "";
            _prohibitEconomic.SetActive(true);
            _economicOK = false;
        }

        if (myItemList.player[k].factor_health != "") 
        {
            _factorFolder.transform.Find("Health").Find("Text").GetComponent<Text>().text = _factor_health;
            _prohibitHealth.SetActive(false);
            _healthOK = true;
        }
        else
        {
            _factorFolder.transform.Find("Health").Find("Text").GetComponent<Text>().text = "";
            _prohibitHealth.SetActive(true);
            _healthOK = false;
        }

        if (myItemList.player[k].factor_food != "") 
        {
            _factorFolder.transform.Find("Food").Find("Text").GetComponent<Text>().text = _factor_food; 
            _prohibitFood.SetActive(false);
            _foodOK = true;
        }
        else
        {
            _factorFolder.transform.Find("Food").Find("Text").GetComponent<Text>().text = "";
            _prohibitFood.SetActive(true); 
            _foodOK = false; 
        }
    }
    
    void Materials(int j)
    {
        _materials_ability_protect = myItemList.player[j].materials_ability_protect;
        _economic_ability_protect = 0; // обнуляем, т.к. выбрали бонус по Сырью 
        _economic_ability_attack = 0; // обнуляем, т.к. выбрали бонус по Сырью 
        _health_ability_protect = 0;// обнуляем, т.к. выбрали бонус по Сырью 
        _food_ability_protect = 0; // обнуляем, т.к. выбрали бонус по Сырью 
        _food_ability_attack = 0; // обнуляем, т.к. выбрали бонус по Сырью 
    }

    void Economic(int j)
    {
        _materials_ability_protect = 0;
        _economic_ability_protect = myItemList.player[j].economic_ability_protect; // баф защиты 
        _economic_ability_attack = myItemList.player[j].economic_ability_attack; // баф атаки
        _health_ability_protect = 0;
        _food_ability_protect = 0;
        _food_ability_attack = 0;
    }

    void Health(int j)
    {
        _materials_ability_protect = 0;
        _economic_ability_protect = 0;
        _economic_ability_attack = 0;
        _health_ability_protect = myItemList.player[j].health_ability_protect;
        _food_ability_protect = 0;
        _food_ability_attack = 0;
    }

    void Food(int j)
    {
        _materials_ability_protect = 0;
        _economic_ability_protect = 0;
        _economic_ability_attack = 0;
        _health_ability_protect = 0;
        _food_ability_protect = myItemList.player[j].food_ability_protect;
        _food_ability_attack = myItemList.player[j].food_ability_attack; /////////////////// 
    }

    void Reset()
    {
        _materials_ability_protect = 0; 
        _economic_ability_protect = 0; 
        _economic_ability_attack = 0; 
        _health_ability_protect = 0;
        _food_ability_protect = 0;
        _food_ability_attack = 0;
    }

    void ResetText()
    {
        _factorFolder.transform.Find("Materials").Find("Text").GetComponent<Text>().text = "";
        _factorFolder.transform.Find("Economic").Find("Text").GetComponent<Text>().text = "";
        _factorFolder.transform.Find("Health").Find("Text").GetComponent<Text>().text = "";
        _factorFolder.transform.Find("Food").Find("Text").GetComponent<Text>().text = "";
    }

    void WriteVariables()
    {
        // передаём в компоненты текста нужные значения переменных. Можно было использовать тот же массив texts[].text, но, изменив порядок текстовых блоков, можно получить другие сообщения 
        transform.Find("Text_Level").GetComponent<Text>().text = "" + _level;
        transform.Find("Text_BufDiplomation").GetComponent<Text>().text = "" + _buff_diplomation;
        transform.Find("Text_BufFortune").GetComponent<Text>().text = "" + _buff_fortune;
        transform.Find("Text_BufProtection").GetComponent<Text>().text = "" + _buff_protection;
        transform.Find("Text_BufAttack").GetComponent<Text>().text = "" + _buff_attack;

        if (_buff_diplomation_delta > 0) transform.Find("Text_BufDiplomationDelta").GetComponent<Text>().text = "+" + _buff_diplomation_delta;
        else transform.Find("Text_BufDiplomationDelta").GetComponent<Text>().text = "" + _buff_diplomation_delta;

        if (_buff_fortune_delta > 0) transform.Find("Text_BufFortuneDelta").GetComponent<Text>().text = "+" + _buff_fortune_delta;
        else transform.Find("Text_BufFortuneDelta").GetComponent<Text>().text = "" + _buff_fortune_delta;

        if (_buff_protection_delta > 0) transform.Find("Text_BufProtectionDelta").GetComponent<Text>().text = "+" + _buff_protection_delta;
        else transform.Find("Text_BufProtectionDelta").GetComponent<Text>().text = "" + _buff_protection_delta; 

        if (_buff_attack_delta > 0) transform.Find("Text_BufAttackDelta").GetComponent<Text>().text = "+" + _buff_attack_delta;
        else transform.Find("Text_BufAttackDelta").GetComponent<Text>().text = "" + _buff_attack_delta;

        ColorRect(); // рисуем подчеркивания 
    }

    void ColorRect()
    {
        if (_buff_attack_delta > 0) // если параметр, влияющий на итоговый баф, положительный
        {
            ImageBuf[0].GetComponent<Image>().color = new Color(0, 1, 0); // зелёный 
        }
        else
        {
            if (_buff_attack_delta == 0) // если 0
            {
                ImageBuf[0].GetComponent<Image>().color = new Color(1, 1, 1); // белый
            }
            else // если он отрицательный 
            {
                ImageBuf[0].GetComponent<Image>().color = new Color(1, 0, 0); //красный
            }
        }
        
        if (_buff_protection_delta > 0) ImageBuf[1].GetComponent<Image>().color = new Color(0, 1, 0); // если баф положительный
        else
        {
            if (_buff_protection_delta == 0) ImageBuf[1].GetComponent<Image>().color = new Color(1, 1, 1);
            else ImageBuf[1].GetComponent<Image>().color = new Color(1, 0, 0);
        }

        if (_buff_fortune_delta > 0) ImageBuf[2].GetComponent<Image>().color = new Color(0, 1, 0); // если баф положительный
        else
        {
            if (_buff_fortune_delta == 0) ImageBuf[2].GetComponent<Image>().color = new Color(1, 1, 1); // если 0
            else ImageBuf[2].GetComponent<Image>().color = new Color(1, 0, 0); // если отрицательный
        }
        
        if (_buff_diplomation_delta > 0) ImageBuf[3].GetComponent<Image>().color = new Color(0, 1, 0);// если баф положительный
        else
        {
            if (_buff_diplomation_delta == 0) ImageBuf[3].GetComponent<Image>().color = new Color(1, 1, 1);
            else ImageBuf[3].GetComponent<Image>().color = new Color(1, 0, 0);
        }
    }

    void CalcClimate() // коррекция изменения бафа из-за климата 
    {
 
        string _TotalClimate = DataHolder._GeneralClimate; // достаём глобальное поле
        if (_climate != _TotalClimate) // Климат не совпал 
        {
            _buff_attack_delta = _buff_attack_delta - 1;
            _buff_diplomation_delta = _buff_diplomation_delta - 1;
            _buff_fortune_delta = _buff_fortune_delta - 1;
            _buff_protection_delta = _buff_protection_delta - 1;
        }
        else
        {
            _buff_attack_delta += 2; // Климат совпал
            _buff_diplomation_delta += 2;
            _buff_fortune_delta += 2;
            _buff_protection_delta += 2;
        }
    }

    public void SaveXP() // сохраняем все ХП в глобальные переменные (можно было в JSON, времени нет) 
    {
        int n = 0;
        if (transform.name == "Lincoln") n = 0; // пока ОЧЕНЬ не гибко, потом переделать 
        if (transform.name == "FidelCastro") n = 1;
        if (transform.name == "Clinton") n = 2; 

        DataHolder.id[n] = _name;
        DataHolder._level[n] = _level;
        DataHolder._buff_attack[n] = _buff_attack;
        DataHolder._buff_fortune[n] = _buff_fortune;
        DataHolder._buff_protection[n] = _buff_protection;
        DataHolder._buff_diplomation[n] = _buff_diplomation;
        DataHolder._buff_attack_delta[n] = _buff_attack_delta;
        DataHolder._buff_fortune_delta[n] = _buff_fortune_delta;
        DataHolder._buff_protection_delta[n] = _buff_protection_delta;
        DataHolder._buff_diplomation_delta[n] = _buff_diplomation_delta;
        DataHolder._BUFFmaterials[n] = _BUFFmaterials;
        DataHolder._BUFFhealth[n] = _BUFFhealth;
        DataHolder._BUFFfood[n] = _BUFFfood;
        DataHolder._BUFFeconomic[n] = _BUFFeconomic;
        DataHolder._materials_ability_protect[n] = _materials_ability_protect;
        DataHolder._economic_ability_protect[n] = _economic_ability_protect;
        DataHolder._economic_ability_attack[n] = _economic_ability_attack;
        DataHolder._health_ability_protect[n] = _health_ability_protect;
        DataHolder._food_ability_protect[n] = _food_ability_protect;
        DataHolder._food_ability_attack[n] = _food_ability_attack;
    }
} 