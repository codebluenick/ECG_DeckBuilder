using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Required for .Select()

/// <summary>
/// Manages the player's current hand of cards, handles card addition logic and hand capacity limits.
/// </summary>
public class HandSystem
{
    // Fetch the list of CardData currently in the player's hand
    private List<CardData> hand = new List<CardData>();
    
    private int max = 8;//Max no. of cards that the player can handle at once

    // Func to add a card to the hand if there is remaining capacity.
    public void Add(CardData card)
    {
        if (hand.Count < max)
        {
            hand.Add(card);
            Debug.Log($"Added {card.cardName} to hand. Current count: {hand.Count}");
        }
        else
        {
            Debug.Log("Hand is full! Cannot add more cards.");
        }
    }

    //Helper to check if the hand has reached its maximum capacity.
    public bool IsFull() => hand.Count >= max;

    // Extracts a list of unique IDs from the cards in hand. (Useful for syncing with UI or sending data to a server/save file.)
    public List<string> GetIDs()
    {
        // Transforms the list of CardData objects into a list of their ID strings
        return hand.Select(c => c.id).ToList();
    }
    
    // Func to remove a specific card from the hand when played.
    public void Remove(CardData card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
        }
    }
}