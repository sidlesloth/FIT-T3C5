using UnityEngine;
using System;
using System.IO; 
using System.Text;
using System.Collections.Generic;

public class NavigationalStrategy_ADAPTME : MonoBehaviour
{
    /* --------------------------------------------------------
     * STUDENT VERSION
     * --------------------------------------------------------
     */

    [SerializeField]
    public string GroupName = "Group 5"; // ADAPT: add your group name here (in one word, e.g. 'Group-Anne-James-Louise').

    // Reference to boat
    [SerializeField]
    private GameObject RowBoatPrefab;

    GameObject eventManager;
    string filename = "";
    private string date;
    private string time;
    private int startingTimeMillisec;
    private float nextWriteTime = 0f;
    private float PrivateAngleOfLighthouseToBoat; // Easy shorthand for us to utilise the angle of the lighthouse relative to the boat which we get from the TargetLocator script.

    private List<Vector3> DangerZoneLocations; // A list of danger zone positions, which you can access (and which are printed in the console at the start)
    private float distanceBoatToDangerZone; // Variable already created for you for convenience: a measure of how far the boat is to the danger zones (so you might issue a warning to your user to change course...)
    private float angleBoatToDangerZone;
    private float[] dangerZoneDistances;
    public int TriggerDistanceDangerZone = 20;

    /* --------------------------------------------------------
     * BELOW ARE THE VARIABLES THAT CAN TURN ON SPECIFIC PARTS OF THE HARDWARE
     * - TurnOnHeater variables turn on specific heater towers or individual heaters. The variable is binary, so either true (= on) or false (= off).
     * - The Hairdryer1Ventilator and Hairdryer2Ventilator variabls indicates which of the two hair dryer ventilators are on (currently positioned as 1 = right and 2 = left). The variable is a float, meaning that you can change the intensity of the ventialtors (0.5f = 50%, 1.0f = 100% etc., make sure to add the 'f' after the number to indicate to Unity it is a float). Make sure not to go below 0.2f (=20% intensity), because at the low levels the ventialtor behaves unstable.
     * --------------------------------------------------------
     */

    // Bools for turning on specific heaters (as in the example)
    public bool TurnOnHeatTOWERLEFT = false; // Turns on heaters 1 (upper) and 2 (bottom) to the left of the user
    public bool TurnOnHeatTOWERRIGHT = false; // Turns on heaters 3 (upper) and 4 (bottom) to the right of the user
    public bool TurnOnHeatTOWERBACK = false; // Turns on heaters 5 (upper) and 6 (bottom) behind the user
    public bool TurnOnHeatTOWERFRONT = false; // Turns on heaters 7 (upper) and 8 (bottom) in front of the user

    // Ignore but keep bools below, for visualisation purposes only
    public bool TurnOnHeaterLEFT = false;
    public bool TurnOnHeaterLEFTMIDDLE = false;
    public bool TurnOnHeaterRIGHTMIDDLE = false;
    public bool TurnOnHeaterRIGHT = false;

    // To be used if you want to turn on specific heaters
    public bool TurnOnHeater1 = false;
    public bool TurnOnHeater2 = false;
    public bool TurnOnHeater3 = false;
    public bool TurnOnHeater4 = false;
    public bool TurnOnHeater5 = false;
    public bool TurnOnHeater6 = false;
    public bool TurnOnHeater7 = false;
    public bool TurnOnHeater8 = false;

    // Floats to turn on specific hairdryers
    public float Hairdryer1Ventilator = 0f;
    public float Hairdryer2Ventilator = 0f;
    public bool ShutDownHairdryers = false; // Code to turn off hairdryers

    // Bools for auditory cues
    public bool Sound1On = false;
    public bool Sound2On = false;
    public bool Sound3On = false;
    public bool Sound4On = false;
    public bool Sound5On = false;
    public bool Sound6On = false;
    public bool Sound7On = false;
    public bool Sound8On = false;


