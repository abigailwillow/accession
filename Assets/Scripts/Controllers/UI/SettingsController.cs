using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

namespace Accession.Controllers {
    public class SettingsController : MonoBehaviour {
        [SerializeField] private TMP_Dropdown dropdown;
        private void Awake() {
            LocalizationSettings.AvailableLocales.Locales.ForEach(locale => {
                dropdown.options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));
            });
        }

        public void OnLocaleChanged(TMP_Dropdown dropdown) {
            Debug.Log($"Locale changed to {dropdown.value}");
        }

        public void SwitchPanel(GameObject panel) {
            this.gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }
}