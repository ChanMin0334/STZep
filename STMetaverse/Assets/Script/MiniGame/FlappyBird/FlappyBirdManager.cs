using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyBirdManager : MonoBehaviour
{
    public static FlappyBirdManager Instance { get; private set; }

    public GameObject[] pipePrefabs;   // 위아래가 한 세트로 구성된 파이프 프리팹 배열
    public float spawnInterval = 2f;   // 파이프 생성 간격(초)
    public float minY = -1f;           // 파이프 세트의 최소 Y 위치
    public float maxY = 3f;            // 파이프 세트의 최대 Y 위치
    public float pipeXPos = 5f;        // 파이프 생성 X 위치
    public float pipeMoveSpeed = 2f;   // 파이프 이동 속도
    public bool isGameStarted = false;

    public GameObject bird; // 플레이어 조작용 새 오브젝트
    public GameObject StartPanel; // 게임 시작 패널
    public GameObject HelpText; // 게임 방법 텍스트


    private float timer = 0f;
    private List<GameObject> pipes = new List<GameObject>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        StopGame(); // 게임 시작 전에는 시간 멈춤
    }

    void Update()
    {
        if (!isGameStarted)
            return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnRandomPipeSet();
            timer = 0f;
        }

        MovePipes();
        CleanupPipes();
    }

    public void StartGame()
    {
        isGameStarted = true;
        Time.timeScale = 1f;
        timer = 0f;
        StartPanel.SetActive(false); // 게임 시작 시 시작 패널 비활성화
        HelpText.SetActive(true); // 게임 방법 텍스트 비활성화
    }

    public void StopGame()
    {
        isGameStarted = false;
        Time.timeScale = 0f;
        timer = 0f;
        StartPanel.SetActive(true); // 게임 멈추면 시작 패널 활성화
        HelpText.SetActive(false); // 게임 방법 텍스트 비활성화

        // 생성된 파이프 모두 제거
        for (int i = pipes.Count - 1; i >= 0; i--)
        {
            if (pipes[i] != null)
            {
                Destroy(pipes[i]);
            }
            pipes.RemoveAt(i);
        }

        if (bird != null)
        {
            bird.transform.position = new Vector3(-6.5f, 0, 0);
            Rigidbody2D rb = bird.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }


    void SpawnRandomPipeSet()
    {
        if (pipePrefabs == null || pipePrefabs.Length == 0)
            return;

        float setY = Random.Range(minY, maxY);
        GameObject pipeSetPrefab = pipePrefabs[Random.Range(0, pipePrefabs.Length)];

        GameObject pipeSet = Instantiate(pipeSetPrefab, new Vector3(pipeXPos, setY, 0), Quaternion.identity);
        pipes.Add(pipeSet);
    }

    void MovePipes()
    {
        for (int i = 0; i < pipes.Count; i++)
        {
            if (pipes[i] != null)
            {
                pipes[i].transform.position += Vector3.left * pipeMoveSpeed * Time.deltaTime;
            }
        }
    }

    void CleanupPipes()
    {
        for (int i = pipes.Count - 1; i >= 0; i--)
        {
            if (pipes[i] != null && pipes[i].transform.position.x < -10f)
            {
                Destroy(pipes[i]);
                pipes.RemoveAt(i);
            }
        }
    }

    // MainScene으로 이동하는 함수 (버튼에 연결)
    public void GoToMainScene()
    {
        // MainScene으로 씬 전환
        SceneManager.LoadScene("MainScene");
    }
}
