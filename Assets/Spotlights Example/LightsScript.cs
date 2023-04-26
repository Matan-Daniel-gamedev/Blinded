using UnityEngine;

public class LightsScript : MonoBehaviour {

	public bool areLightsOn = false;
	[Range(0, 1)] public float BackgroundLightIntensity = 0.25f;
	public int NumberOfLights = 1;
	public GameObject LightPrefab;
	Light myLight;
	GameObject Player;
	void Awake() {
		Player = GameObject.Find("Player");
		
		var newLight = Instantiate(LightPrefab, Player.transform);
		newLight.name = "Light";
		myLight= newLight.gameObject.GetComponent<Light>();
		myLight.intensity = 1 - BackgroundLightIntensity;
		myLight.gameObject.SetActive(areLightsOn);
		

		//Initialize ambient light
		RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
		if (areLightsOn) {
			RenderSettings.ambientLight = new Color(BackgroundLightIntensity, BackgroundLightIntensity, BackgroundLightIntensity);
		}
		else {
			RenderSettings.ambientLight = Color.white;
		}
	}

	void Update() {
		//Change ambient light intensity
		if (areLightsOn) {
			RenderSettings.ambientLight = new Color(BackgroundLightIntensity, BackgroundLightIntensity, BackgroundLightIntensity);
		}
		else {
			RenderSettings.ambientLight = Color.white;
		}

		//Activate or deactivate lights and set intensity

		myLight.intensity = 1 - RenderSettings.ambientLight.grayscale;//gray out rest of the area
		myLight.gameObject.SetActive(areLightsOn);
		
	}
}