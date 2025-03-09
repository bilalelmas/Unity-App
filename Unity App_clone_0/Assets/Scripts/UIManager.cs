using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public UIDocument uiDocument; // UI Document
    public VisualTreeAsset mainMenuUI;  // MainMenu.uxml
    public VisualTreeAsset createGameUI; // CreateGame.uxml

    private void Start()
    {
        ShowMainMenu(); // Açılışta ana menüyü göster
    }

    public void ShowMainMenu()
    {
        if (uiDocument == null || mainMenuUI == null)
        {
            Debug.LogError("UI Document veya MainMenu.uxml eksik!");
            return;
        }

        // UI temizle ve yeni ekranı ekle
        uiDocument.rootVisualElement.Clear();
        var menu = mainMenuUI.Instantiate();
        uiDocument.rootVisualElement.Add(menu);

        // "Oyun Kur" butonunu bağla
        Button startGameButton = menu.Q<Button>("StartGameButton");
        if (startGameButton != null)
            startGameButton.clicked += ShowCreateGame;
    }

    public void ShowCreateGame()
    {
        if (uiDocument == null || createGameUI == null)
        {
            Debug.LogError("UI Document veya CreateGame.uxml eksik!");
            return;
        }

        // UI temizle ve yeni ekranı ekle
        uiDocument.rootVisualElement.Clear();
        var createGame = createGameUI.Instantiate();
        uiDocument.rootVisualElement.Add(createGame);

        // **Geri dön butonunu bağla**
        Button backButton = createGame.Q<Button>("back-button");
        if (backButton != null)
            backButton.clicked += ShowMainMenu;
    }
}
