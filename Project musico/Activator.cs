using System.Collections;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public KeyCode key;
    private bool active = false;
    private int perfectScore = 100;
    private int goodScore = 50;
    private int okayScore = 20;
    private int notesHit = 0; // Track the number of notes hit
    public AudioClip clap;

    private GameObject note;
    public PointCollecter pointCollecter;
    public Animator anim;

    private ClapFlash clapFlash; 
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        clapFlash = FindObjectOfType<ClapFlash>();
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            anim.SetBool("KeyPressed", true);

            if (active)
            {
                if (note != null)
                {
                    float notePosition = note.transform.position.x;
                    float activatorPosition = transform.position.x;

                    float distance = Mathf.Abs(notePosition - activatorPosition);
                    Destroy(note);

                    // Calculate score based on distance
                    int scoreToAdd = CalculateScore(distance);

                    // Increment the notesHit count
                    notesHit++;

                    // Invoke the event when a note is hit successfully
                    pointCollecter.AddScore(scoreToAdd);

                    // Play the sound immediately
                    clapFlash.Flash();
                    SoundManager.Instance.PlaySound(clap);
                    
                }
            }
        }
        // Reset the "KeyPressed" parameter to false after a short delay
        if (anim.GetBool("KeyPressed"))
        {
            StartCoroutine(ResetKeyPressed());
        }
    }

    private IEnumerator ResetKeyPressed()
    {
        // Wait for a short duration (adjust this as needed)
        yield return new WaitForSeconds(0.1f);

        // Set the "KeyPressed" parameter to false
        anim.SetBool("KeyPressed", false);
    }



    private int CalculateScore(float distance)
    {
        int score = 0;

        if (distance < 0.04f) // Perfect hit
        {
            score = perfectScore;
        }
        else if (distance < 0.15f) // Good hit
        {
            score = goodScore;
        }
        else if (distance < 0.30f) // Okay hit
        {
            score = okayScore;
        }

        return score;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Note")
        {
            active = true;
            note = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }
}
