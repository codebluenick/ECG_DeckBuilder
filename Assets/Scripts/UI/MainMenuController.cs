using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the main menu logic - session intialization and navigation btw the DeckBuilder and DeckViewer
/// </summary>
public class MainMenuController : MonoBehaviour
{
    public GameObject continueBtn;

    void Start()
    {
         if (continueBtn != null)
         continueBtn.SetActive(UUIDManager.Exists());
    }
    //Func to overwrite existing id and start a fresh session
    public void OnNewUser()
    {
        // Generates a new GUID and saves it to PlayerPrefs
        UUIDManager.Create();
        
        // Transitions to the DeckBuilder scene for initial deck creation
        SceneManager.LoadScene("DeckBuilder");
    }
    //Func to continue session
    public void OnContinue()
    {
        // Transitions to the DeckViewer to see previously created cards/decks
        SceneManager.LoadScene("DeckViewer");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}