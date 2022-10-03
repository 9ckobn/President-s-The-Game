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
    private Transform _parent; // ����������� �������� �������� 
    public Transform _factorFolder; // ����� � ��������� 

    public JSONController jSONController;
    JSONController.ItemList myItemList;
    public string _name = "";
    public int _level = 0;
    public string _climate = ""; // ������ ���������� 
    public int _buff_diplomation = 0;
    public int _buff_diplomation_delta = 0;
    public int _buff_fortune = 0;
    public int _buff_fortune_delta = 0;
    public int _buff_protection = 0;
    public int _buff_protection_delta = 0;
    public int _buff_attack = 0;
    private int _BUFF_Initial = 10; // ������������� ��� �������� ����� 10 
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

    public string _enterFactor = ""; // ������, ������� ��������� ����� �� ���������� 

    private int counter_player = 0; // ������� ����� ���������� �� ������� ����������� (j � �����) 
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

    private void Start() // ����� ����, ���������� ����������� ��������� ���� �����������
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>(); // ������ ����� � �������� ������� ���������� 
        _parent = transform.parent; // ��� �������� ��������, Place1,2,3 
        _mainCanvas = transform.parent.GetComponentInParent<Canvas>(); // ������ 

        childrenFactors = new Transform[_factorFolder.childCount]; // �������, �� ������� ������ ����������� 

        for (int i = 0; i < childrenFactors.Length; i++) // �������� ������ childrenFactors ��������� �����. ����� ���� ������������ foreach
        {
            childrenFactors[i] = _factorFolder.GetChild(i).transform;
        }

        _placePresidents[0] = _mainCanvas.transform.Find("PlacePresident1"); // ���� ��� ������� 
        _presidentsArray[0] = _placePresidents[0].GetChild(0).transform;

        _placePresidents[1] = _mainCanvas.transform.Find("PlacePresident2");
        _presidentsArray[1] = _placePresidents[1].GetChild(0).transform;

        _placePresidents[2] = _mainCanvas.transform.Find("PlacePresident3");
        _presidentsArray[2] = _placePresidents[2].GetChild(0).transform;


        // ������� � ���������� ���� ������ 
        _prohibitMaterials = _factorFolder.transform.Find("Materials").Find("Prohibit").gameObject;
        _prohibitEconomic = _factorFolder.transform.Find("Economic").Find("Prohibit").gameObject;
        _prohibitHealth = _factorFolder.transform.Find("Health").Find("Prohibit").gameObject;
        _prohibitFood = _factorFolder.transform.Find("Food").Find("Prohibit").gameObject;

        ProhibitDisable(); // ��������� ��� ������ 

        ////////// ������������ JSON � ���, ��� � ��� ������� 
        myItemList = jSONController.myItemList; // ���������� ���� �� ������� 
        for (int j = 0; j < myItemList.player.Length; j++) // ����������� �� ���� ����������� �� JSON-����� 
        {
            if (name == myItemList.player[j].id) // ����� ���������� ���������� ����� ������� � ������������ � JSON
            {
                counter_player = j; // ���������, � ����� ������ ������� ������ player (���������)

                // ����� � ���������� �� �� JSON 
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
                _factor_materials = myItemList.player[j].factor_materials; // �������� �������
                _factor_economic = myItemList.player[j].factor_economic; // �������� �������
                _factor_health = myItemList.player[j].factor_health; // �������� �������
                _factor_food = myItemList.player[j].factor_food; // �������� �������
                _BUFFmaterials = (_buff_fortune + _buff_attack) / 2;
                _BUFFeconomic = (_buff_attack + _buff_diplomation) / 2;
                _BUFFhealth = (_buff_protection + _buff_diplomation) / 2;
                _BUFFfood = (_buff_protection + _buff_fortune) / 2;
            }
        }
        CalcClimate(); // ������ ����������� ������� 
        WriteVariables(); // ������ �� ����� ������� �������� 
    }

    void ProhibitDisable()
    {
        _prohibitMaterials.SetActive(false);
        _prohibitEconomic.SetActive(false);
        _prohibitHealth.SetActive(false);
        _prohibitFood.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData) // ������ ������������� ���������� 
    {
        transform.SetParent(_parent); // ������� � ������������ �������� (� ������ ���������� �� �������), ����� ��� ����������� ���� ������ �������� 
        ProhibitDisable(); // ��������� �������� 
        Reset(); // ���������� ��� ����� ��������� �� 
        ResetText(); // � ����� 

        //Debug.Log("_selectPresident " + _targetPlace.GetComponent<Factor_Slot>()._selectPresident);
        //Debug.Log("this.transform " + this.transform);

        for (int i = 0; i < childrenFactors.Length; i++)
        {
            if (childrenFactors[i].GetComponent<Factor_Slot>()._selectPresident != null && childrenFactors[i].GetComponent<Factor_Slot>()._selectPresident != this.transform) // ���� ��� ���-�� ����, �� �� ���� �����, � �� ������ �������������, �� ���� ��������� "�����" 
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
        
        _canvasGroup.blocksRaycasts = false; // ������ ���������� �������� 

        VariationText(counter_player); // ������� ����� � ����� 
        _triggerFactorOnDrop = false; // ��-��������� false 
        for (int i = 0; i < _factorFolder.childCount; i++) // �������� ������ � ���� ��������-�������� 
        {
            childrenFactors[i].GetComponent<Factor_Slot>()._targetDragIsTrue = false; 
        } 
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor; // ����������� ���������� 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        for (int j = 0; j < _presidentsArray.Length; j++) // �������� ���� ����������� 
        {
            _presidentsArray[j].gameObject.SetActive(true);
        }

        for (int i = 0; i < _factorFolder.childCount; i++) 
        {
            if (childrenFactors[i].GetComponent<Factor_Slot>()._targetDragIsTrue == true) // ���� ������ � ������� �������, ��� �� ���� ������ pointerDrag 
            {

                if (childrenFactors[i].name == "Materials" && _materialOK ||
                    childrenFactors[i].name == "Economic" && _economicOK ||
                    childrenFactors[i].name == "Health" && _healthOK ||
                    childrenFactors[i].name == "Food" && _foodOK) 
                { 
                    _triggerFactorOnDrop = true;
                    _selectFactor = childrenFactors[i];

                    childrenFactors[i].GetComponent<Factor_Slot>()._selectPresident = this.transform; // � Factor_Slot �����, ����� ��������� ������ 
                    transform.SetParent(childrenFactors[i]); // ������ �������� - ��������� � _targetPlace 
                    transform.localPosition = new Vector3(0, -20); // �������� ������� 

                    _canvasGroup.blocksRaycasts = true; // �������� ���������� �������� 


                    if (childrenFactors[i].name == "Materials") Materials(counter_player); // ��������� ��� �� ���������� ������� 
                    else if (childrenFactors[i].name == "Economic") Economic(counter_player);
                    else if (childrenFactors[i].name == "Health") Health(counter_player);
                    else if (childrenFactors[i].name == "Food") Food(counter_player);

                    foreach (Transform child in _selectFactor.GetComponentsInChildren<Transform>()) // ������� ����������, ������� ����� �� ����� ����� 
                    {
                        if (child != transform && child.tag == "Card")
                        {
                            if (child == _presidentsArray[0]) ResetTransformPresident(child.transform, _placePresidents[0]); // ���� ��-����������� 
                            if (child == _presidentsArray[1]) ResetTransformPresident(child.transform, _placePresidents[1]);
                            if (child == _presidentsArray[2]) ResetTransformPresident(child.transform, _placePresidents[2]);
                        }
                    }
                }
            }
        }

        if (_triggerFactorOnDrop == false) // ���� �� �� ���� �� ������, �� ����� ����� 
        {
            ResetTransformPresident(this.transform, _parent);
            Reset();
            if (_selectFactor != null) _selectFactor.GetComponent<Factor_Slot>()._selectPresident = null;
        }

        ResetText(); // � � ����� ������ ����� ������ 
        ProhibitDisable(); // � ��������� 
    }

    void ResetTransformPresident(Transform x, Transform Place)
    {
        x.transform.SetParent(Place); // ������ �������� �������, �� Place1,2,3 
        x.transform.localPosition = Vector3.zero; // �������� ������� 
        x.GetComponent<CanvasGroup>().blocksRaycasts = true; // �������� ���������� �������� 
    } 
    
    void VariationText(int k)
    {
        if (myItemList.player[k].factor_materials != "") // ���� �������� � ����� �� ������, �� ������������ ���  
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

        if (myItemList.player[k].factor_economic != "") // ���������� 
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
        _economic_ability_protect = 0; // ��������, �.�. ������� ����� �� ����� 
        _economic_ability_attack = 0; // ��������, �.�. ������� ����� �� ����� 
        _health_ability_protect = 0;// ��������, �.�. ������� ����� �� ����� 
        _food_ability_protect = 0; // ��������, �.�. ������� ����� �� ����� 
        _food_ability_attack = 0; // ��������, �.�. ������� ����� �� ����� 
    }

    void Economic(int j)
    {
        _materials_ability_protect = 0;
        _economic_ability_protect = myItemList.player[j].economic_ability_protect; // ��� ������ 
        _economic_ability_attack = myItemList.player[j].economic_ability_attack; // ��� �����
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
        // ������� � ���������� ������ ������ �������� ����������. ����� ���� ������������ ��� �� ������ texts[].text, ��, ������� ������� ��������� ������, ����� �������� ������ ��������� 
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

        ColorRect(); // ������ ������������� 
    }

    void ColorRect()
    {
        if (_buff_attack_delta > 0) // ���� ��������, �������� �� �������� ���, �������������
        {
            ImageBuf[0].GetComponent<Image>().color = new Color(0, 1, 0); // ������ 
        }
        else
        {
            if (_buff_attack_delta == 0) // ���� 0
            {
                ImageBuf[0].GetComponent<Image>().color = new Color(1, 1, 1); // �����
            }
            else // ���� �� ������������� 
            {
                ImageBuf[0].GetComponent<Image>().color = new Color(1, 0, 0); //�������
            }
        }
        
        if (_buff_protection_delta > 0) ImageBuf[1].GetComponent<Image>().color = new Color(0, 1, 0); // ���� ��� �������������
        else
        {
            if (_buff_protection_delta == 0) ImageBuf[1].GetComponent<Image>().color = new Color(1, 1, 1);
            else ImageBuf[1].GetComponent<Image>().color = new Color(1, 0, 0);
        }

        if (_buff_fortune_delta > 0) ImageBuf[2].GetComponent<Image>().color = new Color(0, 1, 0); // ���� ��� �������������
        else
        {
            if (_buff_fortune_delta == 0) ImageBuf[2].GetComponent<Image>().color = new Color(1, 1, 1); // ���� 0
            else ImageBuf[2].GetComponent<Image>().color = new Color(1, 0, 0); // ���� �������������
        }
        
        if (_buff_diplomation_delta > 0) ImageBuf[3].GetComponent<Image>().color = new Color(0, 1, 0);// ���� ��� �������������
        else
        {
            if (_buff_diplomation_delta == 0) ImageBuf[3].GetComponent<Image>().color = new Color(1, 1, 1);
            else ImageBuf[3].GetComponent<Image>().color = new Color(1, 0, 0);
        }
    }

    void CalcClimate() // ��������� ��������� ���� ��-�� ������� 
    {
 
        string _TotalClimate = DataHolder._GeneralClimate; // ������ ���������� ����
        if (_climate != _TotalClimate) // ������ �� ������ 
        {
            _buff_attack_delta = _buff_attack_delta - 1;
            _buff_diplomation_delta = _buff_diplomation_delta - 1;
            _buff_fortune_delta = _buff_fortune_delta - 1;
            _buff_protection_delta = _buff_protection_delta - 1;
        }
        else
        {
            _buff_attack_delta += 2; // ������ ������
            _buff_diplomation_delta += 2;
            _buff_fortune_delta += 2;
            _buff_protection_delta += 2;
        }
    }

    public void SaveXP() // ��������� ��� �� � ���������� ���������� (����� ���� � JSON, ������� ���) 
    {
        int n = 0;
        if (transform.name == "Lincoln") n = 0; // ���� ����� �� �����, ����� ���������� 
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