using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public partial class RobotBehaviour : MonoBehaviour
{
    private StateMachine machine = new StateMachine();

    [SerializeField] private Transform hands;
    [SerializeField] private Transform boxHandler;
    private BoxScript pickedUpBox = null;

    private void Awake() {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start() {
        hands.gameObject.SetActive(false);
        machine.ChangeState(new State_RobotWalkToBox(this));
    }

    private void Update() {
        machine.UpdateState();
    }

    public void PickupBox(BoxScript box) {
        pickedUpBox = box;
        hands.gameObject.SetActive(true);
        box.GetComponent<Collider2D>().enabled = false;
        box.body.velocity = Vector2.zero;
        Level.Current.MoveBox(box, boxHandler);
        box.transform.localPosition = Vector3.zero;
    }

    public void ThrowBox(Vector2 velocity) {
        hands.gameObject.SetActive(false);
        var box = pickedUpBox;
        pickedUpBox = null;
        Level.Current.ReturnBox(box);
        box.GetComponent<Collider2D>().enabled = true;
        box.body.velocity = velocity;
        box.wasThrown = true;

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), box.GetComponent<Collider2D>(), true);
        Timer.StartOneshotTimer(this, 0.3f, () => {
            if (box != null) {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), box.GetComponent<Collider2D>(), false);
            }
        });
    }

}

public partial class RobotBehaviour : MonoBehaviour
{
    public class State_RobotWalkToBox : StateBase {
        
        private RobotBehaviour robot;
        private List<BoxScript> excludedBoxes;
        public State_RobotWalkToBox(RobotBehaviour robot, List<BoxScript> excludedBoxes = null) { 
            this.robot = robot; 
            this.excludedBoxes = excludedBoxes ?? new List<BoxScript>();    
        }

        private BoxScript targetBox = null;
        private GameObject targetPlate = null;

        public override void OnStateEntered() {
            targetBox = Level.Current.FindRandomBox(robot.transform.position, excludedBoxes);
            if (targetBox == null || !Level.Current.IsBoxPickable(targetBox)) {
                targetPlate = Level.Current.FindPressurePlate(robot.transform.position);
            }
            Timer.StartTimer(this, 1.5f, () => {
                if (robot != null) {
                    if (targetBox == null || !Level.Current.IsBoxPickable(targetBox)) {
                        targetBox = Level.Current.FindRandomBox(robot.transform.position, excludedBoxes);
                        if (targetBox == null || !Level.Current.IsBoxPickable(targetBox)) {
                            targetPlate = Level.Current.FindPressurePlate(robot.transform.position);
                        }
                    }
                    else {                        
                        targetPlate = Level.Current.FindPressurePlate(robot.transform.position);
                    }
                }
            });
            Timer.StartOneshotTimer(this, 1.0f, () => {
                excludedBoxes.Clear();
            });        
        }

        public override void OnStateUpdate() {
            var agent = robot.GetComponent<NavMeshAgent>();
            if (targetBox != null && Level.Current.IsBoxPickable(targetBox)) {
                robot.GetComponent<Animator>().Play("RobotAnimation_Walk");
                agent.isStopped = false;
                agent.SetDestination(targetBox.transform.position);

                var dis = (robot.transform.position - targetBox.transform.position).magnitude;
                if (dis < 1.0f) {
                    robot.PickupBox(targetBox);
                    robot.machine.ChangeState(new State_RobotGoToPlayer(robot));
                }
            }
            else if (targetPlate != null) {
                agent.isStopped = false;
                robot.GetComponent<Animator>().Play("RobotAnimation_Walk");
                agent.SetDestination(targetPlate.transform.position);

                var dis = (robot.transform.position - targetPlate.transform.position).magnitude;
                if (dis < 0.1f) {
                    targetBox = Level.Current.FindNearestBox(robot.transform.position, excludedBoxes);
                    if (targetBox != null) {
                        targetPlate = Level.Current.FindPressurePlate(robot.transform.position, new () {targetPlate});    
                        if (targetPlate == null) {
                            agent.SetDestination(Level.Current.Player.transform.position);
                        }
                    }
                }
            }
            else {
                if (!agent.isStopped) {   
                    targetBox = Level.Current.FindRandomBox(robot.transform.position, excludedBoxes);
                    if (targetBox == null || !Level.Current.IsBoxPickable(targetBox)) {
                        targetPlate = Level.Current.FindPressurePlate(robot.transform.position);
                    }
                }
                agent.isStopped = true;
                robot.GetComponent<Animator>().Play("RobotAnimation_Idle");
            }
        }
    }

    public class State_RobotGoToPlayer : StateBase {
        
        private RobotBehaviour robot;
        public State_RobotGoToPlayer(RobotBehaviour robot) { this.robot = robot; }

        public override void OnStateUpdate() {
            var agent = robot.GetComponent<NavMeshAgent>();
            var player = Level.Current.Player;
            var path = agent.SetDestination(player.transform.position);

            var vecToPlayer = player.transform.position - robot.transform.position;
            var dis = vecToPlayer.magnitude;

            if (dis <= 3.0f) {
                robot.machine.ChangeState(new State_RobotThrowingBox(robot));
            }
        }
    }

    public class State_RobotThrowingBox : StateBase {
        
        private RobotBehaviour robot;
        public State_RobotThrowingBox(RobotBehaviour robot) { this.robot = robot; }

        public override void OnStateEntered() {
            var agent = robot.GetComponent<NavMeshAgent>();
            robot.GetComponent<Animator>().Play("RobotAnimation_Idle");
            agent.isStopped = true;

            Timer.StartOneshotTimer(this, 0.05f, () => {
                var player = Level.Current.Player;
                var vecToPlayer = player.transform.position - robot.boxHandler.transform.position;
                var vel = vecToPlayer.normalized * 16.0f;
                var throwedBox = robot.pickedUpBox;
                robot.ThrowBox(vel);
                Timer.StartOneshotTimer(this, 0.05f, () => {
                    if (robot != null) {
                        robot.machine.ChangeState(new State_RobotWalkToBox(robot, new() {throwedBox}));
                    }
                });
            });
        }
    }

}