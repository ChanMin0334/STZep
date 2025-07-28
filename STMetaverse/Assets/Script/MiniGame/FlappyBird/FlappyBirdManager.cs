using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyBirdManager : MonoBehaviour
{
    public static FlappyBirdManager Instance { get; private set; }

    public GameObject[] pipePrefabs;   // ���Ʒ��� �� ��Ʈ�� ������ ������ ������ �迭
    public float spawnInterval = 2f;   // ������ ���� ����(��)
    public float minY = -1f;           // ������ ��Ʈ�� �ּ� Y ��ġ
    public float maxY = 3f;            // ������ ��Ʈ�� �ִ� Y ��ġ
    public float pipeXPos = 5f;        // ������ ���� X ��ġ
    public float pipeMoveSpeed = 2f;   // ������ �̵� �ӵ�
    public bool isGameStarted = false;

    public GameObject bird; // �÷��̾� ���ۿ� �� ������Ʈ
    public GameObject StartPanel; // ���� ���� �г�
    public GameObject HelpText; // ���� ��� �ؽ�Ʈ


    private float timer = 0f;
    private List<GameObject> pipes = new List<GameObject>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        StopGame(); // ���� ���� ������ �ð� ����
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
        StartPanel.SetActive(false); // ���� ���� �� ���� �г� ��Ȱ��ȭ
        HelpText.SetActive(true); // ���� ��� �ؽ�Ʈ ��Ȱ��ȭ
    }

    public void StopGame()
    {
        isGameStarted = false;
        Time.timeScale = 0f;
        timer = 0f;
        StartPanel.SetActive(true); // ���� ���߸� ���� �г� Ȱ��ȭ
        HelpText.SetActive(false); // ���� ��� �ؽ�Ʈ ��Ȱ��ȭ

        // ������ ������ ��� ����
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

    // MainScene���� �̵��ϴ� �Լ� (��ư�� ����)
    public void GoToMainScene()
    {
        // MainScene���� �� ��ȯ
        SceneManager.LoadScene("MainScene");
    }
}
