using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour {
    private GameObject Cut;

    private LineRenderer LineRenderer;
    public Material KatanaM;
    
    private EdgeCollider2D EdgeCollider;

    //Lineとcolliderの点
    private List<Vector2> Keypoint = new List<Vector2>();
    private List<Vector3> Keypoint3D = new List<Vector3>();

    private int PointCounter = 0;

    private bool CheckExist = false;

	void Start () {
        Cut = new GameObject("Line");
        Input.multiTouchEnabled = false;
        //define line renderer    
        LineRenderer = Cut.AddComponent<LineRenderer>();
        LineRenderer.material = KatanaM;
        LineRenderer.startWidth = 0.3f; //0.3F
        LineRenderer.endWidth = 0.13f; //0.13F
        LineRenderer.positionCount = 0;
        //define line renderer

        Rigidbody2D Rigidbody = Cut.AddComponent<Rigidbody2D>();
        Rigidbody.gravityScale = 0;
        Cut.transform.position = new Vector3(30, 30, 3);
    }
	
	void FixedUpdate () {
		if(Input.touchCount>0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Cut.GetComponent<EdgeCollider2D>() != null)
                {
                    Destroy(Cut.GetComponent<EdgeCollider2D>());
                }
                EdgeCollider = Cut.AddComponent<EdgeCollider2D>();
                CheckExist = true;
            }

            if(CheckExist)
            {
                Vector3 TouchPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 3);
                Vector3 TouchPositionReal = new Vector3(Camera.main.ScreenToWorldPoint(TouchPosition).x,
                    Camera.main.ScreenToWorldPoint(TouchPosition).y, Camera.main.ScreenToWorldPoint(TouchPosition).z);

                Keypoint3D.Add(TouchPositionReal);

                Vector2 TouchPosition2D= new Vector2(Camera.main.ScreenToWorldPoint(TouchPosition).x-30, Camera.main.ScreenToWorldPoint(TouchPosition).y-30);

                Keypoint.Add(TouchPosition2D);
            }
                
        }

        if(Keypoint.Count > 1)
        {
            //set edgecollider point
            EdgeCollider.points = Keypoint.ToArray();
            //set linerenderer point
            LineRenderer.positionCount = Keypoint3D.Count;
            LineRenderer.SetPositions(Keypoint3D.ToArray());
        }

        if(PointCounter < 20)
        {
            PointCounter++;
        }

        //List Remove
        if(PointCounter > 19)
        {
            if(Keypoint.Count > 1)
            {
                Keypoint.RemoveAt(0);          //EdgeCollider
                Keypoint3D.RemoveAt(0);        //Linerenderer
            }
            //reset timer
            if(Keypoint.Count < 2)
            {
                PointCounter = 0;
            }
        }

        if (Input.touchCount <= 0)   //when mouse isn't keeping down
        {
            //remove line
            LineRenderer.positionCount = 0;

            //remove edge collision
            Destroy(EdgeCollider);
            Keypoint = new List<Vector2>();
            Keypoint3D = new List<Vector3>();
            CheckExist = false;
        }            

	}
}
