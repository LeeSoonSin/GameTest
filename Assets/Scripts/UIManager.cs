using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;
    public static UIManager instance { get { return _instance; } }
    // 스테이터스
    public Image hpbar;
    public Image RageBar;
    public Image AttackImage;
    public Image GuardImage;
    public Image FireImage;
    public Image HealImage;
    public Image FoxFireImage;
    public Image BuffImage;

    public Sprite unTransAttack;
    public Sprite TransAttack;
    public Sprite unTransGuard;
    public Sprite TransGuard;
    //검은 화면
    public GameObject BackGround;

    //Card
    public GameObject CardUI;
    public GameObject[] Card = new GameObject[9]; //카드 배열
    private int cardIndex; //카드 인덱스
    private int cardStep = 0; //카드 나열 순서
    private bool[] showCard = new bool[9]; //나열된 카드
    private int[] showCardIndex = new int[3]; //나열된 카드 인덱스
    public Sprite frontSprite;
    public Sprite backSprite;

    public bool IsSelect = false;
    //UI
    public GameObject StatusUI;
    public GameObject Panel;


    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        CardUI.SetActive(false);
        DontDestroyOnLoad(gameObject);
        BackGround.SetActive(false);
    }

    private void Update()
    {
        if(IsSelect)
        {
            StatusUI.SetActive(false);
        }
        else
        {
            StatusUI.SetActive(true);
        }
        
        if(GameManager.instance.Dead)
        {
            Panel.SetActive(true);
        }
    }

    public void SetHP(int curHP, int HP)
    {
        hpbar.fillAmount = (float)curHP / HP;
    }

    public void SetRage(int curRage, int Rage)
    {
        RageBar.fillAmount = (float)curRage / Rage;
    }

    public void SetAttackDelay(float curTime, float coolTime)
    {
        AttackImage.fillAmount = curTime / coolTime;
    }

    public void SetSkill(float curTime, float coolTime)
    {
        if(GameManager.instance.SelectedCard[0])
        {
            FireImage.gameObject.SetActive(true);
            FireImage.fillAmount = curTime / coolTime;
        }
        else
        {
            FireImage.gameObject.SetActive(false);
        }

        if (GameManager.instance.SelectedCard[3])
        {
            FoxFireImage.gameObject.SetActive(true);
            FoxFireImage.fillAmount = curTime / coolTime;
        }
        else
        {
            FoxFireImage.gameObject.SetActive(false);
        }
    }

    public void SetBuff()
    {
        if (GameManager.instance.SelectedCard[1])
        {
            HealImage.gameObject.SetActive(true);
        }
        else
        {
            HealImage.gameObject.SetActive(false);
        }

        if (GameManager.instance.SelectedCard[2])
        {
            BuffImage.gameObject.SetActive(true);
        }
        else
        {
            BuffImage.gameObject.SetActive(false);
        }
    }
    public IEnumerator SelectCard()
    {
        IsSelect = true;
        CardUI.SetActive(true);
        BackGround.SetActive(true);
        for (int i = 0; i < 9; i++)
        {
            showCard[i] = false;
        }

        while (cardStep < 3)
        {
            cardIndex = Random.Range(0, 9);
            if (!GameManager.instance.SelectedCard[cardIndex] && !showCard[cardIndex])
            {
                showCard[cardIndex] = true;
                showCardIndex[cardStep] = cardIndex;
                if (cardStep == 0)
                {
                    RectTransform rect = Card[showCardIndex[cardStep]].GetComponent<RectTransform>();
                    rect.anchoredPosition = new Vector2(-400, 0);
                }
                else if (cardStep == 1)
                {
                    RectTransform rect = Card[showCardIndex[cardStep]].GetComponent<RectTransform>();
                    rect.anchoredPosition = new Vector2(0, 0);
                }
                else if (cardStep == 2)
                {
                    RectTransform rect = Card[showCardIndex[cardStep]].GetComponent<RectTransform>();
                    rect.anchoredPosition = new Vector2(400, 0);
                }
                SetActiveCard(Card[showCardIndex[cardStep]]);
                cardStep++;
            }
            yield return null;
        }
        cardStep = 0;
    }//카드 초기화 및 랜덤 선택

    public void OnClickButton() //카드 적용
    {
        GameObject _card = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        if (!GameManager.instance.SelectedCard[int.Parse(_card.name) - 1])
        {
            GameManager.instance.SelectedCard[int.Parse(_card.name) - 1] = true;
        }
        for (int i = 0; i < 9; i++)
        {
            Card[i].SetActive(false);
        }
        IsSelect = false;
        BackGround.SetActive(false);
        CardUI.SetActive(false);
        TransferMap transferMap = GameObject.Find("Door").GetComponent<TransferMap>();
        transferMap.NextStage();
    }

    public void SelectButton() //카드 선택
    {
        GameObject _card = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        StartCoroutine(RotateCard(_card));
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            if (showCardIndex[i] != (int.Parse(_card.name) - 1))
            {
                Card[showCardIndex[i]].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    private void SetActiveCard(GameObject _card) //카드 활성화
    {
        _card.SetActive(true);
        _card.transform.GetChild(1).gameObject.SetActive(true);
        for (int i = 2; i < _card.transform.childCount; i++)
        {
            _card.transform.GetChild(i).gameObject.SetActive(false);
        }
        Image image = _card.transform.GetChild(0).GetComponent<Image>();
        image.sprite = backSprite;
    }

    IEnumerator RotateCard(GameObject _card) //카드 회전
    {
        RectTransform rect = _card.GetComponent<RectTransform>();
        Image image = _card.transform.GetChild(0).GetComponent<Image>();
        rect.localRotation = Quaternion.Euler(0, 180, 0);
        while (true)
        {
            rect.Rotate(new Vector3(0, -50 * Time.deltaTime, 0));
            if (rect.localEulerAngles.y < 90f && rect.localEulerAngles.y > 1f)
            {
                image.sprite = frontSprite;
                for (int i = 2; i < _card.transform.childCount; i++)
                {
                    _card.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else if (rect.localEulerAngles.y < 1f)
            {
                rect.rotation = Quaternion.Euler(0, 0, 0);
                break;
            }
            yield return null;
        }
    }

    public void goMain()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        
    }
}
