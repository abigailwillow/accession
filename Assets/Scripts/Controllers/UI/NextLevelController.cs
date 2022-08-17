using UnityEngine;
using Accession.Managers;

namespace Accession.Controllers {
    public class NextLevelController : MonoBehaviour {
        [SerializeField] GameObject panel;

        private void Awake() {
            BoardController.instance.onBoardCompleted += OnBoardCompleted;
            this.panel.SetActive(false);
        }

        private void OnBoardCompleted() => this.panel.SetActive(true);

        // TODO: Check if there is actually a next level before loading it
        public void OnNextLevelClicked() => GameManager.instance.LoadLevel($"Levels/Level{++GameManager.instance.level}");
    }
}