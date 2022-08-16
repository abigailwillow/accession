using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Accession.Controllers {
    public class LevelSelectController : MonoBehaviour {
        [SerializeField] private GameObject levelSelectGrid;
        [SerializeField] private GameObject levelSelectButton;

        private void Awake() {
            Resources.FindObjectsOfTypeAll<TextAsset>().ToList().ForEach(textAsset => {
                if (textAsset.name.Contains("Level")) {
                    string levelName = Regex.Match(textAsset.name, @"\d+").Value;
                    GameObject levelSelectButton = Instantiate(this.levelSelectButton, this.levelSelectGrid.transform);
                    levelSelectButton.GetComponentInChildren<TextMeshProUGUI>().text = levelName;
                    levelSelectButton.GetComponent<Button>().onClick.AddListener(() => {
                        this.LoadLevel(textAsset.name);
                    });
                }
            });
        }

        private void LoadLevel(string path) {
            SceneManager.LoadScene("Board Scene", LoadSceneMode.Additive);
            BoardController.instance.LoadBoard(path);
            SceneManager.UnloadSceneAsync("Main Menu");
        }
    }
}