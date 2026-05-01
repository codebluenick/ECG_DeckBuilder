using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class DeckViewerController : MonoBehaviour
{
    public APIManager apiManager;

    public GameObject deckPrefab;
    public GameObject cardPrefab;
    public Transform content;

    public List<CardData> allCards;

    //For loading panel
    public GameObject loadingPanel;
    public GameObject SpinnerImg;
    public GameObject scrollView;
    public GameObject backBtn;

    void Start()
    {
        loadingPanel.SetActive(true);
        scrollView.SetActive(false);
        backBtn.SetActive(false);

        StartCoroutine(apiManager.LoadDecks(OnLoaded));
    }
    void Update()//small animation for loading screen
    {
        SpinnerImg.transform.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360)
        .SetLoops(-1)
        .SetEase(Ease.Linear);
    }

    void OnLoaded(APIManager.SaveData data)
    {
        loadingPanel.SetActive(false);
        scrollView.SetActive(true);
        backBtn.SetActive(true);

        if (data == null) return;

        Debug.Log("Deck count: " + data.decks.Count);

        string id = PlayerPrefs.GetString("USER_UUID");

        if (data.user_id != id) return;

        foreach (var deck in data.decks)
        {
            CreateDeck(deck);
        }
    }

    void CreateDeck(List<string> deck)
{
    GameObject deckUI = Instantiate(deckPrefab, content);

    Transform row = deckUI.transform.Find("CardRow");

    if (row == null)
    {
        Debug.LogError("CardRow not found in deckPrefab!");
        return;
    }

    foreach (string cardId in deck)
    {
        CardData data = allCards.Find(c => c.id == cardId);

        if (data == null)
        {
            Debug.LogWarning("Card not found: " + cardId);
            continue;
        }

        GameObject card = Instantiate(cardPrefab, row);

        CardView view = card.GetComponent<CardView>();
        view.Setup(data);
        view.ShowBack(false);

        card.AddComponent<CardFocusViewer>();
    }
}
}