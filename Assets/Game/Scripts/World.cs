using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class World : MonoBehaviour, IWorld
{

    [SerializeField] GameObject playerRenderer;

    //TEMP
    [SerializeField] Transform floor0;
    [SerializeField] Transform floor1;
    [SerializeField] Transform floor2;
    [SerializeField] Transform floor3;
    [SerializeField] Transform floor4;
    [SerializeField] Transform floor5;

    public void setActiveRoom(int stencil) {
        playerRenderer.GetComponent<Renderer>().material.SetInt("_StencilMask", stencil);
        foreach (Transform trans in transform) {
            var room = trans.GetComponent<Room>();
            if (room == null) continue;
            //Debug.Log("Room: " + room.stencilNumber + " Stencil: " + stencil) ;
            if (room.stencilNumber == stencil) {
                ChangeLayer(trans, 6);
            } else {
                ChangeLayer(trans, 7);
            }
        }
    }

    private void ChangeLayer(Transform room, int layer) {
        foreach (Transform childs in room.GetComponentsInChildren<Transform>()) {
            childs.gameObject.layer = layer;
        }
    }

    private void Awake() {

        //TEMP
        var playArea = new HmdQuad_t();
        var chaperone = OpenVR.Chaperone;
        bool success = (chaperone != null) && chaperone.GetPlayAreaRect(ref playArea);

        if (success) {
            Vector3 newScale = new Vector3(Mathf.Abs(playArea.vCorners0.v0 - playArea.vCorners2.v0), Mathf.Abs(playArea.vCorners0.v2 - playArea.vCorners2.v2), this.transform.localScale.y);

            floor0.localScale = newScale;
            floor1.localScale = newScale;
            floor2.localScale = newScale;
            floor3.localScale = newScale;
            floor4.localScale = newScale;
            floor5.localScale = newScale;
        }
    }

    public Room getRoomFromStencil(int stencil) {
        foreach (Transform rooms in transform) {
            var room = rooms.GetComponent<Room>();
            if (room == null) continue;
            if (room.stencilNumber == stencil) return room;
        }
        return null;
    }

}
