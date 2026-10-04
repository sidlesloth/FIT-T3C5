using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameObject soundPrefab1; // Assign your prefab in the Inspector
    public GameObject soundPrefab2; // Assign your prefab in the Inspector
    public GameObject soundPrefab3; // Assign your prefab in the Inspector
    public GameObject soundPrefab4; // Assign your prefab in the Inspector
    public GameObject soundPrefab5; // Assign your prefab in the Inspector
    public GameObject soundPrefab6; // Assign your prefab in the Inspector
    public GameObject soundPrefab7; // Assign your prefab in the Inspector
    public GameObject soundPrefab8; // Assign your prefab in the Inspector
    private Vector3 spawnPosition;

    private GameObject instance;  // Prefab that we connect the sound to
    private AudioSource audioSource; // Audiosource connected to the prefab

    GameObject eventManager;

    // Bools for checking if sound was turned on
    private bool Sound1WasTurnedOn = false;
    private bool Sound2WasTurnedOn = false;
    private bool Sound3WasTurnedOn = false;
    private bool Sound4WasTurnedOn = false;
    private bool Sound5WasTurnedOn = false;
    private bool Sound6WasTurnedOn = false;
    private bool Sound7WasTurnedOn = false;
    private bool Sound8WasTurnedOn = false;

    void Awake()
    {
        eventManager = GameObject.Find("EventManager"); // Get the eventManager object such that we can access other scripts and variables
    }


    void Update()
    {
        if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound1On == true && Sound1WasTurnedOn == false) 
        {   
            PlaySound(soundPrefab1); 
            print("Sound 1 is playing");
            Sound1WasTurnedOn = true;
            Sound2WasTurnedOn = false;
            Sound3WasTurnedOn = false;
            Sound4WasTurnedOn = false;
            Sound5WasTurnedOn = false;
            Sound6WasTurnedOn = false;
            Sound7WasTurnedOn = false;
            Sound8WasTurnedOn = false;
        }
        //if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound1On == false) { StopSound(soundPrefab1); print("Sound 1 has ended"); }
        else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound2On == true && Sound2WasTurnedOn == false) 
        { 
            PlaySound(soundPrefab2); 
            print("Sound 2 is playing");
            Sound1WasTurnedOn = false;
            Sound2WasTurnedOn = true;
            Sound3WasTurnedOn = false;
            Sound4WasTurnedOn = false;
            Sound5WasTurnedOn = false;
            Sound6WasTurnedOn = false;
            Sound7WasTurnedOn = false;
            Sound8WasTurnedOn = false;
        }
        //else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound2On == false) { StopSound(soundPrefab2); print("Sound 2 has ended"); }
        else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound3On == true && Sound3WasTurnedOn == false) 
        { 
            PlaySound(soundPrefab3); 
            print("Sound 3 is playing");
            Sound1WasTurnedOn = false;
            Sound2WasTurnedOn = false;
            Sound3WasTurnedOn = true;
            Sound4WasTurnedOn = false;
            Sound5WasTurnedOn = false;
            Sound6WasTurnedOn = false;
            Sound7WasTurnedOn = false;
            Sound8WasTurnedOn = false;
        }
        //else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound3On == false) { StopSound(soundPrefab3); print("Sound 3 has ended"); }
        else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound4On == true && Sound4WasTurnedOn == false) 
        { 
            PlaySound(soundPrefab4); 
            print("Sound 4 is playing");
            Sound1WasTurnedOn = false;
            Sound2WasTurnedOn = false;
            Sound3WasTurnedOn = false;
            Sound4WasTurnedOn = true;
            Sound5WasTurnedOn = false;
            Sound6WasTurnedOn = false;
            Sound7WasTurnedOn = false;
            Sound8WasTurnedOn = false;
        }
        //else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound4On == false) { StopSound(soundPrefab4); print("Sound 4 has ended"); }
        else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound5On == true && Sound5WasTurnedOn == false) 
        { 
            PlaySound(soundPrefab5); 
            print("Sound 5 is playing");
            Sound1WasTurnedOn = false;
            Sound2WasTurnedOn = false;
            Sound3WasTurnedOn = false;
            Sound4WasTurnedOn = false;
            Sound5WasTurnedOn = true;
            Sound6WasTurnedOn = false;
            Sound7WasTurnedOn = false;
            Sound8WasTurnedOn = false;
        }
        //else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound5On == false) { StopSound(soundPrefab5); print("Sound 5 has ended"); }
        else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound6On == true && Sound6WasTurnedOn == false) 
        { 
            PlaySound(soundPrefab6); 
            print("Sound 6 is playing");
            Sound1WasTurnedOn = false;
            Sound2WasTurnedOn = false;
            Sound3WasTurnedOn = false;
            Sound4WasTurnedOn = false;
            Sound5WasTurnedOn = false;
            Sound6WasTurnedOn = true;
            Sound7WasTurnedOn = false;
            Sound8WasTurnedOn = false;
        }
        //else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound6On == false) { StopSound(soundPrefab6); print("Sound 6 has ended"); }
        else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound7On == true && Sound7WasTurnedOn == false) 
        { 
            PlaySound(soundPrefab7); 
            print("Sound 7 is playing");
            Sound1WasTurnedOn = false;
            Sound2WasTurnedOn = false;
            Sound3WasTurnedOn = false;
            Sound4WasTurnedOn = false;
            Sound5WasTurnedOn = false;
            Sound6WasTurnedOn = false;
            Sound7WasTurnedOn = true;
            Sound8WasTurnedOn = false;
        }
        //else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound7On == false) { StopSound(soundPrefab7); print("Sound 7 has ended"); }
        else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound8On == true && Sound8WasTurnedOn == false) 
        { 
            PlaySound(soundPrefab8); 
            print("Sound 8 is playing");
            Sound1WasTurnedOn = false;
            Sound2WasTurnedOn = false;
            Sound3WasTurnedOn = false;
            Sound4WasTurnedOn = false;
            Sound5WasTurnedOn = false;
            Sound6WasTurnedOn = false;
            Sound7WasTurnedOn = false;
            Sound8WasTurnedOn = true;
        }
        //else if (eventManager.GetComponent<NavigationalStrategy_ADAPTME>().Sound8On == false) { StopSound(soundPrefab8); print("Sound 8 has ended"); }

        else if (eventManager.GetComponent<TargetLocator>().EndScene) // Stop the sound
        {
            StopSound();
            // print("All sounds ended!");

            Sound1WasTurnedOn = false;
            Sound2WasTurnedOn = false;
            Sound3WasTurnedOn = false;
            Sound4WasTurnedOn = false;
            Sound5WasTurnedOn = false;
            Sound6WasTurnedOn = false;
            Sound7WasTurnedOn = false;
            Sound8WasTurnedOn = false;
    
}

    }

    void PlaySound(GameObject soundPrefab)
    {
        // If there's already an instance playing, stop the previous sound
        if (instance != null && audioSource != null && audioSource.isPlaying)
        {
            StopSound();
        }

        spawnPosition = soundPrefab.transform.position;

        // Instantiate prefab at a position
        instance = Instantiate(soundPrefab, spawnPosition, Quaternion.identity);
        instance.SetActive(true);

        // Get the AudioSource component
        audioSource = instance.GetComponent<AudioSource>();
        //audioSource.enabled = true;

        // Play the sound
        if (audioSource != null && audioSource.clip != null) // There's an audio source with a clip assigned to the object
        {
            audioSource.spatialBlend = 1f; // Double check that audio is spatial
            //audioSource.Play();

            //Destroy(instance, audio.clip.length); // Automatically destroy the clip after a specific time

        }
        else
        {
            print("There's a missing AudioSource or AudioClip on the prefab.");
        }

        // Optionally destroy the object after sound finishes
    }

    void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();

            Destroy(instance);
            instance = null;
            audioSource = null;
        }
    }
}
