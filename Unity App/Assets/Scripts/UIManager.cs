using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private UIDocument uiDocument;
    public VisualTreeAsset mainMenuUI;  // MainMenu.uxml
    public VisualTreeAsset createGameUI; // CreateGame.uxml
    public VisualTreeAsset mainGameUI;

    // MainUI için gerekli değişkenler
    private Button micButton;
    private Button volumeButton;
    private Label statusLabel;
    private bool isMicActive = false;
    private bool isVolumeActive = true;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component'i bulunamadı!");
            return;
        }
    }

    private void Start()
    {
        // Direkt ShowMainMenu'yü çağır
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        if (mainMenuUI == null) return;

        // UI'ı temizle ve ana menüyü yükle
        var root = uiDocument.rootVisualElement;
        root.Clear(); // Mevcut UI'ı temizle
        var menu = mainMenuUI.Instantiate();
        root.Add(menu);

        var startGameButton = menu.Q<Button>("StartGameButton");
        if (startGameButton != null)
        {
            startGameButton.clicked += ShowCreateGame;
        }
    }

    public void ShowCreateGame()
    {
        if (createGameUI == null) return;

        var root = uiDocument.rootVisualElement;
        root.Clear();
        var createGame = createGameUI.Instantiate();
        root.Add(createGame);

        var backButton = createGame.Q<Button>("back-button");
        if (backButton != null)
        {
            backButton.clicked += ShowMainMenu;
        }

        var playerNameField = createGame.Q<TextField>("player-name");
        var startButton = createGame.Q<Button>("start-game-button");

        if (startButton != null && playerNameField != null)
        {
            startButton.SetEnabled(false);
            playerNameField.value = "";

            playerNameField.RegisterValueChangedCallback(evt =>
            {
                startButton.SetEnabled(!string.IsNullOrEmpty(evt.newValue.Trim()));
            });

            startButton.clicked += () => ShowMainGameUI();
        }
    }

    public void ShowMainGameUI()
    {
        if (mainGameUI == null) return;

        var root = uiDocument.rootVisualElement;
        root.Clear();
        var gameUI = mainGameUI.Instantiate();
        root.Add(gameUI);

        InitializeMainGameUI(gameUI);
    }

    private void InitializeMainGameUI(VisualElement gameUI)
    {
        // Butonları ve label'ı bul
        micButton = gameUI.Q<Button>("MicButton");
        volumeButton = gameUI.Q<Button>("VolumeButton");
        statusLabel = gameUI.Q<Label>("StatusLabel");

        // Butonların başlangıç durumlarını ayarla
        UpdateMicButtonUI();
        UpdateVolumeButtonUI();

        // GPS durumu için click event
        if (statusLabel != null)
        {
            statusLabel.RegisterCallback<ClickEvent>(ev =>
            {
                statusLabel.text = statusLabel.text == "GPS Kapalı" ? "GPS Açık" : "GPS Kapalı";
            });
        }

        // Mikrofon butonu için click event
        if (micButton != null)
        {
            micButton.clicked += () =>
            {
                isMicActive = !isMicActive;
                UpdateMicButtonUI();
                HandleMicrophoneToggle();
            };
        }

        // Ses butonu için click event
        if (volumeButton != null)
        {
            volumeButton.clicked += () =>
            {
                isVolumeActive = !isVolumeActive;
                UpdateVolumeButtonUI();
                HandleVolumeToggle();
            };
        }

        // Diğer butonları bul ve click event'lerini ekle
        SetupMainGameButtons(gameUI);
    }

    private void UpdateMicButtonUI()
    {
        if (micButton != null)
        {
            micButton.text = isMicActive ? "🎤" : "🎤";
            // İsterseniz mikrofon durumuna göre stil değişikliği yapabilirsiniz
            // micButton.style.backgroundColor = isMicActive ? Color.red : Color.white;
        }
    }

    private void UpdateVolumeButtonUI()
    {
        if (volumeButton != null)
        {
            volumeButton.text = isVolumeActive ? "🔊" : "🔈";
            // İsterseniz ses durumuna göre stil değişikliği yapabilirsiniz
            // volumeButton.style.backgroundColor = isVolumeActive ? Color.green : Color.gray;
        }
    }

    private void HandleMicrophoneToggle()
    {
        Debug.Log($"Mikrofon {(isMicActive ? "Açıldı" : "Kapatıldı")}");
        // Burada mikrofon işlemlerini yapabilirsiniz
    }

    private void HandleVolumeToggle()
    {
        Debug.Log($"Ses {(isVolumeActive ? "Açıldı" : "Kapatıldı")}");
        // Burada ses işlemlerini yapabilirsiniz
    }

    private void SetupMainGameButtons(VisualElement gameUI)
    {
        var buttonContainer = gameUI.Q("ButtonContainer");
        if (buttonContainer != null)
        {
            // Tüm butonları bul
            var buttons = buttonContainer.Query<Button>().ToList();
            foreach (var button in buttons)
            {
                button.clicked += () => HandleMainGameButtonClick(button.text);
            }
        }
    }

    private void HandleMainGameButtonClick(string buttonText)
    {
        switch (buttonText)
        {
            case "Bas Konuş":
                Debug.Log("Bas Konuş aktif");
                break;
            case "Sağırlaştır":
                Debug.Log("Sağırlaştır aktif");
                break;
            case "Harita":
                Debug.Log("Harita açıldı");
                break;
            case "Kendini Öldür":
                Debug.Log("Kendini öldür aktif");
                break;
            case "Ekran Kapa":
                Debug.Log("Ekran kapatıldı");
                break;
            case "GPS":
                Debug.Log("GPS tıklandı");
                break;
            case "Oyundan Çık":
                var root = uiDocument.rootVisualElement;
                root.Clear(); // Mevcut UI'ı temizle
                ShowMainMenu(); // Ana menüye dön
                break;
        }
    }
}
