using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;

public class MainWayBuilder : MonoBehaviour
{
  private TextMeshProUGUI getItemText;

  private GameObject AI;
  private GameObject _start;
  private GameObject _end;
  private GameObject _textFrom;
  private GameObject _textTo;
  private GameObject _cancel;
  private GameObject _wayManager;
  private Button _firstFloorButton;
  private GameObject _firstFloor;
  private Button _secondFloorButton;
  private GameObject _secondFloor;
  private Button _thirdFloorButton;
  private GameObject _thirdFloor;
  private Button _fourthFloorButton;
  private GameObject _fourthFloor;

  private string[] _firstFloorObjects =
  {
    "�������� ����������", "���.����� �107", "�������������",
    "�������� 1 ����", "������ � ����������", "������ � ����������",
    "������ � �207", "������ �/� �107.1", "������ �137", "�/� �178",
    "������ �205", "������ �216", "������ �206"
  };

  private string[] _secondFloorObjects =
  {
    "������� ��8", "������� ��7", "������� ��6", "������� ��5", "������� ��4", "������� ��3", "������� ��2",
    "������� ��1", "����� �213", "����� �214", "����� �313", "����� �306",
    "����� ����", "����� �309", "���� ���������", "����/�����",
    "�������� �������", "������", "������ � �208", "������ � �213",
    "������ � �1", "������ � �214", "������� �/� �218", "������ �/� �327",
    "������ �/� �301", "������ �/� �327", "������ �/� �315"
  };

  private string[] _thirdFloorObjects =
  {
    "������� ��18", "������� ��17", "������� ��16", "������� ��15", "������� ��14", "������� ��13", "������� ��12",
    "������� ��11", "������� ��10", "������� ��9", "�������� �401(�)", "�������� �403(�)",
    "�������� �404(�)", "�������� �408", "�������� �415", "�������� �417",
    "������ � �311", "������ � �325", "������ � �427", "������ �/� �408",
    "������ �/� �403(�)", "������ �/� �216", "�����������"
  };

  private string[] _fourthFloorObjects = {"������ � �412", "������ � �412", "������ � �425"};

  private bool _isObjectOnFirstFloor;
  private bool _isObjectOnSecondFloor;
  private bool _isObjectOnThirdFloor;
  private bool _isObjectOnFourthFloor;

  public void Start()
  {
    Button button = gameObject.GetComponent<Button>();
    button.onClick.AddListener(WayDrawing);

    AI = GameObject.FindWithTag("AI");
    _start = GameObject.FindWithTag("Start");
    _end = GameObject.FindWithTag("End");
    _textFrom = GameObject.FindWithTag("TextFrom");
    _textTo = GameObject.FindWithTag("TextTo");
    _cancel = GameObject.FindWithTag("Cancel");
    _wayManager = GameObject.FindWithTag("WayManager");
    _firstFloor = GameObject.FindWithTag("FirstFloor");
    _firstFloorButton = GameObject.FindWithTag("FirstFloorButton").GetComponent<Button>();
    _secondFloor = GameObject.FindWithTag("SecondFloor");
    _secondFloorButton = GameObject.FindWithTag("SecondFloorButton").GetComponent<Button>();
    _thirdFloor = GameObject.FindWithTag("ThirdFloor");
    _thirdFloorButton = GameObject.FindWithTag("ThirdFloorButton").GetComponent<Button>();
    _fourthFloor = GameObject.FindWithTag("FourthFloor");
    _fourthFloorButton = GameObject.FindWithTag("FourthFloorButton").GetComponent<Button>();
  }

