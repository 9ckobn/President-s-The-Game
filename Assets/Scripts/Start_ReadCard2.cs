using UnityEngine;
using UnityEngine.UI;

public class Start_ReadCard2 : MonoBehaviour
{
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

    private void Start() // Старт игры, отображаем изначальные параметры карт президентов
    {

        ////////// Обрабатываем JSON и все, что с ним связано 
        myItemList = jSONController.myItemList; // вытягиваем лист со скрипта 
        for (int j = 0; j < myItemList.player.Length; j++) // пробежались по всем президентам из JSON-файла 
        {
            if (name == myItemList.player[j].id) // нашли совпадения обладателя этого скрипта с президентами и JSON
            {
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
                _BUFFmaterials = (_buff_fortune + _buff_attack) / 2;
                _BUFFeconomic = (_buff_attack + _buff_diplomation) / 2;
                _BUFFhealth = (_buff_protection + _buff_diplomation) / 2;
                _BUFFfood = (_buff_protection + _buff_fortune) / 2;
            }
        }
        CalcClimate();
        WriteVariables(); // запись на карту текущих значений при старте 
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
        else transform.Find("Text_BufDiplomationDelta").GetComponent<Text>().text = "" + _buff_diplomation_delta; // если будет -N, то так и будет "" + "-N" 

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
        /* ImageBuf[0] = transform.Find("Image_BufAttack").GetComponent<Image>();
        ImageBuf[1] = transform.Find("Image_BufProtection").GetComponent<Image>();
        ImageBuf[2] = transform.Find("Image_BufFortune").GetComponent<Image>();
        ImageBuf[3] = transform.Find("Image_BufDiplomation").GetComponent<Image>();
        */

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

    public void CalcClimate() // коррекция изменения бафа из-за климата 
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