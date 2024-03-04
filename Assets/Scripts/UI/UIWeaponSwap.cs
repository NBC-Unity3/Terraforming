using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponSwap : PopupUIBase
{
    [Header("Wheel")]
    public Sprite wheelSprite;
    public Sprite[] weaponSprites;
    public float wheelSpriteScale;
    public float gapBetweenPiece;
    public GameObject wheelParentGO;
    public GameObject pieceGO;

    private GameObject[] pieces;

    private PlayerInventory inventory;

    private void Awake()
    {
        inventory = PlayerController.instance.inventory;
    }

    // Start is called before the first frame update
    void Start()
    {
        pieces = new GameObject[inventory.playerGuns.Length];

        var degree = 360f / inventory.playerGuns.Length;

        var iconDist = Vector3.Distance(pieceGO.GetComponent<Piece>().icon.transform.position, pieceGO.GetComponent<Piece>().piece.transform.position);

        for(int i = 0; i < inventory.playerGuns.Length; i++)
        {
            pieces[i] = Instantiate(pieceGO, wheelParentGO.transform);
            pieces[i].transform.localPosition = Vector3.zero;
            pieces[i].transform.localRotation = Quaternion.identity;

            Piece pieceOfWheel = pieces[i].GetComponent<Piece>();

            Image icon = pieceOfWheel.icon;
            Image piece = pieceOfWheel.piece;

            piece.fillAmount = 1f / inventory.playerGuns.Length - gapBetweenPiece / 360f;
            piece.transform.localRotation = Quaternion.Euler(0, 0, degree / 2f + gapBetweenPiece / 2f - i * degree);
            piece.color = new Color(1f, 1f, 1f, 0.3f);

            icon.sprite = weaponSprites[i];
            icon.transform.localPosition = Quaternion.AngleAxis(-i * degree, Vector3.forward) * Vector3.up * iconDist;
            icon.preserveAspect = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var stepLength = 360f / inventory.playerGuns.Length;
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Input.mousePosition - transform.position, Vector3.up, Vector3.forward) + stepLength / 2f);
        var activeElement = (int)(mouseAngle / stepLength);

        for(int i = 0; i< inventory.playerGuns.Length; i++)
        {
            if (i == activeElement && inventory.playerGuns[i].isUnlock)
                pieces[i].GetComponent<Piece>().piece.color = new Color(1f, 1f, 1f, 0.7f);
            else
                pieces[i].GetComponent<Piece>().piece.color = new Color(1f, 1f, 1f, 0.3f);
        }

        // 클릭 시 해당 무기를 장착하는 기능 추가해야 함 //
        ///////////////////////////////////////////////////
    }

    private float NormalizeAngle(float a) => (a + 360f) % 360f;
}
