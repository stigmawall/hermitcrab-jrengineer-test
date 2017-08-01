using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseFeedback : MonoBehaviour {
    public Material HoverMaterialAble;
    public Material HoverMaterialUnable;

    private List<Renderer> _meshes = new List<Renderer>();
    private Dictionary<Renderer, Material[]> _outlineInactive = new Dictionary<Renderer, Material[]>();
    private Dictionary<Renderer, Material[]> _outlineActive = new Dictionary<Renderer, Material[]>();

    private void SetOutline(bool active)
    {
        var targetDictionary = (active ? _outlineActive : _outlineInactive);
        _meshes.Clear();
        GetComponentsInChildren<Renderer>(_meshes);
        foreach (var renderer in _meshes)
        {
            if (!_outlineActive.ContainsKey(renderer))
            {
                var inactiveMaterials = renderer.sharedMaterials;

                var activeMaterials = new Material[inactiveMaterials.Length + 1];
                Array.Copy(inactiveMaterials, activeMaterials, inactiveMaterials.Length);

                activeMaterials[activeMaterials.Length - 1] = HoverMaterialAble;

                _outlineInactive.Add(renderer, inactiveMaterials);
                _outlineActive.Add(renderer, activeMaterials);
            }
            renderer.sharedMaterials = targetDictionary[renderer];
        }
    }

    public virtual void OnHoverEnter()
    {
        SetOutline(true);
    }

    public virtual void OnHoverExit()
    {
        SetOutline(false);
    }
}
