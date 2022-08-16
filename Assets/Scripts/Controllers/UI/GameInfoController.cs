using UnityEngine;
using TMPro;

namespace Accession {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GameInfoController : MonoBehaviour {
        void Awake() {
            TextMeshProUGUI versionLabel = GetComponent<TextMeshProUGUI>();
            if (Application.isEditor) {
                versionLabel.text = $"{Application.productName} Development";
            } else {
                versionLabel.text = $"{Application.productName} {Application.version}";
            }
        }
    }
}