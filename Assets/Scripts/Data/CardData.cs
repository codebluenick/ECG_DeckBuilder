using UnityEngine;
/// <summary>
/// Defines the data structure for the game card asset
/// </summary>
[CreateAssetMenu(menuName = "Card")]
public class CardData : ScriptableObject
{
    public string id;
    public string cardName;
    public int cost;
    public string stats;
    public string description;
    public Sprite artwork;
}