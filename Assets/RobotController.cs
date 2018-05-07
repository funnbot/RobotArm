using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour {

	public float baseRotationSpeed = 2f;
	public float lowerArmSwivelSpeed = 1f;
	public float upperArmSwivelSpeed = 1f;
	public float clawSwivelSpeed = 1f;
	public float clawRotationSpeed = 100f;
	public float finger1SwivelSpeed = 1f;
	public float finger2SwivelSpeed = 1f;

	Robot robot;

	float lowerArmSwivel = 0f;
	float upperArmSwivel = 0f;
	float clawSwivel = 0f;
	float finger1Swivel = 0f;
	float finger2Swivel = 0f;

	string baseR = "d";
	string baseL = "a";
	string lowerArmR = "w";
	string lowerArmL = "s";
	string upperArmR = "q";
	string upperArmL = "e";
	string clawL = "z";
	string clawR = "c";
	string clawRotateR = "u";
	string clawRotateL = "p";
	string finger1R = "k";
	string finger1L = "i";
	string finger2R = "l";
	string finger2L = "o";

	void Start() {
		// robot = new Robot(transform);

		/*RotateAround(robot.Finger1.Lower, robot.Finger1.LowerSwivel, 45);
		RotateAround(robot.Finger1.Upper, robot.Finger1.UpperSwivel, -90);
		RotateAround(robot.Finger2.Lower, robot.Finger2.LowerSwivel, -45);
		RotateAround(robot.Finger2.Upper, robot.Finger2.UpperSwivel, 90);
		RotateAround(robot.Finger3.Lower, robot.Finger3.LowerSwivel, 45);
		RotateAround(robot.Finger3.Upper, robot.Finger3.UpperSwivel, -90);
		RotateAround(robot.Finger4.Lower, robot.Finger4.LowerSwivel, -45);
		RotateAround(robot.Finger4.Upper, robot.Finger4.UpperSwivel, 90);*/

		transform.GetChild(1).GetChild(2).transform.localEulerAngles = new Vector3(45, 0, 0);
	}

	void Update() {
		/* BaseRotator();
		ArmLower();
		ArmUpper();
		Claw();
		ClawRotator();
		Finger1();
		Finger2();*/
	}

	void Finger2() {
		float val = 0f;
		if (Key(finger2R)) {
			val += Bound(ref finger2Swivel, finger2SwivelSpeed, 0, 75);
		}
		if (Key(finger2L)) {
			val += Bound(ref finger2Swivel, -finger2SwivelSpeed, 0, 75);
		}
		RotateAround(robot.Finger4.Lower, robot.Finger4.LowerSwivel, -val);
		RotateAround(robot.Finger3.Lower, robot.Finger3.LowerSwivel, val);
	}

	void Finger1() {
		float val = 0f;
		if (Key(finger1R)) {
			val += Bound(ref finger1Swivel, finger1SwivelSpeed, 0, 75);
		}
		if (Key(finger1L)) {
			val += Bound(ref finger1Swivel, -finger1SwivelSpeed, 0, 75);
		}
		RotateAround(robot.Finger1.Lower, robot.Finger1.LowerSwivel, val);
		RotateAround(robot.Finger2.Lower, robot.Finger2.LowerSwivel, -val);
	}

	void ClawRotator() {
		if (Key(clawRotateR)) Rotate(robot.ClawRotator, clawRotationSpeed);
		if (Key(clawRotateL)) Rotate(robot.ClawRotator, -clawRotationSpeed);
	}

	void Claw() {
		Swivel(robot.Claw, robot.ClawSwivel, clawR, clawL, ref clawSwivel, clawSwivelSpeed, -120, 120);
	}

	void ArmUpper() {
		Swivel(robot.ArmUpper, robot.ArmUpperSwivel, upperArmR, upperArmL, ref upperArmSwivel, upperArmSwivelSpeed, -120, 120);
	}

	void ArmLower() {
		Swivel(robot.ArmLower, robot.ArmLowerSwivel, lowerArmR, lowerArmL, ref lowerArmSwivel, lowerArmSwivelSpeed, -90, 90);
	}

	void BaseRotator() {
		if (Key(baseR)) Rotate(robot.BaseRotator, baseRotationSpeed);
		if (Key(baseL)) Rotate(robot.BaseRotator, -baseRotationSpeed);
	}

	void Swivel(Transform t, Transform s, string k1, string k2, ref float swivel, float speed, float lower, float upper) {
		if (Key(k1)) {
			float val = Bound(ref swivel, speed, lower, upper);
			RotateAround(t, s, swivel);
		}
		if (Key(k2)) {
			float val = Bound(ref swivel, -speed, lower, upper);
			RotateAround(t, s, swivel);
		}
	}

	float Bound(ref float val, float speed, float lower, float upper) {
		val += speed;
		if (val < lower || val > upper) {
			val -= speed;
			return 0f;
		}
		return speed;
	}

	bool Key(string k) {
		return Input.GetKey(k);
	}

	void Rotate(Transform t, float y) {
		t.Rotate(0, y, 0);
	}

	void RotateAround(Transform t, Transform s, float a) {
		Rigidbody rb = t.GetComponent<Rigidbody>();
		Quaternion q = Quaternion.AngleAxis(a, rb.transform.up);
		rb.MovePosition(q * (rb.transform.position - s.position) + s.position);
		rb.MoveRotation(rb.transform.rotation * q);

		// t.RotateAround(s.position, t.right, a);
	}
}

public struct Robot {
	public Transform BaseRotator; // Robot child[1]

	public Transform ArmLower; // BaseRotator child[2]
	public Transform ArmLowerSwivel; // ArmLower child[0]

	public Transform ArmUpper; // ArmLower child[2]
	public Transform ArmUpperSwivel; // ArmUpper child[0]

	public Transform Claw; // ArmUpper child[2]
	public Transform ClawSwivel; // Claw child[0]

	public Transform ClawRotator; // Claw child[2]

	public Finger Finger1; // ClawRotator child[2]
	public Finger Finger2; // ClawRotator child[3]
	public Finger Finger3; // ClawRotator child[4]
	public Finger Finger4; // ClawRotator child[5]

	public Robot(Transform rob) {
		BaseRotator = rob.GetChild(1);
		ArmLower = BaseRotator.GetChild(2);
		ArmLowerSwivel = ArmLower.GetChild(0);
		ArmUpper = ArmLower.GetChild(2);
		ArmUpperSwivel = ArmUpper.GetChild(0);
		Claw = ArmUpper.GetChild(2);
		ClawSwivel = Claw.GetChild(0);
		ClawRotator = Claw.GetChild(2);

		Finger1 = new Finger(ClawRotator.GetChild(2));
		Finger2 = new Finger(ClawRotator.GetChild(3));
		Finger3 = new Finger(ClawRotator.GetChild(4));
		Finger4 = new Finger(ClawRotator.GetChild(5));
	}
}
public struct Finger {
	public Transform Lower; // ClawRotator child[2,3,4,5]
	public Transform LowerSwivel; // Lower child[0]

	public Transform Upper; // Lower child[2]
	public Transform UpperSwivel; // Upper child[0]

	public Finger(Transform lower) {
		Lower = lower;
		LowerSwivel = Lower.GetChild(0);
		Upper = Lower.GetChild(2);
		UpperSwivel = Upper.GetChild(0);
	}
}