  public void WayDrawing()
  {
    Button button = gameObject.GetComponent<Button>();
    getItemText = button.GetComponentInChildren<TextMeshProUGUI>();

      string _tempRoomName2 = $"������� �{getItemText.text}";
      string _tempRoomName = getItemText.text;
      
      ButtonFloorCheck(_tempRoomName, _tempRoomName2,_firstFloorObjects, ref _isObjectOnFirstFloor);
      ButtonFloorCheck(_tempRoomName, _tempRoomName2,_secondFloorObjects,ref _isObjectOnSecondFloor);
      ButtonFloorCheck(_tempRoomName, _tempRoomName2,_thirdFloorObjects,ref _isObjectOnThirdFloor);
      ButtonFloorCheck(_tempRoomName, _tempRoomName2,_fourthFloorObjects,ref _isObjectOnFourthFloor);

    if ((_tempRoomName2.StartsWith("������� ��1") && _tempRoomName2 != "������� ��1" &&
         _tempRoomName2 != "������� ��10" && _tempRoomName2 != "������� ��11" && _tempRoomName2 != "������� ��12" &&
         _tempRoomName2 != "������� ��13" && _tempRoomName2 != "������� ��14" && _tempRoomName2 != "������� ��15" &&
         _tempRoomName2 != "������� ��16" && _tempRoomName2 != "������� ��17" && _tempRoomName2 != "������� ��18") ||
        _isObjectOnFirstFloor || _tempRoomName2.StartsWith("������� ��2") || _tempRoomName2.StartsWith("������� ��2") ||
        _tempRoomName2.StartsWith("������� ��2") || _tempRoomName2.StartsWith("������� ��2"))
    {
      RoomsButtonFloorCheck(_firstFloorButton, button, _tempRoomName2, _firstFloor, 14.1f);
    }
    else if ((_tempRoomName2.StartsWith("������� ��2") && _tempRoomName2 != "������� ��2") || _isObjectOnSecondFloor ||
             _tempRoomName2.StartsWith("������� ��3") || _tempRoomName2.StartsWith("������� ��3") ||
             _tempRoomName2.StartsWith("������� ��3") || _tempRoomName2.StartsWith("������� ��3") ||
             _tempRoomName2.StartsWith("������� ��2"))
    {
      RoomsButtonFloorCheck(_secondFloorButton, button, _tempRoomName2, _secondFloor, 9.1f);
    }
    else if ((_tempRoomName2.StartsWith("������� ��3") && _tempRoomName2 != "������� ��3") || _isObjectOnThirdFloor ||
             _tempRoomName2.StartsWith("������� ��4") || _tempRoomName2.StartsWith("������� ��4") ||
             _tempRoomName2.StartsWith("������� ��4") || _tempRoomName2.StartsWith("������� ��4") ||
             _tempRoomName2.StartsWith("������� ��3"))
    {
      RoomsButtonFloorCheck(_thirdFloorButton, button, _tempRoomName2, _thirdFloor, 4.1f);
    }
    else if ((_tempRoomName2.StartsWith("������� ��4") && _tempRoomName2 != "������� ��4") || _isObjectOnFourthFloor)
    {
      RoomsButtonFloorCheck(_fourthFloorButton, button, _tempRoomName2, _fourthFloor, -0.1f);
    }
  }

  public void ButtonCheck(Button button, float posx, float posy, float posz)
  {
    if (button.tag == "WayButtonsFrom")
    {
      _start.transform.position = new Vector3(posx, posy, posz);
      _start.GetComponent<Animation>().Play("StartPutting");

      _textFrom.GetComponent<TMP_InputField>().text = getItemText.text;
      _textFrom.GetComponent<CustomInputField>().UpdateState();

      AI.GetComponent<AILerp>().enabled = false;

      AI.transform.position = new Vector3(posx, posy, posz);

      AI.GetComponent<AILerp>().enabled = enabled;

      _cancel.GetComponent<ListCleaner>().OnClick();

      _wayManager.GetComponent<WayManager>()._isFromInPlace = true;

      _wayManager.GetComponent<WayManager>()._isFromButtonActivated = true;

      if (AI.GetComponentInChildren<TrailRenderer>() != null)
      {
        Destroy(AI.GetComponentInChildren<TrailRenderer>());
      }
    }

    if (button.tag == "WayButtonsTo")
    {
      _end.transform.position = new Vector3(posx, posy, posz);
      _end.GetComponent<Animation>().Play("EndPutting");

      _textTo.GetComponent<TMP_InputField>().text = getItemText.text;
      _textTo.GetComponent<CustomInputField>().UpdateState();

      _cancel.GetComponent<ListCleaner>().OnClick();

      _wayManager.GetComponent<WayManager>()._isToInPlace = true;

      _wayManager.GetComponent<WayManager>()._isToButtonActivated = true;

      if (AI.GetComponentInChildren<TrailRenderer>() != null)
      {
        Destroy(AI.GetComponentInChildren<TrailRenderer>());
      }
    }
  }

  public void ButtonFloorCheck(string _tempRoomName,string _tempRoomName2, string[] _floorObjects, ref bool IsObjectOnFloor)
  {
    foreach (var objects in _floorObjects)
    {
      if (objects == _tempRoomName || objects == _tempRoomName2)
      {
        IsObjectOnFloor = true;
      }
    }
  }

  public void RoomsButtonFloorCheck(Button _floorButton, Button _gameobjectButton, string _tempRoomName, GameObject _floor, float _posz)
  {
    _floorButton.onClick.Invoke();
    for (int i = 0; i < _floor.transform.GetChild(0).transform.childCount; i++)
    {
      if (_floor.transform.GetChild(0).transform.GetChild(i).name == _tempRoomName)
      {
        ButtonCheck(_gameobjectButton,
          _floor.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().bounds.center.x,
          _floor.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().bounds.center.y,
          _posz
          );
      }
    }
  }
}
