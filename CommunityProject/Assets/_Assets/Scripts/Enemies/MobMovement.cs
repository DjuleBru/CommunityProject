using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class MobMovement : MonoBehaviour
{

    #region PATH COMPONENTS

    protected Seeker seeker;
    protected Path path;
    protected Vector3 velocity;
    [SerializeField] protected Rigidbody2D rb;

    #endregion

    #region PATH PARAMETERS

    // Length of the path
    protected int theGScoreToStopAt = 6000;

    [SerializeField] protected float nextWaypointDistance = 1.5f;
    [SerializeField] protected float roamPointRadius = 4f;

    protected int currentWaypoint = 0;
    protected bool reachedEndOfPath;

    protected float pathCalculationRate = .2f;
    protected float pathCalculationTimer = 0;
    protected float roamCalculationTimer;
    [SerializeField] protected float roamCalculationRate;
    #endregion

    private Mob mob;
    private float moveSpeed;
    private bool dead;

    protected Vector3 moveDirNormalized;
    protected Vector2 moveDir2DNormalized;

    private void Awake() {
        mob = GetComponent<Mob>();
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = mob.GetMobSO().mobMoveSpeed;
    }
    protected virtual void Start() {
        seeker = GetComponent<Seeker>();
        mob.OnMobDied += Mob_OnMobDied;
        pathCalculationTimer = pathCalculationRate;
    }


    protected virtual void Update() {
        if (path != null) {
            FollowPath(path);
        }
    }
    protected virtual void FixedUpdate() {
        pathCalculationTimer -= Time.fixedDeltaTime;
        roamCalculationTimer -= Time.fixedDeltaTime;
        if (path != null) {
            Move(velocity);
        }
    }

    protected void FollowPath(Path path) {
        reachedEndOfPath = false;
        float distanceToWaypoint;

        if (currentWaypoint < path.vectorPath.Count) {
            while (true) {
                distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

                if (distanceToWaypoint < nextWaypointDistance) {

                    if (currentWaypoint + 1 < path.vectorPath.Count) {
                        currentWaypoint++;
                    }
                    else {
                        reachedEndOfPath = true;
                        break;
                    }
                }
                else {
                    break;
                }
            }
            var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

            moveDirNormalized = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            moveDir2DNormalized = new Vector2(moveDirNormalized.x, moveDirNormalized.y);
            velocity = moveDir2DNormalized * moveSpeed * speedFactor;
        }
    }

    protected virtual void Move(Vector3 velocity) {
        if (!reachedEndOfPath & !dead) {
            rb.AddForce(velocity * Time.fixedDeltaTime * 100);
        }
    }

    public void CalculatePath(Vector3 startPoint, Vector3 destinationPoint) {
        if (pathCalculationTimer <= 0) {
            seeker.StartPath(startPoint, destinationPoint, PathComplete);
            pathCalculationTimer = pathCalculationRate;
        }
    }

    protected void PathComplete(Path p) {
        path = p;
        currentWaypoint = 0;
    }
    public Vector3 GetMoveDirNormalized() {
        return moveDirNormalized;
    }
    public bool GetReachedEndOfPath() {
        return reachedEndOfPath;
    }

    private void Mob_OnMobDied(object sender, System.EventArgs e) {
        StopMoving();
    }

    public void StopMoving() {
        dead = true;
        rb.velocity = Vector3.zero;
    }

}
