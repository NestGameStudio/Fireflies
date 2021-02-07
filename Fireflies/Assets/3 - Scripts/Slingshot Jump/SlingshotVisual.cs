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
 
    // ------------- Circunferência de Impulso ------------------
    [Header("Limite de impulso")]
    [Space(0.3f)]

    [Tooltip("Objeto que vai fazer a animação de aparição da circunferência")]
    public GameObject circunference;

    // ------------- Centro de referência do giro ------------------
    [Header("Teste & Debug - Centro de referência do giro")]
    [Space(0.3f)]

    [Tooltip("Icone que reprensenta referência de giro na scene")]
    public GameObject centerReference;

    [Space(0.3f)]

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
        Cursor.visible = false;

        if (Setup.Instance.setReferenceInCenter && ControlManager.Instance.getCurrentControlScheme() == ControlScheme.KeyboardMouse) {
            SetFinalLinePosition(new Vector2(Screen.width / 2, Screen.height / 2));
        }
       
    }

    // Update is called once per frame
    void Update() {

        if (isActive) {

            if (ControlManager.Instance.getCurrentControlScheme() == ControlScheme.KeyboardMouse) {

                if (Setup.Instance.ClickReferenceInPlayer && Setup.Instance.ReferenceFollowPlayer) {
                    lineCenterPosition = this.transform.position;
                } else if (Setup.Instance.cincunferenceInPlayer) {
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

    // ------------- Faz o setup visual do slingshot ------------------
    public void SlingshotVisualSetup(Vector2 centerReferencePosition) {

        lineCenterPosition = centerReferencePosition;
        //if(ControlManager.Instance.getCurrentControlScheme() == ControlScheme.KeyboardMouse) {
            SetFinalLinePosition(new Vector2(Screen.width / 2, Screen.height / 2));
        //}

        if (Setup.Instance.showReference) {
            CreateCenterReference();
        }

        if (Setup.Instance.showCincunference) {

            if (Setup.Instance.cincunferenceInPlayer) {
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

        //circunference.transform.localScale = new Vector2(LineMaxRadius / 2, LineMaxRadius / 2);
        //circunference.GetComponentInChildren<Animator>().SetTrigger("Shoot");

    }

    // Inicializa a linha
    private void lineSetup() {

        line.widthMultiplier = Setup.Instance.LineWidth;
        line.positionCount = 2;
        //line.enabled = true;
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
        Vector2 PointA = lineCenterPosition;  // Final (ponta do cursor/analógico)
        Vector2 PointB = lineCenterPosition;  // o Inverso ou no centro da Cali

        // Ponto A (posição do mouse)
        if (!Setup.Instance.InvertedSlingshot) {
            PointA = (Vector2)this.transform.position + lineFinalPos - lineCenterPosition;
        } else {
            PointA = (Vector2)this.transform.position - lineFinalPos + lineCenterPosition;
        }

        if (Setup.Instance.TransversalArrow) {

            if (!Setup.Instance.InvertedSlingshot) {
                PointB = (Vector2) this.transform.position - lineFinalPos + lineCenterPosition;
            } else {
                PointB = (Vector2) this.transform.position + lineFinalPos - lineCenterPosition;
            }

        } else {
            PointB = this.transform.position;
        }

        // O cursor está dentro do limite máximo    
        if (Vector2.Distance(lineCenterPosition, lineFinalPos) <= Setup.Instance.LineMaxRadius) {

            // Desenha a linha com a Cali como centro
            line.SetPosition(0, PointA);
            line.SetPosition(1, PointB);


        // Clicou alem do raio máximo da Cali (seta no máximo e move a direção)
        } else {

            float dist = Mathf.Clamp(Vector3.Distance(lineCenterPosition, lineFinalPos), 0, Setup.Instance.LineMaxRadius);

            if (Setup.Instance.InvertedSlingshot) {

                Vector2 dir = lineCenterPosition - lineFinalPos;
                PointA = (Vector2) this.transform.position + (dir.normalized * dist);

            } else {

                Vector2 dir = lineFinalPos - lineCenterPosition;
                PointA = (dir.normalized * dist) + (Vector2) this.transform.position;
            }

            if (Setup.Instance.TransversalArrow) {

                if (Setup.Instance.InvertedSlingshot) {

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
        return Setup.Instance.LineMinRadius;
    }

    public float GetMaxLine() {
        return Setup.Instance.LineMaxRadius;
    }

    public void SetFinalLinePosition(Vector2 position) {
        print("position:" + position);
        lineFinalPosition = position;
    }
    
}
