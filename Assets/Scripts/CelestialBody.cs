using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class CelestialBody : GravityObject
{
    public float radius;
    public float surfaceGravity;
    public Vector3 initialVelocity;
    public Vector3 velocity {get; private set;}

    public float mass { get { return GravMath.Mass(surfaceGravity, radius, true); } }

    Rigidbody rb;

    public Rigidbody Rigidbody { get { return rb; } }
    public Vector3 Position { get { return rb.position; } }
    
    private void OnValidate() 
    {
        if (rb != null) 
        {
            rb.mass = mass;
            rb.velocity = initialVelocity;
        }
        else 
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.mass = mass;
            rb.velocity = initialVelocity;
        }
        if(transform.childCount <= 0) 
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.parent = transform;
            sphere.transform.localPosition = Vector3.zero;
            sphere.transform.localScale = Vector3.one * radius * 2;
            sphere.GetComponent<MeshRenderer>().material = new Material(Shader.Find("HDRP/Lit"));
        }
        else
        {
            transform.GetChild(0).transform.localScale = Vector3.one * radius * 2;
        }
    }

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = mass;
        velocity = initialVelocity;
    }
    //Calculate the force of gravity between all celestial bodies objects
    public Vector3 CalculateVelocity(CelestialBody[] bodies)
    {
        Vector3 forceVector = Vector3.zero;
        foreach (CelestialBody other in bodies)
        {
            if (other != this)
            {
                Vector3 direction = (other.rb.position - rb.position).normalized;
                float distance = Vector3.Distance(other.rb.position, rb.position);
                float acceleration = GravMath.GravAccel(other.mass, distance);
                forceVector = direction * acceleration; 
            }
        }
        return forceVector;
    }
    public void UpdateVelocity(Vector3 forceVector, float timeStep)
    {
        velocity += forceVector * timeStep;
    }
    public void UpdatePosition(float timeStep)
    {
        rb.MovePosition (rb.position + velocity * timeStep);
    }


}
