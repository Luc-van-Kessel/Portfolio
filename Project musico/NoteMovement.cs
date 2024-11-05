using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    [SerializeField] private Conductor conductor;

    public float beatTempo;

    public bool hasStarted;

    void Start()
    {
        beatTempo = conductor.secPerBeat;
    }

    void Update()
    {
        if (!hasStarted)
        {
            hasStarted = true;
        }
        else
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
        }
    }
}
