using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DeckBuilderController : MonoBehaviour
{
    [Header("Data & Prefabs")]
    public List<CardData> allCards;
    public GameObject cardPrefab;

    [Header("Spawn Points")]
    public Transform deckPos;
    public Transform handArea;

    private DeckSystem deckSystem = new DeckSystem();
    private HandSystem handSystem = new HandSystem();

    private bool isAnimating = false;

    void Start()
    {
        deckSystem.Init(allCards);
        SpawnDeck();
    }

    void SpawnDeck()
    {
        foreach (var card in allCards)
        {
            var obj = Instantiate(cardPrefab, deckPos);
            var view = obj.GetComponent<CardView>();

            obj.transform.SetAsLastSibling();

            view.Setup(card);
            view.ShowBack(true);

            obj.GetComponent<Button>().onClick.AddListener(OnDeckClicked);
        }
    }

    public void OnDeckClicked()
    {
        if (isAnimating || handSystem.IsFull()) return;

        // ALWAYS take visual top card first
        var cardObj = deckPos.GetChild(deckPos.childCount - 1);
        var view = cardObj.GetComponent<CardView>();

        StartCoroutine(AnimateCard(cardObj.gameObject, view, view.GetData()));

    }

    IEnumerator AnimateCard(GameObject cardObj, CardView card, CardData data)
    {
        isAnimating = true;

        RectTransform rect = cardObj.GetComponent<RectTransform>();

        Sequence seq = DOTween.Sequence();

        // 1. Lift
        seq.Append(rect.DOAnchorPosY(200f, 0.3f));

        // 2. Flip
        seq.Append(rect.DORotate(new Vector3(0, 90, 0), 0.2f)
            .OnComplete(() => card.ShowBack(false)));

        seq.Append(rect.DORotate(Vector3.zero, 0.2f));

        // 3. Center focus
        seq.Append(rect.DOAnchorPos(Vector2.zero, 0.3f));
        seq.Append(rect.DOScale(1.5f, 0.3f));

        yield return seq.WaitForCompletion();

        // 4. Pause
        yield return new WaitForSeconds(0.6f);

        // 5. Move to hand
        rect.DOScale(1f, 0.3f);

        yield return new WaitForSeconds(0.2f);

        rect.DOScale(1f, 0.25f);

        yield return new WaitForSeconds(0.2f);

        // move to hand
        rect.SetParent(handArea, false);

        // reset position inside layout
        rect.anchoredPosition = Vector2.zero;

        // start smaller scale in hand
        rect.localScale = Vector3.one;

        // animate shrink into hand size
        rect.DOScale(0.4f, 0.25f);

        handSystem.Add(data);

        // disable further clicks on this card
        var cg = cardObj.GetComponent<CanvasGroup>();
        if (cg == null) cg = cardObj.AddComponent<CanvasGroup>();
        cg.blocksRaycasts = false;

        isAnimating = false;
    }
}