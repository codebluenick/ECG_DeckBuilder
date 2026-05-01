using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Required for OrderBy

///<summary>
/// Script to manage the deck state during gameplay.
/// Handles shuffling and drawing mechanics using a Stack collection
/// </summary>
public class DeckSystem
{
    private Stack<CardData> deck = new Stack<CardData>();//stack would be ideal for decks (last-in/first-out)

}