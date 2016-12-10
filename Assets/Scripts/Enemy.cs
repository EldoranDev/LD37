using UnityEngine;

class Enemy : MonoBehaviour, IKillable
{

    public float Speed;
    public int Points;

    public GameObject Corpse;
    public MeeleWeapon Weapon;
    public Transform Hand;

    public DropInfo[] Drops;

    Transform _target;
    private Rigidbody2D _body;

    // Use this for initialization
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_target == null)
        {
            var player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                _target = player.transform;
            }
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _target.position - transform.position);
            _body.MovePosition(_body.position + new Vector2(transform.up.normalized.x, transform.up.normalized.y)* Time.deltaTime * Speed);

            if(Vector2.Distance(Hand.position, _target.position) < Weapon.Range)
            {
                Weapon.Use();
            } else
            {

            }
        }
    }

    public void Kill()
    {
        Instantiate(Corpse, transform.position, Quaternion.identity);
        FindObjectOfType<WorldManager>().Points += Points;

        for(var i = 0; i < Drops.Length; i++)
        {
            var c = Random.Range(0f, 1f);

            if(c < Drops[i].Chance)
            {
                Instantiate(Drops[i].Item, transform.position, Quaternion.identity);
                break;
            }
        }

        Destroy(gameObject);
    }
}
