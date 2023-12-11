using IJunior.TypedScenes;
using UnityEngine;

public class SceneLoadHandler : MonoBehaviour, ISceneLoadHandler<SceneLoadHandler>
{
    [SerializeField] private Player _player;
    [SerializeField] private WeaponChanger _weaponChanger;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LevelManager _levelManager;

    private int _goldAmount => _wallet.GoldAmount;
    private int _currentLevel => _levelManager.CurrentLevel;
    private int _openedLevels => _levelManager.OpenedLevels;
    private int _currentWeapon => _weaponChanger.CurrentWeapon;

    public void OnSceneLoaded(SceneLoadHandler argument)
    {
        _wallet.ChangeGoldAmount(argument._goldAmount);
        _levelManager.SaveLevels(argument._currentLevel, argument._openedLevels);
        _weaponChanger.ChangeScriptableObject(argument._currentWeapon);
    }
}
