using UnityEngine;


public class RobotInTurretLevel : MonoBehaviour
{
    public float speed = 10f;

    private Turret turret;
    private Animator animator;
    private Executor executor;

    private Vector3 spawnPosition;

    private void Awake()
    {
        spawnPosition = transform.position;
        animator = GetComponent<Animator>();
        turret = FindFirstObjectByType<Turret>();
        executor = FindFirstObjectByType<Executor>();
    }

    private void Update()
    {
        if (!executor.IsRunning)
            return;

        animator.SetBool("Walk_Anim", true);

        transform.LookAt(turret.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, turret.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, turret.transform.position) < 0.001f)
        {
            gameObject.SetActive(false);
            turret.gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        transform.position = spawnPosition;
        animator.SetBool("Walk_Anim", false);
    }
}

