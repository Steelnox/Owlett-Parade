using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaRainAttack : Skill
{
    public GameObject pinPrefab;
    public GameObject barragePrefab;

    public float pinPlacementRange = 5f;

    public GameObject placedPin;

    public override void Enter()
    {

        if (placedPin) SendBarrage();
        else PlacePin();
    }

    public override void Exit()
    {

    }

    private void PlacePin()
    {
        Vector3 pinPosition = MathExtension.MouseWorldPosition("Floor");

        pinPosition = transform.position + ((pinPosition - transform.position).normalized * pinPlacementRange);

        placedPin = Instantiate(pinPrefab, pinPosition, Quaternion.identity);

        controller.ReturnToBaseState();
    }

    private void SendBarrage()
    {
        Instantiate(barragePrefab, placedPin.transform.position, Quaternion.identity);

        Destroy(placedPin);
        placedPin = null;

        controller.ReturnToBaseState();

        base.Exit();
    }
}
