using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    public UIDocument uiDocument;

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        // Butonları tanımla
        Button micButton = root.Q<Button>("MicButton");
        Button volumeButton = root.Q<Button>("VolumeButton");
        Label statusLabel = root.Q<Label>("StatusLabel");

        // GPS yazısına tıklanınca değiştirme
        statusLabel.RegisterCallback<ClickEvent>(ev =>
        {
            statusLabel.text = statusLabel.text == "GPS Kapalı" ? "GPS Açık" : "GPS Kapalı";
        });

        // Ses butonu
        volumeButton.clicked += () =>
        {
            Debug.Log("Ses butonuna tıklandı!");
        };

        // Mikrofon butonu
        micButton.clicked += () =>
        {
            Debug.Log("Mikrofon butonuna tıklandı!");
        };
    }
}
