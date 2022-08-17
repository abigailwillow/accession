using UnityEngine;
using UnityEngine.SceneManagement;

namespace Accession.Controllers {
    public class MenuButtonController : MonoBehaviour {
        public void OnClick() => SceneManager.LoadScene("Main Menu");
    }
}