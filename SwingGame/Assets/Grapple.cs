using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask wallLayer; // Layer for the walls
    [SerializeField] private LineRenderer rope;
    [SerializeField] private Transform wallCheck; // A point on the player to check for walls
    [SerializeField] private float wallCheckRadius; // How close to check for a wall
    [SerializeField] private DistanceJoint2D joint;

    private bool isGrappling = false;

    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    void Update()
    {
        // Check if 'F' key is pressed and the player is not already grappling

        if (Input.GetKeyDown(KeyCode.F) && !isGrappling)
        {
            // Detect walls around the WallCheck position
            Collider2D[] hits = Physics2D.OverlapCircleAll(wallCheck.position, wallCheckRadius, wallLayer);

            // Find the closest wall if there are multiple
            Collider2D closestWall = null;
            float closestDistance = float.MaxValue;
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject != gameObject) // Make sure it's not the player
                {
                    float distance = (wallCheck.position - hit.transform.position).sqrMagnitude;
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestWall = hit;
                    }
                }
            }

            // If a wall was found, attach the grappler to it
            if (closestWall != null)
            {
                // Here you might want to find the exact point on the collider to attach to, or just use the hit point
                Vector2 closestPoint = closestWall.ClosestPoint(wallCheck.position);
                joint.connectedAnchor = closestPoint;
                joint.enabled = true;
                joint.distance = grappleLength;

                rope.SetPosition(0, closestPoint);
                rope.SetPosition(1, transform.position);
                rope.enabled = true;

                isGrappling = true;
            }
        }

        // Check if 'F' key is released and the player is grappling
        if (Input.GetKeyUp(KeyCode.F) && isGrappling)
        {
            this.ReleaseGrapple();
        }

        // If the rope is enabled, update its position to follow the player and grapple point
        if (isGrappling)
        {
            rope.SetPosition(0, joint.connectedAnchor);
            rope.SetPosition(1, transform.position);
        }
    }

    public bool IsGrappling()
    {
        return isGrappling;
    }

    public void ReleaseGrapple()
    {
        joint.enabled = false;
        rope.enabled = false;
        isGrappling = false;

    }
    void OnDrawGizmosSelected()
    {
        if (wallCheck == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
    }
}
    