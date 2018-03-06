using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour {
    private GameObject Cut;

    private LineRenderer LineRenderer;
    public Material KatanaM;
    
    private EdgeCollider2D EdgeCollider;

    //each points of line and collider
    private List<Vector2> Keypoint = new List<Vector2>();
    private List<Vector3> Keypoint3D = new List<Vector3>();
    //each points of line and collider

    private int PointCounter = 0;//Destory Controller

	// Use this for initialization
	void Start () {
        Cut = new GameObject("Line");
        //define line renderer    
        LineRenderer = Cut.AddComponent<LineRenderer>();
        LineRenderer.material = KatanaM;
        LineRenderer.startWidth = 0.15f;
        LineRenderer.endWidth = 0.08f;
        LineRenderer.numPositions = 0;
        //define line renderer

        Rigidbody2D Rigidbody = Cut.AddComponent<Rigidbody2D>();
        Rigidbody.gravityScale = 0;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.touchCount>0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                EdgeCollider = Cut.AddComponent<EdgeCollider2D>();
            }

            if(Input.GetTouch(0).phase==TouchPhase.Moved)
            {
                Vector3 TouchPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 5);
                Vector3 TouchPositionReal = new Vector3(Camera.main.ScreenToWorldPoint(TouchPosition).x,
                    Camera.main.ScreenToWorldPoint(TouchPosition).y, Camera.main.ScreenToWorldPoint(TouchPosition).z);

                Keypoint3D.Add(TouchPositionReal);

                Vector2 TouchPosition2D= new Vector2(Camera.main.ScreenToWorldPoint(TouchPosition).x, Camera.main.ScreenToWorldPoint(TouchPosition).y);

                Keypoint.Add(TouchPosition2D);
            }
                
        }

        if(Keypoint.Count > 1)
        {
            //set edgecollider point
            EdgeCollider.points = Keypoint.ToArray();
            //set linerenderer point
            LineRenderer.numPositions = Keypoint3D.Count;
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
            LineRenderer.numPositions = 0;

            //remove edge collision
            Destroy(EdgeCollider);
            Keypoint = new List<Vector2>();
            Keypoint3D = new List<Vector3>();
        }            

	}
}
