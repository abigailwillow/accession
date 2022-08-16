using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Accession.Controllers;

namespace Accession.Managers {
    public class GameManager : MonoBehaviour {
        public static GameManager instance { get; private set; }

        private void Awake() {
            instance ??= this;
            if (instance != null && instance != this) Destroy(this);

            DontDestroyOnLoad(this);
        }

        public void LoadLevel(string path) {
            //StartCoroutine(this.LoadLevelAsync(path));
            SceneManager.activeSceneChanged += (_, _) => {
                BoardController.instance.LoadBoard(path);
            };
            SceneManager.LoadScene("Board Scene");
        }

        // private IEnumerator LoadLevelAsync(string path) {
        //     SceneManager.LoadScene("Board Scene");
            
        //     while(SceneManager.Active)

        //     SceneManager.GetActiveScene().GetRootGameObjects().ToList().Find(gameObject => gameObject.GetComponent<BoardController>() != null).GetComponent<BoardController>().LoadBoard(path);
        // }
    }
}