    //personal new vars
    public AudioSource 
    void Awake()
    {
        Debug.Log($"Trial started for group: {GroupName}"); 

        eventManager = GameObject.Find("EventManager"); // Get the GameControl object such that we can access other scripts and variables
        
        // General information for output documentation 
        date = DateTime.Now.ToString("dd-MM-yyyy");
        time = DateTime.Now.ToString("HH-mm-ss");
        startingTimeMillisec = (((DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second) * 1000) + DateTime.Now.Millisecond;
        filename = Application.dataPath + "/SavedData/" + date + "--" + time + "--GroupName-" + GroupName + ".csv"; // This will be the name of your excel file in which all data is stored

        // Setting up Danger Zone locations
        DangerZoneLocations = new List<Vector3>();
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone1.transform.position);
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone2.transform.position);
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone3.transform.position);
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone4.transform.position);
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone5.transform.position);
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone6.transform.position);
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone7.transform.position);
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone8.transform.position);
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone9.transform.position);
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone10.transform.position);
        DangerZoneLocations.Add(eventManager.GetComponent<TargetLocator>().DangerZone11.transform.position);

        dangerZoneDistances = new float[DangerZoneLocations.Count];

    }

    /* CALLING SPECIFIC DANGER ZONES
     * Example of calling the location of a danger zone: 
     * eventManager.GetComponent<TargetLocator>().DangerZone1.transform.position
     */

    /* EXAMPLE OF A NAVIGATIONAL STRATEGY
     * Example thermal:
     * The thermal cues are used to lead a person towards the lighthouse, with the panel turning on that allows a user to point towards the lighthouse.
     * The angle at which the lighthouse is located with respect to the boat is used to determine which heater should be turned on.
     * Heaters stay continuously turned on.
     * 
     * Example airflow:
     * Changing the value of the hair dryer ventilators.
     * 
     * Example sound: we turn on one of the sound prefabs. Note: the sound prefabs are located 360deg around the user, so the orientation
     * is different from the other modalities. If you want, you can adapt the location of the prefabs by referencing them here as is done in the 
     * SoundController.cs script and updating their position to your preferred position by changing their coordinates in the Update void.
     * Because of the 360deg positioning of the sound prefabs, moving and turning the boat does not change where the sound is coming from (after all,
     * the sound prefabs stay exactly the same relative to the boat). 
    */

    void Update()
    {
        //Debug.Log($"The angle of the lighthouse with respect to the boat is: {eventManager.GetComponent<TargetLocator>().angleOfLighthouseToBoat:F2}"); // Example of how you can print what the relative angle of the lighthouse is. This cloggs up the console, so comment this out for readability of the other print statements
        PrivateAngleOfLighthouseToBoat = eventManager.GetComponent<TargetLocator>().angleOfLighthouseToBoat; // Put it in our local shorthand. Note: this is an unnecessary step as we can continuously get the value directly from the TargetLocator script, but to avoid typo's and reference mistakes we make the transfer here.
        
        /* INTERPRETING THE PrivateAngleOfLighthouseToBoat VARIABLE
         * The PrivateAngleOfLighthouseToBoat variable provides a value between -180 and +180. Between -180 and 0 are the angles to the left of the boat; 0 to +180 are to the right of the boat.
         * So if we want to have the correct heater on to steer a user according to where the tower is; we can turn on specific heat sources when we have a specific angle. Because of the placement of the heaters
         * in real life (placed in a half circle in front of the user), the angles of -180 to -60 and +60 to +180 belong to the outermost left and right heater respectively.
         */

        if (!eventManager.GetComponent<TargetLocator>().EndScene) // If we are not yet at the lighthouse, keep this line in and adapt your navigational strategy below
        {

            if (!eventManager.GetComponent<TargetLocator>().EndScene) // If we are not yet at the lighthouse, we need to navigate towards it. Keep this line in and adapt your navigational strategy within this if-statement.
            {
                if (PrivateAngleOfLighthouseToBoat >= -45.0f && PrivateAngleOfLighthouseToBoat <= 45.0f) // The lighthouse is in front of the user
                {
                    TurnOnHeatTOWERFRONT = true; // This is the heater we want to turn on
                    TurnOnHeatTOWERRIGHT = false; // All other heaters get turned off
                    TurnOnHeatTOWERBACK = false;
                    TurnOnHeatTOWERLEFT = false;
                    
                    // Example sound:
                    Sound1On = true;
                    Sound2On = false;
                    Sound3On = false;
                    Sound4On = false;
                    Sound5On = false;
                    Sound6On = false;
                    Sound7On = false;
                    Sound8On = false;
                }
                else if (PrivateAngleOfLighthouseToBoat > 45.0f && PrivateAngleOfLighthouseToBoat <= 135.0f)
                {
                    TurnOnHeatTOWERFRONT = false;
                    TurnOnHeatTOWERRIGHT = true; // This is the heater we want to turn on
                    TurnOnHeatTOWERBACK = false;
                    TurnOnHeatTOWERLEFT = false;

                    // Example sound
                    Sound1On = false;
                    Sound2On = true;
                    Sound3On = false;
                    Sound4On = false;
                    Sound5On = false;
                    Sound6On = false;
                    Sound7On = false;
                    Sound8On = false;
                }

                else if (PrivateAngleOfLighthouseToBoat > 135.0f || PrivateAngleOfLighthouseToBoat <= -135.0f)
                {
                    TurnOnHeatTOWERFRONT = false;
                    TurnOnHeatTOWERRIGHT = false;
                    TurnOnHeatTOWERBACK = true; // This is the heater we want to turn on
                    TurnOnHeatTOWERLEFT = false;

                    // Example sound
                    Sound1On = false;
                    Sound2On = false;
                    Sound3On = true;
                    Sound4On = false;
                    Sound5On = false;
                    Sound6On = false;
                    Sound7On = false;
                    Sound8On = false;
                }
                else if (PrivateAngleOfLighthouseToBoat > -135.0f && PrivateAngleOfLighthouseToBoat < -45.0f)
                {
                    TurnOnHeatTOWERFRONT = false;
                    TurnOnHeatTOWERRIGHT = false;
                    TurnOnHeatTOWERBACK = false;
                    TurnOnHeatTOWERLEFT = true; // This is the heater we want to turn on

                    // Example sound
                    Sound1On = false;
                    Sound2On = false;
                    Sound3On = false;
                    Sound4On = true;
                    Sound5On = false;
                    Sound6On = false;
                    Sound7On = false;
                    Sound8On = false;
                }

                // Ignore processing step below
                TurnOnHeaterLEFT = TurnOnHeatTOWERLEFT;
                TurnOnHeaterLEFTMIDDLE = TurnOnHeatTOWERRIGHT;
                TurnOnHeaterRIGHTMIDDLE = TurnOnHeatTOWERBACK;
                TurnOnHeaterRIGHT = TurnOnHeatTOWERFRONT;

            }
        }

        /* EXAMPLE: checking if we are getting close to a danger zone
         * Based on the position of the danger zones, we check how close we are (remember that the circles have a range of 10 units).
         * I'll leave it to you to implement the right cues to avoid a user hitting a danger zone!
         */
        // Reset all distances to infinity
        for (int i = 0; i < dangerZoneDistances.Length; i++)
        {
            dangerZoneDistances[i] = float.MaxValue;
        }

        // Calculate distances to all danger zones
        int index = 0;
        foreach (Vector3 DZlocation in DangerZoneLocations)
        {
            distanceBoatToDangerZone = Vector3.Distance(DZlocation, RowBoatPrefab.transform.position);
            angleBoatToDangerZone = CalculateAngleToDangerZone(DZlocation);
            dangerZoneDistances[index++] = distanceBoatToDangerZone;

            //Debug.Log($"Distance of boat to danger zone = {distanceBoatToDangerZone}");

            // Check if we're close to this danger zone
            if (distanceBoatToDangerZone < TriggerDistanceDangerZone)
            {
                Debug.Log("We're getting really close to a danger zone... Watch out!");

                // ---------------------------------------------------------
                // Turning on hairdryers to blow us away from the danger
                //
                // 0�   = danger directly in front
                // 90�  = danger on the right  -> Hairdryer 1 maximum
                // 180� = danger directly behind
                // 270� = danger on the left   -> Hairdryer 2 maximum
                // ---------------------------------------------------------

                if (angleBoatToDangerZone <= 180f)
                {
                    // Right side
                    // 0�   -> 0
                    // 90�  -> 1
                    // 180� -> 0

                    Hairdryer1Ventilator = Mathf.Sin(angleBoatToDangerZone * Mathf.Deg2Rad);
                    Hairdryer2Ventilator = 0f;
                }
                else
                {
                    // Left side
                    // 180� -> 0
                    // 270� -> 1
                    // 360� -> 0

                    Hairdryer1Ventilator = 0f;
                    Hairdryer2Ventilator = Mathf.Sin((360f - angleBoatToDangerZone) * Mathf.Deg2Rad);
                }

            }
        }

        // Check if we should turn off all hairdryers
        bool allDistancesSafe = true;
        foreach (float distance in dangerZoneDistances)
        {
            if (distance < 30)
            {
                allDistancesSafe = false;
                break;
            }
        }

        if (allDistancesSafe && (Hairdryer1Ventilator > 0 | Hairdryer2Ventilator > 0)) // Only if the hairdryer is not already on 0
        {
            Hairdryer1Ventilator = 0.0f;
            Hairdryer2Ventilator = 0.0f;
            Debug.Log("All danger zones cleared - turning off hairdryers");
            allDistancesSafe = false;
        }


        /* FOR YOU TO FIGURE OUT: GETTING CLOSE TO THE EDGE OF THE PLAYING FIELD
         * If your player gets to the edge of the playing field, they might not notice that they are not moving forward (because they do not see their movement) and get stuck. How can you avoid this? Implement this in your navigational strategy!
         */

        if (Time.time >= nextWriteTime) // For logging purposes
        {
            WriteCSV(); // Log the movement of the boat at every frame at once per second for you to analyse. 
            nextWriteTime = Time.time + 1f;  // Set next write time to 1 second from now.
        }
    }

    private float CalculateAngleToDangerZone(Vector3 LocationDZ)
    {
        Vector3 direction = LocationDZ - RowBoatPrefab.transform.position;
        Vector3 horizontalDir = new Vector3(direction.x, 0f, direction.z);
        float signedAngle = Vector3.SignedAngle(RowBoatPrefab.transform.forward, horizontalDir, Vector3.up);
        return Mathf.Repeat(signedAngle + 360f, 360f);
    }

    public void WriteCSV() // This void will write all relevant data to an Excel file saved in the folder 'SavedData'. This CSV is now updated at every second (see the Update() void). You can adapt this to better capture relevant data from your trial.
    {
        // First write the header of each column
        if (!File.Exists(filename))
        {
            using (TextWriter tw = new StreamWriter(filename, false))
            {
                tw.WriteLine("Date;Time;Timestamp in milliseconds;Group name;Angle of lighthouse w.r.t. the boat; Position boat - x; Position boat - y; Position boat - z");
            }
        }

        // Then write the data
        using (TextWriter tw = new StreamWriter(filename, true))
        {
            string date = DateTime.Now.ToString("dd-MM-yyyy");
            string time = DateTime.Now.ToString("HH:mm:ss");
            int TimeStampMilliseconds = (((DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second) * 1000) + DateTime.Now.Millisecond - startingTimeMillisec; // Current time in milliseconds minus the starting time in milliseconds

            tw.WriteLine($"{date};{time};{TimeStampMilliseconds};{GroupName};{PrivateAngleOfLighthouseToBoat};{eventManager.GetComponent<TargetLocator>().updatePositionBoat.x};{eventManager.GetComponent<TargetLocator>().updatePositionBoat.y};{eventManager.GetComponent<TargetLocator>().updatePositionBoat.z}");
        }
    }

}
