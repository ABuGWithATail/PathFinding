using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] StateMachine;
    public int WaypointIndex;
    public float speed;
    public GameObject Sphere;
    private Transform Target;
    private Transform Mover;
    public bool increase;

    void Awake()
    {
        Mover = Sphere.transform;


        // Update is called once per frame

    }
    void Start()
    {
        increase = true;
    }
    void Update()
    {
        Target = StateMachine[WaypointIndex].transform;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(Mover.position, Target.position, step);
        if (Mover.position == Target.position && increase == true)//distance < 0.01f
        {

            if (WaypointIndex >= 49)
            {
                increase = false;
            }

            WaypointIndex++;


        }
        if (Mover.position == Target.position && increase == false)//distance < 0.01f
        {

            if (WaypointIndex <= 0)
            {
                increase = true;
            }

            WaypointIndex--;


        }
    }
}
