using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katana : MonoBehaviour {
    private GameObject cut;

    private LineRenderer lineRenderer;
    public Material katana_m;

    private EdgeCollider2D ec;
    private bool is_ecexist = false; //edgecollider`s flag

    //each points of line and collider
    private List<Vector2> keypoint = new List<Vector2>();  
    private List<Vector3> keypoint_3d = new List<Vector3>();
    //each points of line and collider

    private int ctrl = 0;//Destory Controller

    void Start() {
        cut = new GameObject("Line");
        cut.layer = 14;
        //define line renderer
        cut.AddComponent<LineRenderer>();
        lineRenderer = cut.GetComponent<LineRenderer>();
        lineRenderer.material = katana_m; 
        lineRenderer.startWidth = 0.15F;
        lineRenderer.endWidth = 0.08f;
        lineRenderer.numPositions = 0;
        //define rigidbody
        Rigidbody2D rb = cut.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

    }

    void FixedUpdate()
    {
        //edition of using mouse    
        if (Input.GetMouseButton(0))             //when mouse is keeping down
        {

            if(!is_ecexist)
            {
                ec = cut.AddComponent<EdgeCollider2D>();

                is_ecexist = true;
            }

            if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)  //when mouse is moving
                                                                                  
            {
  
                Vector3 mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,5);
                Vector3 mPosition_real = new Vector3(Camera.main.ScreenToWorldPoint(mPosition).x,
                    Camera.main.ScreenToWorldPoint(mPosition).y, Camera.main.ScreenToWorldPoint(mPosition).z);

                keypoint_3d.Add(mPosition_real);

                Vector2 mposition2D = new Vector2(Camera.main.ScreenToWorldPoint(mPosition).x, Camera.main.ScreenToWorldPoint(mPosition).y);

                keypoint.Add(mposition2D);                    
            }
        }

        if (keypoint.Count > 1)
        {
            //set edgecollider point
            ec.points = keypoint.ToArray();
            //set linerenderer point
            lineRenderer.numPositions = keypoint_3d.Count;
            lineRenderer.SetPositions(keypoint_3d.ToArray());
        }

        if (ctrl < 20)
        {
            ctrl++;
        }

        //List Remove
        if (ctrl > 19)
        {
            if (keypoint.Count > 1)
            {
                keypoint.RemoveAt(0);       //EdgeCollider
                keypoint_3d.RemoveAt(0);    //linerenderer
            }
            //reset Timer
            if (keypoint.Count < 2)
            {
              ctrl = 0;
            }
        }


        if (!Input.GetMouseButton(0)) // when mouse isnt keeping down
        {
            //remove line
            lineRenderer.numPositions = 0;

            //remove edge collision
            Destroy(ec);
            is_ecexist = false;
            keypoint = new List<Vector2>();
            keypoint_3d = new List<Vector3>();

        }
    }

}