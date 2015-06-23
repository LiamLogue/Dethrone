////////////////////////////////
/// File   : AbilityHoverTooltip.cs
/// Author : Liam Logue
/// Desc   : Shows a tooltip 
///          with the ability 
///          information. 
////////////////////////////////
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityHoverTooltip : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public string TooltipTitle;
	public string TooltipText;
	public Text TooltipTitleObject;
	public Text TooltipTextObject;
	public GameObject directionIndicator;
	public GameObject radiusIndicator;
	
	/// <summary>
	/// Shows the tooltip.
	/// </summary>
	public void ShowTooltip() {		
		//Set the text to be shown
		TooltipTitleObject.text = TooltipTitle.ToUpper();
		TooltipTextObject.text = TooltipText.ToUpper();
		
		//Enable our tooltip canvas
		TooltipTitleObject.transform.parent.gameObject.SetActive(true);
		
		//Which type of indicator to display?
		if(this.transform.gameObject.name == "AbilityQ" || this.transform.gameObject.name == "AbilityE") {
			//Show directional indicator
			directionIndicator.transform.gameObject.SetActive(true);
		}
		else {
			//Show radius indicator
			radiusIndicator.transform.gameObject.SetActive(true);
		}
	}
	
	/// <summary>
	/// Hides the tooltip.
	/// </summary>
	public void HideTooltip() {
		//Disable our tooltip canvas
		TooltipTitleObject.transform.parent.gameObject.SetActive(false);
		
		//Hide indicators
		directionIndicator.transform.gameObject.SetActive(false);
		radiusIndicator.transform.gameObject.SetActive(false);			
	}
}
