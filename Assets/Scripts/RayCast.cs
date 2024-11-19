using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Health Enemy;
    [SerializeField] private Transform enemy;
    [SerializeField] private Rigidbody enemyBody;
    [SerializeField] private float coolDown;
    [SerializeField] private float bulletForce;
    private float timer = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && timer >= coolDown)
        {
            HitController();
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
        if (Enemy.health == 0)
        {
            enemyBody.useGravity = false;
            enemyBody.AddForce(Vector3.up);
        }

    }
    
    private void HitController()
    {
        Ray ray = MainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Rigidbody hitedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
            if(hitedBody != null)
            {
                hitedBody.AddForce(ray.direction * bulletForce, ForceMode.Impulse);
            }
            if(hit.transform.tag == "Finish")
            {
                Enemy.health -= 10;
                enemyBody.AddForce(ray.direction * bulletForce, ForceMode.Impulse);
                if (Enemy.health < 0)
                {
                    Enemy.health = 0;
                }
            }
        }
    }
}
