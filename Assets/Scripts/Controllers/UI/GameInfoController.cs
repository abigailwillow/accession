using UnityEngine;
using TMPro;

namespace Accession {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GameInfoController : MonoBehaviour {
        void Awake() {
            TextMeshProUGUI versionLabel = GetComponent<TextMeshProUGUI>();
            versionLabel.text = $"{Application.productName} {(Application.version != "" ? Application.version : "Development Build")}";
        }
    }
}