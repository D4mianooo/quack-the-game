using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _clips;
    [SerializeField] private TMP_Text _framerate;
    [SerializeField] private Slider _experience;
    
    private void Start(){
        InvokeRepeating("UpdateFramerateUI", 0f, 0.5f);
    }

    private void UpdateFramerateUI(){
        _framerate.text = $"{Mathf.RoundToInt(1f / Time.deltaTime)} FPS";
    }

    public void UpdateUIClips(int current, int max){
        _clips.text = $"{current}/{max}";
    }
    public void UpdateExperienceSlider(float experience){
        _experience.value += 1f / experience;
    }
}
