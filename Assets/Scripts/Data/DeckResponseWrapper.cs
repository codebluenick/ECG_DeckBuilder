using System.Collections.Generic;

[System.Serializable]
public class DeckResponseWrapper
{
    public DeckResponse record;
}

[System.Serializable]
public class DeckResponse
{
    public string user_id;
    public List<List<string>> decks;
}
