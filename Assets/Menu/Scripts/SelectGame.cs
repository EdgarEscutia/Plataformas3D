using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SelectGame : MonoBehaviour
{
    public void PlayerSelection(string selectPlayer)
    {SceneManager.LoadScene(selectPlayer); }
        
    public void Tutorial(string tutorial)
    {SceneManager.LoadScene(tutorial); }
        
    public void Pratice(string practice)
    { SceneManager.LoadScene(practice); }
       
    public void Minigame(string minigame)
    {  SceneManager.LoadScene(minigame); }

    public void Back(string back)
    { SceneManager.LoadScene(back); }
}
