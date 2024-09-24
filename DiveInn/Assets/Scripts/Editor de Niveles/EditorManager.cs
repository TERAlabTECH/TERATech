using System.Collections;
using System.Collections.Generic;
using AutomataUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorManager : MonoBehaviour
{
    
    public GameObject Screen;
    public GameObject amountOfNhTextBox;

    Text amountOfNhText;
    MNCA mnca;

    public GameObject menu;


    void Start()
    {
        mnca= Screen.GetComponent<MNCA>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMode(){
       
        if(mnca.kernelToggle==0){
            mnca.kernelToggle=1;
            mnca.SendRandomData(32,0);
            mnca.SendRandomData(32,1);
            mnca.SendRandomData(32,2);
            mnca.SendRandomData(32,3);
        } else{
            mnca.kernelToggle=0;
        }
        

        Debug.Log("Clicked");
    }

    public void ChangeBaseRender(){
        mnca.ChangeBaseRenderTexture();

    }

    public void CleanScreen(){
        mnca.CleanScreen();
    }

    public void SaveScreenAsSprite(){
         RenderTexture screen=(RenderTexture) mnca.render.material.GetTexture("_MainTex");
         AutomataHelper.SaveRender(screen);
    }
    public void AddBackground(){
        mnca.kernelToggle=2;
    }
    public void SendSequence(string input){
        mnca.SetAutomaton(input);
    }
    public void AddToAmountOfNeighborhoods(){
        if(mnca.amountOfNhValue<12){
            mnca.amountOfNhValue++;
            for(int i=0; i<4; i++){
                mnca.randomDataStorage[i][32]=mnca.amountOfNhValue;
            }
            ChangeValueOfAmountOfNhText();
            
        }

    }
    public void SubtractToAmountOfNeighborhoods(){
        if(mnca.amountOfNhValue>1){
            mnca.amountOfNhValue--;
            for(int i=0; i<4; i++){
                mnca.randomDataStorage[i][32]=mnca.amountOfNhValue;
            }
            ChangeValueOfAmountOfNhText();
        }
    }

    public void ChangeValueOfAmountOfNhText(){
        amountOfNhText=amountOfNhTextBox.GetComponent<Text>();
        amountOfNhText.text=$"{mnca.amountOfNhValue}";
    }

    public void OpenHideMenu(){
        if(menu.activeSelf){
            menu.SetActive(false);
        }else{
            menu.SetActive(true);
        }
    }

}
