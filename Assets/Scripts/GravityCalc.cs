using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCalc : MonoBehaviour
{
    static GravityCalc instance;
    CelestialBody[] bodies;

    // Start is called before the first frame update
    void Awake()
    {
        bodies = FindObjectsOfType<CelestialBody>();
        Time.fixedDeltaTime = Universe.physicsTimeStep;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*for (int i = 0; i < bodies.Length; i++)
        {
            Vector3 acceleration = CalculateAcceleration(bodies[i].Position, bodies[i]);
            bodies[i].UpdateVelocity(acceleration, Universe.physicsTimeStep);
        }
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(Universe.physicsTimeStep);
        }*/
        
        foreach (CelestialBody body in bodies)
        {
            body.UpdateVelocity(body.CalculateVelocity(bodies), Universe.physicsTimeStep);
        }
        foreach (CelestialBody body in bodies)
        {
            body.UpdatePosition(Universe.physicsTimeStep);
        }
    }

    public static Vector3 CalculateAcceleration (Vector3 point, CelestialBody ignoreBody = null) {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.bodies) {
            if (body != ignoreBody) {
                float sqrDst = (body.Position - point).sqrMagnitude;
                Vector3 forceDir = (body.Position - point).normalized;
                acceleration += forceDir * Universe.gravitationalConstant * body.mass / sqrDst;
            }
        }

        return acceleration;
    }

     public static CelestialBody[] Bodies {
        get {
            return Instance.bodies;
        }
    }

    static GravityCalc Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GravityCalc> ();
            }
            return instance;
        }
    }
}
