using UnityEngine;  
using System.Collections;  
  
public class GetGPS : MonoBehaviour {  
  
    public string gps_info = "";  
    public int flash_num = 1;  
  
    // Use this for initialization  
    void Start () {  
      
    }  
      
    void OnGUI () {  
        GUI.skin.label.fontSize = 28;  
        GUI.Label(new Rect(20,20,1200,48),this.gps_info);   
        GUI.Label(new Rect(20,50,1200,48),this.flash_num.ToString());   
          
        GUI.skin.button.fontSize = 50;  
        if (GUI.Button(new Rect(Screen.width/2-110,200,500,85),"GPS localize"))  
        {  
            // start with a coroutine program 
            StartCoroutine(StartGPS());  
        }  
        if (GUI.Button(new Rect(Screen.width/2-110,500,500,85),"Refresh GPS"))  
        {  
            this.gps_info = "Latitude:" + Input.location.lastData.latitude + " Longitude:"+Input.location.lastData.longitude + " Height:"+Input.location.lastData.altitude;
            //this.gps_info = this.gps_info + " Time:" + Input.location.lastData.timestamp;  
            this.flash_num += 1;   
        }  
    }  
  
    // Input.location = LocationService  
    // LocationService.lastData = LocationInfo   
  
    void StopGPS () {  
        Input.location.Stop();  
    }  
  
    IEnumerator StartGPS () {  
        // Input.location  
        // LocationService.isEnabledByUser
        if (!Input.location.isEnabledByUser) {  
            this.gps_info = "isEnabledByUser value is:"+Input.location.isEnabledByUser.ToString()+" Please turn on the GPS";   
            yield break;  
        }  
  
        // LocationService.Start()  for updates 
        Input.location.Start(10.0f, 10.0f);  
  
        int maxWait = 20;  
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {  
            // yield for 1 second 
            yield return new WaitForSeconds(1);  
            maxWait--;  
        }  
  
        if (maxWait < 1) {  
            this.gps_info = "Init GPS service time out";  
            yield break;  
        }  
  
        if (Input.location.status == LocationServiceStatus.Failed) {  
            this.gps_info = "Unable to determine device location";  
            yield break;  
        }   
        else {
            this.gps_info = "Latitude:" + Input.location.lastData.latitude + " Longitude:"+Input.location.lastData.longitude + " Height:"+Input.location.lastData.altitude;  
            //this.gps_info = this.gps_info + " Time:" + Input.location.lastData.timestamp;  
            yield return new WaitForSeconds(100);  
        }  
    }  
}