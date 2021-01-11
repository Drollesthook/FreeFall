using UnityEngine;

public class Coin : MonoBehaviour
{
   void Update() {
      transform.Rotate(Vector3.up * 100 * Time.deltaTime);  //Jazz for my soul
   }

   void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player"))
         Collect();
   }

   void Collect() {
      //SoftCurrencyController.Instance.AddCoin();
      //UIController.Instance.UpdateMenuTexts();
      gameObject.SetActive(false);
   }
}
