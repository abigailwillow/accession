using UnityEngine;
using Accession.Managers;

namespace Accession.Controllers {
    public class NextLevelController : MonoBehaviour {
        [SerializeField] GameObject panel;

        private void Awake() => BoardController.instance.onBoardCompleted += OnBoardCompleted;

        private void OnBoardCompleted() => panel.SetActive(true);

        public void OnNextLevelClicked() {
            // TODO: Save progress to retrieve latest completed level and get next level;
            GameManager.instance.LoadLevel("Levels/Level2");
        }
    }
}