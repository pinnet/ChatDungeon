using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers.Interfaces;
using AIProviders;
using OpenAI_API;
using OpenAI_API.Models;

[CreateAssetMenu(fileName = "OpenAIModelData", menuName = "AIManager/OpenAIModelData", order = 1)]
public class OpenAIModelDataSO : ScriptableObject
{
    public int maxTokens = 50;
    public double temperature = 0.1;
    public Model model = Model.ChatGPTTurbo;  
}
