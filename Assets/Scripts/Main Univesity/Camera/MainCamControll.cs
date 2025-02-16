using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class MainCamControll : MonoBehaviour
{
  private Vector2 _startPos;
  private Camera cam;

  [SerializeField] private float SpeedOfZoom;

  [SerializeField] private float _stepsOfDoubleTapsZoom;

  public float targetPosx;
  public float targetPosy;
  [SerializeField] private float speed;

  [SerializeField] private float ZoomMin = 5.5f;
  [SerializeField] private float ZoomMax = 65;

  [SerializeField] private float XMin = -28.7f;
  [SerializeField] private float XMax = 28.7f;
  [SerializeField] private float YMin = -1.5f;
  [SerializeField] private float YMax = 10;

  [SerializeField] private float _minTrailWight = 0.15f;
  [SerializeField] private float _maxTrailWight = 0.3f;
  [SerializeField] float speedOfTrail;

  [SerializeField] private float _markerMinValue = 0.23f;
  [SerializeField] private float _markerMaxValue = 0.4f;
  [SerializeField] private float _markerValueIncrease;
  
  [SerializeField] private float _rotationSpeed;

  [SerializeField] private GameObject _drawWay;
  //[SerializeField] private GameObject _list;
  public List<GameObject> _textList;

  [SerializeField] private GameObject _marker;

  private float _lastClickTime;

  private const float TIME_BETWEEN_CLICKS = 0.2f;

  private float _rotationAngle;
  
  [SerializeField] private GameObject _firstFloor;
  [SerializeField] private GameObject _secondFloor;
  [SerializeField] private GameObject _thirdFloor;
  [SerializeField] private GameObject _fourthFloor;

  [SerializeField] private float _increment;
  [SerializeField] private GameObject _markerMain;
  [Space]
  [SerializeField] private GameObject AI;
  [SerializeField] private GameObject End;
  [SerializeField] private WayDetailsController _wayDetailsController;

  private float _zPosFirst;
  private float _zPosSecond;

  [SerializeField] private Transform _listOfDetails;

  [SerializeField] private GameObject _uiCollisionCheck;
  [SerializeField] private bool _isCameraCanMove = true;
  public string _endPointText { get; set; }

  private void Start()
  {
    cam = GetComponent<Camera>();
    targetPosx = transform.position.x;
    targetPosy = transform.position.y;

    Application.targetFrameRate = 60;
    
    GetAllChilds(_firstFloor);
    GetAllChilds(_secondFloor);
    GetAllChilds(_thirdFloor);
    GetAllChilds(_fourthFloor);
    
    _drawWay.SetActive(true);
    StartCoroutine(AllRoomsGetterTimer());
  }
  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      _startPos = cam.ScreenToWorldPoint(Input.mousePosition);
      
      PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
      eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
      List<RaycastResult> results = new List<RaycastResult>();
      EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
      foreach (var go in results)
      {
        if (go.gameObject.name == "WayDetailsBackGround")
        {
          _isCameraCanMove = false;
          _uiCollisionCheck.SetActive(true);
        }
        else if (go.gameObject.name == "UICollisionCheck")
        {
          _isCameraCanMove = true;
          _uiCollisionCheck.SetActive(false);
        }
      }
      
      /*
      float _timeSinceFirstClick = Time.time - _lastClickTime;

      if (_timeSinceFirstClick <= TIME_BETWEEN_CLICKS)
      {
        if (cam.orthographicSize <= ZoomMin + _stepsOfDoubleTapsZoom)
        {
          float _maxZoom = cam.orthographicSize - ZoomMin;

          cam.orthographicSize -= _maxZoom;
        }
        else if (cam.orthographicSize != ZoomMin)
        {
          cam.orthographicSize -= _stepsOfDoubleTapsZoom;
        }
      }
      */
      _lastClickTime = Time.time;
    }
    else if (Input.GetMouseButton(0) && _isCameraCanMove)
    {
      float posx = cam.ScreenToWorldPoint(Input.mousePosition).x - _startPos.x;
      float posy = cam.ScreenToWorldPoint(Input.mousePosition).y - _startPos.y;

      targetPosx = Mathf.Clamp(transform.position.x - posx, XMin, XMax);
      targetPosy = Mathf.Clamp(transform.position.y - posy, YMin, YMax);
    }
    transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosx, speed * Time.deltaTime), Mathf.Lerp(transform.position.y, targetPosy, speed * Time.deltaTime), transform.position.z);
    
    if (Input.touchCount == 2)
    {
      Touch touchFirst = Input.GetTouch(0);
      Touch touchSecond = Input.GetTouch(1);

      Vector2 touchFirstLastPos = touchFirst.position - touchFirst.deltaPosition;
      Vector2 touchSecondLastPos = touchSecond.position - touchSecond.deltaPosition;

      float distanceTouch = (touchFirstLastPos - touchSecondLastPos).magnitude;
      float currentDistance = (touchFirst.position - touchSecond.position).magnitude;

      float difference = distanceTouch - currentDistance;

      Zoom(difference * SpeedOfZoom, difference * speedOfTrail, difference * _markerValueIncrease);

      if (touchSecondLastPos != touchSecond.position)
      {
        var _angle = Vector3.SignedAngle(touchSecond.position - touchFirst.position, touchSecondLastPos - touchFirstLastPos, -cam.transform.forward);
        cam.transform.RotateAround(cam.transform.position, -cam.transform.forward, _angle);
      }

      foreach (var child in _textList)
      {
        child.transform.rotation = new Quaternion(0, 0, cam.transform.rotation.z, cam.transform.rotation.w);
      }
      
      _marker.transform.rotation = new Quaternion(0, 0, cam.transform.rotation.z, cam.transform.rotation.w);
    }
  
    if (Input.touchCount == 0)
    {
      _rotationAngle = cam.transform.localRotation.eulerAngles.z;

      _rotationAngle = (_rotationAngle < 180f) ? _rotationAngle : _rotationAngle - 360;

      if (_rotationAngle >= -30 && _rotationAngle <= 30)
      {
        cam.transform.localRotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(_rotationAngle, 0, _rotationSpeed));
      }
      else if (_rotationAngle >= 60 && _rotationAngle <= 120)
      {
        cam.transform.localRotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(_rotationAngle, 90, _rotationSpeed));
      }
      else if ((_rotationAngle >= 150 && _rotationAngle >= -150) || (_rotationAngle <= 150 && _rotationAngle <= -150))
      {
        cam.transform.localRotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(_rotationAngle, 180, _rotationSpeed));
      }
      else if (_rotationAngle >= -120 && _rotationAngle <= -60)
      {
        cam.transform.localRotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(_rotationAngle, 270, _rotationSpeed));
      }
      foreach (var child in _textList)
      {
        child.transform.rotation = new Quaternion(0, 0, cam.transform.rotation.z, cam.transform.rotation.w);
      }
      _marker.transform.rotation = new Quaternion(0, 0, cam.transform.rotation.z, cam.transform.rotation.w);
    }
  }
  private void Zoom(float increment, float trailIncrement, float markerIncrement)
  {
    cam.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + increment, ZoomMin, ZoomMax);
    if (GameObject.FindWithTag("Trail").TryGetComponent(out TrailRenderer renderer))
    {
      renderer.startWidth = Mathf.Clamp(renderer.startWidth + (trailIncrement / 15000), _minTrailWight, _maxTrailWight);
      renderer.endWidth = Mathf.Clamp(renderer.endWidth + (trailIncrement / 15000), _minTrailWight, _maxTrailWight);
    }

    _markerMain.transform.localScale = new Vector3(Mathf.Clamp(_markerMain.transform.localScale.x + (markerIncrement / _increment), _markerMinValue, _markerMaxValue),Mathf.Clamp(_markerMain.transform.localScale.y + (markerIncrement / _increment), _markerMinValue, _markerMaxValue),1);
  }
  private void GetAllChilds(GameObject child)
  {
    for (int i = 0; i < child.transform.childCount; i++)
    {
      _textList.Add(child.transform.GetChild(i).gameObject);
    }
  }

  IEnumerator AllRoomsGetterTimer()
  {
    yield return 0.1f;
    _drawWay.SetActive(false);
  }
  public IEnumerator ObjectPossitionCheck()
  {
    while (true)
    {
      _zPosFirst = AI.transform.position.z;
      if (Mathf.Round(AI.transform.localPosition.x) == Mathf.Round(End.transform.localPosition.x) && Mathf.Round(AI.transform.localPosition.y) == Mathf.Round(End.transform.localPosition.y))
      {
        _wayDetailsController.AddPointToWayDetails(_endPointText);
        yield break;
      }
      yield return new WaitForSeconds(0.3f);
      _zPosSecond = AI.transform.position.z;

      if (_zPosFirst == _zPosSecond && !_listOfDetails.transform.GetChild(_listOfDetails.childCount - 1).GetComponentInChildren<TextMeshProUGUI>().text.ToLower().Contains("�������"))
      {
        switch (AI.transform.position.z)
        {
          case 19.5f:
            _wayDetailsController.AddPointToWayDetails("������� �� ������� �����");
            break;
          case 14.5f:
            _wayDetailsController.AddPointToWayDetails("������� �� ������ �����");
            break;
          case 9.5f:
            _wayDetailsController.AddPointToWayDetails("������� �� ������ �����");
            break;
          case 4.3f:
            _wayDetailsController.AddPointToWayDetails("������� �� ������� �����");
            break;
          case -0.5f:
            _wayDetailsController.AddPointToWayDetails("������� �� ��������� �����");
            break;
        }
      }
    }
  }
}
