using UnityEngine;
using StateSystem;
using TurkeyWork.AI.State;
using Sirenix.OdinInspector;

public class AIController : MonoBehaviour
{
    [SerializeField, AssetsOnly]
    protected State currentState;

    [HideInInspector]
    public bool LGroundRay { get; set; }
    public bool RGroundRay { get; set; }

    [Range(-5.0f, 5.0f)]
    [SerializeField]
    private float rayOffSet = 0.5f;

    [Range(5.0f, 30.0f)]
    public float raycastDistance = 5.0f;

    private Vector2 dir1, dir2;
    private Vector3 dir3, dir4;

    public LayerMask GroundMask;

    public GameObject[] PlayersInInstance { get; set; }
    [AssetsOnly]
    public StateMachine stateMachine;
    public TurkeyWork.Actors.PlatformerMotor2D motor;
    public TurkeyWork.Actors.ActorBody Actor;
    public float mSpeed;
    public RaycastHit2D[] raycastHits;
    public float cdTimer;
    [Range(1,10)]
    public int damage;

    [HideInInspector]
    public GameObject target;

    private void Awake()
    {
        // List all player tagged gameobjects in to array
        PlayersInInstance = GameObject.FindGameObjectsWithTag("Player");

        // Vectors used in raycast
        // Set vectors perpendicular to object
        dir1 = new Vector2(1, 0);
        dir2 = new Vector2(-1, 0);

        // Setting vectors at an angle
        // Dir3 from object origin to right, downward angle
        // Dir4 from object origin to left, downward angle
        dir3 = Quaternion.AngleAxis(-85f, Vector3.forward) * transform.right;
        dir4 = Quaternion.AngleAxis(85f, Vector3.forward) * (-transform.right);

        // Raycast check boolean initialize
        LGroundRay = true;
        RGroundRay = true;

        mSpeed = -2.0f;
    }

    public State Simulate(State state)
    {
        cdTimer += Time.deltaTime;
        CheckRayCast();
        return stateMachine.Evaluate(state, this);
    }

    private void Update()
    {
        Actor.UpdateBounds ();
        currentState = Simulate(currentState);
    }

    private void LateUpdate()
    {
        // Update list of players in instance
        PlayersInInstance = GameObject.FindGameObjectsWithTag("Player");
    }

    /// <summary>
    /// Shoot multiple raycasts from the actor and return hit values
    /// </summary>
    /// <returns></returns>
    public void CheckRayCast()
    {
        var origin1 = new Vector2(transform.position.x + rayOffSet, transform.position.y);
        var origin2 = new Vector2(transform.position.x + rayOffSet * -1, transform.position.y);
        raycastHits = new RaycastHit2D[]
        {
            Physics2D.Raycast(origin1, dir3, 2.0f, GroundMask),
            Physics2D.Raycast(origin2, dir4, 2.0f, GroundMask),
            //Physics2D.Raycast(origin1, dir1, 5.0f, GroundMask),
            //Physics2D.Raycast(origin2, dir2, 5.0f, GroundMask)          
        };

        RGroundRay = raycastHits[0];
        LGroundRay = raycastHits[1];
    }
}
