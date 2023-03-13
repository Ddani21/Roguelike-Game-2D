using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthBar : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField]private Slider slider;
    public static BossHealthBar instance;
    private void Awake(){
        instance = this;
    }

    public void SetHealth(int health){
        slider.value = health;
        
    }
    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
        
    }
}
