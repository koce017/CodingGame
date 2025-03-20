using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float lastShoot;
    private RobotInTurretLevel[] robots;

    private int killedRobots;
    private RobotInTurretLevel currentTarget;
    private readonly Queue<RobotInTurretLevel> targets = new();

    private Executor executor;

    internal readonly float FireDelay = 1f;

    void Start()
    {
        lastShoot = Time.time;
        robots = FindObjectsByType<RobotInTurretLevel>(FindObjectsSortMode.None);
        executor = FindFirstObjectByType<ExecutorInTurretLevel>();
    }

    void Update()
    {
        if (!executor.IsRunning)
            return;

        if (killedRobots == robots.Length)
        {
            Success();
            return;
        }

        if (targets.Count == 0 && currentTarget == null)
            return;

        if (currentTarget == null)
            currentTarget = targets.Dequeue();

        if (Time.time - lastShoot > FireDelay)
        {
            transform.LookAt(currentTarget.transform.position);
            var go = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            var bullet = go.GetComponent<Bullet>();
            bullet.Target = currentTarget;
            bullet.Turret = this;
            lastShoot = Time.time;
            currentTarget = null;
        }

    }

    private void Success()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
            SceneManager.LoadScene(nextSceneIndex);
    }

    public void Reset()
    {
        targets.Clear();
        killedRobots = 0;
        gameObject.SetActive(true);
    }

    public void RegisterKill()
    {
        ++killedRobots;
    }

    public object X(object[] _)
    {
        return transform.position.x;
    }

    public object Y(object[] _)
    {
        return transform.position.z;
    }

    public object RobotX(object[] args)
    {
        return robots[(int)args[0]].transform.position.x;
    }

    public object RobotY(object[] args)
    {
        return robots[(int)args[0]].transform.position.y;
    }

    public object RobotCount(object[] args)
    {
        return robots.Length;
    }

    public object Shoot(object[] args)
    {
        targets.Enqueue(robots[(int)args[0]]);
        return null;
    }
}

