using System.Collections;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;    
    [SerializeField] private Health Enemy;
    [SerializeField] private Transform enemy;
    [SerializeField] private Rigidbody enemyBody;
    [SerializeField] private float coolDown;
    [SerializeField] private float bulletForce;
    [SerializeField] private float recoilDispersion;
    private float timerCoolDown = 0f;
    private Quaternion originalRotation;
    
    private void Start()    
    {
        MainCamera = Camera.main;
        
        originalRotation = transform.localRotation;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && timerCoolDown >= coolDown)
        {
            HitController();
            StartCoroutine(DoRecoil());
            timerCoolDown = 0f;
        }
        else
        {
            timerCoolDown += Time.deltaTime;
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
        }
    }

    private IEnumerator DoRecoil()
    {
        for (float i = 0; i < coolDown / 2; i += Time.deltaTime)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, new Quaternion(originalRotation.x - recoilDispersion, originalRotation.y, originalRotation.z, originalRotation.w), i);
            yield return null;
        }
        for (float i = 0; i < coolDown / 2; i += Time.deltaTime)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, originalRotation, i);
            yield return null;
        }
        transform.localRotation = originalRotation;
    }
}
