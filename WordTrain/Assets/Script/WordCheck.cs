using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordCheck : MonoBehaviour {

    private AbstractAPI api;
	public GameObject hand;
	public GameObject a;
	public GameObject b;
	public GameObject c;
	public GameObject d;
	public GameObject e;
	public GameObject f;
	public GameObject g;
	public GameObject h;
	public GameObject i;
	public GameObject j;
	public GameObject k;
	public GameObject l;
	public GameObject m;
	public GameObject n;
	public GameObject o;
	public GameObject p;
	public GameObject q;
	public GameObject r;
	public GameObject s;
	public GameObject t;
	public GameObject u;
	public GameObject v;
	public GameObject w;
	public GameObject x;
	public GameObject y;
	public GameObject z;
	string word = "";

    public Text totalPointText;
    public Text numPointsText;
    public Text multiplierText;

    int numPoints = 0;
    int totalPoints = 0;
    int numSyllables = 0;

    private delegate void APIRequest(string word);

	// Use this for initialization
	void Start () {
        api = WordsAPI.Instance;
		addCards ();
	}
	
	// Update is called once per frame
	void Update () {
        totalPointText.text = totalPoints.ToString();
        numPointsText.text = numPoints.ToString();
        multiplierText.text = "x" + numSyllables.ToString();

		if (Input.GetKeyUp(KeyCode.Space))
		{
            Validate();

		}
		
	}

    public void Validate()
    {
        word = "";
        for (int i = 0; i < this.transform.childCount; i++)
        {
            word = word + this.transform.GetChild(i).GetComponent<CardControl>().letter;
        }

        if (!string.IsNullOrEmpty(word))
        {
            FillAPIData(word);


        }
    }

    public void FillAPIData(string word)
    {
        api.Reset();
        api.SendWordToAPI(word);
        StartCoroutine(ex());
    }

	IEnumerator ex()
	{
		while(api.InProgress)
		{
			yield return new WaitForSeconds(.3f);
		}

		//execute method based on the validity
		if (api.Valid)
		{
			Debug.LogFormat("The word {0} is valid", word);
            Debug.LogFormat("The word '{0}' has {1} syllable(s)", word, api.Syllable);

            numSyllables = api.Syllable;
            AddPoints();
			removeCards ();
			addCards();
		}
		else
		{
			Debug.LogFormat("The word {0} is invalid", word);
		}

        api.Reset();
	}

    void AddPoints()
    {
        numPoints = 0;
        for (int i = this.transform.childCount - 2; i >= 0; i--)
        {
            CardControl c = transform.GetChild(i).GetComponent<CardControl>();
            numPoints += c.value;
        }
        totalPoints += numPoints * numSyllables;
    }

	void removeCards(){


		for (int i = this.transform.childCount - 2; i >= 0; i--)
		{
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}
	}

	void addCards(){

		int cards = hand.transform.childCount;

		for(int i = cards; i < 7; i++){
			addCard ();
		}

	}

	void addCard(){

		int ij = UnityEngine.Random.Range (1, 98);
		GameObject temp;
		if (ij <= 12) {
			temp = Instantiate (e);
		}else if (ij <= 21){
			temp = Instantiate (a);
		}else if (ij <= 30){
			temp = Instantiate (i);
		}else if (ij <= 38){
			temp = Instantiate (o);
		}else if (ij <= 44){
			temp = Instantiate (n);
		}else if (ij <= 50){
			temp = Instantiate (r);
		}else if (ij <= 56){
			temp = Instantiate (t);
		}else if (ij <= 60){
			temp = Instantiate (l);
		}else if (ij <= 64){
			temp = Instantiate (s);
		}else if (ij <= 68){
			temp = Instantiate (u);
		}else if (ij <= 72){
			temp = Instantiate (d);
		}else if (ij <= 75){
			temp = Instantiate (g);
		}else if (ij <= 77){
			temp = Instantiate (b);
		}else if (ij <= 79){
			temp = Instantiate (c);
		}else if (ij <= 81){
			temp = Instantiate (m);
		}else if (ij <= 83){
			temp = Instantiate (p);
		}else if (ij <= 85){
			temp = Instantiate (f);
		}else if (ij <= 87){
			temp = Instantiate (h);
		}else if (ij <= 89){
			temp = Instantiate (v);
		}else if (ij <= 91){
			temp = Instantiate (w);
		}else if (ij <= 93){
			temp = Instantiate (y);
		}else if (ij <= 94){
			temp = Instantiate (k);
		}else if (ij <= 95){
			temp = Instantiate (j);
		}else if (ij <= 96){
			temp = Instantiate (x);
		}else if (ij <= 97){
			temp = Instantiate (q);
		}else{
			temp = Instantiate (z);
		}

		temp.transform.SetParent (hand.transform);
		temp.transform.localScale = new Vector3(1, 1, 1);
		temp.transform.localPosition = new Vector3(temp.transform.localPosition.x, temp.transform.localPosition.y, 0);


	}


}
