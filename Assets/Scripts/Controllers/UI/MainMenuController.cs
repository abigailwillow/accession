using System.Data;
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
            versionLabel.text = (Application.version != "" ? Application.version : "Development");

            #if UNITY_WEBGL
                // TODO: Remove quit button as WebGL doesn't support it.
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