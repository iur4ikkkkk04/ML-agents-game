using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : Controller
{
    public Text MyscoreText;

    public void Start() => MyscoreText.text = $"Score Player: {Score}";

    public void Update() => MyscoreText.text = $"Score Player: {Score}";
}
