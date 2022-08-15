using UnityEngine;
using TMPro;

namespace Accession {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GameInfoController : MonoBehaviour {
        void Awake() {
            #if UNITY_EDITOR
                GetComponent<TextMeshProUGUI>().text = $"{Application.productName} Development";
            #else
                GetComponent<TextMeshProUGUI>().text = $"{Application.productName} {Application.version}";
            #endif
        }
    }
}