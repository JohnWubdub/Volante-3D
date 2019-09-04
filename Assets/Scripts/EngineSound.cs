using UnityEngine;

public class EngineSound : MonoBehaviour 
{
    public GameObject plane;

    public int startingPitch = 1;

    AudioSource audioSource;

    //variables for equation
    float maxMoveSpeed;
    float minMoveSpeed;
    float maxPitch = 2f;
    float minPitch = .5f;


    void Start ()
    {
        PlayerMovement playerMovement = plane.GetComponent<PlayerMovement>();

        audioSource = GetComponent<AudioSource>();

        audioSource.pitch = startingPitch;

        maxMoveSpeed = playerMovement.maxMoveSpeed;
        minMoveSpeed = playerMovement.minMoveSpeed;

    }
	

	

    private void Update()
    {
        if(audioSource.pitch > maxPitch)
        {
            audioSource.pitch = maxPitch;
        }

        if (audioSource.pitch < minPitch)
        {
            audioSource.pitch = minPitch;
        }
    }

    public void PitchChanger(float moveSpeed)
    {
        float newPitch = (((maxPitch - minPitch) * (moveSpeed - minMoveSpeed)) / (maxMoveSpeed - minMoveSpeed)) + minPitch;
        audioSource.pitch = newPitch;
    }
}
