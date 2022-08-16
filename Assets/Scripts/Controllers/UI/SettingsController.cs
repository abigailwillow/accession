using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

namespace Accession.Controllers {
    public class SettingsController : MonoBehaviour {
        [SerializeField] private TMP_Dropdown dropdown;
        private List<Locale> locales = LocalizationSettings.AvailableLocales.Locales;
        private void Awake() {
            this.locales.ForEach(locale => {
                dropdown.options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));
            });
        }

        public void OnLocaleChanged(int option) {
            Debug.Log($"Locale changed to {locales[option].LocaleName}");
        }

        public void SwitchPanel(GameObject panel) {
            this.gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }
}