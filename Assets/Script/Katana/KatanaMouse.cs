using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaMouse : MonoBehaviour
{
    private GameObject Cut;

    private LineRenderer LineRenderer;
    public Material KatanaM;

    private EdgeCollider2D EdgeCollider;
    private bool IsEcexist = false; //edgecollider's flag

    //each points of line and collider
    private List<Vector2> Keypoint = new List<Vector2>();
    private List<Vector3> Keypoint3D = new List<Vector3>();
    //each points of line and collider

    private int PointCounter = 0;//Destory Controller

    // Use this for initialization
    void Start()
    {
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
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (!IsEcexist)
            {
                EdgeCollider = Cut.AddComponent<EdgeCollider2D>();

                IsEcexist = true;
            }

            if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
            {
                Vector3 MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
                Vector3 MousePositionReal = new Vector3(Camera.main.ScreenToWorldPoint(MousePosition).x,
                    Camera.main.ScreenToWorldPoint(MousePosition).y, Camera.main.ScreenToWorldPoint(MousePosition).z);

                Keypoint3D.Add(MousePositionReal);

                Vector2 MousePosition2D = new Vector2(Camera.main.ScreenToWorldPoint(MousePosition).x, Camera.main.ScreenToWorldPoint(MousePosition).y);

                Keypoint.Add(MousePosition2D);
            }

        }

        if (Keypoint.Count > 1)
        {
            //set edgecollider point
            EdgeCollider.points = Keypoint.ToArray();
            //set linerenderer point
            LineRenderer.numPositions = Keypoint3D.Count;
            LineRenderer.SetPositions(Keypoint3D.ToArray());
        }

        if (PointCounter < 20)
        {
            PointCounter++;
        }

        //List Remove
        if (PointCounter > 19)
        {
            if (Keypoint.Count > 1)
            {
                Keypoint.RemoveAt(0);          //EdgeCollider
                Keypoint3D.RemoveAt(0);        //Linerenderer
            }
            //reset timer
            if (Keypoint.Count < 2)
            {
                PointCounter = 0;
            }
        }

        if (!Input.GetMouseButton(0))   //when mouse isn't keeping down
        {
            //remove line
            LineRenderer.numPositions = 0;

            //remove edge collision
            Destroy(EdgeCollider);
            IsEcexist = false;
            Keypoint = new List<Vector2>();
            Keypoint3D = new List<Vector3>();
        }

    }
}
