using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    public float aliveTime;
    float timer;
    float damageAmount;
    float speed;
    bool shot = false;
    Vector3 direction = Vector3.back;

    // Set values from settings file asset
    public void Init()
    {
        damageAmount = MainManager.Get.settings.data.bulletDamage;
        speed = MainManager.Get.settings.data.bulletSpeed;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if(shot)
        {
            if(timer <= 0)
            {
                Finish();
            } else
            {
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
                timer -= Time.deltaTime;
            }
        }
    }

    // Sets the bullet to start moving in the direction
    public void Shoot(Vector3 startingPosition, Vector3 direction)
    {
        transform.position = startingPosition;
        this.direction = direction;
        transform.rotation = Quaternion.LookRotation(direction);
        timer = aliveTime;
        shot = true;
        gameObject.SetActive(true);
    }

    // Stops the bullet for reuse
    public void Finish()
    {
        gameObject.SetActive(false);
        transform.position = direction = Vector3.zero;
        shot = false;
        timer = 0;
    }

    // Checks for collision with player to do damage
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().TakeDamage((int)damageAmount);
            Finish();
        }
    }
}
