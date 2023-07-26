using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private Timer TimeController;
   [SerializeField] private DialogueUI dialogueUI;
    public List<float> positionx;
    public List<float> positiony;
    public List<float> positionz;
    public List<float> positionxSave;
    public List<float> positionySave;
    public List<float> positionzSave;
    public float positionX;
    public float positionY;
    public float positionZ;
    private Vector2 vector2;

    public IInteractable Interactable {get; set;}

    public DialogueUI DialogueUI => dialogueUI;
    public Rigidbody2D rb;

    private float inputHorizontal;
    private float inputVertical;
    public float moveSpeed = 5f;
    public float speedLimiter = 0.7f;


    void Start()
    {
        positionx = new List<float>();
        positiony = new List<float>();
        positionz = new List<float>();
        positionxSave = new List<float>();
        positionySave = new List<float>();
        positionzSave = new List<float>();

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

       
    }
 
    void Update()
    {
        if (TimeController.Rewinding == true)
        {
          Rewind();
        }

      if(Input.GetKeyDown(KeyCode.F) && TimeController.updateTimer == true)
      {
        if(Interactable != null)
        {
          Interactable.Interact(this);
        }
      }


        if (TimeController.updateTimer == false)
        {

            rb.velocity = Vector2.zero;
        }

        if(TimeController.updateTimer == true)
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");
        }
        else
        {
            inputHorizontal = 0;
            inputVertical = 0;
        }



    }

    void FixedUpdate()
    {
      if(TimeController.updateTimer == true)
      {
        Record();
      }

        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            rb.velocity = new Vector2(inputHorizontal * moveSpeed, inputVertical * moveSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
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

    }
    else{
        TimeController.Rewinding = false;
    }
}
    public void SavePosition()
    {
        if(TimeController.updateTimer == false && TimeController.time.Count > 0)
        {
       positionxSave.Clear();
       positionxSave.AddRange(positionx);
       positionySave.Clear();
       positionySave.AddRange(positiony);
       positionzSave.Clear();
       positionzSave.AddRange(positionz);
        }


    }

    public void LoadPosition()
    {
      if(TimeController.updateTimer == false && positionxSave.Count > 0)
        {
       positionx.Clear();
       positionx.AddRange(positionxSave);
       positiony.Clear();
       positiony.AddRange(positionySave);
       positionz.Clear();
       positionz.AddRange(positionzSave);
            float positionX = positionxSave[0];
            float positionY = positionySave[0];
            float positionZ = positionzSave[0];
            transform.position = new Vector3(positionX, positionY, positionZ);
            positionx.RemoveAt(0);
            positiony.RemoveAt(0);
            positionz.RemoveAt(0);
        }




    }
}
