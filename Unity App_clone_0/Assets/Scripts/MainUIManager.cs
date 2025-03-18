using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    private UIDocument uiDocument;

    private void Awake()
    {
        // UIDocument component'ini otomatik al
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("MainUI GameObject'inde UIDocument component'i bulunamadı!");
        }
    }

    private void OnEnable()
    {
        if (uiDocument == null) return; // UIDocument yoksa çık

        var root = uiDocument.rootVisualElement;
        if (root == null) return; // Root element yoksa çık

        // Butonları tanımla
        Button micButton = root.Q<Button>("MicButton");
        Button volumeButton = root.Q<Button>("VolumeButton");
        Label statusLabel = root.Q<Label>("StatusLabel");

        // GPS yazısına tıklanınca değiştirme
        if (statusLabel != null)
        {
            statusLabel.RegisterCallback<ClickEvent>(ev =>
            {
                statusLabel.text = statusLabel.text == "GPS Kapalı" ? "GPS Açık" : "GPS Kapalı";
            });
        }

        // Ses butonu
        if (volumeButton != null)
        {
            volumeButton.clicked += () =>
            {
                Debug.Log("Ses butonuna tıklandı!");
            };
        }

        // Mikrofon butonu
        if (micButton != null)
        {
            micButton.clicked += () =>
            {
                Debug.Log("Mikrofon butonuna tıklandı!");
            };
        }
    }
}
