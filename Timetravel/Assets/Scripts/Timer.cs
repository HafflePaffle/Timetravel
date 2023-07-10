using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class Timer : MonoBehaviour
{
    public float timerInSecond = 0f;
    public float levelTimer = 0.0f;
    public bool updateTimer = false;
    public Slider slider;
    [SerializeField] private TextMeshProUGUI slidertext;
    [SerializeField] private PostProcessVolume PPVolume;
    private ColorGrading CG;
    public List<float> time;
    public List<float> timeSave;
    public bool Rewinding = false;
    public int maxTime;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private GameObject saveLoadButtons;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject loadMark;
    [SerializeField] private GameObject timelineEndDot;


    
 
void Start()
{
   updateTimer = true;
   levelTimer = 0.0f;
   slider.onValueChanged.AddListener((v) =>{ slidertext.text = v.ToString("0");
        });

    PPVolume.profile.TryGetSettings(out CG);
    time = new List<float>();
    timeSave = new List<float>();

 
}
 
void Update()
{
   if (updateTimer)  
      levelTimer += Time.deltaTime*1;

//Rewind time arrgh
 if(updateTimer == false && Input.GetKey(KeyCode.Q) && dialogueUI.IsOpen == false && pauseMenu.isPaused == false)
    {
        Rewinding = true;
    }else
    {
        Rewinding = false;
    }

      if(Rewinding == true)
      {
          Rewind();
      }
     
 
    /// float to int
    timerInSecond = Mathf.Round (levelTimer);

    slider.value = timerInSecond;


    //Pausing when the time runs out
    if (levelTimer >maxTime)
    {
        updateTimer = false;
    }

    //Push E to pause time
    if (Input.GetKeyDown(KeyCode.E) && levelTimer <maxTime && Rewinding == false && pauseMenu.isPaused == false)
    {
        if(updateTimer == true)
        {
            updateTimer = false;
        }
        else
        {
            updateTimer = true;
        }
        
    }

    //Turns everything grayscale when paused
    if(updateTimer == false)
    {
        CG.active = true;
        saveLoadButtons.SetActive(true);

    }
    else{
       CG.active = false;
       saveLoadButtons.SetActive(false);
    }

    if(dialogueUI.IsOpen == true)
    {
        updateTimer = false;
    }
}
 
void FixedUpdate()
{
      if(updateTimer == true)
      {
        Record();
      }
}

void Record()
{
    time.Insert(0, levelTimer);

     
}

void Rewind()
{
    if(time.Count > 0)
    {
    
    levelTimer = time[0];
    time.RemoveAt(0);
    }
    else{
        Rewinding = false;
    }
    
}

public void CGToggle(bool value)
{
    CG.active = value;
}

void LevelEnded()
{
   updateTimer = false;
 
   ///Save Time
   PlayerPrefs.SetFloat("Time In Second", timerInSecond );
}

public void SaveTime()
{
    if(updateTimer == false && time.Count > 0)
    {
    timeSave.Clear();
    timeSave.AddRange(time);

    //destroys the load time marker if there are any
     GameObject[] marks = GameObject.FindGameObjectsWithTag("markClone");
     foreach (GameObject markClone in marks)
     {
        GameObject.Destroy(markClone);
     }
                
     //creates the load time marker
    GameObject childprefab = Instantiate(loadMark, timelineEndDot.transform.position, Quaternion.identity) as GameObject;
    childprefab.transform.SetParent(GameObject.Find("UI Canvas").transform);
    childprefab.tag = "markClone";
        }

}

public void LoadTime()
{
    if(updateTimer == false && timeSave.Count > 0)
    {
    time.Clear();
    time.AddRange(timeSave);
    levelTimer = time[0];
    }


    
  

}


}
