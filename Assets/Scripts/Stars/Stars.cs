using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StarLevel", menuName = "Star")]
public class Stars : ScriptableObject
{
    [Range(0, 4)]
    public int starCollected;
    /* starCollected=0 neprejdený level, žiadne hviezdy 
     * starCollected=1 žiadna hviezda   
     * starCollected=2 iba timer     
     * starCollected=3 iba secret    
     * starCollected=4 obe hviezdy  
    */
    public float timeReward;
    public bool LevelUnlocked;
}
