using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Accession.Managers;

namespace Accession.Controllers {
    public class LevelSelectController : MonoBehaviour {
        [SerializeField] private GameObject levelSelectGrid;
        [SerializeField] private GameObject levelSelectButton;

        private void Awake() {
            Resources.LoadAll<TextAsset>("Levels").ToList().ForEach(textAsset => {
                if (textAsset.name.Contains("Level")) {
                    string levelName = Regex.Match(textAsset.name, @"\d+").Value;
                    GameObject levelSelectButton = Instantiate(this.levelSelectButton, this.levelSelectGrid.transform);
                    levelSelectButton.GetComponentInChildren<TextMeshProUGUI>().text = levelName;
                    levelSelectButton.GetComponent<Button>().onClick.AddListener(() => GameManager.instance.LoadLevel($"Levels/{textAsset.name}"));
                }
            });
        }
    }
}