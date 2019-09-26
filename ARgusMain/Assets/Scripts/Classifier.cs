using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Classifier : MonoBehaviour
{
    public GameObject btnclos;
    bool fst = true;
    public GameObject vid;
    public Text querytxt;

    [Header("Inspector Stuff")]
    public Camera cam;
    Vector3 campos;
    public MessageBehavior messageBehavior;
    string fromServer;
    Texture2D imgtex;

    [Header("Inspector Stuff")]
    public GameObject StudentAmenityCenter;
    public GameObject Budget_Studies;
    public GameObject Engineering_Sciences;
    public GameObject Library;
    public GameObject Old_SOE;
    public GameObject Photonics;
    public GameObject PostOffice;
    public GameObject CSiS;
    public GameObject btn1;

    public AudioSource AudioStudentAmenityCenter;
    public AudioSource AudioBudget_Studies;
    public AudioSource AudioEngineering_Sciences;
    public AudioSource AudioLibrary;
    public AudioSource AudioOld_SOE;
    public AudioSource AudioPhotonics;
    public AudioSource AudioPostOffice;
    public AudioSource AudioCSiS;
    public GameObject DetailStudentAmenityCenter;
    public GameObject DetailBudget_Studies;
    public GameObject DetailEngineering_Sciences;
    public GameObject DetailLibrary;
    public GameObject DetailOld_SOE;
    public GameObject DetailPhotonics;
    public GameObject DetailPostOffice;
    public GameObject DetailCSiS;
    void Start()
    {
        vid.SetActive(false);
        DisableAll();
        btn1.SetActive(false);
        btnclos.SetActive(false);
    }

    void Update()
    {
        campos = cam.gameObject.transform.position;
        Swipe();
    }

    void DisableAll()
    {
        StudentAmenityCenter.SetActive(false);
        Budget_Studies.SetActive(false);
        Engineering_Sciences.SetActive(false);
        Library.SetActive(false);
        Old_SOE.SetActive(false);
        Photonics.SetActive(false);
        PostOffice.SetActive(false);
        CSiS.SetActive(false);
        DetailStudentAmenityCenter.SetActive(false);
        DetailBudget_Studies.SetActive(false);
        DetailEngineering_Sciences.SetActive(false);
        DetailLibrary.SetActive(false);
        DetailOld_SOE.SetActive(false);
        DetailPhotonics.SetActive(false);
        DetailPostOffice.SetActive(false);
        DetailCSiS.SetActive(false);
    }

    IEnumerator ProcessImage()
    {
        yield return new WaitForEndOfFrame();
        imgtex = ScreenCapture.CaptureScreenshotAsTexture();
        //imgtex.Resize(1280,720, imgtex.format, false);
        //imgtex.Apply();
        var imgdata = Convert.ToBase64String(imgtex.EncodeToPNG());
        //StartCoroutine(Post("http://172.16.14.5:5000/postjson", "{\"filename\":\"test.jpeg\",\"image\":\""+imgdata+"\"}"));
        StartCoroutine(Post("http://35.154.166.238:5000/postjson", "{\"filename\":\"test.jpeg\",\"image\":\""+imgdata+"\"}"));
    }

    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        //messageBehavior.ShowMessage(request.downloadHandler.text.ToString());
        //Debug.Log("Status Code: " + request.responseCode);
        //Debug.Log(request.downloadHandler.text.ToString());
        fromServer = request.downloadHandler.text.ToString();
        switch(fromServer.Split('"')[1])
        {
            case "amenity": messageBehavior.ShowMessage("Student Amenity Centre");
                            PlaceModel(StudentAmenityCenter,DetailStudentAmenityCenter);
                            PlayAudio(AudioStudentAmenityCenter);
                            break;
            case "budget": messageBehavior.ShowMessage("Center For Budget Studies");
                           PlaceModel(Budget_Studies,DetailBudget_Studies);
                           PlayAudio(AudioBudget_Studies);
                           break;
            case "engscience": messageBehavior.ShowMessage("Engineering Sciences");
                               PlaceModel(Engineering_Sciences,DetailEngineering_Sciences);
                               PlayAudio(AudioEngineering_Sciences);
                               break;
            case "library": messageBehavior.ShowMessage("University Library");
                            PlaceModel(Library,DetailLibrary);
                            PlayAudio(AudioLibrary);
                            break;
            case "oldsoe": messageBehavior.ShowMessage("Old SOE Block");
                           PlaceModel(Old_SOE,DetailOld_SOE);
                           PlayAudio(AudioOld_SOE);
                           break;
            case "photonics": messageBehavior.ShowMessage("International School Of Photonics");
                              PlaceModel(Photonics,DetailPhotonics);
                              PlayAudio(AudioPhotonics);
                              break;
            case "postoffice": messageBehavior.ShowMessage("CUSAT Post Office");
                               PlaceModel(PostOffice,DetailPostOffice);
                               PlayAudio(AudioPostOffice);
                               break;
            case "scipark": messageBehavior.ShowMessage("C-SIS Science Park");
                            PlayAudio(AudioCSiS);
                            vid.SetActive(true);
                            btnclos.SetActive(true);
                            PlaceModel(CSiS,DetailCSiS);
                            
                            break;
            default : messageBehavior.ShowMessage("Try Again !!!");break;
        }
    }
    void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    void PlaceModel(GameObject post,GameObject detail)
    {
        DisableAll();
        post.SetActive(true);
        Vector3 newpos = new Vector3(Mathf.Abs(campos.x) + 1,campos.y - 2,campos.z + 5);
        post.transform.position = newpos;
        GameObject temp =  (GameObject)Instantiate(post,newpos,Quaternion.identity);
        temp.tag = "clone";
        post.SetActive(false);
        /*
        var tpx = newpos.x;
        var tpy = newpos.y;
        var p2x = campos.x;
        var p2y = campos.z;
        Quaternion angle = cam.gameObject.transform.rotation;
        var rtx1 = (tpx - p2x) * Mathf.Cos(angle.y) - (tpy - p2y) * Mathf.Sin(angle.y) + p2x;
        var rty1 = (tpx - p2x) * Mathf.Sin(angle.y) + (tpy - p2y) * Mathf.Cos(angle.y) + p2y;
        post.transform.position = newpos;
        */
        detail.SetActive(true);
        newpos = new Vector3(Mathf.Abs(campos.x) - 3,campos.y - 2,campos.z + 5);
        detail.transform.position = newpos;
        GameObject temp1 =  (GameObject)Instantiate(detail,newpos,Quaternion.identity);
        temp1.tag = "clone";
        detail.SetActive(false);
    }


    void PlaceModel(GameObject post)
    {
        DisableAll();
        post.SetActive(true);
        Vector3 newpos = new Vector3(Mathf.Abs(campos.x) + 1,campos.y - 2,campos.z + 5);
        post.transform.position = newpos;
        Instantiate(post,newpos,Quaternion.identity);
        post.SetActive(false);
    }



    public void restartall()
    {
        cam.gameObject.transform.position = new Vector3(0,0,0);
        cam.gameObject.transform.rotation = Quaternion.identity;
        Application.LoadLevel(Application.loadedLevel);
    }
    public void btnq()
    {
        ProcessText();
    }

    void ProcessText()
    {
        try {
        var clones = GameObject.FindGameObjectsWithTag ("clone");
        if(clones.Length != 0)
        {
    foreach (var clone in clones){
        Destroy(clone);
    }
        }
        }
        catch {
            Debug.Log("No clone");
        }
       // var image = camFeed.getImagefromCam();
        Debug.Log("ProcessingText...");
        var response = StartCoroutine(PostQ("https://api.dialogflow.com/v1/query?v=20150910",
            "{\"contexts\":[],\"lang\":\"en\",\"query\":\""+querytxt.text+"\",\"sessionId\":\"12345\",\"timezone\":\"Asia/Colombo\"}"));
        // messageBehavior.ShowMessage();
    }

    IEnumerator PostQ(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer 1cff7bde0a664366890c1f5104be08c9");

        yield return request.SendWebRequest();
        // yield return request.Send();
        // messageBehavior.ShowMessage(request.responseCode.ToString());
        // messageBehavior.ShowMessage("Dialog " + request.responseCode.ToString());
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Dialog " + request.downloadHandler.text.ToString());
        }
        else
        {
           // Debug.Log("Data:" + request.downloadHandler.text);
            var response = DialogData.CreateFromJSON(request.downloadHandler.text);
            //Debug.Log("Intent: "+response.result.metadata.intentName);
            string strdialog = response.result.metadata.intentName.ToString();
          messageBehavior.ShowMessage(strdialog);

          switch(strdialog)
        {
            case "amenity": messageBehavior.ShowMessage("Student Amenity Centre");
                            PlaceModel(DetailStudentAmenityCenter);
                            PlayAudio(AudioStudentAmenityCenter);
                            break;
            case "budget": messageBehavior.ShowMessage("Center For Budget Studies");
                           PlaceModel(DetailBudget_Studies);
                           PlayAudio(AudioBudget_Studies);
                           break;
            case "engscience": messageBehavior.ShowMessage("Engineering Sciences");
                               PlaceModel(DetailEngineering_Sciences);
                               PlayAudio(AudioEngineering_Sciences);
                               break;
            case "library": messageBehavior.ShowMessage("University Library");
                            PlaceModel(DetailLibrary);
                            PlayAudio(AudioLibrary);
                            break;
            case "oldsoe": messageBehavior.ShowMessage("Old SOE Block");
                           PlaceModel(DetailOld_SOE);
                           PlayAudio(AudioOld_SOE);
                           break;
            case "photonics": messageBehavior.ShowMessage("International School Of Photonics");
                              PlaceModel(DetailPhotonics);
                              PlayAudio(AudioPhotonics);
                              break;
            case "postoffice": messageBehavior.ShowMessage("CUSAT Post Office");
                               PlaceModel(DetailPostOffice);
                               PlayAudio(AudioPostOffice);
                               break;
            case "scipark": messageBehavior.ShowMessage("C-SIS Science Park");
                            PlayAudio(AudioCSiS);
                            vid.SetActive(true);
                            btnclos.SetActive(true);
                            PlaceModel(DetailCSiS);
                            break;
            default : messageBehavior.ShowMessage("Try Again !!!");break;
        }
          
        }
        //Debug.Log("Status Code: " + request.responseCode);
    }



    Vector2 firstPressPos;
