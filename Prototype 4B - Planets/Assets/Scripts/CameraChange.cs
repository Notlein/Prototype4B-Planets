using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{

    public int state;
    public CinemachineRecomposer camera_recomp;
    public CinemachineCameraOffset camera_offset;
    public Cinemachine.CinemachineVirtualCamera camera_virtual;
    public Cinemachine.CinemachineHardLockToTarget cam_transposer;

    // Start is called before the first frame update
    void Start()
    {
        camera_recomp = GetComponent<CinemachineRecomposer>();
        camera_offset = GetComponent<CinemachineCameraOffset>();
        camera_virtual = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cam_transposer = camera_virtual.GetCinemachineComponent<Cinemachine.CinemachineHardLockToTarget>();
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            switch (state)
            { 
                case 0:
                    ChangeCamera(1);
                    state++;
                    break;
                case 1:
                    ChangeCamera(2);
                    state++;
                    break;
                case 2:
                    ChangeCamera(0);
                    state = 0;
                    break;

            }


        }
    }

    void ChangeCamera(int val)
    {

        if (val == 0)
        {
            cam_transposer.m_Damping = 2.0f;
            camera_recomp.m_Tilt = 4.0f;
            camera_offset.m_Offset.y = 1.0f;
            camera_offset.m_Offset.x = 1.0f;
            camera_offset.m_Offset.z = -5.0f;
        }
        if (val == 1) {
            cam_transposer.m_Damping = 1.0f;
            camera_recomp.m_Tilt = 70.0f;
            camera_offset.m_Offset.x = 0.0f;
            camera_offset.m_Offset.y = 12.0f;
            camera_offset.m_Offset.z = -1.0f;

        }
        if (val == 2)
        {
            cam_transposer.m_Damping = 0.0f;
            camera_recomp.m_Tilt = 20.0f;
            camera_offset.m_Offset.y = 0.0f;
            camera_offset.m_Offset.x = 0.0f;
            camera_offset.m_Offset.z = 0.0f;

        }


    }
}
