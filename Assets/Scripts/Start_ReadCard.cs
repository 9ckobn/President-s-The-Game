using UnityEngine;
using UnityEngine.UI;

public class Start_ReadCard : MonoBehaviour
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
}