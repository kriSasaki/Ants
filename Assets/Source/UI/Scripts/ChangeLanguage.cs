using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage: MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;
    [SerializeField] private string _language;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(Change);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Change);
    }

    private void Change()
    {
        _leanLocalization.SetCurrentLanguage(_language);
    }
}
