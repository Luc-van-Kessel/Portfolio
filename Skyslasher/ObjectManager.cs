using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// handles the switching of objects/levels and moves the grandchildren of the active object this script is referencing
/// </summary>
public class ObjectManager : MonoBehaviour
{
    public GameObject[] objectsToSwitch; // Array of objects to switch 

    public GameObject particleEffectPrefab; // Particle effect prefab to be instantiated

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float downSpeed = 2f;
    [SerializeField] private float moveDistance;

    private int currentIndex = 0; // Index of the current object

    // event onobject switched 
    public delegate void ObjectMoved();
    public event ObjectMoved OnObjectsMoved;

    public AudioSource AudioSource;
    //Layermask for particle layer 
        
    private CinemachineCameraShakeController camShake;

    private void Start()
    {
        // By making mytrue false at the start we skip the first object in the array by checking if ifTrue is not true  we will set iftrue to true and set that object to iftrue, making it true.
        // Then we will continue to make sure we only set one active. The we will revert ifTrue that is true by usnig ! infront of iftrue making it false.
        bool ifTrue = false;

        foreach (GameObject objectsToSwitch in objectsToSwitch)
        {
            if (ifTrue != true)
            {
                ifTrue = true;
                objectsToSwitch.SetActive(ifTrue);
                continue;
            }

            objectsToSwitch.SetActive(!ifTrue);
        }

        StartCoroutine(SetParticles(objectsToSwitch[currentIndex].transform, false, 0f));


        camShake = FindAnyObjectByType<CinemachineCameraShakeController>();
    }
    public void SwitchObjects()
    {
        StartCoroutine(MoveObjectsDownAndSwitch());

        //start coroutine set particles
        StartCoroutine(SetParticles(objectsToSwitch[currentIndex].transform, false, 0f));


    }

    public void MoveObjectsUp()
    {
        if (objectsToSwitch.Length == 0) return; // Check if there are any objects to switch
        AudioSource.Play();
        StartCoroutine(SetParticles(objectsToSwitch[currentIndex].transform, true, 0f));
        StartCoroutine(MoveChildren(objectsToSwitch[currentIndex].transform));
    }

    IEnumerator MoveObjectsDownAndSwitch()
    {
        Transform currentObject = objectsToSwitch[currentIndex].transform;

        // Loop through each child of the current object and move it down
        foreach (Transform child in currentObject)
        {
            Vector3 startPos = child.position;
            Vector3 targetPos = startPos - Vector3.up * moveDistance;
            float distance = Vector3.Distance(startPos, targetPos);
            float duration = distance / downSpeed;

            float t = 0;
            while (t < 1)
            {
                t += Time.unscaledDeltaTime / duration;
                child.position = Vector3.Lerp(startPos, targetPos, t);
                yield return null;
            }
        }

        objectsToSwitch[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % objectsToSwitch.Length;
        objectsToSwitch[currentIndex].SetActive(true);
        yield return null;
    }

    IEnumerator MoveChildren(Transform parent)
    {
        camShake.ShakeCamera(1.15f, 2.55f);
        // Loop through each child of the parent GameObject
        foreach (Transform child in parent)
        {
            // Loop through each child of the current child (grandchildren)
            foreach (Transform grandchild in child)
            {
                Vector3 startPos = grandchild.position;
                Vector3 targetPos = new Vector3(startPos.x, startPos.y + moveDistance, startPos.z); // Use moveDistance
                float distance = Vector3.Distance(startPos, targetPos);
                int steps = Mathf.CeilToInt(distance / (moveSpeed * Time.unscaledDeltaTime)); // Calculate the number of steps using Time.deltaTime
                float stepSize = 1f / steps; // Determine the step size


                for (int k = 0; k <= steps; k++)
                {
                    float t = k * stepSize;
                    grandchild.position = Vector3.Lerp(startPos, targetPos, t);

                    if (particleEffectPrefab != null)
                    {
                        ObjectPoolManager.SpawnObject(particleEffectPrefab, grandchild.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
                    }
                    yield return null; // Wait for the next frame
                }


            }
        }

        // Deactivate the particle systems
        StartCoroutine(SetParticles(objectsToSwitch[currentIndex].transform, false, 2f));


        if (OnObjectsMoved != null)
        {
            OnObjectsMoved.Invoke();
        }

    }

    private IEnumerator SetParticles(Transform parent, bool setActive, float time)
    {
        yield return new WaitForSeconds(time); // Wait for the cooldown duration

        foreach (Transform child in parent)
        {
            ParticleSystem particleSystem = child.GetComponentInChildren<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.gameObject.SetActive(setActive);
            }
        }
    }

    // Make a object appear 
    public void ShowStandBox(GameObject obj)
    {
        obj.SetActive(true);
    }

    // Make a object disappear
    public void HideStandBox(GameObject obj)
    {
        obj.SetActive(false);
    }
}