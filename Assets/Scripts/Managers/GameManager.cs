using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using Accession.Controllers;

namespace Accession.Managers {
    public class GameManager : MonoBehaviour {
        public static GameManager instance { get; private set; }
        public int level;
        private string path;

        private void Awake() {
            if (instance != null && instance != this) {
                Destroy(this.gameObject);
                return;
            } else {
                instance = this;
            }

            LocalizationSettings.InitializationOperation.Completed += _ => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("locale", 0)];

            this.level = PlayerPrefs.GetInt("level", 1);

            SceneManager.sceneLoaded += OnSceneLoaded();
        }

        public void LoadLevel(string path) {
            this.path = path;
            SceneManager.LoadScene("Board Scene");
        }

        public void SaveLevel() {
            PlayerPrefs.SetInt("level", level);
            PlayerPrefs.Save();
        }

        private UnityAction<Scene, LoadSceneMode> OnSceneLoaded() => (_, _) => {
            if (BoardController.instance != null) {
                BoardController.instance.LoadBoard(this.path);
                Debug.Log($"Board loaded ({this.path})");
            }
        };
    }
}
