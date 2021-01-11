using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class SlingshotController : MonoBehaviour {

    [Header("Controle da Movimentação")]
    [Space(0.3f)]

    [Range(0.0f, 1.0f)] public float TimeSlow = 0.02f;
    public float ImpulseForce = 330f;

    [Header("Configuração da linha do slingshot")]
    [Space(0.3f)]

    // Linha que indica a direção e intensidade que vai sair a movimentação da Cali 
    public GameObject arrow;
    public LineRenderer line;
    public float LineWidth = 0.2f;
    [Tooltip("O tamanho máximo afeta o impulso final da Cali")]
    public float LineMaxRadius = 3.0f;
    [Tooltip("O tamanho mínimo afeta o impulso final da Cali")]
    public float LineMinRadius = 1.5f;

    [Header("Teste & Debug - Referência de Giro")]
    [Space(0.3f)]

    [Tooltip ("(Mouse/Keyboard & Mouse) Define a ponto de centro para que aconteça a rotação da linha de slingshot da Cali, ele está indicado como um Gizmo no editor." +
              "Caso seja true, o ponto de referência deste centro está acompenhando a movimentação da Cali. Caso seja false, o ponto de referência está aonde foi clicado com o mouse." +
              "Caso esteja usando Gamepad o ponto de referência é sempre o jogador.")]
    public bool ClickReferenceInPlayer = true;

    //bool para criar ponto de referencia no player apenas uma vez
    private bool oneCheckPlayerRef = true;

    [Tooltip("(Mouse/Keyboard & Mouse/Gamepad) Valido apenas para quando o ponto de referencia está no player. Se true, o ponto de referência segue a posição da Cali que se move mesmo em slow motion." +
             "Se falso, ele usa como ponto de referência a posição original da Cali quando clicou com o mouse.")]
    public bool ReferenceFollowPlayer = true;
    [Tooltip("Mostra a circunferencia.")]
    public bool showCincunference = true;

    //bool para animacao de entrada do efeito de circulo
    private bool oneCheckEffect = true;

    [Tooltip("Mostra a circunferencia no player. Caso contrário a circunferência aparecerá no local de referência.")]
    public bool cincunferenceInPlayer = true;

    public GameObject circleEffect;

    [Tooltip("Icone que reprensenta referência de giro na scene")]
    public GameObject centerReference;

    [Header("Teste & Debug - Linha do Slingshot")]
    [Space(0.3f)]

    public bool TransversalArrow = true;
    public bool InvertedSlingshot = true;

    // -> Controles

    private enum ControlScheme { Gamepad, KeyboardMouse }

    private PlayerControls controls;
    private ControlScheme currentControlScheme = ControlScheme.KeyboardMouse;

    private InputAction slowMotion;
    private InputAction slingshotMovementDirection;
     

    // -> Variáveis da movimentação

    // Entrou no slowMotion com um clique
    private bool isOnSlowMotion = false;

    // Está com o pulo carregado
    private bool canJump = true;
    
    // Direção atualizada conforme pega os valores do analógico/mouse
    private Vector2 direction;
    // o centro da linha
    private Vector2 lineCenterPos;
    private GameObject currentReference;

    [Tooltip ("Numero maximo de segmentos para a circunferência.")]
    private int numSegments = 50;

    private Vector2 impulseVector = Vector2.zero;


    // -> Controles
    private void Awake() {

        controls = new PlayerControls();

        // Define os controles do input system (Keyboard & mouse, mouse e gamepad)
        slowMotion = controls.Gameplay.SlingshotSlowMotion;
        slingshotMovementDirection = controls.Gameplay.SlingshotMovementDirection;

        slowMotion.performed += EnterSlowMotionMode;
        slowMotion.canceled += ExitSlowMotionMode;
    }

    private void OnEnable() {

        slowMotion.Enable();
        slingshotMovementDirection.Enable();

        InputUser.onChange += onInputDeviceChange;

        // linha de desenho da movimentação
        lineSetup();
    }

    private void OnDisable() {

        slowMotion.Disable();
        slingshotMovementDirection.Disable();

        InputUser.onChange += onInputDeviceChange;
    }

    private void Update() {

        direction = slingshotMovementDirection.ReadValue<Vector2>();

        // Se está apertando o slowMotion, ajeita a posição do slingshot de acordo com a movimentação do analógico/mouse
        if (isOnSlowMotion) {

            if (ClickReferenceInPlayer & ReferenceFollowPlayer) {
                CenterReference();
            }

            if (cincunferenceInPlayer) {

                //for (int i = 0; i < circle.positionCount; i++) {
                //circle.SetPosition(i, circle.GetPosition(i) + this.transform.position);
                //}

                circleEffect.SetActive(true);

                //ajustar tamanho baseado no maximo de forca que se pode aplicar

                circleEffect.transform.localScale = new Vector2(LineMaxRadius/2, LineMaxRadius/2);

                if (oneCheckEffect)
                {
                    oneCheckEffect = false;
                    circleEffect.GetComponentInChildren<Animator>().SetTrigger("Shoot");
                }
            }
            else
            {
                circleEffect.transform.position = currentReference.transform.position;
                circleEffect.SetActive(true);
            }

            adjustSlingshot();
        }
        else
        {
            circleEffect.SetActive(false);

            //resetar bool de ponto de ancoragem no player
            oneCheckPlayerRef = true;

            //resetar bool para animacao de entrada do efeito de circulo
            oneCheckEffect = true;
        }
    }
    public static float Damp(float source, float target, float smoothing, float dt)
    {
        return Mathf.Lerp(source, target, 1 - Mathf.Pow(smoothing, dt));
    }

    private void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device) {

        if (change == InputUserChange.ControlSchemeChanged) {

            print(user.controlScheme.Value.name);

            switch (user.controlScheme.Value.name) {
                case "Keyboard and Mouse":
                    print("keyboard");
                    currentControlScheme = ControlScheme.KeyboardMouse;
                    break;
                default:
                    print("gamepad");
                    currentControlScheme = ControlScheme.Gamepad;
                    break;
            }
        }
    }
   

    // -> Slow Motion 
    private void EnterSlowMotionMode(InputAction.CallbackContext context) {

        if (canJump) {

            print("Time Slow");
            Time.timeScale = TimeSlow;
            Time.fixedDeltaTime = TimeSlow * Time.deltaTime;    // faz com que o slowmotion não fique travado

            CenterReference();
            if (showCincunference) {
                //MaxRadiusReference();
            }

            isOnSlowMotion = true;
            line.enabled = true;
        }
    }

    private void ExitSlowMotionMode(InputAction.CallbackContext context) {

        if (canJump) {

            print("Time Normal");
            Time.timeScale = 1f;

            Jump();
            Destroy(currentReference);
            arrow.SetActive(false);

            isOnSlowMotion = false;
            //canJump = false;
            line.enabled = false;
        }
    }

    // -> Slingshot

    // Define as propriedades da seta que vai indicar a movimentação da Cali
    private void lineSetup() {

        line.widthMultiplier = LineWidth;
        line.positionCount = 2;
    }

    // Pega a referência do centro da rotação da linha do slingshot conforme o padrão de controles
    private void CenterReference() {

        if (ClickReferenceInPlayer || currentControlScheme == ControlScheme.Gamepad) {

            lineCenterPos = this.gameObject.transform.position;

            //criar referencia apenas uma vez
            if (oneCheckPlayerRef)
            {
                // Indica aonde ocorreu o clique
                currentReference = Instantiate(centerReference, lineCenterPos, centerReference.transform.rotation);

                currentReference.transform.parent = gameObject.transform;

                oneCheckPlayerRef = false;
            }

        } else if (!ClickReferenceInPlayer) {

            lineCenterPos = Camera.main.ScreenToWorldPoint(direction);

            //criar referencia apenas uma vez
            if (oneCheckPlayerRef)
            {
                // Indica aonde ocorreu o clique
                currentReference = Instantiate(centerReference, lineCenterPos, centerReference.transform.rotation);

                //currentReference.transform.parent = gameObject.transform;

                oneCheckPlayerRef = false;
            }
        }

        //if (!ReferenceFollowPlayer) {


        
        //}


    }
    /*
    private void MaxRadiusReference() {

        circle.positionCount = numSegments + 2;
        circle.startWidth = 0.2f;
        circle.endWidth = 0.2f;
        //circle.useWorldSpace = false;

        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i=0; i < (numSegments + 2); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * LineMaxRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * LineMaxRadius;

            if (cincunferenceInPlayer)
                circle.SetPosition(i, new Vector2(x, y));

            angle += (360f / numSegments);

        }

    }
    */
    // Ajusta a posição da linha do slingshot quando movimenta o analógico/mouse
    private void adjustSlingshot() {

        // Posição atual do Mouse ou Analógico
        Vector2 lineFinalPos = Camera.main.ScreenToWorldPoint(direction);

        // Dois pontos da reta
        Vector2 PointA = Vector2.zero;  // Final (ponta do cursor/analógico)
        Vector2 PointB = Vector2.zero;  // o Inverso ou no centro da Cali

        // Impulso do pulo da Cali
        Vector2 impulse = new Vector2(lineCenterPos.x - lineFinalPos.x, lineCenterPos.y - lineFinalPos.y) * ImpulseForce;

        

        // Calcula o raio máximo que a linha pode chegar (equivale ao impulso máximo) a partir do ponto de centro do giro

        // Ponto A (posição do mouse)
        if (!InvertedSlingshot) {
            PointA = new Vector2(this.transform.position.x + lineFinalPos.x - lineCenterPos.x,
                                 this.transform.position.y + lineFinalPos.y - lineCenterPos.y);
        } else {
            PointA = new Vector2(this.transform.position.x - lineFinalPos.x + lineCenterPos.x,
                                 this.transform.position.y - lineFinalPos.y + lineCenterPos.y);
        }

        if (TransversalArrow) {

            if (!InvertedSlingshot) {
                PointB = new Vector2(this.transform.position.x - lineFinalPos.x + lineCenterPos.x,
                                     this.transform.position.y - lineFinalPos.y + lineCenterPos.y);
            } else {
                PointB = new Vector2(this.transform.position.x + lineFinalPos.x - lineCenterPos.x,
                                     this.transform.position.y + lineFinalPos.y - lineCenterPos.y);
            }

        } else {
            PointB = this.transform.position;
        }

        // O cursor está dentro do limite máximo    
        if (Vector2.Distance(lineCenterPos, lineFinalPos) <= LineMaxRadius) {

            // Desenha a linha com a Cali como centro
            line.SetPosition(0, PointA);
            line.SetPosition(1, PointB);

            impulseVector = lineCenterPos - lineFinalPos;
        
        // Clicou alem do raio máximo da Cali (seta no máximo e move a direção)
        } else {

            float dist = Mathf.Clamp(Vector3.Distance(lineCenterPos, lineFinalPos), 0, LineMaxRadius);

            if (InvertedSlingshot) {

                Vector2 dir = lineCenterPos - lineFinalPos;
                PointA = (Vector2) this.transform.position + (dir.normalized * dist);

            } else {
                
                Vector2 dir = lineFinalPos - lineCenterPos;
                PointA = (dir.normalized * dist) + (Vector2) this.transform.position;
            }

            if (TransversalArrow) {

                if (InvertedSlingshot) {

                    Vector2 dir = lineFinalPos - lineCenterPos;
                    PointB = (dir.normalized * dist) + (Vector2) this.transform.position;

                } else {
                    Vector2 dir = lineCenterPos - lineFinalPos;
                    PointB = (Vector2) this.transform.position + (dir.normalized * dist);
                }

            }

            line.SetPosition(0, PointA);
            line.SetPosition(1, PointB);

            impulseVector = PointA;


            //mostra predicao do pulo em arco

            //GetComponentInChildren<TrajetoriaPredicao>().angle = Vector2.Angle(PointB, PointA);

            //TA AQUI
            //GetComponentInChildren<TrajetoriaPredicao>().angle = Angle();

            GetComponentInChildren<TrajetoriaPredicao>().velocity = ImpulseForce*10;
            GetComponentInChildren<TrajetoriaPredicao>().RenderArc();
        }

        // Ajeita a ponta da seta 
        if (impulseVector.magnitude > LineMinRadius) {

            // Desenha a linha do impulso
            line.enabled = true;

            // Ajeita a ponta da seta
            arrow.SetActive(true);
            arrow.transform.position = line.GetPosition(0);

            Vector3 dir = line.GetPosition(0) - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        } else {
            // apaga a linha
            line.enabled = false;
            arrow.SetActive(false);
        }

    }
    public static float Angle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

    private void Jump() {

        //evita pulos de força zero (game breaking)
        if (impulseVector.magnitude > LineMinRadius) {

            // Zera a velocidade do player antes de dar um novo impulso para não ter soma de vetores
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;

            // Calcula o impulso
            Vector2 impulse = new Vector2(impulseVector.x, impulseVector.y) * ImpulseForce;
            rb.AddForce(impulse);
        }

        impulseVector = Vector2.zero;

    }

}
