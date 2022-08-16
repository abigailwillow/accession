using UnityEngine;
using TMPro;

namespace Accession.Controllers {
    public class MainMenuController : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI titleLabel;
        [SerializeField] private TextMeshProUGUI versionLabel;
        [SerializeField] private GameObject levelSelectPanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject quitButton;

        private void Awake() {
            titleLabel.text = Application.productName;
            versionLabel.text = (Application.version != "" ? Application.version : "Development Build");

            #if UNITY_WEBGL
                quitButton.SetActive(false);
            #endif
        }

        public void SwitchPanel(GameObject panel) {
            this.gameObject.SetActive(false);
            panel.SetActive(true);

        }

        public void OnQuitButtonClicked() {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
            #else
                Application.Quit();
            #endif
        }
    }
}