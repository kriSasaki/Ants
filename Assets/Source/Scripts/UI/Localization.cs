using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class Localization : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _leanLocalization;

        private string _russian = "Russian";
        private string _english = "English";
        private string _turkish = "Turkish";

        private void Start()
        {
            var currentLanguage = YandexGamesSdk.Environment.i18n.lang;

            switch (currentLanguage)
            {
                case "en":
                    _leanLocalization.SetCurrentLanguage(_english);
                    break;
                case "tr":
                    _leanLocalization.SetCurrentLanguage(_turkish);
                    break;
                case "ru":
                    _leanLocalization.SetCurrentLanguage(_russian);
                    break;
            }
        }
    }
}