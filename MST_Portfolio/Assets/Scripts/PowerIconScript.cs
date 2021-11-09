using UnityEngine;

public class PowerIconScript : MonoBehaviour
{
    private Transform _characterTransform;
    private bool _hasMagnet;
    private bool _isExploading;
    private Vector3 _explodingPos;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 100);
        if (_hasMagnet)
            transform.position = Vector3.MoveTowards(transform.position, _characterTransform.position + Vector3.up, Time.deltaTime * 6f);
        if(_isExploading )
        {
            transform.position = Vector3.MoveTowards(transform.position, _explodingPos, Time.deltaTime * 13f);
            if (transform.position == _explodingPos)
                _isExploading = false;
        }
    }

    public void SetVisibility(bool setActive)
    {
        gameObject.SetActive(setActive);

        if (!setActive && _hasMagnet)
            _hasMagnet = false;
        if(!setActive && _isExploading)
            _isExploading = false;
    }

    public void MagnetPower(Transform characterTransform)
    {
        _characterTransform = characterTransform;
        _hasMagnet = true;
    }

    public void explodingPowerIconPos(Vector3 pos)
    {
        _explodingPos = pos;
        _isExploading = true;
    }
}