using UnityEngine;

[CreateAssetMenu(fileName = "New MythologicalCharacter", menuName = "Characters")]
public class MythologicalCharacter : ScriptableObject
{
    [SerializeField] private string _characterName;
    [SerializeField] private Sprite _characterImage;
    [TextArea]
    [SerializeField] private string _characterDescription;
    
    [SerializeField] private Sprite _cardImage;
    [SerializeField] private Sprite _cardBackImage;

    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;
    public string CharacterDescription => _characterDescription;
    public Sprite CardFront => _cardImage;
    public Sprite CardBack => _cardBackImage;

}
