using UnityEngine;
using UnityEngine.UI;

public class Start_ReadCard2 : MonoBehaviour
{
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

    public int _BUFFmaterials;
    public int _BUFFeconomic;
    public int _BUFFhealth;
    public int _BUFFfood;

    public Image[] ImageBuf = new Image[4]; 

    private void Start() // ����� ����, ���������� ����������� ��������� ���� �����������
    {

        ////////// ������������ JSON � ���, ��� � ��� ������� 
        myItemList = jSONController.myItemList; // ���������� ���� �� ������� 
        for (int j = 0; j < myItemList.player.Length; j++) // ����������� �� ���� ����������� �� JSON-����� 
        {
            if (name == myItemList.player[j].id) // ����� ���������� ���������� ����� ������� � ������������ � JSON
            {
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
                _BUFFmaterials = (_buff_fortune + _buff_attack) / 2;
                _BUFFeconomic = (_buff_attack + _buff_diplomation) / 2;
                _BUFFhealth = (_buff_protection + _buff_diplomation) / 2;
                _BUFFfood = (_buff_protection + _buff_fortune) / 2;
            }
        }
        CalcClimate();
        WriteVariables(); // ������ �� ����� ������� �������� ��� ������ 
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
        else transform.Find("Text_BufDiplomationDelta").GetComponent<Text>().text = "" + _buff_diplomation_delta; // ���� ����� -N, �� ��� � ����� "" + "-N" 

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
        /* ImageBuf[0] = transform.Find("Image_BufAttack").GetComponent<Image>();
        ImageBuf[1] = transform.Find("Image_BufProtection").GetComponent<Image>();
        ImageBuf[2] = transform.Find("Image_BufFortune").GetComponent<Image>();
        ImageBuf[3] = transform.Find("Image_BufDiplomation").GetComponent<Image>();
        */

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

    public void CalcClimate() // ��������� ��������� ���� ��-�� ������� 
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
        int n = 3;
        if (transform.parent.name == "cardsPanel") n = 3; 
        if (transform.parent.name == "cardsPanel (1)") n = 4;
        if (transform.parent.name == "cardsPanel (2)") n = 5;

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