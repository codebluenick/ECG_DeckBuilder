using UnityEngine;
using UnityEngine.UI;

///<summary>
/// Manages the visual display of the card in UI
/// </summary>
public class CardView : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("The image where the card's artwork is displayed.")]
    public Image artwork;

    [Tooltip("The text for the card's display name.")]
    public Text nameText;
    [Tooltip("The text for combat stats (e.g., '3/2').")]
    public Text statsText;

    [Tooltip("The text for the ability or description.")]
    public Text descText;

    [Header("Visual States")]
    [Tooltip("The GameObject representing the back of the card (used for hidden cards or deck view).")]
    public GameObject back;

    private CardData data; //Internal ref to the data being displayed

///<summary>
/// Func to inject a CardData ScriptableObject into the UI elements.
/// </summary>
public void Setup(CardData cardData)
    {
        data = cardData;

        //Apply data from the ScriptableObject to the UI
        artwork.sprite = data.artwork;
        nameText.text = data.cardName;
        statsText.text = data.stats;
        descText.text = data.description;
    }
//Toggle the visibility of the card back
public void ShowBack(bool value)
    {
        back.SetActive(value);
    }
}
