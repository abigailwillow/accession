using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI versionText;
    private void Awake() {
        titleText.text = Application.productName;
        #if UNITY_EDITOR
            versionText.text = "Development";
        #else
            versionText.text = Application.version;
        #endif
        versionText.text = Application.version;
    }

    public void OnLevelSelectClicked() {
        throw new System.NotImplementedException();
    }

    public void OnSettingsButtonClicked() {
        throw new System.NotImplementedException();
    }

    public void OnQuitButtonClicked() {
        Application.Quit();
    }
}
