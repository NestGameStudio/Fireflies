using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotVisual : MonoBehaviour
{
    // ------------------- Seta da Cali -------------------------
    [Header("Configuração da linha do slingshot")]
    [Space(0.3f)]
    
    public GameObject arrow;            // ponta da seta
    public LineRenderer line;           // a linha
    public float LineWidth = 0.2f;
    [Tooltip("O tamanho máximo afeta o impulso final da Cali")]
    public float LineMaxRadius = 3.0f;
    [Tooltip("O tamanho mínimo afeta o impulso final da Cali")]
    public float LineMinRadius = 1.5f;

    // ------------- Orientação da seta ------------------
    [Header("Teste & Debug - Orientação e formato da linha")]
    [Space(0.3f)]

    public bool TransversalArrow = true;
    public bool InvertedSlingshot = true;

    // ------------- Circunferência de Impulso ------------------
    [Header("Teste & Debug - Limite de impulso")]
    [Space(0.3f)]

    [Tooltip("Objeto que vai fazer a animação de aparição da circunferência")]
    public GameObject circunference;

    [Tooltip("Mostra a circunferencia.")]
    public bool showCincunference = true;

    [Tooltip("Mostra a circunferencia no player. Caso contrário a circunferência aparecerá no local de referência.")]
    public bool cincunferenceInPlayer = true;

    // ------------- Centro de referência do giro ------------------
    [Header("Teste & Debug - Centro de referência do giro")]
    [Space(0.3f)]

    [Tooltip("Icone que reprensenta referência de giro na scene")]
    public GameObject centerReference;

    [Tooltip("Mostra ou não visualmente o ponto de referência na scene")]
    public bool showReference = true;

    public SlingshotController slingshotControl;

    // ------------- Variáveis privadas ------------------

    // Linha
    private Vector2 lineCenterPosition;
    private Vector2 lineFinalPosition;

    private bool isActive = false;

    // ------------- inicio ------------------

    private void Start()
    {
        line.enabled = false;
    }

    // ------------- Faz o setup visual do slingshot ------------------

    public void SlingshotVisualSetup(Vector2 centerReferencePosition) {

        lineCenterPosition = centerReferencePosition;

        if (showReference) {
            CreateCenterReference();
        }

        if (showCincunference) {

            if (cincunferenceInPlayer) {
                CreateCircunference(this.transform.position);
            } else {
                CreateCircunference(lineCenterPosition);
            }
        }

        lineSetup();

        isActive = true;
    }

    public void DisableSlingshotVisuals() {

        centerReference.SetActive(false);
        circunference.SetActive(false);
        line.enabled = false;
        arrow.SetActive(false);

        isActive = false;

    }

    // Update is called once per frame
    void Update() {

        if (isActive) {

            if (ControlManager.Instance.getCurrentControlScheme() == ControlScheme.KeyboardMouse) {

                if (slingshotControl.ClickReferenceInPlayer && slingshotControl.ReferenceFollowPlayer) {
                    lineCenterPosition = this.transform.position;
                } else if (cincunferenceInPlayer) {
                    adjustCenterReference();
                } else {
                    adjustCircunference();
                    adjustCenterReference();
                }

            // Está no controle -> referencia sempre na Cali
            } else {
                lineCenterPosition = this.transform.position;
            }
            
            adjustSlingshotLine();
            adjustArrowPointer();
        }

    }

    // ------------- cria os visuais ------------------

    // Inicializa a posição da referência
    private void CreateCenterReference() {

        centerReference.transform.position = lineCenterPosition;
        centerReference.SetActive(true);
    }

    // Inicializa a circunferencia
    private void CreateCircunference(Vector2 position) {

        circunference.transform.position = position;
        circunference.SetActive(true);

        circunference.transform.localScale = new Vector2(LineMaxRadius / 2, LineMaxRadius / 2);
        circunference.GetComponentInChildren<Animator>().SetTrigger("Shoot");

    }

    // Inicializa a linha
    private void lineSetup() {

        line.widthMultiplier = LineWidth;
        line.positionCount = 2;
        line.enabled = true;
    }

    // ------------- Faz o update do arrow ------------------
    private void adjustCenterReference() {

        // Mantem o ponto parado aonde foi clicado
        centerReference.transform.position = lineCenterPosition;
    }

    private void adjustCircunference() {

        // Mantem a circunferencia parada aonde foi clicado
        circunference.transform.position = lineCenterPosition;
    }


    private void adjustSlingshotLine() {

        // Posição atual do Mouse ou Analógico
        Vector2 lineFinalPos = Camera.main.ScreenToWorldPoint(lineFinalPosition);

        // Dois pontos da reta
        Vector2 PointA = Vector2.zero;  // Final (ponta do cursor/analógico)
        Vector2 PointB = Vector2.zero;  // o Inverso ou no centro da Cali

        // Ponto A (posição do mouse)
        if (!InvertedSlingshot) {
            PointA = (Vector2)this.transform.position + lineFinalPos - lineCenterPosition;
        } else {
            PointA = (Vector2)this.transform.position - lineFinalPos + lineCenterPosition;
        }

        if (TransversalArrow) {

            if (!InvertedSlingshot) {
                PointB = (Vector2) this.transform.position - lineFinalPos + lineCenterPosition;
            } else {
                PointB = (Vector2) this.transform.position + lineFinalPos - lineCenterPosition;
            }

        } else {
            PointB = this.transform.position;
        }

        // O cursor está dentro do limite máximo    
        if (Vector2.Distance(lineCenterPosition, lineFinalPos) <= LineMaxRadius) {

            // Desenha a linha com a Cali como centro
            line.SetPosition(0, PointA);
            line.SetPosition(1, PointB);


        // Clicou alem do raio máximo da Cali (seta no máximo e move a direção)
        } else {

            float dist = Mathf.Clamp(Vector3.Distance(lineCenterPosition, lineFinalPos), 0, LineMaxRadius);

            if (InvertedSlingshot) {

                Vector2 dir = lineCenterPosition - lineFinalPos;
                PointA = (Vector2) this.transform.position + (dir.normalized * dist);

            } else {

                Vector2 dir = lineFinalPos - lineCenterPosition;
                PointA = (dir.normalized * dist) + (Vector2) this.transform.position;
            }

            if (TransversalArrow) {

                if (InvertedSlingshot) {

                    Vector2 dir = lineFinalPos - lineCenterPosition;
                    PointB = (dir.normalized * dist) + (Vector2) this.transform.position;

                } else {
                    Vector2 dir = lineCenterPosition - lineFinalPos;
                    PointB = (Vector2) this.transform.position + (dir.normalized * dist);
                }

            }

            line.SetPosition(0, PointA);
            line.SetPosition(1, PointB);
        }

    }

    private void adjustArrowPointer() {

        // Desenha a linha do impulso
        line.enabled = true;

        // Ajeita a ponta da seta
        arrow.SetActive(true);
        arrow.transform.position = line.GetPosition(0);

        Vector3 dir = line.GetPosition(0) - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // desenha a trajetória
        //this.GetComponentInChildren<TrajetoriaPredicao>().angle = angle;
        //this.GetComponentInChildren<TrajetoriaPredicao>().RenderArc();

    }

    // ------------- Valores visuais ------------------

    public float GetMinLine() {
        return LineMinRadius;
    }

    public float GetMaxLine() {
        return LineMaxRadius;
    }

    public void SetFinalLinePosition(Vector2 position) {
        lineFinalPosition = position;
    }
    
}
