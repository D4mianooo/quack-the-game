using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat<T>{
    [SerializeField] private T _baseValue;
    public T GetValue(){
        return _baseValue;
    }
}


public class PlayerStats : MonoBehaviour{
    [SerializeField] private float experience;
    [SerializeField] public Stat<int> _attackDamage;
    [SerializeField] public Stat<float> _attackSpeed;
    
    private GameHUD _gameHUD;
    
    private void Awake() {
        _gameHUD = FindObjectOfType<GameHUD>();
    }
    
    public void AddExperience(float exp){
        experience += exp;
        _gameHUD.UpdateExperienceSlider(exp);
    }
    
}
