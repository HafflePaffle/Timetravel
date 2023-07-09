using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public NPCWalk[] npcWalks;
    [SerializeField] private Timer TimeController;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private Player player;
    


    private void Start()
    {
        // Find all NPCWalk scripts in the scene
        npcWalks = FindObjectsOfType<NPCWalk>();
    }

    public void SaveAllPositions()
    {
        if(TimeController.updateTimer == false && TimeController.time.Count > 0 && pauseMenu.isPaused == false)
        {
        // Save positions for all NPCWalk scripts
        foreach (NPCWalk npcWalk in npcWalks)
        {
            npcWalk.SavePosition();
        }
            TimeController.SaveTime();
            player.SavePosition();
        }

    }

    public void LoadAllPositions()
    {
        if(TimeController.updateTimer == false && TimeController.timeSave.Count > 0 && pauseMenu.isPaused == false)
        {
        // Load positions for all NPCWalk scripts
        foreach (NPCWalk npcWalk in npcWalks)
        {
            npcWalk.LoadPosition();
        }
            TimeController.LoadTime();
            player.LoadPosition();
        }
    }
}
