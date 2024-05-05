using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class MenuManager : MonoBehaviour
{  
  public Panel currentPanel = null;
  private List<Panel> panelHistory = new List<Panel>();
  public SteamVR_Action_Boolean triggerAction;
  public SteamVR_Input_Sources handType;

  private void Start()
  {
    SetupPanels();
  }

private void SetupPanels()
  {
    Panel[] panels = GetComponentsInChildren<Panel>();
    foreach (Panel panel in panels)
        panel.Setup(this);
    currentPanel.Show();
  }
  private void Update()
  {
    if (triggerAction.GetStateDown(handType)) //user input
      GoToPrevious();
  }
  public void GoToPrevious()
  {
    if(panelHistory.Count == 0)
      return;
    int lastIndex = panelHistory.Count - 1;
    SetCurrent(panelHistory[lastIndex]);
    panelHistory.RemoveAt(lastIndex);
  }
  public void SetCurrentWithHistory(Panel newPanel)
  {
    panelHistory.Add(currentPanel);
    SetCurrent(newPanel);
  }

  private void SetCurrent(Panel newPanel)
  {
        currentPanel.Hide();
        currentPanel = newPanel;
        currentPanel.Show();
        
  }
}
