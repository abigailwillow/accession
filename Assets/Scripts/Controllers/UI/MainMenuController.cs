using UnityEngine;
using TMPro;

namespace Accession.Controllers {
    public class MainMenuController : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI titleLabel;
        [SerializeField] private TextMeshProUGUI versionLabel;
        [SerializeField] private GameObject levelSelectPanel;
        [SerializeField] private GameObject settingsPanel;

        private void Awake() {
            titleLabel.text = Application.productName;
            if (Application.isEditor) {
                versionLabel.text = "Development";
            } else {
                versionLabel.text = Application.version;
            }
        }

        public void SwitchPanel(GameObject panel) {
            this.gameObject.SetActive(false);
            panel.SetActive(true);
        }

        public void OnQuitButtonClicked() {
            if (Application.isEditor) {
                UnityEditor.EditorApplication.ExitPlaymode();
            } else {
                Application.Quit();
            }
        }
    }
}