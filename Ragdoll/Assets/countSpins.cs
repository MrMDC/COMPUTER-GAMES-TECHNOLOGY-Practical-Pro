using System.Collections;
using UnityEngine;

public class countSpins : MonoBehaviour
{
    public GameObject explosion;    
    public GameObject explodingObject;

    private string recordHits = "";
    private string matchString = "1231231231231231";
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spinner")
        {
            recordHits += other.gameObject.name;
            if (recordHits.Contains(matchString)) { 
                Instantiate(explosion,explodingObject.transform.position , Quaternion.identity);

                Destroy(explodingObject);
            }

            //clean out recordHits string to 
            //stop it getting too big
            if(recordHits.Length > (matchString.Length*2)) {
                //remove first set of characters
                //that are the same length but 
                //don't match the string
                recordHits = recordHits.Substring(matchString.Length);

                Debug.Log(recordHits);
            }
        }
    }
}