Vector2 secondPressPos;
Vector2 currentSwipe;
 
public void Swipe()
{
     if(Input.touches.Length > 0)
     {
         Touch t = Input.GetTouch(0);
         if(t.phase == TouchPhase.Began)
         {
              //save began touch 2d point
             firstPressPos = new Vector2(t.position.x,t.position.y);
         }
         if(t.phase == TouchPhase.Ended)
         {
              //save ended touch 2d point
             secondPressPos = new Vector2(t.position.x,t.position.y);
                           
              //create vector from the two points
             currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
               
             //normalize the 2d vector
             currentSwipe.Normalize();
 
             //swipe upwards
             if(currentSwipe.y > 0  && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
             {
                 Debug.Log("up swipe");
             }
             //swipe down
             if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
             {
                 Debug.Log("down swipe");
             }
             //swipe left
             if(currentSwipe.x < 0  && currentSwipe.y > -0.5f  && currentSwipe.y < 0.5f)
             {
                  Debug.Log(" swipe");
             }
             //swipe right
             if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
             {
                 
                 fst = false;
            //PlayAudio(AudioStudentAmenityCenter);
            //messageBehavior.ShowMessage("International School Of Photonics");
            StartCoroutine(ProcessImage());
            btn1.SetActive(true);
             }
         }
     }
}

public void closevid()
{
    vid.SetActive(false);
    btnclos.SetActive(false);
}
}
