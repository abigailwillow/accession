using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GameInfoController : MonoBehaviour {
    void Awake() {
        GetComponent<TextMeshProUGUI>().text = $"{Application.productName} {Application.version}";
    }
}
