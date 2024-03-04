using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

//�ӽ� ��ũ��Ʈ, UI �����ϴ� ��ũ��Ʈ�� �̵��� ����
public class SelectPopupUI : PopupUIBase
{
    public Button storeButton;
    public Button questButton;
    public Button healthButton; //������ �÷��̾� �ڵ����� �̵���Ű��. +�̶� ������ �̵��ϹǷ� enable = false �ʿ�.
    public Button closeButton;


    //������ UI�� SetActive�� ����ϱ� ���ؼ� GameObject�� ��������.
    [HideInInspector] public GameObject storePrefab;
    [HideInInspector] public GameObject questListPrefab;
    public GameObject selectBackground;
    public Image blinkImage;
    QuestListPopupUI questListPopup;
    StoreUI storeUI;

    private void Start()
    {
        StartButtonSetting();
    }

    //��ư ����--------------------------UI���� �� ��ũ��Ʈ�� �־��ִ� ��
    //AddListener�� �� �Ѱ���
    public void StartButtonSetting()
    {

        storeButton.onClick.AddListener(() => OnStore());

        questButton.onClick.AddListener(() => OnQuestList());

        healthButton.onClick.AddListener(() => OnMoveForHealth());

        closeButton.onClick.AddListener(() => OffSelectPopup());
    }

    public void OffSelectPopupchildren()
    {
        selectBackground.SetActive(false);
    }

    public void OnSelectPopupchildren()
    {
        selectBackground.SetActive(true);
    }

    public void OffSelectPopup()
    {
        gameObject.SetActive(false);
    }

    public void OnSelectPopup()
    {
        gameObject.SetActive(true);
    }

    public void OnStore()
    {
        OffSelectPopupchildren();
        if (storePrefab == null)
        {
            storeUI = PopupUIManager.Instance.OpenPopupUI<StoreUI>();
            storeUI.closeBtn.onClick.AddListener(() => OnSelectPopupchildren());
            storePrefab = storeUI.gameObject;
        }
        storePrefab.SetActive(true);
    }

    public void OnQuestList()
    {
        OffSelectPopupchildren();
        if (questListPrefab == null)
        {
            questListPopup = PopupUIManager.Instance.OpenPopupUI<QuestListPopupUI>();
            questListPopup.closeButton.onClick.AddListener(() => OnSelectPopupchildren());
            questListPrefab = questListPopup.gameObject;
        }
        questListPrefab.SetActive(true);
    }



    //gameObject.SetActive(true) ���¿��� �÷��̾ ������ �� �����Ƿ� �����ӿ� ���� �κ��� ���� ���ص� ��.
    public void OnMoveForHealth()
    {
        StartCoroutine(OnHealth());
        OffSelectPopupchildren();
    }

    //�ð� ����..
    //ȭ�� �����Ӹ� �߰�
    IEnumerator OnHealth()
    {
        Color color = blinkImage.color;
        while (blinkImage.color.a <= 1)
        {
            color.a += 3f / 255f;
            blinkImage.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        while (blinkImage.color.a >= 0)
        {
            color.a -= 1f / 255f;
            blinkImage.color = color;
            yield return null;
        }
        OnSelectPopupchildren();
        OffSelectPopup();
    }
}
