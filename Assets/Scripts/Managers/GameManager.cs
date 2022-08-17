using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
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
        }

        public void LoadLevel(string path) {
            SceneManager.sceneLoaded += SceneLoadedEventListener(path);
            SceneManager.LoadScene("Board Scene");
        }

        private UnityAction<Scene, LoadSceneMode> SceneLoadedEventListener(string path) {
            return (_, _) => {
                BoardController.instance.LoadBoard(path);
                SceneManager.sceneLoaded -= SceneLoadedEventListener(path);
            };
        }
    }
}
