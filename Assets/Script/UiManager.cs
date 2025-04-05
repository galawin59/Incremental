using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject inventoryPanel;
    public Item item; // Référence au ScriptableObject Item

    void Start()
    {
        CreateStatsPanel();
    }

    void CreateStatsPanel()
    {
        // Créer un panneau pour les statistiques
        GameObject statsPanel = new GameObject("StatsPanel");
        RectTransform statsRectTransform = statsPanel.AddComponent<RectTransform>();
        statsRectTransform.SetParent(inventoryPanel.transform, false);

        // Configurer le RectTransform du panneau de stats
        statsRectTransform.anchoredPosition = new Vector2(0, -250); // Position sous l'inventaire
        statsRectTransform.sizeDelta = new Vector2(990, 300); // Taille du panneau

        // Ajouter un Image pour le fond du panneau
        Image panelImage = statsPanel.AddComponent<Image>();
        panelImage.color = new Color(0.1f, 0.1f, 0.1f, 0.8f); // Couleur de fond

        // Ajouter des éléments de texte pour les statistiques
        string[] statNames = {
            "Strength", "Agility", "Intelligence", "Critical Chance", "Critical Damage",
            "Parry", "Counter Attack", "Counter Attack Damage", "Dodge", "Vitality",
            "Physical Armor", "Magical Armor", "Combo Chance", "Combo Damage", "Regen", "Life Steal"
        };

        ItemStats stats = item.stats;

        float[] statValues = {
            stats.strength, stats.agility, stats.intelligence, stats.criticalChance,
            stats.criticalDamage, stats.parry, stats.counterAttack, stats.counterAttackDamage,
            stats.dodge, stats.vitality, stats.physicalArmor, stats.magicalArmor,
            stats.comboChance, stats.comboDamage, stats.regen, stats.lifeSteal
        };

        float elementWidth = statsRectTransform.sizeDelta.x / 3;
        float elementHeight = 30; // Hauteur fixe pour chaque élément

        for (int i = 0; i < statNames.Length; i++)
        {
            GameObject statTextObject = new GameObject("StatText_" + statNames[i]);
            RectTransform statRectTransform = statTextObject.AddComponent<RectTransform>();
            statRectTransform.SetParent(statsPanel.transform, false);

            Text statText = statTextObject.AddComponent<Text>();
            statText.text = statNames[i] + ": " + statValues[i];
            statText.color = Color.white;
            statText.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font; 
            statText.fontSize = 24;

            statRectTransform.sizeDelta = new Vector2(elementWidth, elementHeight);

            // Positionner chaque texte en trois colonnes
            int column = i % 3;
            int row = i / 3;
            statRectTransform.anchoredPosition = new Vector2(-300 + column * elementWidth, 50 - row * elementHeight);
        }
    }
}
