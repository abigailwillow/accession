using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI titleLabel;
    [SerializeField]
    private TextMeshProUGUI versionLabel;
    private void Awake() {
        titleLabel.text = Application.productName;
        if (Application.isEditor) {
            versionLabel.text = "Development";
        } else {
            versionLabel.text = Application.version;
        }
    }

    public void OnLevelSelectClicked() {
        throw new System.NotImplementedException();
    }

    public void OnSettingsButtonClicked() {
        throw new System.NotImplementedException();
    }

    public void OnQuitButtonClicked() {
        if (Application.isEditor) {
            UnityEditor.EditorApplication.ExitPlaymode();
        } else {
            Application.Quit();
        }
}
}
