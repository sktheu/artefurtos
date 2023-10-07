using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerObjective : MonoBehaviour
{
    #region Variáveis Globais:
    // Referências:
    private CollisionLayersManager _collisionLayersManager;

    // Tesouros coletados:
    private List<Sprite> _currentTreasureSprites = new List<Sprite>();
    private int _treasuresLeft;

    // Mochila:
    private static Animator _backpackAnimator;
    private static TextMeshPro _backpackText;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _collisionLayersManager = GameObject.FindObjectOfType<CollisionLayersManager>();
        _treasuresLeft = GameObject.FindObjectsOfType<TreasureBehaviour>().Length;
        _backpackAnimator = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_currentTreasureSprites.Count > 0 && col.gameObject.layer == _collisionLayersManager.Backpack.Index)
            RemoveTreasure();
    }
    #endregion

    #region Funções Próprias
    public void AddTreasure(Sprite newTreasureSprite) => _currentTreasureSprites.Add(newTreasureSprite);

    public void RemoveTreasure()
    {
        _currentTreasureSprites.RemoveAt(0);
        _treasuresLeft--;

        _backpackAnimator.Play("BackpackDeposit");
        if (_treasuresLeft <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }   
    #endregion
}
