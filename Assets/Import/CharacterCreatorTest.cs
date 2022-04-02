using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreatorTest : MonoBehaviour
{
    public static CharacterCreatorTest instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public GameObject male;
    public GameObject female;

    public GameObject activeObject;

    public Color color1 = Color.white;
    [HideInInspector] public float offset = 0.25f;

    Color gray = Color.gray;

    public bool _sex = true;

    [Space]
    public RectTransform _texture;
    public Texture2D _refSprite;

    public void OnClickPickerColor()
    {
        SetColor();
    }

    private void SetColor()
    {
        Vector3 imagePos = _texture.position;
        float globalPosX = Input.mousePosition.x - imagePos.x;
        float globalPosY = Input.mousePosition.y - imagePos.y;

        int localPosX = (int)(globalPosX * (_refSprite.width / _texture.rect.width));
        int localPosY = (int)(globalPosY * (_refSprite.height / _texture.rect.width));

        Color c = _refSprite.GetPixel(localPosX, localPosY);
        SetActualColor(c);
    }

    void SetActualColor(Color c)
    {
        color1 = c;
        activeObject.GetComponentsInChildren<SkinnedMeshRenderer>()[1].material.color = color1;
        activeObject.GetComponentsInChildren<SkinnedMeshRenderer>()[0].material.color = color1 * 0.25f;
    }

    public void SetSex(bool sex)
    {
        if (sex)
        {
            male.SetActive(true);
            activeObject = male;
            female.SetActive(false);
        }
        else
        {
            male.SetActive(false);
            activeObject = female;
            female.SetActive(true);
        }

        _sex = sex;
        activeObject.GetComponentsInChildren<SkinnedMeshRenderer>()[1].material.color = color1;
        activeObject.GetComponentsInChildren<SkinnedMeshRenderer>()[0].material.color = color1 * 0.25f;
    }
}
