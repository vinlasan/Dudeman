using UnityEngine;
using System.Collections;

public class Curve : Projectile {

    public Vector3 curvePoint;
    float curveRate,
        arcPivot;

    public bool pointChosen;

	// Use this for initialization
	void Start () {
        fltThrowSpeed = 0.75f;
        fltDamage = 1.5f;
        blnThrown = false;
        despawnTimer = 6.5f;
        curveRate = 45;
        arcPivot = 10;
        pointChosen = false;
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (blnThrown && !pointChosen)
        {
            ApplyDirection();
        }
        else if(pointChosen)
        {
            if(vecThrowDirection.x > 0)
                gameObject.transform.RotateAround(curvePoint, Vector3.forward, curveRate * fltThrowSpeed * Time.deltaTime);
            else if(vecThrowDirection.x < 0)
                gameObject.transform.RotateAround(curvePoint, Vector3.back, curveRate * fltThrowSpeed * Time.deltaTime);
        }
    }

    public override void Setup()
    {
        StartCoroutine(ChooseCurvePoint());
        base.Setup();
    }

    IEnumerator ChooseCurvePoint()
    {
        yield return new WaitForSeconds(0.5f);
        pointChosen = true;
        curvePoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + arcPivot, gameObject.transform.position.z);
    }
}
