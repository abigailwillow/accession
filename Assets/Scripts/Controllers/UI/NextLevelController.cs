using UnityEngine;
using Accession.Managers;

namespace Accession.Controllers {
    public class NextLevelController : MonoBehaviour {
        [SerializeField] GameObject panel;

        private void Awake() {
            BoardController.instance.onBoardCompleted += OnBoardCompleted;
            this.panel.SetActive(false);
        }

        private void OnBoardCompleted() {
            this.panel.SetActive(true);

            if (Resources.Load($"Levels/Level{GameManager.instance.level + 1}") == null) {
                this.gameObject.SetActive(false);
            }
        }

        public void OnNextLevelClicked() { 
                GameManager.instance.LoadLevel($"Levels/Level{++GameManager.instance.level}");
        }
    }
}