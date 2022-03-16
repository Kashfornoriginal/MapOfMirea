using UnityEngine;
using UnityEngine.Animations;

public class FloorChangingButton : MonoBehaviour
{
  [SerializeField] private GameObject _firstFloor;
  [SerializeField] private GameObject _secondFloor;
  [SerializeField] private GameObject _thirdFloor;
  [SerializeField] private GameObject _fourthFloor;

  [SerializeField] private GameObject _secondFloorBG;
  [SerializeField] private GameObject _thirdFloorBG;
  [SerializeField] private GameObject _fourthFloorBG;

  [SerializeField] private GameObject _firstFloorCanvas;
  [SerializeField] private GameObject _secondFloorCanvas;
  [SerializeField] private GameObject _thirdFloorCanvas;
  [SerializeField] private GameObject _fourthFloorCanvas;

  [SerializeField] private GameObject _camera;

  public void OnFirstFloorClick()
  {
    if(_firstFloor.GetComponent<SpriteRenderer>().color.a == 0)
    {
      AllFloorsClean();
      _firstFloor.GetComponent<Animation>().Play("FirstFloor");
      _firstFloorCanvas.SetActive(true);

      _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, 11);
    }
  }
  public void OnSecondFloorClick()
  {
    if(_secondFloor.GetComponent<SpriteRenderer>().color.a == 0)
    {
      AllFloorsClean();
      _secondFloor.GetComponent<Animation>().Play("SecondFloor");
      _secondFloorCanvas.SetActive(true);

      _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, 4);
    }
  }
  public void OnThirdFloorClick()
  {
    if(_thirdFloor.GetComponent<SpriteRenderer>().color.a == 0)
    {
      AllFloorsClean();
      _thirdFloor.GetComponent<Animation>().Play("ThirdFloor");
      _thirdFloorCanvas.SetActive(true);

      _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, -1);
    }
    
  }
  public void OnFourthFloorClick()
  {
    if (_fourthFloor.GetComponent<SpriteRenderer>().color.a == 0)
    {
      AllFloorsClean();
      _fourthFloor.GetComponent<Animation>().Play("FourthFloor");
      _fourthFloorCanvas.SetActive(true);

      _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, -3);
    }
  }
  public void AllFloorsClean()
  {
    _firstFloor.GetComponent<SpriteRenderer>().color = new Color(_firstFloor.GetComponent<SpriteRenderer>().color.r, _firstFloor.GetComponent<SpriteRenderer>().color.g, _firstFloor.GetComponent<SpriteRenderer>().color.b, 0);
    _secondFloor.GetComponent<SpriteRenderer>().color = _firstFloor.GetComponent<SpriteRenderer>().color;
    _thirdFloor.GetComponent<SpriteRenderer>().color = _firstFloor.GetComponent<SpriteRenderer>().color;
    _fourthFloor.GetComponent<SpriteRenderer>().color = _firstFloor.GetComponent<SpriteRenderer>().color;

    _secondFloorBG.GetComponent<SpriteRenderer>().color = new Color(_secondFloorBG.GetComponent<SpriteRenderer>().color.r, _secondFloorBG.GetComponent<SpriteRenderer>().color.g, _secondFloorBG.GetComponent<SpriteRenderer>().color.b, 0);
    _thirdFloorBG.GetComponent<SpriteRenderer>().color = _secondFloorBG.GetComponent<SpriteRenderer>().color;
    _fourthFloorBG.GetComponent<SpriteRenderer>().color = _secondFloorBG.GetComponent<SpriteRenderer>().color;

    _firstFloorCanvas.SetActive(false);
    _secondFloorCanvas.SetActive(false);
    _thirdFloorCanvas.SetActive(false);
    _fourthFloorCanvas.SetActive(false);
  }
}
