using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    // Start is called before the first frame update
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !rope.enabled)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin: transform.position,
                direction: Vector2.zero,
                distance: Mathf.Infinity,
                layerMask: grappleLayer
            ) ;

            if (hit.collider != null && (grappleLayer.value & (1 << hit.collider.gameObject.layer)) > 0) // assigns grappler point to ceiling
            {
                grapplePoint = hit.point;
                grapplePoint.z = 0;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                rope.SetPosition(0, grapplePoint);
                rope.SetPosition(1, transform.position);
                rope.enabled = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.F) && rope.enabled) {

            joint.enabled = false;
            rope.enabled = false;
        }

        if (rope.enabled)
        {
            rope.SetPosition(0, grapplePoint);
            rope.SetPosition(1, transform.position);
        }
    }
}
