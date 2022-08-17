using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

namespace Accession.Controllers {
    public class SettingsController : MonoBehaviour {
        [SerializeField] private TMP_Dropdown dropdown;
        private List<Locale> locales;
        private void Awake() {
            locales = LocalizationSettings.AvailableLocales.Locales;

            this.locales.ForEach(locale => {
                dropdown.options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));
            });

            dropdown.value = locales.IndexOf(LocalizationSettings.SelectedLocale);
        }

        public void OnLocaleChanged(TMP_Dropdown dropdown) {
            LocalizationSettings.SelectedLocale = locales[dropdown.value];
            PlayerPrefs.SetInt("locale", dropdown.value);
            PlayerPrefs.Save();
            Debug.Log($"Locale changed to {locales[dropdown.value].LocaleName}");
        }

        public void SwitchPanel(GameObject panel) {
            this.gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }
}