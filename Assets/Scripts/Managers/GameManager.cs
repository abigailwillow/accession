using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using Accession.Controllers;

namespace Accession.Managers {
    public class GameManager : MonoBehaviour {
        public static GameManager instance { get; private set; }

        private void Awake() {
            if (instance != null && instance != this) {
                Destroy(this.gameObject);
            } else {
                instance = this;
            }

            LocalizationSettings.InitializationOperation.Completed += (_) => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("locale", 0)];
        }

        public void LoadLevel(string path) {
            SceneManager.sceneLoaded += SceneLoadedEventListener(path);
            SceneManager.LoadScene("Board Scene");
        }

        public void SaveLevel(int level) => PlayerPrefs.SetInt("level", level);

        private UnityAction<Scene, LoadSceneMode> SceneLoadedEventListener(string path) {
            return (_, _) => {
                BoardController.instance.LoadBoard(path);
                SceneManager.sceneLoaded -= SceneLoadedEventListener(path);
            };
        }
    }
}
