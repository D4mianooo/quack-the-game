using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private GameHUD gameHUD;
    [SerializeField] private int _maxAmmo;
    private int _currentAmmo;
    public int CurrentAmmo{ 
        get{ 
            return _currentAmmo;
        } 
        set{
                _currentAmmo = value;
                if(_currentAmmo > _maxAmmo) _currentAmmo = _maxAmmo;
                    gameHUD.UpdateUIClips(_currentAmmo, _maxAmmo);
        }
    }
    
    private void Awake() {
        CurrentAmmo = _maxAmmo;
    }
    
    public void ResetAmmo(){
        CurrentAmmo = _maxAmmo;
    }
}
