using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class NPCWalk : MonoBehaviour
{
    [SerializeField] private Timer TimeController;
    public float speed;
    private float walkingSpeed;
    private Vector3 dir = Vector3.left;
    private List<float> positionx;
    private List<float> positiony;
    private List<float> positionz;
    private float positionX;
    private float positionY;
    private float positionZ;
    public List<GameObject> npcList;
    private List<float> positionxSave;
    private List<float> positionySave;
    private List<float> positionzSave;

    /*public Sprite normalSprite;
    public Sprite hoverSprite;
    private SpriteRenderer spriteR;*/

    //Waypoint system
    public GameObject[] waypoints;
    private int currentPoint = 0;
    float WPRadius = 1;
    public float waitTime;
    public bool stopped = false;
    private List<int> waypointTime;




    void Start()
    {
        positionx = new List<float>();
        positiony = new List<float>();
        positionz = new List<float>();
        positionxSave = new List<float>();
        positionySave = new List<float>();
        positionzSave = new List<float>();

        npcList = new List<GameObject>();
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("npc");       
        npcList.AddRange(npcs);

        //spriteR = gameObject.GetComponent<SpriteRenderer>();

        waypointTime = new List<int>();
        
    }



 void Update(){
       
      if(TimeController.updateTimer == true)
      {
          walkingSpeed = speed;
      }else
      {
          walkingSpeed = 0;
      }
    


      if(TimeController.Rewinding == true)
      {
          Rewind();
      }

      /*if(TimeController.updateTimer == false)
        {
            spriteR.sprite = normalSprite;
        }*/

 }

    /*void OnMouseOver()
    {
        if(TimeController.updateTimer == true)
        {
            spriteR.sprite = hoverSprite;
        }

    }

    void OnMouseExit()
    {
        spriteR.sprite = normalSprite;
    }*/
       
 
void FixedUpdate()
{
      if(TimeController.updateTimer == true)
      {
        Record();
      }


        if (Vector3.Distance(waypoints[currentPoint].transform.position, transform.position) < WPRadius && stopped == false)
        {
            waypointTime.Clear();
            waypointTime.Insert(0, currentPoint);
            stopped = true;
            Invoke(nameof(updateCurrentPoint), waitTime);

        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint].transform.position, Time.deltaTime * walkingSpeed);

    }

    


void Record()
{
    positionx.Insert(0, transform.position.x);
    positiony.Insert(0, transform.position.y);
    positionz.Insert(0, transform.position.z);


}

void Rewind()
{
    if(positionx.Count > 0)
    {
    
    float positionX = positionx[0];
    float positionY = positiony[0];
    float positionZ = positionz[0];
    transform.position = new Vector3(positionX, positionY, positionZ);

    positionx.RemoveAt(0);
    positiony.RemoveAt(0);
    positionz.RemoveAt(0);

        if(!waypointTime.Contains(currentPoint) && waypointTime.Count > 0)
            {
                
                currentPoint = waypointTime[0];
            }

    }
    else{
        TimeController.Rewinding = false;
    }
    
}

public void SavePosition()
{

       positionxSave.Clear();
       positionxSave.AddRange(positionx);
       positionySave.Clear();
       positionySave.AddRange(positiony);
       positionzSave.Clear();
       positionzSave.AddRange(positionz);
    
}

public void LoadPosition()
{
       positionx.Clear();
       positionx.AddRange(positionxSave);
       positiony.Clear();
       positiony.AddRange(positionySave);
       positionz.Clear();
       positionz.AddRange(positionzSave);
       
    float positionX = positionx[0];
    float positionY = positiony[0];
    float positionZ = positionz[0];
    transform.position = new Vector3(positionX, positionY, positionZ);
        positionx.RemoveAt(0);
    positiony.RemoveAt(0);
    positionz.RemoveAt(0);

        if (!waypointTime.Contains(currentPoint) && waypointTime.Count > 0)
        {

            currentPoint = waypointTime[0];
        }



    }

    private void updateCurrentPoint()
    {
        if(transform.position == waypoints[currentPoint].transform.position)
        currentPoint++;
        if (currentPoint >= waypoints.Length)
        {
            currentPoint = 0;
        }
        stopped = false;
    }

}
    

