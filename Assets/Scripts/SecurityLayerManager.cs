using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SecurityLayerManager : MonoBehaviour
{
    public string jsonURL = "https://mindspiritdesign.com/licenses/pcfc-vh/lic.json";
    public GameObject UIBlock;
    public int WaitTimeForSecurityCheck = 500;
    int passcode;
    private void Start()
    {

        UIBlock.SetActive(true);
        
    }

    private void CheckInternetConnection()
    {
       /* switch (Application.internetReachability)
        {
            case NetworkReachability.NotReachable:
                Debug.Log("No internet connection");
                // Handle the case when there is no internet connection
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                Debug.Log("Connected via cellular data network");
                // Handle the case when connected via cellular data
                break;
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                Debug.Log("Connected via Wi-Fi or LAN");
                // Handle the case when connected via Wi-Fi or LAN
                break;
        }*/

        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            UIBlock.SetActive(true);
        }
        else
        {
            UIBlock.SetActive(false);
        }
    }

    public void OnCallBackDataLoadedFromJson()
    {
        passcode = THHDataManager.Instance.Data.PassCode;
        //CheckInternetConnection();
        StartCoroutine(CheckJsonData());
        StartCoroutine(SecurityTimer());
    }

    IEnumerator SecurityTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(WaitTimeForSecurityCheck);
            {
                StartCoroutine(CheckJsonData());
            }
        }
    }
    IEnumerator CheckJsonData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(jsonURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                //Debug.LogError("Failed to load JSON data: " + webRequest.error);
                UIBlock.SetActive(true);
            }
            else
            {
                string jsonText = webRequest.downloadHandler.text;

                // Deserialize the JSON data into an array of Person objects
                PasscodeData data = JsonUtility.FromJson<PasscodeData>(jsonText);

                print("Data in file = " + passcode);
                print("Data in file = " + data.lic);
                if (data.lic == passcode)
                {
                    UIBlock.SetActive(false);
                }
                else
                {
                    UIBlock.SetActive(true);
                }
            }
        }
    }
}
