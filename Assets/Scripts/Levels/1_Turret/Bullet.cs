using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;

    internal Turret Turret { get; set; }
    internal RobotInTurretLevel Target { get; set; }

    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, Target.transform.position) < 0.001f)
        {
            Destroy(gameObject);
            Turret.RegisterKill();
            Target.gameObject.SetActive(false);
        }
    }
}

