using Godot;
using System;

public partial class SimBeginScene : Node3D
{
    MeshInstance3D anchor;
	MeshInstance3D ball;
	SpringModel spring; 

	Label KELabel;

	Label PELabel;

	Label TELabel;

	PendSim pend;
	double xA, yA, zA;
	float length;
	float length0;  //length of pend
	double angle;  //pendulum angle
	double angleInit; //initial pendulum angle
	double time;

	double PE;

	double KE;

	double TE;



	

	Godot.Vector3 endA; //end pt anchor
	Godot.Vector3 endB; //end pt pendulum


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello MEE 381 in Godot!");
		xA = 0.0; yA = 1.2; zA = 0.0;
		anchor = GetNode<MeshInstance3D>("Anchor");
		ball = GetNode<MeshInstance3D>("Ball1");
		spring = GetNode<SpringModel>("SpringModel");
	
		endA = new Godot.Vector3((float)xA, (float)yA,(float)zA);
		anchor.Position = endA;
		PELabel = GetNode<Label>("PELabel");
		//KELabel = GetNode<Label>("KELabel");
		//TELabel = GetNode<Label>("TELabel");


		pend = new PendSim();


		length0 = length = 0.9f;
		spring.GenMesh(0.05f, 0.015f, length, 6.0f, 62);

		angleInit = Mathf.DegToRad(60.0);
		float angleF = (float) angleInit;
		pend.Angle = (double)angleInit;

		endB.X = endA.X + length*Mathf.Sin(angleF);
		endB.Y = endA.Y - length*Mathf.Cos(angleF);
		endB.Z = endA.Z;
		PlacePendulum(endB);
		//PlacePendulum((float)angle); 

		time = 0.0;


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float angleF = 1.0f*(float)Math.Sin(2.0 * time);
		float angleA = (float)(0.4*time);
		length = length0 + 0.3f * (float)Math.Cos(4.0*time);

		PE = 1.4 * 9.81 * (length);

		//KE = 0.5 * 0.9 * Math.Pow(velocity,2);

		PELabel.Text = PE.ToString("PE: " + "0.00");
		//KELabel.Text = KE.ToString("KE: " + "0.00");
		//TELabel.Text = TE.ToString("TE: " + "0.00");


		//float angleA = 0.0f
		//float angleF = (float)pend.Angle;

		

		float hz = length*Mathf.Cos(angleA);
		endB.X = endA.X + length*Mathf.Sin(angleA);
		endB.Y = endA.Y - length*Mathf.Cos(angleF);
		endB.Z = endA.Z + hz*Mathf.Sin(angleA);
		PlacePendulum(endB);
		time += delta;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		pend.StepRK2(time, delta); 
	}
	private void PlacePendulum(Godot.Vector3 endBB)
	{
		spring.PlaceEndPoints(endA, endB);
		ball.Position = endBB;

	}
}
