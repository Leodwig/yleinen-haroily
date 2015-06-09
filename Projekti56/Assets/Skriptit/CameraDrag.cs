    using UnityEngine;
     
    public class CameraDrag : MonoBehaviour
    {
        //public float dragSpeed = 2;
        //private Vector3 dragOrigin;
     
     
        //void Update()
        //{
          //  if (Input.GetMouseButtonDown(0))
            //{
              //  dragOrigin = Input.mousePosition;
                //return;
            //}
     
    //        if (!Input.GetMouseButton(0)) return;
     //
       //     Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
         //   Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
     
           // transform.Translate(move, Space.World);  
        //}
                  if(Input.GetMouseButtonDown(0))
             {
                 bDragging = true;
                 oldPos = transform.position;
                 panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);                    //Get the ScreenVector the mouse clicked
             }
     
             if(Input.GetMouseButton(0))
             {
                 Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;    //Get the difference between where the mouse clicked and where it moved
                 transform.position = oldPos + -pos * panSpeed;                                         //Move the position of the camera to simulate a drag, speed * 10 for screen to worldspace conversion
             }
     
             if(Input.GetMouseButtonUp(0))
             {
                 bDragging = false;
             }
     
    }
