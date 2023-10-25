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
    private LevelManager _levelManager;
    private AudioManager _audioManager;

    // Tesouros coletados:
    [SerializeField] private List<Sprite> _currentTreasureSprites = new List<Sprite>();
    private int _treasuresLeft;

    // Mochila:
    private Animator _backpackAnimator;
    private TextMeshProUGUI _backpackText;
    private bool _canDeposit = true;
    private int _curSfxIndex = 0;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _collisionLayersManager = GameObject.FindObjectOfType<CollisionLayersManager>();
        //_audioManager = GameObject.FindObjectOfType<AudioManager>();
        _treasuresLeft = GameObject.FindObjectsOfType<TreasureBehaviour>().Length;
        _backpackAnimator = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Animator>();
        _levelManager = GameObject.FindObjectOfType<LevelManager>();
        _backpackText = _backpackAnimator.gameObject.transform.Find("Canvas").gameObject.transform.Find("TxtTreasuresLeft").gameObject.GetComponent<TextMeshProUGUI>();
        _backpackText.text = _treasuresLeft.ToString();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (_currentTreasureSprites.Count > 0 && col.gameObject.layer == _collisionLayersManager.Backpack.Index &&
            _canDeposit)
        {
            _canDeposit = false;
            RemoveTreasure();
            StartCoroutine(SetDepositInterval(0.5f));
        }
            
    }
    #endregion

    #region Funções Próprias
    public void AddTreasure(Sprite newTreasureSprite) => _currentTreasureSprites.Add(newTreasureSprite);

    public void RemoveTreasure()
    {
        _currentTreasureSprites.RemoveAt(0);
        _treasuresLeft--;
        _backpackAnimator.Play("BackpackDeposit");
        _backpackText.text = _treasuresLeft.ToString();
        
        if (_treasuresLeft <= 0)
        {
            _treasuresLeft = 0;
            //_audioManager.PlaySFX("game_win");
            _levelManager.End();
        }
    }

    private IEnumerator SetDepositInterval(float t)
    {
        //_audioManager.PlaySFX("mochila_depositando_" + _curSfxIndex);
        yield return new WaitForSeconds(t);
        _canDeposit = true;
        _curSfxIndex++;
        if (_curSfxIndex > 2)
            _curSfxIndex = 0;
    }
    #endregion
}
