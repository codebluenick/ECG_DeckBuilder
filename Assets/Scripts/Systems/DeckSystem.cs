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

    //Func to initialize the deck by shuffling a list of card data and pushing them to the stack
    public void Init(List<CardData> cards)
    {
        deck.Clear();
        //Shuffle the cards using LINQ and Random.value then add them to the stack
        foreach(var c in cards.OrderBy(x => Random.value))
        {
            deck.Push(c);
        }

        Debug.Log($"Deck initialized with {deck.Count} cards.");
    }

    //Remove and return the top card from the deck
    public CardData Draw()
    {
        if(deck.Count > 0)
        {
            return deck.Pop();
        }

        Debug.LogWarning("Attempted to draw from an empty deck!");
        return null;
    }

    public int RemainingCards => deck.Count;//return the current number of cards remaining in the deck
}