using UnityEngine;

public class PoliceCarController : MonoBehaviour
{
    public float driveSpeed = 12f;
    public float steeringSpeed = 60f;
    public Light leftHeadlight;
    public Light rightHeadlight;
    public Light leftWarningLight;
    public Light rightWarningLight;
    public AudioSource warningSound;

    private bool headlightsOn = false;
    private bool warningModeActive = false;
    private float lightToggleDelay = 0.7f;
    private float lightToggleTimer = 0.5f;
    private bool isLeftWarningLightOn = false;
    private bool isRightWarningLightOn = false;

    void Start()
    {
        InitializeLightsAndSound();
    }

    void Update()
    {
        DrivePoliceCar();
        ToggleHeadlights();
        UpdateWarningLights();
        ControlWarningSound();
    }

    private void InitializeLightsAndSound()
    {
        leftWarningLight.intensity = 100;
        rightWarningLight.intensity = 100;
        if (warningSound != null)
        {
            warningSound.Stop();
        }
    }

    private void DrivePoliceCar()
    {
        float forwardMovement = Input.GetAxis("Vertical") * driveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * forwardMovement);

        float rotation = Input.GetAxis("Horizontal") * steeringSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotation);
    }

    private void ToggleHeadlights()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            headlightsOn = !headlightsOn;
            leftHeadlight.enabled = headlightsOn;
            rightHeadlight.enabled = headlightsOn;
        }
    }

    private void UpdateWarningLights()
    {
        lightToggleTimer += Time.deltaTime;

        if (lightToggleTimer >= lightToggleDelay)
        {
            if (isLeftWarningLightOn)
            {
                leftWarningLight.enabled = false;
                rightWarningLight.enabled = true;
            }
            else
            {
                leftWarningLight.enabled = true;
                rightWarningLight.enabled = false;
            }

            isLeftWarningLightOn = !isLeftWarningLightOn;
            isRightWarningLightOn = !isRightWarningLightOn;
            lightToggleTimer = 0f;
        }
    }

    private void ControlWarningSound()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            warningModeActive = !warningModeActive;

            if (warningModeActive)
            {
                leftWarningLight.intensity = 400;
                rightWarningLight.intensity = 400;

                if (warningSound != null && !warningSound.isPlaying)
                {
                    warningSound.Play();
                }
            }
            else
            {
                leftWarningLight.intensity = 100;
                rightWarningLight.intensity = 100;

                if (warningSound != null && warningSound.isPlaying)
                {
                    warningSound.Stop();
                }
            }
        }
    }
}
