using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    MeshRenderer renderer;
    [SerializeField]
    TextMeshPro text;
    [SerializeField]
    public List<string> values;

    [SerializeField]
    GameObject textfield;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    bool canGoNext;
    
    // Start is called before the first frame update
    void Start()
    {
        canGoNext=true;
        renderer.sortingOrder=12;
        text.SetText("Hello User");
    }

   void resetCanGo(){
    canGoNext=true;
   }
    void FixedUpdate()
    {
        if(values.Count<=0){
            spriteRenderer.enabled=false;
            textfield.SetActive(false);
            return;
        }
        if(values.Count>0 && spriteRenderer.enabled==false){
            text.SetText(values[0]);
            spriteRenderer.enabled=true;
            textfield.SetActive(true);
            canGoNext=false;
            
                Invoke("resetCanGo",1);
        }

        if(canGoNext && Input.GetMouseButtonDown(0)){
            values.RemoveAt(0);
            if(values.Count<=0){
                spriteRenderer.enabled=false;
                textfield.SetActive(false);
                return;
            }else{
                text.SetText(values[0]);
                canGoNext=false;
                Invoke("resetCanGo",1);
                
            }

        }

    }

}
