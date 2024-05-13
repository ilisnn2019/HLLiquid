using UnityEngine;

[ExecuteInEditMode]
public class LiquidController : MonoBehaviour {

    public GameObject bottle;
    public GameObject waterSurf;
    public Material liquid;
    public Material topLiquidSurface;

    public float BottleHeight;
    public float BottleWidth;

    public float THRESHOLD = 1.1f;

    [Space]
    public Vector3 offset;
    public float liquidHeight;
    [Space]
    public float recoveryTime = 10;
    public float facingScale = 20;
    public float velocitySpeed = 10;

    Vector3 normal;
    Vector3 pos;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastVelocity;
    Vector3 lastRot;  
    Vector3 angularVelocity;
    Vector3 lastAngularVelocity;
    Vector3 facing;

    float stopElapsedTime = 0f;
    float moveElapsedTime = 0f;
    float stopAngularElapsedTime = 0f;
    float moveAngularElapsedTime = 0f;

    void Start () {
        initMovement();
        updatePlaneTransform();
        updateShaderProperties();
    }

    void Update (){
        updatePlaneTransform();
<<<<<<< HEAD

=======
        
>>>>>>> ceaac58c43a53d88d6a5c49aae830a1206fc8ebe
    }

    void initMovement(){
        facing = Vector3.zero;
        normal = waterSurf.transform.TransformVector(new Vector3(0,0,-1));
        pos = waterSurf.transform.position;
        lastPos = transform.position;
        velocity = (lastPos - transform.position) / Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, 1f);
        lastRot = transform.eulerAngles;
        angularVelocity = (lastRot - transform.eulerAngles) / Time.deltaTime;
        angularVelocity = Vector3.ClampMagnitude(angularVelocity, 1f);
    }

    void updatePlaneTransform(){
        //waterSurf position
        Vector3 surfacePos = bottle.transform.position + offset;

<<<<<<< HEAD
        float dist = offset.y;

        Vector3 bottlebottom = bottle.transform.position;
        Vector3 bottletop = bottle.transform.position + transform.TransformDirection(Vector3.up) * liquidHeight;

        //float ratio = Mathf.Clamp( Mathf.Abs(bottle.transform.eulerAngles.z) / 180,dist,BottleHeight - dist*1.4f );

        
        float ratio = transform.localEulerAngles.z <= 180? transform.localEulerAngles.z%180 / 180: (360 - transform.localEulerAngles.z )/ 180;

        Debug.Log(ratio);

        /*
        if (bottletop.y + THRESHOLD < bottlebottom.y){
            bottlebottom = bottletop;
            bottletop = bottle.transform.position;
        }
        */
=======
        Vector3 bottlebottom = bottle.transform.position;
        Vector3 bottletop = bottle.transform.position + transform.TransformDirection(Vector3.up) * liquidHeight;

        float dist = offset.y;
        if(bottletop.y + THRESHOLD < bottlebottom.y){
            bottlebottom = bottletop;
            bottletop = bottle.transform.position;
        }
>>>>>>> ceaac58c43a53d88d6a5c49aae830a1206fc8ebe

        Vector3 heading = bottletop - bottlebottom;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        Vector3 orthogonal = Vector3.Cross(-bottle.transform.forward, heading).normalized;

<<<<<<< HEAD
        //surfacePos = bottlebottom + direction * dist;
        surfacePos = bottlebottom + direction * dist + (BottleHeight - dist * 1.5f) * ratio * direction; //calculate surface position

        surfacePos += ((offset.y / BottleHeight) / 2 - .5f) * BottleWidth * orthogonal;

        waterSurf.transform.position = surfacePos; //set liquid surface position

        //waterSurf rotation
        //water wave
=======
        surfacePos = bottlebottom + direction * dist; //calculate surface position
        if(bottle.transform.rotation.z>=-90 && bottle.transform.rotation.z <=90)
            surfacePos += ((offset.y / BottleHeight) / 2 - .5f ) * BottleWidth * orthogonal;
        else
            surfacePos += -((offset.y / BottleHeight) / 2 - .5f) * BottleWidth * -orthogonal;

        Debug.Log(surfacePos);
        waterSurf.transform.position = surfacePos; //set liquid surface position

        //waterSurf rotation
        //water save
>>>>>>> ceaac58c43a53d88d6a5c49aae830a1206fc8ebe
        if(velocity.magnitude == 0){
            moveElapsedTime = 0;
            stopElapsedTime += Time.deltaTime;
            float t = Mathf.PingPong(stopElapsedTime * recoveryTime, 1);
            Vector3 ppFacing = Vector3.Lerp(-lastVelocity, lastVelocity, t);
            facing = Vector3.Lerp(ppFacing, Vector3.zero, stopElapsedTime / lastVelocity.magnitude);
        }else{
            stopElapsedTime = 0;
            moveElapsedTime += Time.deltaTime;
            facing = facingScale * velocity * moveElapsedTime * lastVelocity.magnitude;
            lastVelocity = velocity * Mathf.Clamp(moveElapsedTime * velocitySpeed, .1f, .51f);
            angularVelocity = Vector3.zero;
            lastAngularVelocity = Vector3.zero;
        }

        if(angularVelocity.magnitude == 0){
            moveAngularElapsedTime = 0;
            stopAngularElapsedTime += Time.deltaTime;
            float t = Mathf.PingPong(stopAngularElapsedTime * recoveryTime, 1);
            Vector3 ppFacing = Vector3.Lerp(-lastAngularVelocity, lastAngularVelocity, t);
            facing += Vector3.Lerp(ppFacing, Vector3.zero, stopAngularElapsedTime / lastAngularVelocity.magnitude);           
        }else{
            stopAngularElapsedTime = 0;
            moveAngularElapsedTime += Time.deltaTime;
            lastAngularVelocity = angularVelocity * Mathf.Clamp(moveAngularElapsedTime * velocitySpeed, .1f, .51f);
        }

        if(lastVelocity.magnitude > 0 ){
            facing = new Vector3(
                facing.x + facing.y,
                0,
                facing.z + facing.y
            );
        }

        if(lastAngularVelocity.magnitude > 0 ){
            facing = Quaternion.AngleAxis(transform.localRotation.eulerAngles.y + 90, Vector3.up) * facing;
            facing = new Vector3(
                facing.x + facing.y/2,
                0,
                facing.z + facing.y/2
            );
        }


        velocity = (lastPos - transform.position) / Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, 0.51f);

        angularVelocity = (lastRot - transform.eulerAngles) / Time.deltaTime;
        angularVelocity = Vector3.ClampMagnitude(angularVelocity, 0.51f);

        lastPos = transform.position;
        lastRot = transform.eulerAngles;

        waterSurf.transform.LookAt(waterSurf.transform.position - Vector3.up + facing);

        updateShaderProperties();
    }

    void updateShaderProperties(){
        pos = waterSurf.transform.position;
        normal = -waterSurf.transform.forward;


        liquid.SetVector("_PlanePosition", pos);
        liquid.SetVector("_PlaneNormal", normal);

        liquid.SetVector("_Inertia", facing);
        topLiquidSurface.SetVector("_Inertia", facing);

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector3 topD = transform.TransformDirection(Vector3.up) * liquidHeight;
        Gizmos.DrawRay(transform.position, topD);
        //Gizmos.color = Color.yellow;
        //Vector3 liquidLevel = -facing * liquidHeight;
        //Gizmos.DrawRay(waterSurf.transform.position, liquidLevel);
    }
}