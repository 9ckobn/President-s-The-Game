    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class FightRules : MonoBehaviour
{
    Transform _GO1 = null;
    Transform _GO2 = null;

    public Transform _Table;
    public GameObject _Arrow;
    private bool _deactivateArrow = false;

    int[] MoralePresident = new int[3];
    int[] MoralePresidentEnemy = new int[3];
    public int _totalMoralePresident = 0;
    public int _totalMoralePresidentEnemy = 0;
    public Canvas _canvasCamera;
    public Camera _camera;

    public Transform[] _Presidents_Our;
    public Transform[] _Presidents_Enemy;
    public Transform[] _Place_after;
    public Transform[] _PlaceEnemy_after;

    public Transform[] _PlaceFactor;
    public Transform[] _PlaceFactorEnemy;
    public Transform _FactorEconomic;
    public Transform _FactorHealth;
    public Transform _FactorMaterials;
    public Transform _FactorFood;

    public Transform _FactorEconomicEnemy;
    public Transform _FactorHealthEnemy;
    public Transform _FactorMaterialsEnemy;
    public Transform _FactorFoodEnemy;

    public int _ourBUFFmaterial = 0;
    public int _ourBUFFhealth = 0;
    public int _ourBUFFfood = 0;
    public int _ourBUFFeconomic = 0;

    public int _enemyBUFFmaterial = 0;
    public int _enemyBUFFhealth = 0;
    public int _enemyBUFFfood = 0;
    public int _enemyBUFFeconomic = 0;

    private int _ourBUFFmaterialPrevious = 0;
    private int _ourBUFFeconomicPrevious = 0;
    private int _ourBUFFhealthPrevious = 0; 
    private int _ourBUFFfoodPrevious = 0;

    private int _enemyBUFFmaterialPrevious = 0;
    private int _enemyBUFFeconomicPrevious = 0;
    private int _enemyBUFFhealthPrevious = 0;
    private int _enemyBUFFfoodPrevious = 0; 

    private int _buff_attack_delta = 0;
    private int _buff_fortune_delta = 0;
    private int _buff_protection_delta = 0;
    private int _buff_diplomation_delta = 0;

    private int _damageOurAdd = 0;
    private int _damageEnemyAdd = 0;

    public JSONController_Card jSONControllerCard;
    JSONController_Card.ItemListCard myItemListCard;
    private Transform[] _FightCardInScene;
    public Transform[] _FightCardOnTable; // Массив боевых карт на столе 
    private Transform[] _FightCardPlace;
    public GameObject Folder_FightCardPlace;
    public GameObject Folder_FightCardInScene;
    private int counter_card = 0; // передаём номер карты из массива карт (j в цикле) 
    private string _idCard = "";
    private int _costCard = 0;
    private int _materialsCard = 0;
    private int _economicCard = 0;
    private int _healthCard = 0;
    private int _foodCard = 0;
    private int _attackCard = 0;
    private int _protectCard = 0;
    private int _diplomationCard = 0;
    private int _fortuneCard = 0;
    private int _deltamorale_positive = 0;
    private int _deltamorale_negative = 0;

    private int _ourBuffAttack = 0; // общая атака всех президентов 
    private int _enemyBuffAttack = 0; // общая атака всех президентов 

    private int _ourBuffFortune = 0; // общая атака всех президентов 
    private int _enemyBuffFortune = 0; // общая атака всех президентов 

    private int _ourBuffDiplomation = 0; // общая атака всех президентов 
    private int _enemyBuffDiplomation = 0; // общая атака всех президентов 

    private bool _helperMain = true;
    private bool _helper = true;
    private bool _helper101 = false; ///////////////
    private bool _helper105 = false; ///////////////
    private bool _helperScaleCardIsTrue = false;
    private bool _boolOutlineToFactor = false;

    private int _counterOur = 10;
    private int _counterEnemy = 10;
    private int _counterOur2 = 10;
    private int _counterEnemy2 = 10;


    private bool _ourCardPatronage = false;
    private bool _enemyCardPatronage = false;

    Transform _FirstSelectionFightCard = null;
    Transform _factorForAnimLogo;

    private Transform _dragFactor = null; // Фактор, который выбрали мы
    // private Transform _dragFactorEnemy = null; // Фактор, который выбрал враг 
    private string _dragFactorName;

    private int _materials_ability_protect = 0;
    private int _economic_ability_protect = 0;
    private int _economic_ability_attack = 0;
    private int _health_ability_protect = 0;
    private int _food_ability_protect = 0;
    private int _food_ability_attack = 0;

    [TextArea]
    private string _testText1;  // ВРЕМЕННО, ЛОГ НА ЭКРАНЕ 
    private string _testText2;  // ВРЕМЕННО, ЛОГ НА ЭКРАНЕ 
    public Transform _scrollViewContent1; // ВРЕМЕННО, ЛОГ НА ЭКРАНЕ 
    public Transform _scrollViewContent2; // ВРЕМЕННО, ЛОГ НА ЭКРАНЕ 

    public Transform[] ImageBuf = new Transform[4];
    private Transform _hitLast = null;

    public Material _redBoom;
    public Material _greenBoom;
    public Material _MaterialBoom;
    public Material _redArrow;
    public Material _greenArrow; 
    public Material _MaterialArrow;

    private int _multiplyBlockOurEconomic = 1; // множители для эффекта от карт защит. Если 1, то никакого действия не оказывает. Если 0, то блокируется урон при подсчёте 
    private int _multiplyBlockOurMaterials = 1;
    private int _multiplyBlockOurFood = 1;
    private int _multiplyBlockOurHealth = 1;

    private int _multiplyBlockEnemyEconomic = 1; // множители для эффекта от карт защит. Если 1, то никакого действия не оказывает. Если 0, то блокируется урон при подсчёте 
    private int _multiplyBlockEnemyMaterials = 1;
    private int _multiplyBlockEnemyFood = 1;
    private int _multiplyBlockEnemyHealth = 1;

 //   private int _deltaMorale = 0;
 //   private int _deltaMoraleEnemy = 0;

    private string path = Application.streamingAssetsPath + "/" + "log.txt"; // создали лог-файл 

    void Start() // заносим в массивы все карты сцены, места для карт за столом, выбираем рандомные, помещаем за стол 
    {
        _FightCardInScene = new Transform[Folder_FightCardInScene.transform.childCount];
        for (int i = 0; i < _FightCardInScene.Length; i++) // пробежались по всем боевым картам в сцене в папке FightCardInScene (16 штук сейчас) 
        {
            _FightCardInScene[i] = Folder_FightCardInScene.transform.GetChild(i).transform; // занесли их в массив _FightCardInScene[]
        }

        _FightCardPlace = new Transform[Folder_FightCardPlace.transform.childCount];
        _FightCardOnTable = new Transform[_FightCardPlace.Length];

        int[] _random = new int[_FightCardPlace.Length]; // формируем упорядоченный массив с 0 до _FightCardPlace.Length (от 0 до 9 включительно)
        for (int i = 0; i < _random.Length; i++) { _random[i] = i; }

        int[] Mix(int[] num) // перемешиваем его 
        {
            for (int i = 0; i < num.Length; i++)
            {
                int currentValue = num[i];
                int randomIndex = Random.Range(i, num.Length);
                num[i] = num[randomIndex];
                num[randomIndex] = currentValue;
            }
            return num;
        }

        int[] MixArray = Mix(_random); // перемешанный массив для выбора случайных карт из _FightCardInScene 

        myItemListCard = jSONControllerCard.myListFightCard; // вытягиваем лист со скрипта jSON 
        for (int i = 0; i < _FightCardPlace.Length; i++) // пробежались по всем Place будущих карт 
        {
            _FightCardPlace[i] = Folder_FightCardPlace.transform.GetChild(i).transform; // занесли их в массив _FightCardPlace[] 
            _FightCardOnTable[i] = _FightCardInScene[MixArray[i]]; // заполнили массив случайными картами
            _FightCardOnTable[i].transform.SetParent(_FightCardPlace[i]); // поместили боевые карты в Place[] 

            _FightCardOnTable[i].transform.localPosition = Vector3.zero; // сбросили позицию 
            _FightCardOnTable[i].transform.localRotation = Quaternion.identity; // сбросили вращение 
            //_FightCardOnTable[i].transform.localScale = Vector3.one;

            for (int j = 0; j < myItemListCard.fight_card.Length; j++) // пробежались по всем картам из JSON-файла 
            {
                if (_FightCardOnTable[i].name == myItemListCard.fight_card[j].id) // нашли совпадения с картой из JSON 
                {
                    _FightCardOnTable[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_Cost").GetComponent<Text>().text = "" + myItemListCard.fight_card[j].cost; // прописали цену для каждой карты за столом 
                    TextForCard(_FightCardOnTable[i]); // записали текст описания в карточки 
                }
            }
        }
        StartButton(); // выставляем карты президентов 

        StartAnimation(); // анимация 
    }

    void StartAnimation()
    {
        for (int i = 0; i < _Presidents_Our.Length; i++) // анимация президентов 
        {
            Animation anim = _Presidents_Our[i].transform.GetComponent<Animation>();
            anim.Play("PresidentAnimation");
        }

        for (int i = 0; i < _Presidents_Enemy.Length; i++) // анимация соперника  
        {
            Animation anim = _Presidents_Enemy[i].transform.GetComponent<Animation>();
            anim.Play("PresidentAnimationEnemy");
        }

        for (int i = 0; i < _FightCardOnTable.Length; i++) // анимация боевых карточек
        {
            Animator animatorOpen = _FightCardOnTable[i].transform.GetComponent<Animator>();
            animatorOpen.SetBool("Start", true);
        }

        Animation animTable = _Table.transform.GetComponent<Animation>(); // анимируется стол 
        animTable.Play("TableRotateAnimation");

    }


    void StartingForFactors()
    {
        _ourBUFFmaterialPrevious = _ourBUFFmaterial; // запоминаем предыдущее значение фактора до расчётов 
        _ourBUFFeconomicPrevious = _ourBUFFeconomic;
        _ourBUFFhealthPrevious = _ourBUFFhealth;
        _ourBUFFfoodPrevious = _ourBUFFfood;

        _enemyBUFFmaterialPrevious = _enemyBUFFmaterial; // запоминаем предыдущее значение фактора 
        _enemyBUFFeconomicPrevious = _enemyBUFFeconomic;
        _enemyBUFFhealthPrevious = _enemyBUFFhealth;
        _enemyBUFFfoodPrevious = _enemyBUFFfood;
    }

    public void StartButton() // выставляем карты президентов 
    {
        for (int i = 0; i < 3; i++) // копируем свойства UI президентов на 3D карты президентов 
        {
            _Presidents_Our[i].transform.SetParent(_Place_after[i]);
            _Presidents_Our[i].transform.localPosition = Vector3.zero;
            _Presidents_Our[i].transform.localRotation = Quaternion.identity;

            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_Level").GetComponent<Text>().text = "" + DataHolder._level[i];
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufAttack").GetComponent<Text>().text = "" + DataHolder._buff_attack[i];
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufFortune").GetComponent<Text>().text = "" + DataHolder._buff_fortune[i];
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufProtection").GetComponent<Text>().text = "" + DataHolder._buff_protection[i];
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufDiplomation").GetComponent<Text>().text = "" + DataHolder._buff_diplomation[i];
            _buff_attack_delta = DataHolder._buff_attack_delta[i];
            _buff_fortune_delta = DataHolder._buff_fortune_delta[i];
            _buff_protection_delta = DataHolder._buff_protection_delta[i];
            _buff_diplomation_delta = DataHolder._buff_diplomation_delta[i];
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufAttackDelta").GetComponent<Text>().text = "" + _buff_attack_delta;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufFortuneDelta").GetComponent<Text>().text = "" + _buff_fortune_delta;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufProtectionDelta").GetComponent<Text>().text = "" + _buff_protection_delta;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufDiplomationDelta").GetComponent<Text>().text = "" + _buff_diplomation_delta;
            
            _ourBUFFmaterial += DataHolder._BUFFmaterials[i];
            _ourBUFFhealth += DataHolder._BUFFhealth[i];
            _ourBUFFfood += DataHolder._BUFFfood[i];
            _ourBUFFeconomic += DataHolder._BUFFeconomic[i];

            _materials_ability_protect += DataHolder._materials_ability_protect[i];
            _economic_ability_protect += DataHolder._economic_ability_protect[i];
            _economic_ability_attack += DataHolder._economic_ability_attack[i];
            _health_ability_protect += DataHolder._health_ability_protect[i];
            _food_ability_protect += DataHolder._food_ability_protect[i];
            _food_ability_attack += DataHolder._food_ability_attack[i];

            _ourBuffAttack = _ourBuffAttack + DataHolder._buff_attack[i]; // суммируем атаки всех президентов
            _ourBuffFortune = _ourBuffFortune + DataHolder._buff_fortune[i]; // суммируем атрибуты удачи всех президентов
            _ourBuffDiplomation = _ourBuffDiplomation + DataHolder._buff_diplomation[i]; // суммируем атрибуты дипломатии всех президентов 

            ImageBuf[0] = _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Image_BufAttack");
            ImageBuf[1] = _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Image_BufFortune");
            ImageBuf[2] = _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Image_BufProtection");
            ImageBuf[3] = _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Image_BufDiplomation");

            ColorRect(); // красим полоски у карт президентов 
            /*

            //_Presidents_before[i].transform.localScale = Vector3.one; 
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_Level").GetComponent<Text>().text = "" + _FactorItemPresident[i]._level;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufAttack").GetComponent<Text>().text = "" + _FactorItemPresident[i]._buff_attack;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufFortune").GetComponent<Text>().text = "" + _FactorItemPresident[i]._buff_fortune;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufProtection").GetComponent<Text>().text = "" + _FactorItemPresident[i]._buff_protection;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufDiplomation").GetComponent<Text>().text = "" + _FactorItemPresident[i]._buff_diplomation;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufAttackDelta").GetComponent<Text>().text = "" + _FactorItemPresident[i]._buff_attack_delta;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufFortuneDelta").GetComponent<Text>().text = "" + _FactorItemPresident[i]._buff_fortune_delta;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufProtectionDelta").GetComponent<Text>().text = "" + _FactorItemPresident[i]._buff_protection_delta;
            _Presidents_Our[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufDiplomationDelta").GetComponent<Text>().text = "" + _FactorItemPresident[i]._buff_diplomation_delta;

            _ourBUFFmaterial += _FactorItemPresident[i]._BUFFmaterials;
            _ourBUFFhealth += _FactorItemPresident[i]._BUFFhealth;
            _ourBUFFfood += _FactorItemPresident[i]._BUFFfood;
            _ourBUFFeconomic += _FactorItemPresident[i]._BUFFeconomic;
            _materials_ability_protect += _FactorItemPresident[i]._materials_ability_protect;
            _economic_ability_protect += _FactorItemPresident[i]._economic_ability_protect;
            _economic_ability_attack += _FactorItemPresident[i]._economic_ability_attack;
            _health_ability_protect += _FactorItemPresident[i]._health_ability_protect;
            _food_ability_protect += _FactorItemPresident[i]._food_ability_protect;
            _ourBuffAttack = _ourBuffAttack + _FactorItemPresident[i]._buff_attack; // суммируем атаки всех президентов
            _ourBuffFortune = _ourBuffFortune + _FactorItemPresident[i]._buff_fortune; // суммируем атрибуты удачи всех президентов
            _ourBuffDiplomation = _ourBuffDiplomation + _FactorItemPresident[i]._buff_diplomation; // суммируем атрибуты дипломатии всех президентов
                    

        */
        }


        for (int i = 0; i < 3; i++) // копируем свойства президентов из DataHolder в 3D карты президентов-противников 
        {
            _Presidents_Enemy[i].transform.SetParent(_PlaceEnemy_after[i]);
            _Presidents_Enemy[i].transform.localPosition = Vector3.zero;
            _Presidents_Enemy[i].transform.localRotation = Quaternion.identity;
            //_PresidentsEnemy_before[i].transform.localScale = Vector3.one; 

            _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_Level").GetComponent<Text>().text = "" + DataHolder._level[i+3];
            _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufAttack").GetComponent<Text>().text = "" + DataHolder._buff_attack[i+3];
            _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufFortune").GetComponent<Text>().text = "" + DataHolder._buff_fortune[i+3];
            _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufProtection").GetComponent<Text>().text = "" + DataHolder._buff_protection[i+3];
            _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufDiplomation").GetComponent<Text>().text = "" + DataHolder._buff_diplomation[i+3];
            _buff_attack_delta = DataHolder._buff_attack_delta[i+3];
            _buff_fortune_delta = DataHolder._buff_fortune_delta[i+3];
            _buff_protection_delta = DataHolder._buff_protection_delta[i+3];
            _buff_diplomation_delta = DataHolder._buff_diplomation_delta[i+3];
            _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufAttackDelta").GetComponent<Text>().text = "" + _buff_attack_delta;
            _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufFortuneDelta").GetComponent<Text>().text = "" + _buff_fortune_delta;
            _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufProtectionDelta").GetComponent<Text>().text = "" + _buff_protection_delta;
            _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Text_BufDiplomationDelta").GetComponent<Text>().text = "" + _buff_diplomation_delta; 

            _enemyBUFFmaterial += DataHolder._BUFFmaterials[i+3];
            _enemyBUFFhealth += DataHolder._BUFFhealth[i+3];
            _enemyBUFFfood += DataHolder._BUFFfood[i+3];
            _enemyBUFFeconomic += DataHolder._BUFFeconomic[i+3];

            _materials_ability_protect += DataHolder._materials_ability_protect[i+3];
            _economic_ability_protect += DataHolder._economic_ability_protect[i+3];
            _economic_ability_attack += DataHolder._economic_ability_attack[i+3];
            _health_ability_protect += DataHolder._health_ability_protect[i+3];
            _food_ability_protect += DataHolder._food_ability_protect[i+3];
            _food_ability_attack += DataHolder._food_ability_attack[i+3];

            _enemyBuffAttack = _enemyBuffAttack + DataHolder._buff_attack[i+3]; // суммируем атаки всех президентов 
            _enemyBuffFortune = _enemyBuffFortune + DataHolder._buff_fortune[i+3]; // суммируем атрибуты удачи всех президентов
            _enemyBuffDiplomation = _enemyBuffDiplomation + DataHolder._buff_diplomation[i+3];// суммируем атрибуты дипломатии всех президентов 

            ImageBuf[0] = _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Image_BufAttack");
            ImageBuf[1] = _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Image_BufFortune");
            ImageBuf[2] = _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Image_BufProtection");
            ImageBuf[3] = _Presidents_Enemy[i].transform.GetComponentInChildren<Canvas>().transform.Find("Image_BufDiplomation");

            ColorRect(); // красим полоски у карт президентов 

        }

        StartingForFactors();
        ReadyFight();
        ReadyFight2();

        // path = Application.streamingAssetsPath + "/" + "log.txt"; // создали лог-файл 
    }

    void calcLocationFactors() // расставляем факторы, пока топорно без зависимости от выбора президента
    {
        _FactorMaterials.transform.SetParent(_PlaceFactor[0]);
        _FactorMaterials.transform.localPosition = Vector3.zero;
        _FactorMaterials.transform.localRotation = Quaternion.identity;
        _FactorMaterials.transform.localScale = Vector3.one;
        _PlaceFactor[0].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _ourBUFFmaterial; // ХП Сырья

        _FactorFood.transform.SetParent(_PlaceFactor[1]);
        _FactorFood.transform.localPosition = Vector3.zero;
        _FactorFood.transform.localRotation = Quaternion.identity;
        _FactorFood.transform.localScale = Vector3.one;
        _PlaceFactor[1].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _ourBUFFfood; // хп продовольствия

        _FactorEconomic.transform.SetParent(_PlaceFactor[2]);
        _FactorEconomic.transform.localPosition = Vector3.zero;
        _FactorEconomic.transform.localRotation = Quaternion.identity;
        _FactorEconomic.transform.localScale = Vector3.one;
        _PlaceFactor[2].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _ourBUFFeconomic; // ХП экономики

        _FactorHealth.transform.SetParent(_PlaceFactor[3]);
        _FactorHealth.transform.localPosition = Vector3.zero;
        _FactorHealth.transform.localRotation = Quaternion.identity;
        _FactorHealth.transform.localScale = Vector3.one;
        _PlaceFactor[3].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _ourBUFFhealth; // хп здравоохранения 

        _FactorMaterialsEnemy.transform.SetParent(_PlaceFactorEnemy[0]);
        _FactorMaterialsEnemy.transform.localPosition = Vector3.zero;
        _FactorMaterialsEnemy.transform.localRotation = Quaternion.identity;
        _FactorMaterialsEnemy.transform.localScale = Vector3.one;
        _PlaceFactorEnemy[0].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _enemyBUFFmaterial; // ХП Сырья 

        _FactorFoodEnemy.transform.SetParent(_PlaceFactorEnemy[1]);
        _FactorFoodEnemy.transform.localPosition = Vector3.zero;
        _FactorFoodEnemy.transform.localRotation = Quaternion.identity;
        _FactorFoodEnemy.transform.localScale = Vector3.one;
        _PlaceFactorEnemy[1].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _enemyBUFFfood; // хп продовольствия

        _FactorEconomicEnemy.transform.SetParent(_PlaceFactorEnemy[2]);
        _FactorEconomicEnemy.transform.localPosition = Vector3.zero;
        _FactorEconomicEnemy.transform.localRotation = Quaternion.identity;
        _FactorEconomicEnemy.transform.localScale = Vector3.one;
        _PlaceFactorEnemy[2].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _enemyBUFFeconomic; // хп экономики

        _FactorHealthEnemy.transform.SetParent(_PlaceFactorEnemy[3]);
        _FactorHealthEnemy.transform.localPosition = Vector3.zero;
        _FactorHealthEnemy.transform.localRotation = Quaternion.identity;
        _FactorHealthEnemy.transform.localScale = Vector3.one;
        _PlaceFactorEnemy[3].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _enemyBUFFhealth; // хп здравоохранения 

        }

    void Update()
    {
        if (_helperMain == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)); // рейкаст, вычисляем наведение мышки на боевую карту 
            RaycastHit _hit;
            if (Physics.Raycast(ray, out _hit, Mathf.Infinity) && _hit.collider.tag == "FightCard") // проверяем объект по тэгу, чтобы лишний раз цикл for не пускать 
            {
                for (int k = 0; k < _FightCardOnTable.Length; k++) // перебираем, попали ли по карте и по какой 
                {
                    if (_hit.transform.name == _FightCardOnTable[k].name) // выяснили имя карты, по которой попали 
                    {
                        if (_FirstSelectionFightCard != _hit.transform && _FirstSelectionFightCard != null && _helper105 == false) // если перескочили с карты на карту 
                        {
                            _helper105 = true;
                            AnimationScaleCardClose(_FirstSelectionFightCard); // закрываем предыдущую 
                        }

                        if (_hit.transform != _hitLast) // если выбрали карту, которая НЕ была на предыдущем шаге
                        {
                            _helper = true; // разрешение щелкнуть мышкой по карте 

                            if (_helper101 == false)
                            {
                                _helper101 = true; // хелпер, чтобы не гонять постоянно в Update
                                counter_card = k;
                                AnimationScaleCardOpen(_FightCardOnTable[counter_card]); // анимация увеличения текущей карты 
                                FightCard(counter_card);
                            }
                        }
                        else _helper = false; // иначе не разрешаем щелкать по ней 
                    }
                }
            }
            else
            {
                if (_helperScaleCardIsTrue == true && _helper101 == true)
                {
                    AnimationScaleCardClose(_FirstSelectionFightCard);
                    _FirstSelectionFightCard = null;
                }
                _helper101 = false;
            }
        }

        if (_helper101 && Input.GetMouseButtonDown(0) && _helper == true) // если мышка наведена и при этом нажали на клавишу, то обрабатываем далее. _helper101 true - значит, прошли предыдущий пункт. _helper - для текущей остановки в Update 
        {
            _helper = false;
            _helperMain = false; // выключаем, чтобы не обрабатывать 1 этап (рейкаст и тп при наведении мышки на боевые карты) 
            AnimationTransformCard(_FightCardOnTable[counter_card]); // анимация вылета выбранной карты на середину колоды 
        }

        if (_boolOutlineToFactor == true) // рейкаст по нужным факторам, когда нажали на боевую карту 
        {
            // если мышка выше линии, то продолжаем выбирать факторы
            // если ниже - сбрасываем всё обратно 
            if (Input.mousePosition.y < Screen.height / 6) // если ниже 1/6 высоты экрана и не выделяется карта - сбрасываем всё обратно 
            {
                _boolOutlineToFactor = false;
                ResetFightCard();
                StopAnimationFactors(); // остановили все логотипы 
                _helperMain = true;
                ResetAnimationArrow(); // Здесь Reset работает, не в Update - нет 
            }
            else
            {
                RaycastFactors();
                AnimationArrow(); 
            }
        }

        if (_deactivateArrow)
        {
            ResetAnimationArrow();
            _deactivateArrow = false; 
        }
    }

    void AnimationArrow() // растягивающаяся стрелка 
    {
        // _deactivateArrow = false;
        //_Arrow.GetComponentInChildren<MeshRenderer>().enabled = true;
        _Arrow.SetActive(true); // включили стрелку 

        // задаём поворот стрелки в зависимости от положения указателя 
        Vector3 Mouse = Input.mousePosition; // координаты мыши на экране 
        Mouse.z = Vector3.Distance(_camera.transform.position, _PlaceFactor[0].transform.position); // взяли z стола, отсчет от нуля камеры 
        Vector3 target = _camera.ScreenToWorldPoint(Mouse); // координаты мыши мировые 
        _Arrow.transform.LookAt(target); // следим за таргетом
        _Arrow.transform.rotation = Quaternion.Euler(_Arrow.transform.rotation.eulerAngles.x, _Arrow.transform.rotation.eulerAngles.y, 0); 

        float _scaleArrowZ = Vector3.Distance(target, _Arrow.transform.position);
        _Arrow.transform.localScale = new Vector3(_scaleArrowZ, _scaleArrowZ, 2.5f * _scaleArrowZ); // растягиваем стрелку 
    }

    void RaycastFactors()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        RaycastHit _hit;
        _GO2 = _GO1; // _GO2 - выбранный в предыдущий момент фактор

        if (Physics.Raycast(ray, out _hit, Mathf.Infinity) && _hit.collider.tag == "Factors") // рейкаст, вычисляем наведение мышки на фактор 
        {
            _GO1 = _hit.transform; // выбранный на данный момент фактор
            if (_attackCard == 1 || _diplomationCard == 1) // атака и дипломатия ориентированы на факторы противника 
            {
                if (_hit.transform.name == "Enemy_Materials" && _enemyBUFFmaterial > 0 || // условие, что только при положительном ХП факторов будет реакция на нажатие 
                    _hit.transform.name == "Enemy_Food" && _enemyBUFFfood > 0 ||
                    _hit.transform.name == "Enemy_Economic" && _enemyBUFFeconomic > 0 ||
                    _hit.transform.name == "Enemy_Health" && _enemyBUFFhealth > 0)
                {
                    //_MaterialArrow.CopyPropertiesFromMaterial(_redArrow); // меняем цвета у стрелки на красный 
                    ResizeAndClick();
                }
            }
            else if (_protectCard == 1 || _fortuneCard == 1)
            {
                if (_hit.transform.name == "Our_Materials" && _ourBUFFmaterial > 0 || // условие, что только при положительном ХП факторов будет реакция на нажатие 
                    _hit.transform.name == "Our_Food" && _ourBUFFfood > 0 ||
                    _hit.transform.name == "Our_Economic" && _ourBUFFeconomic > 0 ||
                    _hit.transform.name == "Our_Health" && _ourBUFFhealth > 0)
                {
                    //_MaterialArrow.CopyPropertiesFromMaterial(_greenArrow); // меняем цвета у стрелки на зеленый 
                    ResizeAndClick();
                }
            }

            void ResizeAndClick()
            {
                if (_GO2 != _GO1 && _GO2 != null)
                {
                    _GO2.transform.localScale = Vector3.one; // если сменили фактор (перескочили с одного на другой), то предыдущий сбрасываем 
                    _GO2 = null; 
                }
                _GO1.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f); // а текущий по-любому выделяем 

                if (Input.GetMouseButtonDown(0))
                {
                    _boolOutlineToFactor = false; // выключили обработку в Update 
                    _dragFactor = _hit.transform; // узнали, какой фактор выбрали 
                    AnimationArrowReaction(); // вызываем дальнейший код 

                    if (_hitLast != null)
                    {
                        _hitLast.transform.localRotation = Quaternion.identity; // и перевернули закрытую карту рубашкой обратно 
                    }
                }
            }
        }
        else
        {
            if (_GO1 != null)
            {
                _GO1.transform.localScale = Vector3.one;
                _GO1 = null;
            }

            if (_GO2 != null) // на всякий случай 
            {
                _GO2.transform.localScale = Vector3.one;
                _GO2 = null;
            } 

            _dragFactor = null;
        }
    }

    void FightCard(int k) // запоминаем параметры выбранной боевой карты для расчёта 
    {
        myItemListCard = jSONControllerCard.myListFightCard; // вытягиваем лист со скрипта 
        for (int j = 0; j < myItemListCard.fight_card.Length; j++) // пробежались по всем картам из JSON-файла 
        {
            if (_FightCardOnTable[k].name == myItemListCard.fight_card[j].id) // нашли совпадения с картой из JSON
            {
                _idCard = myItemListCard.fight_card[j].id;
                _costCard = myItemListCard.fight_card[j].cost;
                _materialsCard = myItemListCard.fight_card[j].materials;
                _economicCard = myItemListCard.fight_card[j].economic;
                _healthCard = myItemListCard.fight_card[j].health;
                _foodCard = myItemListCard.fight_card[j].food;
                _attackCard = myItemListCard.fight_card[j].attack;
                _protectCard = myItemListCard.fight_card[j].protect;
                _diplomationCard = myItemListCard.fight_card[j].diplomation;
                _fortuneCard = myItemListCard.fight_card[j].fortune;
                _deltamorale_positive = myItemListCard.fight_card[j].deltamorale_positive;
                _deltamorale_negative = myItemListCard.fight_card[j].deltamorale_negative;
            }
        }
    }

    void AnimationScaleCardOpen(Transform _y) // анимация размера боевой карты, которая проигрывается при наведении мыши 
    {
        _FirstSelectionFightCard = _y; 
        _y.localPosition = new Vector3(_y.localPosition.x, 0.05f, _y.localPosition.z); // если использовать ротатор для смещения карты на нас (чтобы избежать Z файтинга, когда карта увеличивается), получается глюк, когда карта из раза в раз постепенно смещается от 0,0,0. Поэтому задаем смещение карты на себя без анимации, "железно" 

        Animator animatorOpen = _y.transform.GetComponent<Animator>();
        animatorOpen.SetBool("Open", true);
        _helperScaleCardIsTrue = true; // хелпер переключился, мы знаем, что объект заскейлен 
        _helper105 = false;
    }

    void AnimationScaleCardClose(Transform _y) // анимация размера боевой карты, которая проигрывается при убирании мыши 
    {
        _y.localPosition = new Vector3(_y.localPosition.x, 0, _y.localPosition.z); // если использовать ротатор для смещения карты на нас (чтобы избежать Z файтинга, когда карта увеличивается), получается глюк, когда карта из раза в раз постепенно смещается от 0,0,0. Поэтому задаем смещение карты на себя без анимации, "железно"

        Animator animatorOpen = _y.transform.GetComponent<Animator>();
        animatorOpen.SetBool("Open", false);

        _helperScaleCardIsTrue = false;
        _helper101 = false;
    }

    void AnimationTransformCard(Transform _y) // анимация вылета боевой карты на центр, которая проигрывается при нажатии кнопк мыши, и других 
    {
        _y.transform.position = new Vector3(_camera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0)).x, _y.transform.position.y, _y.transform.position.z); // карта на центр колоды 
        _helper101 = false; 

        // включаем анимацию логотипов у нужных факторов 
        if (_attackCard == 1 || _diplomationCard == 1) // атака и дипломатия ориентированы на факторы противника 
        {
            for (int i = 0; i < _PlaceFactorEnemy.Length; i++)
            {
                if (_PlaceFactorEnemy[i].name == "Enemy_Materials" && _enemyBUFFmaterial > 0 || // условие, что только при положительном ХП факторов будет крутиться лого 
                    _PlaceFactorEnemy[i].name == "Enemy_Food" && _enemyBUFFfood > 0 ||
                    _PlaceFactorEnemy[i].name == "Enemy_Economic" && _enemyBUFFeconomic > 0 ||
                    _PlaceFactorEnemy[i].name == "Enemy_Health" && _enemyBUFFhealth > 0)
                { 
                    AnimationFactors(_PlaceFactorEnemy[i]); // запускаем анимацию всех логотипов, предлагая игроку выбрать фактор 
                    _MaterialArrow.CopyPropertiesFromMaterial(_redArrow); // меняем цвета у стрелки на красный 
                }
            }
        }

        if (_protectCard == 1 || _fortuneCard == 1) // защита и удача ориентированы на свои факторы
        {
            for (int i = 0; i < _PlaceFactor.Length; i++)
            {
                if (_PlaceFactor[i].name == "Our_Materials" && _ourBUFFmaterial > 0 ||
                    _PlaceFactor[i].name == "Our_Food" && _ourBUFFfood > 0 ||
                    _PlaceFactor[i].name == "Our_Economic" && _ourBUFFeconomic > 0 ||
                    _PlaceFactor[i].name == "Our_Health" && _ourBUFFhealth > 0)
                
                {
                    AnimationFactors(_PlaceFactor[i]); // запускаем анимацию всех логотипов, предлагая игроку выбрать фактор 
                    _MaterialArrow.CopyPropertiesFromMaterial(_greenArrow); // меняем цвета у стрелки на зеленый  
                }
            }
        }
    }

    void AnimationFactors(Transform _factorForAnim) // запускаем анимацию Лого факторов 
    {
        _factorForAnimLogo = _factorForAnim.transform.Find(_factorForAnim.name).transform.Find("Logo"); // нашли Логотип фактора 
        Animator animatorFactor = _factorForAnimLogo.transform.GetComponent<Animator>(); // достали аниматор 
        animatorFactor.SetBool("StartRotate", true); // включили вращение логотипа 
        _boolOutlineToFactor = true; // отмечаем, что прошли этап, в Update нужно рейкастить фактор, по которому щелкнем мышью
    }

    void AnimationArrowReaction() 
    {
        StopAnimationFactors(); // остановили все логотипы 
        _deactivateArrow = true; // выключили стрелку. Я хз, почему не работает SetActive. Пока так, в Update  

        Cursor.lockState = CursorLockMode.Locked; // отключаем курсор 
        if (_attackCard == 1 || _diplomationCard == 1) // если атака или диломатия
        {
            _MaterialBoom.CopyPropertiesFromMaterial(_redBoom); // меняем цвета у частиц на красный 
        }
        else if (_protectCard == 1 || _fortuneCard == 1) // если защита или удача 
        {
            _MaterialBoom.CopyPropertiesFromMaterial(_greenBoom); // меняем цвета у частиц на зеленый  
        }

        _dragFactor.Find("Boom").gameObject.SetActive(true); // включили фейерверк 
        StartCoroutine(Pause2());
    }

    void ResetAnimationArrow() // сброс стрелки 
    {
        _Arrow.transform.rotation = Quaternion.identity;
        _Arrow.transform.localScale = Vector3.one;
        _Arrow.SetActive(false); // выключили стрелку 
    }

    void StopAnimationFactors() // пока так топорно и с Find останавливаем анимацию Лого, потом убрать 
    {
        for (int i = 0; i < _PlaceFactor.Length; i++)
        {
            _PlaceFactor[i].transform.Find(_PlaceFactor[i].name).transform.Find("Logo").transform.GetComponent<Animator>().SetBool("StartRotate", false);
        }
        for (int i = 0; i < _PlaceFactorEnemy.Length; i++)
        {
            _PlaceFactorEnemy[i].transform.Find(_PlaceFactorEnemy[i].name).transform.Find("Logo").transform.GetComponent<Animator>().SetBool("StartRotate", false);
        }
    }

    IEnumerator Pause2() // пауза анимации нашего фейерверка 
    {

        yield return new WaitForSeconds(1); // пока так, в секундах. Через 1 сек закончилась анимация взрыва 
        ResetAnimationCard();
    }

    void ResetFightCard()
    {
        AnimationScaleCardClose(_FightCardOnTable[counter_card]); // сбросили анимацию карты 
        _FightCardOnTable[counter_card].transform.localPosition = Vector3.zero; // вернули карту на место 
    }
    void ResetAnimationCard()
    {
        _GO1.transform.localScale = Vector3.one; // сбросили выделенный фактор 
        _GO1 = null;

        _dragFactor.Find("Boom").gameObject.SetActive(false); // выключили фейерверк 
        ResetFightCard();
        _FightCardOnTable[counter_card].transform.localEulerAngles = new Vector3(0, 0, 180); // и перевернули карту рубашкой вверх 
        CalcOurFight(); // вызываем расчёт очков в бою 
    }

    void CalcOurFight() // Расчёт очков в бою после нашего хода 
    {
        _enemyBUFFmaterialPrevious = _enemyBUFFmaterial; // запоминаем предыдущее значение фактора 
        _enemyBUFFeconomicPrevious = _enemyBUFFeconomic; 
        _enemyBUFFhealthPrevious = _enemyBUFFhealth; 
        _enemyBUFFfoodPrevious = _enemyBUFFfood; 

        if (_dragFactor != null)
        {
            RenameFactors(); 
            _testText1 = " We used the card " + _FightCardOnTable[counter_card].name + " to " + _dragFactorName + "\n" + _testText1;
            _testText2 = " We used the card " + _FightCardOnTable[counter_card].name + " to " + _dragFactorName + "\n" + _testText2;

            /*
            _materials_ability_protect
            _economic_ability_protect
            _economic_ability_attack
            _health_ability_protect
            _food_ability_protect
            
            _ourBUFFeconomic = 0;
            _ourBUFFfood = 0;
            _ourBUFFhealth = 0;
            _ourBUFFmaterial = 0;

            _enemyBUFFeconomic = 0;
            _enemyBUFFfood = 0;
            _enemyBUFFhealth = 0;
            _enemyBUFFmaterial = 0; 

            */

            if (_attackCard == 1)
            { 
                CalcDamageAtack(true); // пересчитали урон на мораль или факторы врага 
                CalcCoastFactors(true); // вычли цену карты из фактора 
            }
            else
            {
                if (_protectCard == 1)
                {
                    CalcDamageProtect(true);
                    CalcCoastFactors(true); // вычли цену карты из фактора  
                }
                else
                {
                    if (_diplomationCard == 1) // по дипломатии пока нет функционала 
                    {
                        CalcDamageDiplomation(true);
                        CalcCoastFactors(true); // вычли цену карты из фактора 
                    }
                    else
                    {
                        if (_fortuneCard == 1)
                        {
                            CalcCoastFactors(true); // вычли цену карты из фактора
                            CalcDamageFortune(true); 
                        }
                    }
                }
            }
        }
        

        ReadyFight2(); // Вызываем вывод данных на экран после нашего хода 
        Reset_multiplyBlock(false); // обнуляем эффект от карты соперника "Защита" (на факторы соперника) после нашего хода 
        CalcEnemyFight(); // Ход соперника 
    }

    void CalcEnemyFight() // Соперник "Выбирает" карту 
    {
        int _enemy = Random.Range(0, _FightCardOnTable.Length); // выбираем карту (за столом) наугад 
        FightCard(_enemy); // получаем её данные 

        if (_attackCard == 1 || _diplomationCard == 1) // атака и дипломатия ориентированы на факторы противника 
        {
            int i = Random.Range(0, 4); // int инклюзивный, 4 не входит, выбор от 0 до 3 
            _dragFactor = _PlaceFactor[i].Find(_PlaceFactor[i].name); 
        }

        if (_protectCard == 1 || _fortuneCard == 1) // защита и удача ориентированы на свои факторы
        {
            int i = Random.Range(0, 4); // int инклюзивный, 4 не входит, выбор от 0 до 3 
            _dragFactor = _PlaceFactorEnemy[i].Find(_PlaceFactorEnemy[i].name); 
        }

        RenameFactors();
        _testText1 = " The opponent used the card " + _FightCardOnTable[_enemy].name + " to " + _dragFactorName + "\n" + _testText1;
        _testText2 = " The opponent used the card " + _FightCardOnTable[_enemy].name + " to " + _dragFactorName + "\n" + _testText2; 

        StartCoroutine(PauseEnemyCoroutine()); // пауза после хода соперника 
    }
    IEnumerator PauseEnemyCoroutine() // пауза после выбора карты соперником 
    {
        yield return new WaitForSeconds(1.5f);
        CalcEnemyFight2();
    }

    void RenameFactors()
    {
        if (_dragFactor.name == "Enemy_Materials" || _dragFactor.name == "Our_Materials") _dragFactorName = "Material";
        if (_dragFactor.name == "Enemy_Economic" || _dragFactor.name == "Our_Economic") _dragFactorName = "Economic";
        if (_dragFactor.name == "Enemy_Health" || _dragFactor.name == "Our_Health") _dragFactorName = "Health";
        if (_dragFactor.name == "Enemy_Food" || _dragFactor.name == "Our_Food") _dragFactorName = "Food";
    }


    ////////////////////
    
    void CalcDamageAtack(bool _isOurAttack) // считаем урон от атаки в зависимости от типа карты (и нашего или противника Атрибута атаки) 
    {


        void calcDamage(int _damage)
        {
            if (_dragFactor.name == "Enemy_Materials") 
            {
                _enemyBUFFmaterial = _enemyBUFFmaterial - _damage*_multiplyBlockEnemyMaterials; // Множители для эффекта от карт защит. Если 1, то никакого действия не оказывает. Если 0, то блокируется урон от атаки при подсчёте 
            }
            if (_dragFactor.name == "Enemy_Economic")
            { 
                _enemyBUFFeconomic = _enemyBUFFeconomic - _damage * _multiplyBlockEnemyEconomic; 
            }
            if (_dragFactor.name == "Enemy_Health")
            {
                _enemyBUFFhealth = _enemyBUFFhealth - _damage * _multiplyBlockEnemyHealth; 
            }
            if (_dragFactor.name == "Enemy_Food")
            {
                _enemyBUFFfood = _enemyBUFFfood - _damage * _multiplyBlockEnemyFood; 
            }

            if (_dragFactor.name == "Our_Materials")
            {
                _ourBUFFmaterial = _ourBUFFmaterial - _damage * _multiplyBlockOurMaterials; // это урон нам, когда противник атакует 
            }
            if (_dragFactor.name == "Our_Economic")
            {
                _ourBUFFeconomic = _ourBUFFeconomic - _damage * _multiplyBlockOurEconomic;
            }
            if (_dragFactor.name == "Our_Health")
            {
                _ourBUFFhealth = _ourBUFFhealth - _damage * _multiplyBlockOurHealth; 
            }
            if (_dragFactor.name == "Our_Food")
            {
                _ourBUFFfood = _ourBUFFfood - _damage * _multiplyBlockOurFood;
            }
        }

        int _damage;
        if (_isOurAttack) // если мы атакуем 
        {
            if (_idCard == "airStrike")
            {
                _damage = 3 + _ourBuffAttack + _damageOurAdd;
                calcDamage(_damage);
            }
            else if (_idCard == "intelligenceData")
            {
                _damage = 2 + _ourBuffAttack / 2;
                _damageOurAdd = _damage;
            }
            else if (_idCard == "sunction")
            {
                _damage = _ourBuffAttack / 2; // формула не та, пока так. Надо будет выводить параметры урона наверх, чтобы считать это... 
                _damageOurAdd = _damage;
            }
            else if (_idCard == "isolation")
            {
                _damage = _ourBuffAttack / 3 + _damageOurAdd;
                _enemyBUFFmaterial = _enemyBUFFmaterial - _damage * _multiplyBlockEnemyMaterials;
                _enemyBUFFeconomic = _enemyBUFFeconomic - _damage * _multiplyBlockEnemyEconomic;
                _enemyBUFFhealth = _enemyBUFFhealth - _damage * _multiplyBlockEnemyHealth;
                _enemyBUFFfood = _enemyBUFFfood - _damage * _multiplyBlockEnemyFood;
                _totalMoralePresidentEnemy = _totalMoralePresidentEnemy - 4 * _damage; 
            } 
        }
        else // если враг атакует 
        {
            if (_idCard == "airStrike")
            {
                _damage = 3 + _ourBuffAttack + _damageEnemyAdd;
                calcDamage(_damage);
            }
            else if (_idCard == "intelligenceData")
            {
                _damage = 2 + _ourBuffAttack / 2;
                _damageEnemyAdd = _damage; 
            }
            else if (_idCard == "sunction")
            {
                _damage = _ourBuffAttack / 2; // формула не та, пока так. Надо будет выводить параметры урона наверх, чтобы считать это... 
                _damageEnemyAdd = _damage;
            }
            else if (_idCard == "isolation")
            {
                _damage = _ourBuffAttack / 3 + _damageEnemyAdd;
                _ourBUFFmaterial = _ourBUFFmaterial - _damage * _multiplyBlockOurMaterials; 
                _ourBUFFeconomic = _ourBUFFeconomic - _damage * _multiplyBlockOurEconomic;
                _ourBUFFhealth = _ourBUFFhealth - _damage * _multiplyBlockOurHealth;
                _ourBUFFfood = _ourBUFFfood - _damage * _multiplyBlockOurFood;
                _totalMoralePresident = _totalMoralePresident - 4 * _damage; 
            }
        }
    }

    void CalcDamageProtect(bool _isOurAttack) // считаем эффект от защиты в зависимости от типа карты (и нашего или противника Атрибута атаки) 
    {
        if (_isOurAttack) // если мы ходим  
        {
            if (_idCard == "customsReform")
            {
                _totalMoralePresident = _totalMoralePresident - 5;
                _multiplyBlockOurEconomic = 0;
            }
            else if (_idCard == "militaryPosition")
            {
                _totalMoralePresident = _totalMoralePresident - 5;
                _multiplyBlockOurMaterials = 0;
            }
            else if (_idCard == "pestControl")
            {
                _totalMoralePresident = _totalMoralePresident - 5;
                _multiplyBlockOurFood = 0;
            }
            else if (_idCard == "accession")
            {
                _totalMoralePresident = _totalMoralePresident - 10;
                _multiplyBlockOurEconomic = 0;
                _multiplyBlockOurMaterials = 0;
                _multiplyBlockOurFood = 0;
                _multiplyBlockOurHealth = 0;
            }
        }
        else // если враг атакует 
        {
            if (_idCard == "customsReform")
            {
                _totalMoralePresidentEnemy = _totalMoralePresidentEnemy - 5;
                _multiplyBlockEnemyEconomic = 0;
            }
            else if (_idCard == "militaryPosition")
            {
                _totalMoralePresidentEnemy = _totalMoralePresidentEnemy - 5;
                _multiplyBlockEnemyMaterials = 0;
            }
            else if (_idCard == "pestControl")
            {
                _totalMoralePresidentEnemy = _totalMoralePresidentEnemy - 5;
                _multiplyBlockEnemyFood = 0;
            }

            else if (_idCard == "accession")
            {
                _totalMoralePresidentEnemy = _totalMoralePresidentEnemy - 10;
                _multiplyBlockEnemyEconomic = 0;
                _multiplyBlockEnemyMaterials = 0;
                _multiplyBlockEnemyFood = 0;
                _multiplyBlockEnemyHealth = 0;
            }
        }
    }

    void CalcDamageDiplomation(bool _isOurAttack)
    {
        int _damageOur(int _addBufDiplomation) // расчёт _multiplyBlockOur в зависимости от шансов (прокачанная дипломатия) 
        {
            int result = 0;
            if (Random.Range(0, 101) >= _ourBuffDiplomation + _addBufDiplomation) // выбираем рандомное значение от 0 до 100% с вероятностью выбора в _ourBuffDiplomation + _addBufDiplomation процентов 
            {
                result = 0; // успешная блокировка урона 
            }
            else
            {
                result = 1; // безуспешная блокировка урона 
            }
            return result;
        }

        int _damageEnemy(int _addBufDiplomation) // расчёт _multiplyBlockOur в зависимости от шансов (прокачанная дипломатия) 
        {
            int result = 0;
            if (Random.Range(0, 101) >= _enemyBuffDiplomation + _addBufDiplomation) // выбираем рандомное значение от 0 до 100% с вероятностью выбора в _ourBuffDiplomation + _addBufDiplomation процентов 
            {
                result = 0; // успешная блокировка урона 
            }
            else
            {
                result = 1; // безуспешная блокировка урона 
            }
            return result;
        }

        if (_isOurAttack) // если мы ходим  
        {
            if (_idCard == "patronage")
            {
                _counterOur = 0;
                _ourCardPatronage = true; 

                if (_materialsCard == 1) _multiplyBlockOurMaterials = _damageOur(0);
                else if (_economicCard == 1) _multiplyBlockOurEconomic = _damageOur(0);
                else if (_healthCard == 1) _multiplyBlockOurHealth = _damageOur(0);
                else if (_foodCard == 1) _multiplyBlockOurFood = _damageOur(0);
            }
            if (_idCard == "diplomaticImmunty")
            {
                if (_damageOur(0) == 1) // в patronage фактор не получает урон с верояностью _ourBuffDiplomation + _addBufDiplomation, а здесь наоборот, Вероятность снижена на _ourBuffDiplomation + _addBufDiplomation, поэтому "инвертируем" result 
                {
                    _multiplyBlockOurMaterials = 1;
                    _multiplyBlockOurFood = 1;
                }
                else if (_damageOur(0) == 0)
                {
                    _multiplyBlockOurMaterials = 0;
                    _multiplyBlockOurFood = 0; 
                }
            }
            if (_idCard == "strategicLoan") 
            {
                _counterOur2 = 0; // сбросили счётчик 
                _totalMoralePresident = _totalMoralePresident + _ourBuffDiplomation; 
            }
            if (_idCard == "energyExpansion")
            {
                _multiplyBlockOurMaterials = _damageOur(40);
                _multiplyBlockOurFood = _damageOur(40);
            }
        }
        else // если враг ходит 
        {
            if (_idCard == "patronage")
            {
                _counterEnemy = 0;
                _enemyCardPatronage = true;

                if (_materialsCard == 1) _multiplyBlockEnemyMaterials = _damageEnemy(0);
                else if (_economicCard == 1) _multiplyBlockEnemyEconomic = _damageEnemy(0);
                else if (_healthCard == 1) _multiplyBlockEnemyHealth = _damageEnemy(0);
                else if (_foodCard == 1) _multiplyBlockEnemyFood = _damageEnemy(0);
            }
            if (_idCard == "diplomaticImmunty")
            {
                if (_damageEnemy(0) == 1) // в patronage фактор не получает урон с верояностью _ourBuffDiplomation + _addBufDiplomation, а здесь наоборот, Вероятность снижена на _ourBuffDiplomation + _addBufDiplomation, поэтому "инвертируем" result 
                {
                    _multiplyBlockEnemyMaterials = 1;
                    _multiplyBlockEnemyFood = 1;
                }
                else if (_damageEnemy(0) == 0)
                {
                    _multiplyBlockEnemyMaterials = 0;
                    _multiplyBlockEnemyFood = 0;
                }
            }
            if (_idCard == "strategicLoan") 
            {
                _counterEnemy2 = 0; // сбросили счётчик 
                _totalMoralePresidentEnemy = _totalMoralePresidentEnemy + _enemyBuffDiplomation; 
            }
            if (_idCard == "energyExpansion")
            {
                _multiplyBlockEnemyMaterials = _damageEnemy(40);
                _multiplyBlockEnemyFood = _damageEnemy(40);
            }
        }
    }

    void CalcDamageFortune(bool _isOurAttack)
    {
        int _fortuneDamage = 0;
        if (_isOurAttack) // если мы ходим  
        {
            if (_idCard == "harvest") _fortuneDamage = 10 + _ourBuffFortune;
            else if (_idCard == "elections") _fortuneDamage = 10 + _ourBUFFhealth;
            else if (_idCard == "techological")
            {
                _damageOurAdd = _damageOurAdd + _ourBuffFortune / 2; // Расход экономики уменьшен на 2 единица на следующий ход - пока не реализовал 
            }
            else if (_idCard == "educationalInfrastructure")
            { 
                _fortuneDamage = 10 + _ourBUFFmaterial; // формула не та, пока так. Надо будет выводить параметры урона наверх, чтобы считать это... 
            } 

            if (Random.Range(0, 101) >= _fortuneDamage) // выбираем рандомное значение от 0 до 100% с вероятностью выбора в _fortuneDamage процентов 
            {
                _totalMoralePresident = _totalMoralePresident + _deltamorale_positive; 
            }
            else _totalMoralePresident = _totalMoralePresident + _deltamorale_negative; // в JSON'е _deltamorale_negative отрицательное, поэтому прибавляем 
        }
        else
        {
            if (_idCard == "harvest") _fortuneDamage = 10 + _enemyBuffFortune;
            else if (_idCard == "elections") _fortuneDamage = 10 + _enemyBUFFhealth;
            else if (_idCard == "techological")
            {
                _damageEnemyAdd = _damageEnemyAdd + _enemyBuffFortune / 2; // Расход экономики уменьшен на 2 единица на следующий ход - пока не реализовал 
            }
            else if (_idCard == "educationalInfrastructure") _fortuneDamage = 10 + _enemyBUFFmaterial; // формула не та, пока так. Надо будет выводить параметры урона наверх, чтобы считать это... 

            if (Random.Range(0, 101) >= _fortuneDamage) // выбираем рандомное значение от 0 до 100% с вероятностью выбора в _fortuneDamage процентов 
            {
                _totalMoralePresidentEnemy = _totalMoralePresidentEnemy + _deltamorale_positive;
            }
            else _totalMoralePresidentEnemy = _totalMoralePresidentEnemy + _deltamorale_negative;
        }
    }

    void CalcCoastFactors(bool _isOurAttack) // вычитаем цену карты из факторов и морали 
    {
        if (_isOurAttack) // если мы атакуем 
        {
            _totalMoralePresident = _totalMoralePresident - _costCard; // пока будем дублировать вычитаение цены карты в Морали, потому что не можем в игровом процессе пересчитывать мораль как сумму факторов. Потому что в процессе игры на мораль влияют эффекты, которые не затрагивают факторы.  

            if (_materialsCard == 1) _ourBUFFmaterial = _ourBUFFmaterial - _costCard; //_deltaMorale = _deltaMorale + _costCard;
            if (_economicCard == 1) _ourBUFFeconomic = _ourBUFFeconomic - _costCard; //_deltaMorale = _deltaMorale+_costCard;
            if (_healthCard == 1) _ourBUFFhealth = _ourBUFFhealth - _costCard; //_deltaMorale = _deltaMorale + _costCard;
            if (_foodCard == 1) _ourBUFFfood = _ourBUFFfood - _costCard; //_deltaMorale = _deltaMorale + _costCard;
        }
        else // если враг атакует 
        {
            _totalMoralePresidentEnemy = _totalMoralePresidentEnemy - _costCard; // пока будем дублировать вычитание цены карты в Морали, потому что не можем в игровом процессе пересчитывать мораль как сумму факторов. Потому что в процессе игры на мораль влияют эффекты, которые не затрагивают факторы 

            if (_materialsCard == 1) _enemyBUFFmaterial = _enemyBUFFmaterial - _costCard; //_deltaMoraleEnemy = _deltaMoraleEnemy + _costCard;
            if (_economicCard == 1) _enemyBUFFeconomic = _enemyBUFFeconomic - _costCard; //_deltaMoraleEnemy = _deltaMoraleEnemy + _costCard;
            if (_healthCard == 1) _enemyBUFFhealth = _enemyBUFFhealth - _costCard; //_deltaMoraleEnemy = _deltaMoraleEnemy + _costCard;
            if (_foodCard == 1) _enemyBUFFfood = _enemyBUFFfood - _costCard; // _deltaMoraleEnemy = _deltaMoraleEnemy + _costCard;
        } 
    }

    void CalcEnemyFight2() // расчёт очков от хода соперника 
    {
        _ourBUFFmaterialPrevious = _ourBUFFmaterial; // запоминаем предыдущее значение фактора до расчётов 
        _ourBUFFeconomicPrevious = _ourBUFFeconomic;
        _ourBUFFhealthPrevious = _ourBUFFhealth;
        _ourBUFFfoodPrevious = _ourBUFFfood;

        if (_attackCard == 1) 
        {
            _MaterialBoom.CopyPropertiesFromMaterial(_redBoom); // меняем цвета у частиц на красный
            CalcDamageAtack(false); // считаем урон Атаки  
            CalcCoastFactors(false); // пересчитываем факторы исходя из цены 
        }
        else
        {
            if (_protectCard == 1)
            {
                _MaterialBoom.CopyPropertiesFromMaterial(_greenBoom); // меняем цвета у частиц на красный
                CalcDamageProtect(false);
                CalcCoastFactors(false); // пересчитываем факторы исходя из цены 
            }
            else
            {
                if (_diplomationCard == 1) // по дипломатии пока нет функционала 
                {
                    _MaterialBoom.CopyPropertiesFromMaterial(_redBoom); // меняем цвета у частиц на красный 
                    CalcDamageDiplomation(false);
                    CalcCoastFactors(false); // пересчитываем факторы исходя из цены 
                }
                else
                {
                    if (_fortuneCard == 1)
                    {
                        _MaterialBoom.CopyPropertiesFromMaterial(_greenBoom); // меняем цвета у частиц на красный 
                        CalcDamageFortune(false);
                        CalcCoastFactors(false); // пересчитываем факторы исходя из цены 
                    }
                }
            }
        }

        _dragFactor.Find("Boom").gameObject.SetActive(true); // включили фейерверк от вражеского хода 
        _hitLast = _FightCardOnTable[counter_card]; // записали, что в этом ходу была выбрана такая-то карта 
        

        ReadyFight2(); // вывод на экран расчётов после хода соперника 
        StartCoroutine(Pause3()); 
    }

    IEnumerator Pause3() // пауза анимации фейерверка 
    {
        yield return new WaitForSeconds(2);
        _dragFactor.Find("Boom").gameObject.SetActive(false); // выключили фейерверк 
        Cursor.lockState = CursorLockMode.None; // включаем курсор только после хода соперника 
        _helperMain = true; // возвращаем хелперы в рабочее состояние 
        _helper = true;

        Reset_multiplyBlock(true); // обнуляем эффект от карты Защиты на наши факторы после хода соперника 
    }
    
    void Reset_multiplyBlock(bool _isOurAttack)
    {
        if (_isOurAttack)
        {
            _counterOur = _counterOur + 1; // счётчик ходов
            if (_counterOur >= 3 && _ourCardPatronage == false) // убираем эффекты, только если у нас нет условий от Patronage (или сделано более 3 шагов) 
            {
                _multiplyBlockOurEconomic = 1;
                _multiplyBlockOurMaterials = 1;
                _multiplyBlockOurFood = 1;
                _multiplyBlockOurHealth = 1;
            }

            _counterOur2 = _counterOur2 + 1;
            if (_counterOur2 == 3) // на конец 2 хода вычисляем по алгоритму действий карты strategicLoan 
            {
                _ourBUFFeconomic = _ourBUFFeconomic - _ourBuffDiplomation / 4;
                _ourBUFFhealth = _ourBUFFhealth - _ourBuffDiplomation / 4;
            }
        }
        else
        {
            _counterEnemy = _counterEnemy + 1; // счётчик ходов

            if (_counterEnemy >= 3 && _enemyCardPatronage == false) // убираем эффекты, только если у нас нет условий от Patronage (или сделано более 3 шагов) 
            {
                _multiplyBlockEnemyEconomic = 1;
                _multiplyBlockEnemyMaterials = 1;
                _multiplyBlockEnemyFood = 1;
                _multiplyBlockEnemyHealth = 1;
            } 

            _counterEnemy2 = _counterEnemy2 + 1;
            if (_counterEnemy2 == 3) // на конец 2 хода вычисляем по алгоритму действий карты strategicLoan 
            {
                _enemyBUFFeconomic = _enemyBUFFeconomic - _enemyBuffDiplomation / 4;
                _enemyBUFFhealth = _enemyBUFFhealth - _enemyBuffDiplomation / 4;
            }

        }

        _damageOurAdd = 0;
        _damageEnemyAdd = 0;
    }

    public void ReadyFight() // рассчитывается 1 раз в начале при нажатии кнопки "Ready" 
    {
        calcLocationFactors();

        for (int i = 0; i < 3; i++) // считаем мораль 
        {
            MoralePresident[i] = DataHolder._BUFFmaterials[i] + DataHolder._BUFFeconomic[i] + DataHolder._BUFFhealth[i] + DataHolder._BUFFfood[i];
            _totalMoralePresident += MoralePresident[i];
            MoralePresidentEnemy[i] = DataHolder._BUFFmaterials[i+3] + DataHolder._BUFFeconomic[i+3] + DataHolder._BUFFhealth[i+3] + DataHolder._BUFFfood[i+3];
            _totalMoralePresidentEnemy += MoralePresidentEnemy[i];
        }
    }

    public void ReadyFight2() // Вывод нужных данных на экран 
    {
        if (_ourBUFFmaterial < 0) _ourBUFFmaterial = 0; // очки факторов не могут быть отрицательными 
        if (_ourBUFFeconomic < 0) _ourBUFFeconomic = 0;
        if (_ourBUFFhealth < 0) _ourBUFFhealth = 0;
        if (_ourBUFFfood < 0) _ourBUFFfood = 0;
        if (_enemyBUFFmaterial < 0) _enemyBUFFmaterial = 0; // очки факторов не могут быть отрицательными 
        if (_enemyBUFFeconomic < 0) _enemyBUFFeconomic = 0;
        if (_enemyBUFFhealth < 0) _enemyBUFFhealth = 0;
        if (_enemyBUFFfood < 0) _enemyBUFFfood = 0;

        _PlaceFactor[0].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _ourBUFFmaterial; // ХП Сырья 
        _PlaceFactor[1].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _ourBUFFfood; // ХП продовольствия
        _PlaceFactor[2].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _ourBUFFeconomic; // хп экономики
        _PlaceFactor[3].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _ourBUFFhealth; // хп здравоохранения 
        _PlaceFactorEnemy[0].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _enemyBUFFmaterial; // ХП Сырья
        _PlaceFactorEnemy[1].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _enemyBUFFfood; // ХП продовольствия
        _PlaceFactorEnemy[2].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _enemyBUFFeconomic; // хп экономики
        _PlaceFactorEnemy[3].transform.Find("Canvas").transform.GetComponentInChildren<Text>().text = "" + _enemyBUFFhealth; // хп здравоохранения 

        _totalMoralePresident = _totalMoralePresident - ((_ourBUFFmaterialPrevious - _ourBUFFmaterial) + (_ourBUFFeconomicPrevious - _ourBUFFeconomic) + (_ourBUFFhealthPrevious - _ourBUFFhealth) + (_ourBUFFfoodPrevious - _ourBUFFfood)); // Из нашей морали вычли изменения в факторах за ход противника 
        _totalMoralePresidentEnemy = _totalMoralePresidentEnemy - ((_enemyBUFFmaterialPrevious - _enemyBUFFmaterial) + (_enemyBUFFeconomicPrevious - _enemyBUFFeconomic) + (_enemyBUFFhealthPrevious - _enemyBUFFhealth) + (_enemyBUFFfoodPrevious - _enemyBUFFfood)); // Из морали врага вычли изменения в факторах за наш ход 


        StartingForFactors(); // перезаписали факторы 

        if (_totalMoralePresident < 0) _totalMoralePresident = 0;
        if (_totalMoralePresidentEnemy < 0) _totalMoralePresidentEnemy = 0; 
       
        if (_enemyBUFFmaterial + _enemyBUFFeconomic + _enemyBUFFhealth + _enemyBUFFfood <= 0 || _totalMoralePresidentEnemy <= 0) // если враг проиграл
        {
            DataHolder._winnerHolder = true;
            DataHolder._moralePresidentHolder = _totalMoralePresident;
            StartCoroutine(Pause4());
            Cursor.lockState = CursorLockMode.None; // включаем курсор 
        }
        else if (_ourBUFFmaterial + _ourBUFFeconomic + _ourBUFFhealth + _enemyBUFFfood <= 0 || _totalMoralePresident <= 0) // если мы проиграли 
        {
            DataHolder._winnerHolder = false;
            StartCoroutine(Pause4());
            Cursor.lockState = CursorLockMode.None; // включаем курсор 
        }
        _testText2 = "\n OurMorale " + _totalMoralePresident + "\n" + " MoraleEnemy " + _totalMoralePresidentEnemy + "\n" + _testText2; // вывод данных 

        _canvasCamera.transform.Find("Text_TotalMorale").GetComponent<Text>().text = "You morale " + _totalMoralePresident; // наша мораль 
        _canvasCamera.transform.Find("Text_TotalMoraleEnemy").GetComponent<Text>().text = "Enemy morale " + _totalMoralePresidentEnemy; // мораль противника 

        _scrollViewContent1.transform.GetComponent<Text>().text = _testText1;
        _scrollViewContent2.transform.GetComponent<Text>().text = _testText2;
        SaveTXT();
    }

    IEnumerator Pause4() // пауза перед завершением 
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(2);
    }
    void SaveTXT() // пишем Лог 
    {
        File.WriteAllText(path, ""); // очистили файл 
        File.WriteAllText(path, _testText2); // записали в лог 
    }

    void TextForCard(Transform Card)
    {

        if (Card.name == "airStrike") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "Will strike the enemy. Targets will hit successfully. Enemy will take " + (3 + _ourBuffAttack) + " Morale damage"; // прописали текст для каждой карты 
        if (Card.name == "intelligenceData") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "Scoting will pass valuable data about the movement of enemy troops. Damage from the next attack will be increased by " + (2 + _ourBuffAttack / 2);
        if (Card.name == "sunction") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "Your decision to impose sanctions will be supported by leaders from around the world. The next attack will cause additional damage to Morale, equal to half of the damage to Factor"; 
        if (Card.name == "isolation") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "A possible isolation policy against the enemy will pay off. All Factors will take the damage " + (_ourBuffAttack/3); 

        if (Card.name == "customsReform") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "Customs control reform will enable more successful deals, but not everyone will be happy with it. The Economy Factor will not take the following damage, but your Morale will be reduced by 5 units"; 
        if (Card.name == "militaryPosition") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "Strategic facilities and raw material resources will be protected. But the citizens will be frightened. Raw resources will not take the damage from the next attack, but your Morale will be reduced by 5 units";
        if (Card.name == "pestControl") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "You will be protecting crops from insects, the citizens won't be afraid of pesticides in their food. Food will not take the damage from the next attack, but your Morale will be reduced by 5 units"; 
        if (Card.name == "accession") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "You will manage to negotiate alliances with other States. Not all citizens will support this course of action. All Factors will not take the damage from the next attack, but your Morale will be reduced by 10 units";
                    
        if (Card.name == "harvest") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "The agricultural sector will show excellent results. With a certain chance " + (10 + _ourBuffFortune) + "% you may get +10 to Morale, if you lose your Morale will be reduced by 5 units"; 
        if (Card.name == "elections") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "You will hold an early election to prove to the opposition how much you are loved by the citizens. With a chance equal to " + (10 + _ourBUFFhealth) + "% you will get + 10 to Morale or - 5 to Morale"; 
        if (Card.name == "techological") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "You will possess a technology that far ahead your competitors. Damage from attack cards will be increased by half your Luck. Economy consumption will be reduced by 2 units next turn"; 
        if (Card.name == "educationalInfrastructure") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "Your country's educational institutions will be known around the world. Your scientists will work on the first frontier of knowledge. On your next turn, any attack you make will give you Morale equal to 10% of the damage dealt";

        if (Card.name == "patronage") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "You will provide customs concessions favorable for trading. There will be no point in attacking you. The chosen factor with " + _ourBuffDiplomation + "% will not take damage on the next 3 turns";
        if (Card.name == "diplomaticImmunty") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "The powers of the world will favor you. Your resources will be protected for the next 3 turns. The probability of the successful attack on your food and raw resources will be reduced " + _ourBuffDiplomation + "%";
        if (Card.name == "strategicLoan") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "Lacking resources? You will take a loan of " + _ourBuffDiplomation + " for 2 turns. At the end of these turns, you will receive half of the loan as damage to the economy and health";
        if (Card.name == "energyExpansion") Card.transform.GetComponentInChildren<Canvas>().transform.Find("Text").GetComponent<Text>().text = "Half the world will be tied up in deals with your energy resources. The probability of missing the attack on your food and raw resources will be " + (40 + _ourBuffDiplomation) + "%"; 
    
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
}