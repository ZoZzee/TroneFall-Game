using UnityEngine;
using UnityEngine.AI;



namespace Unity.AI.Navigation.Samples
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToMove : MonoBehaviour
    {
        //[RequireComponent(typeof(NavMeshAgent))]
        private NavMeshAgent p_agent;
        private RaycastHit p_HitInfo = new RaycastHit();
        [SerializeField]
        private Animator p_Animator;

        private void Start()
        {
            p_agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray.origin,ray.direction,out p_HitInfo))
                    p_agent.destination = p_HitInfo.point;
            }
            if(p_agent.velocity.magnitude != 0)
            {
                p_Animator.SetFloat("Velocity", p_agent.velocity.magnitude);
            }
            else
            {
                p_Animator.SetFloat("Velocity", p_agent.velocity.magnitude);
            }
        }

        private void OnAnimatorMove()
        {
            if(p_Animator.GetBool("Running"))
            {
                p_agent.speed = (p_Animator.deltaPosition / Time.deltaTime).magnitude;
            }
        }
    }
}
