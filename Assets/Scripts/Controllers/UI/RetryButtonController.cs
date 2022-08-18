using UnityEngine;
using Accession.Managers;

namespace Accession.Controllers {
    public class RetryButtonController : MonoBehaviour {
        [SerializeField] GameObject panel;

        private void Awake() {
            this.panel.SetActive(false);

            BoardController.instance.onBoardFailed += OnBoardFailed;
        }

        private void OnBoardFailed() => this.panel.SetActive(true);

        public void OnRetryButtonClicked() => GameManager.instance.LoadLevel($"Levels/Level{GameManager.instance.level}");
    }
}